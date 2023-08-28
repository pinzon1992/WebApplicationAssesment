using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplicationAssesment.Application.Users;
using WebApplicationAssesment.Application.Users.Interfaces;
using WebApplicationAssesment.Application.Users.Models;
using WebApplicationAssesment.Common;
using WebApplicationAssesment.Common.Interfaces;
using WebApplicationAssesment.Common.Models;
using WebApplicationAssesment.Domain.Common.CustomExceptions;

namespace WebApplicationAssesment.Controllers.Accounts
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseController, IBaseActionsAsync<AccountDto, CreateAccountDto, long>
    {
        private readonly IAccountService _accountService;
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        public AccountController(IAccountService accountService, IRoleService roleService, IUserService userService,  ILogger logger, IConfiguration configuration) : base(logger, configuration)
        {
            _accountService = accountService;
            _roleService = roleService;
            _userService = userService;
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateAccountDto createDto)
        {
            try
            {
                var response = await _accountService.CreateAsync(createDto);
                if (response != null)
                {
                    return SuccessResponse("Ok", response);
                }
                else
                {
                    return BadRequest("Error trying to create Account");
                }
            }
            catch (EntityNotFound ex)
            {
                return BadRequest($"{ex.Message}");
            }
            catch (NoAffectedRows ex)
            {
                return BadRequest($"{ex.Message}");
            }
            catch (UniqueUsername ex)
            {
                return BadRequest($"{ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error trying to create Account: {ex.Message} {ex.InnerException}");
                return BadRequest($"Error trying to create Account");
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            try
            {
                var response = await _accountService.DeleteByIdAsync(id);
                if (response != null)
                {
                    return SuccessResponse("Ok", response);
                }
                else
                {
                    return BadRequest("Error trying to Delete Account");
                }
            }
            catch (EntityNotFound ex)
            {
                return BadRequest($"{ex.Message}");
            }
            catch (NoAffectedRows ex)
            {
                return BadRequest($"{ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error trying to Delete Account: {ex.Message} {ex.InnerException}");
                return BadRequest($"Error trying to delete Account");
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] long id)
        {
            try
            {
                AccountDto account = await _accountService.GetByIdAsync(id);
                if (account != null)
                {
                    return SuccessResponse("Ok", account);
                }
                else
                {
                    _logger.LogError($"Error trying to get Account with id {id}");
                    return BadRequest($"Error trying to get Account with id {id}");
                }
            }
            catch (EntityNotFound ex)
            {
                return BadRequest($"{ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error trying to get Account with id {id}: {ex.Message} {ex.InnerException}");
                throw;
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("accounts")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _accountService.GetAllAsync();
                if (result != null)
                {
                    return SuccessResponse("Ok", result);
                }
                else
                {
                    return BadRequest("Error trying to Get All Accounts");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error trying to Get All Accounts: {ex.Message} {ex.InnerException}");
                return BadRequest($"Error trying to Get All Accounts");
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut]
        public async Task<IActionResult> Update(AccountDto dto)
        {
            try
            {
                var result = await _accountService.UpdateAsync(dto);
                if (result != null)
                {
                    return SuccessResponse("Ok", result);
                }
                else
                {
                    return BadRequest($"Error trying to update Account");
                }
            }
            catch (EntityNotFound ex)
            {
                return Unauthorized($"Username {dto.Username} doesnt exist");
            }
            catch (NoAffectedRows ex)
            {
                return BadRequest($"{ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error trying to update Account: {ex.Message} {ex.InnerException}");
                return BadRequest($"Error trying to update Account");
            }
        }

        [Authorize(Roles = "Administrator")]
        #region Security
        [AllowAnonymous]
        [HttpPost("GetToken")]
        public async Task<IActionResult> DoLogin(LoginDto user)
        {
            ResponseBaseModel response;
            try
            {
                var accountDto = await _accountService.Login(user);

                if (accountDto is not null)
                {
                    var userDto = await _userService.GetByAccountIdAsync(accountDto.Id);
                    var role = await _roleService.GetByIdAsync(accountDto.RoleId);
                    var issuer = _configuration.GetSection("Jwt").GetSection("Issuer").Value;
                    var audience = _configuration.GetSection("Jwt").GetSection("Audience").Value;
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt").GetSection("Key").Value));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Sid, accountDto.Id.ToString()),
                        new Claim(ClaimTypes.Name, userDto.Firstname),
                        new Claim(ClaimTypes.Email, accountDto.Username),
                        new Claim(ClaimTypes.Surname, userDto.Lastname),
                        new Claim(ClaimTypes.Role, role.Name)
                    };


                    var token = new JwtSecurityToken(
                        issuer: issuer,
                        audience: audience,
                        expires: DateTime.Now.AddMinutes(30),
                        claims: claims,
                        signingCredentials: credentials
                    );
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var stringToken = tokenHandler.WriteToken(token);

                    HttpContext.Response.Cookies.Append("token", stringToken,
                    new CookieOptions
                    {
                        Expires = DateTime.Now.AddMinutes(15),
                        HttpOnly = true,
                        Secure = true,
                        IsEssential = true,
                        SameSite = SameSiteMode.None
                    });

                    LoginResponseDto loginInfo = new LoginResponseDto
                    {
                        Username = accountDto.Username,
                        Firstname = userDto.Firstname,
                        Lastname = userDto.Lastname,
                    };
                    response = new ResponseBaseModel
                    {
                        Code = System.Net.HttpStatusCode.OK,
                        Message = "Ok",
                        Data = loginInfo
                    };

                    return Ok(response);
                }
                else
                {
                    return Unauthorized();
                }

            }
            catch (InvalidCredentials ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                response = new ResponseBaseModel
                {
                    Code = System.Net.HttpStatusCode.InternalServerError,
                    Message = "Bad",
                    Data = null,
                    StackTrace = ex.Message + " " + ex.StackTrace + " " + ex.InnerException
                };
                return BadRequest(response);
            }
        }

        [AllowAnonymous]
        [HttpPost("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            //var cookie = HttpContext.Request.Cookies.FirstOrDefault(x => x.Key == "token");
            //if (!string.IsNullOrEmpty( cookie.Value ))
            //{
            //    Response.Cookies.Delete(cookie.Key);
            //}

            //Erase the data in the cookie
            HttpContext.Response.Cookies.Append("token", "",
            new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(-30),
                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None
            });

            var response = new ResponseBaseModel
            {
                Code = System.Net.HttpStatusCode.OK,
                Message = "Ok",
                Data = null
            };
            return Ok(response);
        }
        #endregion
    }
}
