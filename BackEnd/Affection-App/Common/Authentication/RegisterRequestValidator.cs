

namespace Affection.Contract.Authentication;
public class RegisterRequestValidator:AbstractValidator<RegisterRequest>
{

    public RegisterRequestValidator()
    {

        RuleFor(u => u.UserName)
        .NotEmpty()
        .Length(4, 10);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
           .NotEmpty()
           .Matches(RegexPatterns.Password)
          .WithMessage("Password should be at least 8 digits and should contains Lowercase, NonAlphanumeric and Uppercase");



        RuleFor(x => x.ConfirmPassword)
           .NotEmpty()
           .Equal(x => x.Password)
           .WithMessage("Confirm Password must match the Password.");

        RuleFor(u => u.Country)
           .NotEmpty();


        RuleFor(u => u.City)
              .NotEmpty();
        RuleFor(u => u.KnowAs)
        .NotEmpty()
        .Length(5, 15);

        RuleFor(u => u.DateOfBirth)
          .NotEmpty()
          .WithMessage("Date of Birth is required.")
          .Must(BeAValidDate)
          .WithMessage("Date of Birth must be a valid date.")
          .Must(BeAtLeast18YearsOld)
          .WithMessage("You must be at least 18 years old.")
          .Must(NotBeInTheFuture)
          .WithMessage("Date of Birth cannot be in the future.");



        RuleFor(u => u.Gender)
           .NotEmpty()
           .WithMessage("Gender is required.")
           .Must(BeAValidGender)
           .WithMessage("Invalid gender value.");
    }


    private bool BeAValidDate(DateTime dateOfBirth)
    {
        return dateOfBirth != default(DateTime); // 1996 12 15
    }

    private bool BeAtLeast18YearsOld(DateTime dateOfBirth)
    {
        var today = DateTime.Today;
        var age = today.Year - dateOfBirth.Year;
        if (dateOfBirth > today.AddYears(-age))
            age--;
        return age >= 18;
    }

    private bool NotBeInTheFuture(DateTime dateOfBirth)
    {
        return dateOfBirth <= DateTime.Today;
    }

    private bool BeAValidGender(string gender)
    {
        return Enum.TryParse(typeof(Gender),gender,out var _);
    }
}
   
 
   