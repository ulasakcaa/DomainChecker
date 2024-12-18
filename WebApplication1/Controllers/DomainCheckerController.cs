using Application.Domain;
using Business;
using Entitiess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DomainChecker.Controllers
{
    [Route("api/[controller]")]
    public class DomainCheckerController : BaseApiController
    {
        #region Fields

        private IDomainCheckerBusiness _domainCheckerBusiness;

        #endregion

        #region Ctor

        public DomainCheckerController(IDomainCheckerBusiness domainCheckerBusiness)
        {
            _domainCheckerBusiness = domainCheckerBusiness;
        }
        #endregion

        [HttpGet("GetAll")]
        public async Task<List<DomainListResponse>> GetAllDomains()
        {
            var result = await _domainCheckerBusiness.GetAllDomains();

            return result;
        }

        [HttpGet("CheckDomain")]
        public async Task<IActionResult> CheckDomain(string domainName)
        {
            var result = await _domainCheckerBusiness.CheckDomain(domainName);

            return Ok(new { result.DomainName, result.IsAvailable, result.Id });
        }


        [HttpPost("AddFavorite")]
        public async Task<IActionResult> AddFavorite([FromBody] int domainId)
        {
            try
            {                
                await _domainCheckerBusiness.AddFavorite(domainId);

                return Ok(new { message = "Domain added to favorites!" });
            }
            catch (Exception ex)
            {               
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpDelete("DeleteFavorite/{domainId}")]
        public async Task<IActionResult> DeleteFavorite([FromRoute] int domainId)
        {
            try
            {
                await _domainCheckerBusiness.DeleteFavorite(domainId);
                return Ok(new { message = "Domain deleted from favorites!" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }


    }

}
