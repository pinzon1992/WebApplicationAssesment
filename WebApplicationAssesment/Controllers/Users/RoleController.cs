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
    [Route("api/[controller]")]
    public class RoleController : BaseController, IBaseActionsAsync<RoleDto, CreateRoleDto, long>
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService, ILogger logger, IConfiguration configuration) : base(logger, configuration)
        {
            _roleService = roleService;
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleDto createDto)
        {
            try
            {
                var response = await _roleService.CreateAsync(createDto);
                if (response != null)
                {
                    return SuccessResponse("Ok", response);
                }
                else
                {
                    return BadRequest("Error trying to create Role");
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
                _logger.LogError($"Error trying to create Role: {ex.Message} {ex.InnerException}");
                return BadRequest($"Error trying to create Role");
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]long id)
        {
            try
            {
                var response = await _roleService.DeleteByIdAsync(id);
                if (response != null)
                {
                    return SuccessResponse("Ok", response);
                }
                else
                {
                    return BadRequest("Error trying to Delete Role");
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
                _logger.LogError($"Error trying to Delete Role: {ex.Message} {ex.InnerException}");
                return BadRequest($"Error trying to delete Role");
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]long id)
        {
            try
            {
                RoleDto role = await _roleService.GetByIdAsync(id);
                if (role != null)
                {
                    return SuccessResponse("Ok", role);
                }
                else
                {
                    _logger.LogError($"Error trying to get Role with id {id}");
                    return BadRequest($"Error trying to get Role with id {id}");
                }
            }
            catch (EntityNotFound ex)
            {
                return BadRequest($"{ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error trying to get Role with id {id}: {ex.Message} {ex.InnerException}");
                throw;
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("roles")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _roleService.GetAllAsync();
                if (result != null)
                {
                    return SuccessResponse("Ok", result);
                }
                else
                {
                    return BadRequest("Error trying to Get All Roles");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error trying to Get All Roles: {ex.Message} {ex.InnerException}");
                return BadRequest($"Error trying to Get All Roles");
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPatch]
        public async Task<IActionResult> Update(RoleDto dto)
        {
            try
            {
                var result = await _roleService.UpdateAsync(dto);
                if (result != null)
                {
                    return SuccessResponse("Ok", result);
                }
                else
                {
                    return BadRequest($"Error trying to update Role");
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
                _logger.LogError($"Error trying to update Role: {ex.Message} {ex.InnerException}");
                return BadRequest($"Error trying to update Role");
            }
        }
    }
}
