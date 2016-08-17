using AutoMapper;
using Ecom.API.Infrastructures.Mappings.Profiles;

namespace Ecom.API.Infrastructures.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<DomainToModelMapping>();
                x.AddProfile<DomainToViewModelMapping>();
                x.AddProfile<ViewModelToDomainMapping>();
            });
        }
    }
}
