using AutoMapper;
using WebApplicationAssesment.Application.Users.Interfaces;
using WebApplicationAssesment.Application.Users.Models;
using WebApplicationAssesment.Domain.Users;
using WebApplicationAssesment.Infraestructure.Repositories.Users.Interfaces;

namespace WebApplicationAssesment.Application.Users
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<RoleDto> CreateAsync(CreateRoleDto createDto)
        {
            Role roleEntity = _mapper.Map<CreateRoleDto, Role>(createDto);
            roleEntity = await _roleRepository.CreateAsync(roleEntity);
            RoleDto result = _mapper.Map<Role, RoleDto>(roleEntity);
            return result;
        }

        public async Task<RoleDto> DeleteByIdAsync(long id)
        {
            Role role = await _roleRepository.DeleteAsync(id);
            RoleDto result = _mapper.Map<Role, RoleDto>(role);
            return result;
        }

        public async Task<ICollection<RoleDto>> GetAllAsync()
        {
            var roles = await _roleRepository.GetAllAsync();
            ICollection<RoleDto> result = roles.Select(_mapper.Map<Role, RoleDto>).ToList();
            return result;
        }

        public async Task<RoleDto> GetByIdAsync(long id)
        {
            Role roleEntity = await _roleRepository.GetByIdAsync(id);
            RoleDto result = _mapper.Map<Role, RoleDto>(roleEntity);
            return result;
        }

        public async Task<RoleDto> UpdateAsync(RoleDto updateDto)
        {
            Role roleEntity = await _roleRepository.GetByIdAsync(updateDto.Id);
            roleEntity = _mapper.Map<RoleDto, Role>(updateDto, roleEntity);
            roleEntity = await _roleRepository.UpdateAsync(roleEntity);
            RoleDto result = _mapper.Map<Role, RoleDto>(roleEntity);
            return result;

        }
    }
}
