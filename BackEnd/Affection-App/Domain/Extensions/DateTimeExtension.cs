

namespace Affection.Domain.Extensions;
public static class DateTimeExtension
{


    //dob => date of birth
    public static int CalculateAge(this DateTime dob)
    {
        var today = DateTime.Today;

        var age = today.Year - dob.Year;

        if (dob.Date > today.AddYears(age))
            age--;

        return age;
    }
}


