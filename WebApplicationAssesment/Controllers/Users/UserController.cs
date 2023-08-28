using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationAssesment.Application.Users.Interfaces;
using WebApplicationAssesment.Application.Users.Models;
using WebApplicationAssesment.Common;
using WebApplicationAssesment.Common.Interfaces;
using WebApplicationAssesment.Domain.Common.CustomExceptions;

namespace WebApplicationAssesment.Controllers.Users
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : BaseController, IBaseActionsAsync<UserDto, CreateUserDto, long>
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService, ILogger logger, IConfiguration configuration) : base(logger, configuration)
        {
            _userService = userService;
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto createDto)
        {
            try
            {
                var response = await _userService.CreateAsync(createDto);
                if (response != null)
                {
                    return SuccessResponse("Ok", response);
                }
                else
                {
                    return BadRequest("Error trying to create User");
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
                _logger.LogError($"Error trying to create User: {ex.Message} {ex.InnerException}");
                return BadRequest($"Error trying to create User");
            }
        }

        [Authorize(Roles = "Administrator")]

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            try
            {
                var response = await _userService.DeleteByIdAsync(id);
                if (response != null)
                {
                    return SuccessResponse("Ok", response);
                }
                else
                {
                    return BadRequest("Error trying to Delete User");
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
                _logger.LogError($"Error trying to Delete User: {ex.Message} {ex.InnerException}");
                return BadRequest($"Error trying to delete User");
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] long id)
        {
            try
            {
                UserDto user = await _userService.GetByIdAsync(id);
                if (user != null)
                {
                    return SuccessResponse("Ok", user);
                }
                else
                {
                    _logger.LogError($"Error trying to get User with id {id}");
                    return BadRequest($"Error trying to get User with id {id}");
                }
            }
            catch (EntityNotFound ex)
            {
                return BadRequest($"{ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error trying to get User with id {id}: {ex.Message} {ex.InnerException}");
                throw;
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("users")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _userService.GetAllAsync();
                if (result != null)
                {
                    return SuccessResponse("Ok", result);
                }
                else
                {
                    return BadRequest("Error trying to Get All Users");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error trying to Get All Users: {ex.Message} {ex.InnerException}");
                return BadRequest($"Error trying to Get All Users");
            }
        }

        [AllowAnonymous]
        [HttpGet("users/anonymous")]
        public async Task<IActionResult> GetAllAnonymous()
        {
            try
            {
                var result = await _userService.GetAllAsync();
                if (result != null)
                {
                    return SuccessResponse("Ok", result);
                }
                else
                {
                    return BadRequest("Error trying to Get All Users");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error trying to Get All Users: {ex.Message} {ex.InnerException}");
                return BadRequest($"Error trying to Get All Users");
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPatch]
        public async Task<IActionResult> Update(UserDto dto)
        {
            try
            {
                var result = await _userService.UpdateAsync(dto);
                if (result != null)
                {
                    return SuccessResponse("Ok", result);
                }
                else
                {
                    return BadRequest($"Error trying to update User");
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
                _logger.LogError($"Error trying to update User: {ex.Message} {ex.InnerException}");
                return BadRequest($"Error trying to update User");
            }
        }
    }
}
