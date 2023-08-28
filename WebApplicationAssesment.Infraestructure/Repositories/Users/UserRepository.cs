using Dapper;
using WebApplicationAssesment.Domain.Common.CustomExceptions;
using WebApplicationAssesment.Domain.Users;
using WebApplicationAssesment.Infraestructure.DBContext;
using WebApplicationAssesment.Infraestructure.Repositories.Users.Interfaces;

namespace WebApplicationAssesment.Infraestructure.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly WebApplicationAssesmentContext _context;
        public UserRepository(WebApplicationAssesmentContext context)
        {
            _context = context;
        }

        public async Task<User> CreateAsync(User entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            var sql = "insert into public.user (firstname,lastname, account_id, created_at) VALUES (@Firstname,@Lastname,@AccountId, @CreatedAt) RETURNING id";
            using (var connection = _context.CreateConnection())
            {
                var id = await connection.ExecuteScalarAsync<long>(sql, entity);
                entity.Id = id;
            }
            return entity;
        }

        public async Task<User> DeleteAsync(long id)
        {
            string sql = "select * from public.user where id = @id";
            User result;
            using (var connection = _context.CreateConnection())
            {
                result = (await connection.QueryAsync<User>(sql, new { id }).ConfigureAwait(false)).FirstOrDefault();
                if (result != null)
                {
                    result.IsDeleted = true;
                }
                else
                {
                    throw new EntityNotFound($"User with {id} doesnt exist");
                }

                string sqlDelete = "update public.user set is_deleted = true where id = @id";
                var rowAffected = await connection.ExecuteAsync(sqlDelete, new { id });
                if (rowAffected > 0)
                {
                    return result;
                }
                else
                {
                    throw new NoAffectedRows("User couldnt be deleted");
                }

            }
        }

        public async Task<ICollection<User>> GetAllAsync()
        {
            string sql = "select id as Id, firstname as FirstName, lastname as Lastname, " +
                         "account_id as AccountId, created_at as CreatedAt, updated_at as UpdatedAt, " +
                         "deleted_at as DeletedAt, is_deleted as IsDeleted " +
                         "from public.user where is_deleted = false";
            ICollection<User> result;
            using (var connection = _context.CreateConnection())
            {
                result = (await connection.QueryAsync<User>(sql, new { })).ToList();
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return new List<User>();
                }
            }
        }

        public async Task<User> GetByAccountIdAsync(long accountId)
        {
            string sql = "select id as Id, firstname as FirstName, lastname as Lastname, " +
                         "account_id as AccountId, created_at as CreatedAt, updated_at as UpdatedAt, " +
                         "deleted_at as DeletedAt, is_deleted as IsDeleted " +
                         "from public.user where account_id = @accountId and is_deleted = false";
            User result;
            using (var connection = _context.CreateConnection())
            {
                result = (await connection.QueryAsync<User>(sql, new { accountId }).ConfigureAwait(false)).FirstOrDefault();
                if (result != null)
                {
                    return result;
                }
                else
                {
                    throw new EntityNotFound($"User with accountId {accountId} doesnt exist");
                }
            }
        }

        public async Task<User> GetByIdAsync(long id)
        {
            string sql = "select id as Id, firstname as FirstName, lastname as Lastname, " +
                         "account_id as AccountId, created_at as CreatedAt, updated_at as UpdatedAt, " +
                         "deleted_at as DeletedAt, is_deleted as IsDeleted " +
                         "from public.user where id = @id and is_deleted = false";
            User result;
            using (var connection = _context.CreateConnection())
            {
                result = (await connection.QueryAsync<User>(sql, new { id }).ConfigureAwait(false)).FirstOrDefault();
                if (result != null)
                {
                    return result;
                }
                else
                {
                    throw new EntityNotFound($"User with id {id} doesnt exists");
                }
            }
        }

        public async Task<User> UpdateAsync(User entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            string sqlUpdate = "update public.user set firstname = @Firstname, lastname=@Lastname, updated_at = @UpdatedAt where id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var rowAffected = await connection.ExecuteAsync(sqlUpdate, entity);
                if (rowAffected > 0)
                {
                    return entity;
                }
                else
                {
                    throw new NoAffectedRows("User couldnt be updated");
                }
            }
        }
    }
}
