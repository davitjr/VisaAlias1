using AcbaVisaAliasApi.Application.DTOs.AcbaVisaAlias;
using AcbaVisaAliasApi.Application.DTOs.VisaAlias;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcbaVisaAliasApi.Infrastructure.DBManager
{
    public interface IVisaAliasDB
    {
        public Task InsertVisaAliasactionHisoty<A>(A request, short actionType);
        Task<List<VisaAliasActionHistoryResponse>> GetVisaAliasHistory(ulong customerNumber);
    }
}