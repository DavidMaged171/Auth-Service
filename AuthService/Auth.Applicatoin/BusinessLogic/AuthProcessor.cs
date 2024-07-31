using Auth.Applicatoin.BusinessInterfaces;
using Auth.Applicatoin.DTOs.Requests;
using Auth.Applicatoin.DTOs.Resopnse;
using Auth.Applicatoin.Helpers;
using Auth.Applicatoin.Mappers.FromDTOToEntity;
using Auth.Applicatoin.Models;
using Auth.Infrastructure.Persistence.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.Applicatoin.BusinessLogic
{
    public class AuthProcessor : IAuthProcessor
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly JWT _jwt;
        public AuthProcessor(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _jwt = jwt.Value;
        }
        public async Task<RegestrationResponse> RegisterNewUser(RegisterationRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user != null)
            {
                return new RegestrationResponse { User = user, isRegistered = true };
            }
            user = FromUserDTOToUser.Map(request);

            var res = await _userManager.CreateAsync(user, request.Password);
            if (!res.Succeeded)
            {
                var errors = new List<string>();
                foreach (var error in res.Errors)
                {
                    errors.Add(error.Description);
                }
                return new RegestrationResponse { User = null, errors = errors };
            }
            await _userManager.AddToRoleAsync(user, "User");
            var JwtSecurityToken = await CreateJwtToken(user);
            return new RegestrationResponse() { User = user, isRegistered = true, };
        }
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id.ToString())
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
