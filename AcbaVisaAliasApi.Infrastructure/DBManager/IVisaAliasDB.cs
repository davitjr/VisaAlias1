using System.Threading.Tasks;

namespace AcbaVisaAliasApi.Infrastructure.DBManager
{
    public interface IVisaAliasDB
    {
        public Task InsertVisaAliasactionHisoty(int setNumber, string guid, string cardNumber, string cardType, string alias, int actionType);
    }
}