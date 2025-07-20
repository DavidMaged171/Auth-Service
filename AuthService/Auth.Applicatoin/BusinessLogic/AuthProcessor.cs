using Auth.Applicatoin.BusinessInterfaces;
using Auth.Applicatoin.DTOs.Requests;
using Auth.Applicatoin.DTOs.Resopnse;
using Auth.Applicatoin.Helpers;
using Auth.Infrastructure.Models;
using Auth.Infrastructure.Persistence.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;


namespace Auth.Applicatoin.BusinessLogic
{
    public class AuthProcessor : IAuthProcessor
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly JWT _jwt;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AuthProcessor(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IConfiguration configuration,IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }
        public async Task<GenericResponseClass<RegestrationResponse>> RegisterNewUser(RegisterationRequest request)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user != null)
                {
                    return HandleUserExists(ResponseMessages.EmailAlreadyRegistered);
                }
                user = await _userManager.FindByNameAsync(request.Username);
                if (user != null)
                {
                    return HandleUserExists(ResponseMessages.UserNameAlreadyRegistered);
                }
                ApplicationUser appUser = CreateApplicationUser(request);


                var result =await _userManager.CreateAsync(appUser, request.Password);                
                if (result.Succeeded)
                {
                    return new GenericResponseClass<RegestrationResponse>() 
                    { 
                        Result=new RegestrationResponse()
                        {
                            isRegistered=true,
                            errors=null
                        },
                        ResponseMessage=ResponseMessages.UserRegisteredSuccessfully,
                        Status=Enums.ResponseStatus.Success,
                    };
                }
                else
                {
                    return new GenericResponseClass<RegestrationResponse>()
                    {
                        Result = new RegestrationResponse()
                        {
                            isRegistered = false,
                            errors = GetErrors(result)
                        },
                        ResponseMessage = ResponseMessages.ErrorsWhileRegisterUser,
                        Status = Enums.ResponseStatus.Failed,
                    };
                }
            }
            catch (Exception ex) 
            {
                return null;   
            }
            finally
            {

            }
        }
        private List<string> GetErrors(IdentityResult result)
        {
            var errors = new List<string>();
            foreach (var error in result.Errors)
            {
                errors.Add(error.Description);
            }
            return errors;
        }
        private GenericResponseClass<RegestrationResponse> HandleUserExists(string errorMessage)
        {
            try
            {
                return new GenericResponseClass<RegestrationResponse>() 
                {
                    Result=new RegestrationResponse()
                    {
                        errors = new List<string>()
                        {
                            errorMessage
                        },
                        isRegistered = true,                        
                    },
                    ResponseMessage=ResponseMessages.ErrorsWhileRegisterUser,
                    Status=Enums.ResponseStatus.Failed,
                };
            }
            catch (Exception ex)
            {
                return null;
            }
            finally 
            {
                
            }
        }
        private ApplicationUser CreateApplicationUser(RegisterationRequest request)
        {
            return  new ApplicationUser()
            {
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.Username,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = request.Password,
            };
        }

        public async Task<GenericResponseClass<LoginResponse>> Login(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddMinutes(int.Parse(_configuration["JWT:DurationInMinutes"])),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return new GenericResponseClass<LoginResponse>()
                {
                    Result=new LoginResponse() 
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        ExpirationDate = token.ValidTo
                    },
                    ResponseMessage=ResponseMessages.LoggedInSuccessfully,
                    Status=Enums.ResponseStatus.Success
                };
            }
            else if(user==null)
            {
                return new GenericResponseClass<LoginResponse>()
                {
                    Result = null,
                    ResponseMessage = ResponseMessages.UserNotRegistered,
                    Status = Enums.ResponseStatus.Failed
                };
            }
            else
            {
                return new GenericResponseClass<LoginResponse>()
                {
                    Result = null,
                    ResponseMessage = ResponseMessages.ErrorsWhileLogin,
                    Status = Enums.ResponseStatus.Failed
                };
            }
        }
    }
}
