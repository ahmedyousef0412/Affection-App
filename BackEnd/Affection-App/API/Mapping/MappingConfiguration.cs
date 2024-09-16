

namespace Affection.API.Mapping;

public class MappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<ApplicationUser, MembersResponse>()

           
          .Map(dest => dest.Age,
                      src => (DateTime.Now.Year - src.DateOfBirth.Year)
                                  - 
                    (DateTime.Now < src.DateOfBirth.AddYears(DateTime.Now.Year - src.DateOfBirth.Year) ? 1 : 0)
          )
        
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


    //public void Register(TypeAdapterConfig config)
    //{
    //    // Shared mapping configuration for both MembersResponse and MemberResponse
    //    config.ForType<ApplicationUser, BaseMemberResponse>()
    //        .Map(dest => dest.Age, src => CalculateAge(src.DateOfBirth))
    //        .Map(dest => dest.CreatedOn, src => src.Created)
    //        .Map(dest => dest.MainPhotoUrl, src => src.Photos.FirstOrDefault(p => p.IsMain).Url);

    //    // Map MembersResponse from BaseMemberResponse
    //    config.NewConfig<ApplicationUser, MembersResponse>()
    //        .Inherits<ApplicationUser, BaseMemberResponse>();

    //    // Map MemberResponse from BaseMemberResponse
    //    config.NewConfig<ApplicationUser, MemberResponse>()
    //        .Inherits<ApplicationUser, BaseMemberResponse>();
    //}
}
