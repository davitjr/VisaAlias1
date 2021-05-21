using System.Collections.Generic;

namespace AcbaVisaAliasApi.Application.DTOs.AcbaVisaAlias
{
    public record GetAliasResponse
    (
         string Country,
         string RecipientLastName,
         string RecipientFirstName,
         string RecipientPrimaryAccountNumber,
         string IssuerName,
         string CardType,
         string ConsentDateTime,
         string Guid,
         string Alias,
         string AliasType,
         List<OneAlias> Aliases
    );
    public record OneAlias(string Alias, string AliasType, string IsDefault);
}
