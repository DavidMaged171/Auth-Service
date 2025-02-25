using Auth.Applicatoin.BusinessInterfaces;
using Auth.Applicatoin.DTOs.Requests;
using Auth.Applicatoin.DTOs.Resopnse;
using Auth.Applicatoin.Helpers;
using Auth.Infrastructure.Models;
using Auth.Infrastructure.Persistence.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;


namespace Auth.Applicatoin.BusinessLogic
{
    public class AuthProcessor : IAuthProcessor
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly JWT _jwt;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AuthProcessor(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
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

        
    }
}
