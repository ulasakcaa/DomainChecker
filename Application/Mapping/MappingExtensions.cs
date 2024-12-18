using Shared.Mapping;
using Application.Domain;
using Entitiess;

namespace Application.Mapping
{
    public static class MappingExtensions
    {
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return MappingConfiguration.Mapper.Map<TSource, TDestination>(source);            
        }

        #region Domain

        public static DomainListResponse ToModel(this DomainCheck data)
        {
            return data.MapTo<DomainCheck, DomainListResponse>();
        }
        #endregion


    }
}
