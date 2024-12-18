using Application.Domain;
using AutoMapper;
using Entitiess;
using Shared.Mapping;


namespace Application.Mapping
{
    public class MappingProfile:Profile,IMapFrom
    {
        public MappingProfile()
        {
            #region Login

            CreateMap<DomainCheck, DomainListResponse>();             

            #endregion
        
        }
        public int Order => 0;
    }
}
