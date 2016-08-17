using AutoMapper;
using Ecom.API.ViewModels;

namespace Ecom.API.Infrastructures.Mappings.Profiles
{
    public class DomainToViewModelMapping: Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Entities.Product, ProductVM>();
        }
    }
}
