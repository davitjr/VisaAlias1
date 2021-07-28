using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcbaVisaAliasApi.Application.DTOs.VisaAlias
{
    public class DeleteVisaAliasWithCardRequest
    {
        public string Guid { get; set; }
        public string Alias { get; set; }

        public int SetNumber { get; set; }
    }
}
