
namespace Affection.Infrastructure.Implementation;

public class JWTProvider(IOptions<JWTConfiguration> _jwtConfoguration) : IJWTProvider
{
    private readonly JWTConfiguration _jwtConfoguration = _jwtConfoguration.Value;

    public (string token, int expiresIn) GenerateToken(ApplicationUser user, IEnumerable<string> roles)
    {
        //Set Claims => Payload
        Claim[] claims = [
             new (JwtRegisteredClaimNames.Sub , user.Id),
             new (JwtRegisteredClaimNames.Email , user.Email!),
             new (JwtRegisteredClaimNames.GivenName , user.UserName!),
             new (JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
             new (nameof(roles),JsonSerializer.Serialize(roles) ,JsonClaimValueTypes.JsonArray),

        ];

        // Generate Key for Encryption to Decoding and Encoding
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfoguration.Key));

        //Represents the cryptographic key and security algorithms that are used to generate a digital signature.
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);


        // Token Shape
        var jwtSecurityToken = new JwtSecurityToken(

            issuer: _jwtConfoguration.Issuer,
            audience: _jwtConfoguration.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtConfoguration.ExpireInMinute),
            signingCredentials: signingCredentials

        );

        return (token: new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken), expiresIn: _jwtConfoguration.ExpireInMinute * 60);
    }


    //public string? ValidateToken(string token)
    //{
    //    var tokenHandler = new JwtSecurityTokenHandler();

    //    var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfoguration.Key));

    //    try
    //    {
    //        tokenHandler.ValidateToken(token, new TokenValidationParameters
    //        {
    //            IssuerSigningKey = symmetricSecurityKey,
    //            ValidateIssuerSigningKey = true,
    //            ValidateIssuer = false,
    //            ValidateAudience = false,
    //            ClockSkew = TimeSpan.Zero

    //        }, out SecurityToken validatedToken);

    //        var jwtToken = (JwtSecurityToken)validatedToken;



    //        return jwtToken.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sub).Value; //return UserId
    //    }
    //    catch
    //    {

    //        return null;
    //    }

    //}


    public string? ValidateToken(string token, bool validateLifetime = true)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfoguration.Key));

        try
        {
            // Allow expired tokens to be parsed if validateLifetime is false
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                IssuerSigningKey = symmetricSecurityKey,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = validateLifetime, // Skip lifetime validation if refreshing
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            return jwtToken.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sub).Value; // return UserId
        }
        catch
        {
            return null;
        }
    }

}
