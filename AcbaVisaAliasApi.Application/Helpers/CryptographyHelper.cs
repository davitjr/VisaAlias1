using AcbaVisaAliasApi.Application.DTOs.AcbaVisaAlias;
using AcbaVisaAliasApi.Application.Settings;
using Jose;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AcbaVisaAliasApi.Application.Helpers
{
    public class CryptographyHelper : ICryptographyHelper
    {
        public CryptographyHelper(IOptions<VisaAliasApiOptions> VisaAliasOptions, IWebHostEnvironment environment)
        {
            _VisaAliasApiOptions = VisaAliasOptions.Value;
            _environment = environment;
        }

        private readonly VisaAliasApiOptions _VisaAliasApiOptions;
        private readonly IWebHostEnvironment _environment;

        public string GetEncryptedPayload(string requestBody)
        {
            using X509Store store = new(StoreLocation.LocalMachine);
            store.Open(OpenFlags.OpenExistingOnly);
            X509Certificate2Collection certs = store.Certificates.Find(X509FindType.FindByThumbprint, _VisaAliasApiOptions.MleServerCertificateThumbprint, _VisaAliasApiOptions.AllowInvalidCertificate);
            X509Certificate2 certificate = certs.OfType<X509Certificate2>().FirstOrDefault();
            RSA clientCertificate = certificate.GetRSAPublicKey();
            IDictionary<string, object> extraHeaders = SetExtraHeaders();
            string token = JWT.Encode(requestBody, clientCertificate, JweAlgorithm.RSA_OAEP_256, JweEncryption.A128GCM, null, extraHeaders);
            return $"{{\"encData\":\"{token}\"}}";
        }

        public async Task<T> DecryptResponse<T>(Stream responseStream) where T : class
        {
            EncryptedPayload response = await JsonSerializer.DeserializeAsync<EncryptedPayload>(responseStream, DefaultJsonSettings.Settings);
            string decryptedPayload = GetDecryptedPayload(response.EncData);
            T responseObj = JsonSerializer.Deserialize<T>(decryptedPayload, DefaultJsonSettings.Settings);
            return responseObj;
        }

        private string GetDecryptedPayload(string encryptedPayload)
        {
            return JWT.Decode(encryptedPayload, ImportPrivateKey(_VisaAliasApiOptions.MleClientPrivateKeyPath));
        }

        private IDictionary<string, object> SetExtraHeaders()
        {
            long unixTimeMilliseconds = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            IDictionary<string, object> extraHeaders = new Dictionary<string, object>
                {
                    {"kid", _VisaAliasApiOptions.KeyId},{"iat",unixTimeMilliseconds}
                };
            return extraHeaders;
        }

        private RSA ImportPrivateKey(string privateKeyFilePath)
        {
            string filePath = Path.Combine(_environment.WebRootPath, privateKeyFilePath);
            string pemValue = Encoding.Default.GetString(File.ReadAllBytes(filePath));
            PemReader pr = new(new StringReader(pemValue));
            AsymmetricCipherKeyPair keyPair = (AsymmetricCipherKeyPair)pr.ReadObject();
            RSAParameters rsaParams = DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters)keyPair.Private);

            RSA rsa = RSA.Create();
            rsa.ImportParameters(rsaParams);

            return rsa;
        }
    }
}