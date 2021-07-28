using System;

namespace AcbaVisaAliasApi.Application.DTOs.VisaAlias
{
    public class VisaAliasActionHistoryResponse
    {
        public int SetNumber { get; set; }

        public string Guid { get; set; }

        public ulong CustomerNumber { get; set; }

        public string CardNumber { get; set; }

        public string CardType { get; set; }

        public string Alias { get; set; }

        public DateTime? ActionDate { get; set; }

        public string ActionType { get; set; }

        public string Status { get; set; }

        public DateTime OperDay { get; set; }
        public string RecipientFirstName { get; set; }
        public string recipientLastName { get; set; }
        public string ExpiryDate { get; set; }

    }
}



