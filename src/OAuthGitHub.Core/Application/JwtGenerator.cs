using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using OAuthGithub.Core.Domain;

namespace OAuthGithub.Core.Application
{
    public class JwtGenerator
    {
        private const    int                  TokenDaysDuration = 1;
        private readonly SecretKey            _secret;
        private readonly SecurityTokenHandler _tokenHandler;

        public JwtGenerator(SecretKey secret, SecurityTokenHandler tokenHandler)
        {
            _secret       = secret;
            _tokenHandler = tokenHandler;
        }

        private SecurityTokenDescriptor CreateTokenSpecification(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(_secret.Key);
            var signInCredentials =
                new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            return new SecurityTokenDescriptor
            {
                Subject            = new ClaimsIdentity(claims),
                Expires            = DateTime.Now.AddDays(TokenDaysDuration),
                SigningCredentials = signInCredentials,
            };
        }

        private static IEnumerable<Claim> GenerateClaims(User user)
        {
            return new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,
                    user.Id.ToString(CultureInfo.InvariantCulture))
            };
        }

        public string Generate(User user)
        {
            IEnumerable<Claim>      claims = GenerateClaims(user);
            SecurityTokenDescriptor tokenDescriptor = CreateTokenSpecification(claims);
            SecurityToken           token = _tokenHandler.CreateToken(tokenDescriptor);

            return _tokenHandler.WriteToken(token);
        }
    }
}
