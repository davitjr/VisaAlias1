using AcbaVisaAliasApi.Application.DTOs.AcbaVisaAlias;
using AcbaVisaAliasApi.Application.DTOs.VisaAlias;
using AcbaVisaAliasApi.Infrastructure.ServiceDTOs.AcbaVisaAlias;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcbaVisaAliasApi.Infrastructure.DBManager
{
    public interface IVisaAliasDB
    {
        public Task InsertVisaAliasactionHisoty<A>(A request, short actionType);
        Task<List<VisaAliasActionHistoryResponse>> GetVisaAliasHistory(ulong customerNumber);
        Task<DeleteVisaAliasRequest> GetVisaAliasForDeleteWithCard(DeleteVisaAliasWithCardRequest deleteVisaAliasWithCardRequest);

        Task<VisaAliasActionHistoryResponse> GetVisaAliasHistoryWithCardAsync(string cardNumber);
        Task<CreateVisaAliasRequest> GetVisaAlias(ulong customerNumber);
    }
}