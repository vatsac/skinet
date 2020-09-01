using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Interfaces;
using Core.Model.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.services
{
    //need to generate token to send back to a client
    public class TokenService : ITokenService
    {
        //need our configuration service
        private readonly IConfiguration _config;
        //need a secure key that we use to encrypt that we use to sign the key from server,allows server to trust token
        private readonly SymmetricSecurityKey _key;//symmetric key as a type of encryption where only one key a secret key which we are going to store in our server is used to both encrypt and decrypt our signature in the token
        public TokenService(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));
        }
        //Each user have their claim in the token
        public string CreateToken(AppUser user)
        {
            //claim is bit of information about the 
            //Now these claims inside our token are going to be able to be decoded by the client.So if user had a token they'll be able to look inside the token and they'll be able to see their properties if they wanted to do so
           var claims = new List<Claim>
           {
              new Claim(JwtRegisteredClaimNames.Email, user.Email),
              new Claim(JwtRegisteredClaimNames.GivenName, user.DisplayName) 
           };
           
           //Credential arg:key and algorithm strength of encryption
           var creds = new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);

           //describe content of token
           //populate what we want to put in this token
           var tokenDescriptor = new SecurityTokenDescriptor
           {
               Subject = new ClaimsIdentity(claims),//email and display name
               Expires = DateTime.Now.AddDays(7),
               SigningCredentials = creds,
               Issuer = _config["Token:Issuer"]
            };

            //something to handle our token
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);
           
           return tokenHandler.WriteToken(token);

        }
    }
}