using AutoMapper;

namespace Ecom.API.Infrastructures.Mappings.Profiles
{
    public class DomainToModelMapping: Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Entities.Product, Models.Product>();
        }
    }
}
