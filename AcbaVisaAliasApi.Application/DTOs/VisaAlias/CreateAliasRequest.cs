using System.Linq;

namespace AcbaVisaAliasApi.Application.DTOs.AcbaVisaAlias
{
    public record CreateAliasRequest
    {
        public string Country { get; init; }
        public string RecipientFirstName { get; init; }
        private string recipientLastName;
        public string RecipientLastName { get => recipientLastName; init => recipientLastName = $"{value.First()}."; }
        public string RecipientPrimaryAccountNumber { get; init; }
        public string IssuerName { get; init; }
        public string CardType { get; init; }
        public string ConsentDateTime { get; init; }
        public string AliasType { get; init; }
        public string Guid { get; init; }
        public string Alias { get; init; }
        public int SetNumber { get; set; }        
        public string ExpiryDate { get; init; }
    }
}
