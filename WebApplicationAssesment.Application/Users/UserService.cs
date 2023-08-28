using AutoMapper;
using WebApplicationAssesment.Application.Users.Interfaces;
using WebApplicationAssesment.Application.Users.Models;
using WebApplicationAssesment.Domain.Users;
using WebApplicationAssesment.Infraestructure.Repositories.Users.Interfaces;

namespace WebApplicationAssesment.Application.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> CreateAsync(CreateUserDto createDto)
        {
            User userEntity = _mapper.Map<CreateUserDto, User>(createDto);
            userEntity = await _userRepository.CreateAsync(userEntity);
            UserDto result = _mapper.Map<User, UserDto>(userEntity);
            return result;
        }

        public async Task<UserDto> DeleteByIdAsync(long id)
        {
            User user = await _userRepository.DeleteAsync(id);
            UserDto result = _mapper.Map<User, UserDto>(user);
            return result;
        }

        public async Task<ICollection<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            ICollection<UserDto> result = users.Select(_mapper.Map<User, UserDto>).ToList();
            return result;
        }

        public async Task<UserDto> GetByAccountIdAsync(long accountId)
        {
            User userEntity = await _userRepository.GetByAccountIdAsync(accountId);
            UserDto result = _mapper.Map<User, UserDto>(userEntity);
            return result;
        }

        public async Task<UserDto> GetByIdAsync(long id)
        {
            User userEntity = await _userRepository.GetByIdAsync(id);
            UserDto result = _mapper.Map<User, UserDto>(userEntity);
            return result;
        }

        public async Task<UserDto> UpdateAsync(UserDto updateDto)
        {
            User userEntity = await _userRepository.GetByIdAsync(updateDto.Id);
            userEntity = _mapper.Map<UserDto, User>(updateDto, userEntity);
            userEntity = await _userRepository.UpdateAsync(userEntity);
            UserDto result = _mapper.Map<User, UserDto>(userEntity);
            return result;

        }
    }
}
