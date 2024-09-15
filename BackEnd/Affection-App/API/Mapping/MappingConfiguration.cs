

namespace Affection.API.Mapping;

public class MappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<ApplicationUser, MembersResponse>()

           
          .Map(dest => dest.Age,
         src => (DateTime.Now.Year - src.DateOfBirth.Year)
               - (DateTime.Now < src.DateOfBirth.AddYears(DateTime.Now.Year - src.DateOfBirth.Year) ? 1 : 0))
        
          .Map(dest => dest.CreatedOn , src => src.Created)

          .Map(dest => dest.MainPhotoUrl,
         src => src.Photos.FirstOrDefault(p => p.IsMain).Url);




        config.NewConfig<ApplicationUser, MemberResponse>()
     
        .Map(dest => dest.Age,
       src => (DateTime.Now.Year - src.DateOfBirth.Year) - (DateTime.Now < src.DateOfBirth.AddYears(DateTime.Now.Year - src.DateOfBirth.Year) ? 1 : 0))
        .Map(dest => dest.CreatedOn, src => src.Created)
        .Map(dest => dest.MainPhotoUrl,
       src => src.Photos.FirstOrDefault(p => p.IsMain).Url);

    }
}
