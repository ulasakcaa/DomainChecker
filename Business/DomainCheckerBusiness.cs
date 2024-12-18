using Application.Domain;
using Data.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using Application.Mapping;
using Entitiess;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Business
{
    public class DomainCheckerBusiness : IDomainCheckerBusiness
    {
        private readonly IRepository<DomainCheck> _domainRepository;
        private readonly IRepository<Favorite> _favoriteRepository;

        #region Ctor
        public DomainCheckerBusiness(IRepository<DomainCheck> domainRepository, IRepository<Favorite> favoriteRepository)
        {
            _domainRepository = domainRepository;
            _favoriteRepository = favoriteRepository;
        }
        #endregion

        public async Task<List<DomainListResponse>> GetAllDomains()
        {
            try
            {
                var favoriteDomainIds = await _favoriteRepository.GetListNoTracking()
             .Select(x => x.DomainId)
             .ToListAsync();

                var domains = await _domainRepository.GetListNoTracking()
                    .OrderByDescending(x => x.Id)
                    .ToListAsync();

                var favoriteDomainIdSet = new HashSet<long>(favoriteDomainIds);

                var result = domains.Select(domain => {
                    var model = domain.ToModel();
                    model.IsFavorite = favoriteDomainIdSet.Contains(domain.Id);
                    return model;
                }).ToList();

                return result;
            }
            catch (Exception)
            {
                return new List<DomainListResponse>();
            }         
        }


        public async Task<DomainCheckResponse> CheckDomain(string domainName)
        {
            try
            {
                var url = $"https://rdap.nicproxy.com/domain/{domainName}/";
                using var client = new HttpClient();
                var response = await client.GetAsync(url);

                var result = new DomainCheckResponse
                {
                    DomainName = domainName,
                    IsAvailable = response.StatusCode == HttpStatusCode.NotFound
                };

                var domain = await _domainRepository.GetList().FirstOrDefaultAsync(x => x.DomainName == domainName);

                if (domain != null)
                {
                    domain.IsAvailable = result.IsAvailable;
                    domain.LastCheckedDate = DateTime.Now;

                    await _domainRepository.Update(domain);
                }
                else
                {
                    domain = new DomainCheck
                    {
                        DomainName = domainName,
                        IsAvailable = result.IsAvailable,
                        LastCheckedDate = DateTime.Now,
                        ExpireDate = result.IsAvailable ? (DateTime?)null : DateTime.Now.AddYears(1)
                    };

                    var insertedDomain = await _domainRepository.InsertT(domain);
                    result.Id = insertedDomain.Id;
                }

                return result;
            }
            catch (Exception)
            {
                return new DomainCheckResponse();
            }        
        }

        public async Task AddFavorite(int Id)
        {
            try
            {
                var domain = await _domainRepository.GetList().FirstOrDefaultAsync(x => x.Id == Id);

                if (domain == null)
                {
                    throw new Exception("Domain not found.");
                }

                var already = _favoriteRepository.GetListNoTracking().FirstOrDefault(x => x.DomainId == domain.Id);

                if (already != null)
                    return;

                var favorite = new Favorite
                {
                    DomainId = domain.Id,
                    Domain = domain
                };

                await _favoriteRepository.Insert(favorite);
            }
            catch (Exception)
            {

            }           
        }

        public async Task DeleteFavorite(int Id)
        {
            try
            {
                var domain = await _domainRepository.GetList().FirstOrDefaultAsync(x => x.Id == Id);

                if (domain == null)                
                    throw new Exception("Domain not found.");                

                var favorite = _favoriteRepository.GetList().FirstOrDefault(x => x.DomainId == domain.Id)?.Id;

                if(favorite !=null)
                    await _favoriteRepository.DeleteByIdAsync(favorite);
            }
            catch (Exception)
            {

            }
        }
    }
}
