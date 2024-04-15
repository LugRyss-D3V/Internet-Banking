using Azure;
using InternetBanking.Core.Application.Dtos.Account.Request;
using InternetBanking.Core.Application.Dtos.Account.Response;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace InternetBanking.Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<AuthenticationResponse> GetUserByUserNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);


            AuthenticationResponse response = new();

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No existen cuentas registradas con {userName}";
                return response;
            }

            response.Id = user.Id;
            response.FirstName = user.FirstName;
            response.LastName = user.LastName;
            response.Email = user.Email;
            response.UserName = user.UserName;
            response.PersonalId = user.PersonalId;
            response.Status = user.Status;

            return response;

        }
        public async Task<AuthenticationResponse> GetUserAsync(string id)
        {
            AuthenticationResponse response = new();

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No existe usuario registrado con {id}";
                return response;
            }

            var roles = await _userManager.GetRolesAsync(user);

            response.Id = user.Id;
            response.FirstName = user.FirstName;
            response.LastName = user.LastName;
            response.Email = user.Email;
            response.UserName = user.UserName;
            response.PersonalId = user.PersonalId;
            response.Roles = roles.ToList();
            response.Status = user.Status;

            return response;
        }
        public async Task<List<AuthenticationResponse>> GetUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            var responses = new List<AuthenticationResponse>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                var response = new AuthenticationResponse
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    UserName = user.UserName,
                    PersonalId = user.PersonalId,
                    Roles = roles.ToList(),
                    Status = user.Status
                };

                responses.Add(response);
            }

            return responses;

        }
        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            AuthenticationResponse response = new();

            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No hay cuentas registradas con {request.UserName}";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Credenciales invalidas para {request.UserName}";
                return response;
            }

            if (!user.Status)
            {
                response.HasError = true;
                response.Error = $"Cuenta inactiva para {request.UserName}";
                return response;
            }

            response.Id = user.Id;
            response.FirstName = user.FirstName;
            response.LastName = user.LastName;
            response.Email = user.Email;
            response.UserName = user.UserName;
            response.PersonalId = user.PersonalId;
            response.Status = user.Status;

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = rolesList.ToList();

            return response;
        }
        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<RegisterResponse> RegisterUserAsync(RegisterRequest request)
        {
            RegisterResponse response = new();

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"Nombre de Usuario '{request.UserName}' ya ha sido tomado.";
                return response;
            }

            var user = new ApplicationUser()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                Email = request.Email,
                PersonalId = request.PersonalId,
                Status = false,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, request.TypeUser.ToString());
            }
            else
            {
                response.HasError = true;
                response.Error = $"Ha ocurrido un error al registrar el usuario";
                return response;
            }

            response.Id = user.Id;
            response.IsClient = (request.TypeUser == Roles.Client);

            return response;
        }
        public async Task<UpdateResponse> UpdateUserAsync(UpdateRequest request)
        {
            UpdateResponse response = new();

            var user= await _userManager.FindByIdAsync(request.Id);

            if(user != null && user.Id != request.Id)
            {
                response.HasError = true;
                response.Error = $"El nombre de usuario a tomar se encuentra en uso actualmente";
                return response;
            }

            user.UserName = request.UserName;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.PersonalId = request.PersonalId;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Ha ocurrido un error mientras se registraba el usuario";
                return response;
            }

            response.Id = user.Id;

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.IsClient = rolesList.Any(role => role.Contains("Client"));

            return response;
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request)
        {
            ResetPasswordResponse response = new();

            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No hay cuentas registradas con {request.UserName}";
                return response;
            }
            var result = await _userManager.RemovePasswordAsync(user);
            if (result.Succeeded)
            {
                result = await _userManager.AddPasswordAsync(user, request.Password);
                if (!result.Succeeded)
                {
                    response.HasError = true;
                    response.Error = $"Ha ocurrido un error mientras se actualizaba la contraseña la usuario: {request.UserName}";
                    return response;
                }
            }
            else
            {
                response.HasError = true;
                response.Error = $"Ha ocurrido un error mientras se eliminaba la contraseña la usuario: {request.UserName}";
                return response;
            }

            return response;
        }
        public async Task<ChangeStatusResponse> SwitchStatusAsync(ChangeStatusRequest request)
        {
            ChangeStatusResponse response = new ChangeStatusResponse();

            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"La cuenta {request.UserId} no esta registrada en el sistema";
                return response;
            }

            user.Status = !user.Status;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                if (user.Status)
                {
                    response.HasError = false;
                    response.Error = $"La cuenta ha sido activada";
                }
                else
                {
                    response.HasError = false;
                    response.Error = $"La cuenta ha sido desactivada";
                }
            }
            else
            {
                response.HasError = true;
                response.Error = $"Ha ocurrido un error al cambiar el estado de: {user.UserName}.";
            }

            return response;
        }
        
        public async Task<int> ActiveUsers()
        {
            return await _userManager.Users.Where(u => u.Status == true).CountAsync();
        }
        public async Task<int> DeactiveUsers()
        {
            return await _userManager.Users.Where(u => u.Status == false).CountAsync();
        }
    }
}