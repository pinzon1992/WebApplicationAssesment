using AutoMapper;
using WebApplicationAssesment.Application.Users.Interfaces;
using WebApplicationAssesment.Application.Users.Models;
using WebApplicationAssesment.Domain.Common.CustomExceptions;
using WebApplicationAssesment.Domain.Common.Helpers;
using WebApplicationAssesment.Domain.Users;
using WebApplicationAssesment.Infraestructure.Repositories.Users.Interfaces;

namespace WebApplicationAssesment.Application.Users
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public AccountService(IAccountRepository accountRepository, IUserRepository userRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<AccountDto> CreateAsync(CreateAccountDto createDto)
        {
            Account accountEntity = _mapper.Map<CreateAccountDto, Account>(createDto);
            accountEntity.Password = PasswordHasher.HashPasword(createDto.Password, null, out var salt);
            accountEntity.Salt = Convert.ToHexString(salt);
            accountEntity = await _accountRepository.CreateAsync(accountEntity);

            User userEntity = new User
            {
                Firstname = createDto.FirstName,
                Lastname = createDto.LastName,
                AccountId = accountEntity.Id
            };

            userEntity = await _userRepository.CreateAsync(userEntity);
            AccountDto result = _mapper.Map<Account, AccountDto>(accountEntity);
            return result;

        }

        public async Task<AccountDto> DeleteByIdAsync(long id)
        {
            Account accountEntity = await _accountRepository.DeleteAsync(id);
            AccountDto result = _mapper.Map<Account, AccountDto>(accountEntity);
            return result;
        }

        public async Task<ICollection<AccountDto>> GetAllAsync()
        {
            var accounts = await _accountRepository.GetAllAsync();
            ICollection<AccountDto> result = accounts.Select(_mapper.Map<Account, AccountDto>).ToList();
            return result;
        }

        public async Task<AccountDto> GetByIdAsync(long id)
        {
            Account accountEntity = await _accountRepository.GetByIdAsync(id);
            AccountDto result = _mapper.Map<Account, AccountDto>(accountEntity);
            return result;
        }

        public async Task<AccountDto> UpdateAsync(AccountDto updateDto)
        {
            Account accountEntity = await _accountRepository.GetByIdAsync(updateDto.Id);
            accountEntity = _mapper.Map<AccountDto, Account>(updateDto, accountEntity);
            accountEntity = await _accountRepository.UpdateAsync(accountEntity);
            AccountDto result = _mapper.Map<Account, AccountDto>(accountEntity);
            return result;
        }

        public async Task<AccountDto> Login(LoginDto login)
        {
            Account accountEntity = await _accountRepository.GetByUsernameAsync(login.Username);
            if (accountEntity != null)
            {
                byte[] byteSalt = Convert.FromHexString(accountEntity.Salt);
                string hash = PasswordHasher.HashPasword(login.Password, byteSalt, out var newSalt);
                if (PasswordHasher.VerifyPassword(login.Password, hash, byteSalt))
                {
                    AccountDto result = _mapper.Map<Account, AccountDto>(accountEntity);
                    return result;
                }
                else
                {
                    throw new InvalidCredentials("Invalid credentials to login");
                }
            }
            else
            {
                throw new EntityNotFound($"Account not found with username {login.Username}");
            }
            
        }
    }
}
