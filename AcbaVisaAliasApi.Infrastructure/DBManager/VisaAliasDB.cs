using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AcbaVisaAliasApi.Infrastructure.DBManager
{
    public class VisaAliasDB : IVisaAliasDB 
    {
        private readonly string AccOperBase;

        public VisaAliasDB(IConfiguration configuration)
        {
            AccOperBase = configuration.GetConnectionString("AccOperBase");
        }

        public async Task InsertVisaAliasactionHisoty(int setNumber, string guid ,string cardNumber, string cardType, string alias, short actionType)
        {       
            using SqlConnection dbconn = new(AccOperBase);

            await dbconn.OpenAsync();

            string sqlStr = "pr_Insert_Visa_Alias_Action_History ";

            await using SqlCommand cmd = new(sqlStr, dbconn)
            {
                CommandType = CommandType.StoredProcedure,
            };

            cmd.Parameters.Add("@setNumber", SqlDbType.Int).Value = setNumber;
            cmd.Parameters.Add("@guid", SqlDbType.NVarChar).Value = guid; 
            cmd.Parameters.Add("@cardNumber", SqlDbType.NVarChar).Value = cardNumber;
            cmd.Parameters.Add("@cardType", SqlDbType.NVarChar).Value = cardType;
            cmd.Parameters.Add("@alias", SqlDbType.NVarChar).Value = alias;
            cmd.Parameters.Add("@actionType", SqlDbType.SmallInt).Value = actionType;

            using SqlDataReader dr = await cmd.ExecuteReaderAsync();

        }
    }
}
