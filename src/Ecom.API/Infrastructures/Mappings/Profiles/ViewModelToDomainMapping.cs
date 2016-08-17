using AutoMapper;
using Ecom.API.ViewModels;

namespace Ecom.API.Infrastructures.Mappings.Profiles
{
    public class ViewModelToDomainMapping: Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<ProductVM, Entities.Product>();
        }
    }
}
