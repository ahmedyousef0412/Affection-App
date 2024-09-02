

namespace Affection.Contract.Users;
public class UpdateProfileRequestValidator:AbstractValidator<UpdateProfileRequest>
{

    public UpdateProfileRequestValidator()
    {

        RuleFor(u => u.Country).NotEmpty();

        RuleFor(u => u.City).NotEmpty();

        RuleFor(u => u.LookingFor)
            .NotEmpty()
            .Length(10 , 120);

        RuleFor(u => u.Introduction)
            .NotEmpty()
            .Length(10,120);

        RuleFor(u => u.Intrestes)
           .NotEmpty()
           .Length(10, 120);
    }
}

  
  