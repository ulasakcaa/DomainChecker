
using Entitiess;
using Shared.Common.Interface;

namespace Business
{
    public interface IDomainCheckerBusiness : IBaseBusiness
    {
        public Task<List<DomainListResponse>> GetAllDomains();
        public Task<DomainCheckResponse> CheckDomain(string domainName);       
        public Task AddFavorite(int Id);
        public Task DeleteFavorite(int Id);

    }
}
