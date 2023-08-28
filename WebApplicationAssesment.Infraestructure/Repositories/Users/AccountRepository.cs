using Dapper;
using WebApplicationAssesment.Domain.Common.CustomExceptions;
using WebApplicationAssesment.Domain.Users;
using WebApplicationAssesment.Infraestructure.DBContext;
using WebApplicationAssesment.Infraestructure.Repositories.Users.Interfaces;

namespace WebApplicationAssesment.Infraestructure.Repositories.Accounts
{
    public class AccountRepository : IAccountRepository
    {
        private readonly WebApplicationAssesmentContext _context;
        public AccountRepository(WebApplicationAssesmentContext context)
        {
            _context = context;
        }

        public async Task<Account> CreateAsync(Account entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            var sql = "insert into public.account (username, password, salt, role_id, created_at) VALUES (@Username,@Password, @Salt, @RoleId, @CreatedAt) RETURNING id";
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var id = await connection.ExecuteScalarAsync<long>(sql, entity);
                    entity.Id = id;
                }
                catch (Exception ex)
                {

                    if (ex.Message.Contains("unique_username_constraint"))
                    {
                        throw new UniqueUsername($"Username {entity.Username} is already used by another account, change username");
                    }
                }
            }
            return entity;
        }

        public async Task<Account> DeleteAsync(long id)
        {
            string sql = "select * from public.account where id = @id";
            Account result;
            using (var connection = _context.CreateConnection())
            {
                result = (await connection.QueryAsync<Account>(sql, new { id }).ConfigureAwait(false)).FirstOrDefault();
                if (result != null)
                {
                    result.IsDeleted = true;
                }
                else
                {
                    throw new EntityNotFound($"Account with id {id} doesn't exist");
                }

                string sqlDelete = "update public.account set is_deleted = true where id = @id";
                var rowAffected = await connection.ExecuteAsync(sqlDelete, new { id });
                if (rowAffected > 0)
                {
                    return result;
                }
                else
                {
                    throw new NoAffectedRows("Account couldnt be deleted");
                }

            }
        }

        public async Task<ICollection<Account>> GetAllAsync()
        {
            string sql = "select id as Id, username as Username, password as Password, " +
                         "role_id as RoleId, created_at as CreatedAt, updated_at as UpdatedAt, " +
                         "deleted_at as DeletedAt, is_deleted as IsDeleted " +
                         "from public.account where is_deleted = false";
            ICollection<Account> result;
            using (var connection = _context.CreateConnection())
            {
                result = (await connection.QueryAsync<Account>(sql, new { })).ToList();
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return new List<Account>();
                }
            }
        }

        public async Task<Account> GetByIdAsync(long id)
        {
            string sql = "select id as Id, username as Username, password as Password, " +
                         "role_id as RoleId, created_at as CreatedAt, updated_at as UpdatedAt, " +
                         "deleted_at as DeletedAt, is_deleted as IsDeleted " +
                         "from public.account where id = @id and is_deleted = false";
            Account result;
            using (var connection = _context.CreateConnection())
            {
                result = (await connection.QueryAsync<Account>(sql, new { id }).ConfigureAwait(false)).FirstOrDefault();
                if (result != null)
                {
                    return result;
                }
                else
                {
                    throw new EntityNotFound($"Account with id {id} doesnt exists");
                }
            }
        }

        public async Task<Account> GetByUsernameAsync(string username)
        {
            string sql = "select id as Id, username as Username, password as Password, " +
                         "salt as Salt, role_id as RoleId, created_at as CreatedAt, updated_at as UpdatedAt, " +
                         "deleted_at as DeletedAt, is_deleted as IsDeleted " +
                         "from public.account where username = @username and is_deleted = false";
            Account result;
            using (var connection = _context.CreateConnection())
            {
                result = (await connection.QueryAsync<Account>(sql, new { username }).ConfigureAwait(false)).FirstOrDefault();
                if (result != null)
                {
                    return result;
                }
                else
                {
                    throw new EntityNotFound($"Account with username {username} doesnt exists");
                }
            }
        }

        public async Task<Account> UpdateAsync(Account entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            string sqlUpdate = "update public.account set username = @Username, password=@Password, salt=@Salt, role_id = @RoleId, updated_at = @UpdatedAt where id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var rowAffected = await connection.ExecuteAsync(sqlUpdate, entity);
                if (rowAffected > 0)
                {
                    return entity;
                }
                else
                {
                    throw new NoAffectedRows("Account couldnt be updated");
                }
            }
        }
    }
}
