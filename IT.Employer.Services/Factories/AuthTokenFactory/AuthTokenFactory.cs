﻿using IT.Employer.Services.Models.Auth;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace IT.Employer.Services.Factories.AuthTokenFactory
{
    public class AuthTokenFactory : IAuthTokenFactory
    {
        public JwtSecurityToken CreateToken(string username, IEnumerable<Claim> businessClaims)
        {
            return CreateToken(username, AuthOptions.GetSymmetricSecurityKey(), AuthOptions.ISSUER, AuthOptions.AUDIENCE, businessClaims);
        }

        public JwtSecurityToken CreateToken(string username, SymmetricSecurityKey secret, String issuer, String audience, IEnumerable<Claim> businessClaims)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,username),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, username),
            };

            claims.AddRange(businessClaims);

            SigningCredentials signinCredentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: signinCredentials);

            return jwtSecurityToken;
        }
    }
}
