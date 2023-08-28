using Dapper;
using WebApplicationAssesment.Domain.Common.CustomExceptions;
using WebApplicationAssesment.Domain.Users;
using WebApplicationAssesment.Infraestructure.DBContext;
using WebApplicationAssesment.Infraestructure.Repositories.Users.Interfaces;

namespace WebApplicationAssesment.Infraestructure.Repositories.Users
{
    public class RoleRepository : IRoleRepository
    {
        private readonly WebApplicationAssesmentContext _context;
        public RoleRepository(WebApplicationAssesmentContext context) 
        {
            _context = context;
        }

        public async Task<Role> CreateAsync(Role entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            var sql = "insert into public.role (name,created_at) VALUES (@Name,@CreatedAt) RETURNING id";
            using (var connection = _context.CreateConnection())
            {
                var id = await connection.ExecuteScalarAsync<long>(sql, entity);
                entity.Id = id;
            }
            return entity;
        }

        public async Task<Role> DeleteAsync(long id)
        {
            string sql = "select * from public.role where id = @id";
            Role result;
            using (var connection = _context.CreateConnection())
            {
                result = (await connection.QueryAsync<Role>(sql, new { id}).ConfigureAwait(false)).FirstOrDefault();
                if (result!=null)
                {
                    result.IsDeleted = true;
                }
                else
                {
                    throw new EntityNotFound($"Role with {id} doesnt exist");
                }

                string sqlDelete = "update public.role set is_deleted = true where id = @id";
                var rowAffected = await connection.ExecuteAsync(sqlDelete, new {id});
                if (rowAffected > 0)
                {
                    return result;
                }
                else
                {
                    throw new NoAffectedRows("Role couldn't be deleted");
                }

            }
        }

        public async Task<ICollection<Role>> GetAllAsync()
        {
            string sql = "select id as Id, name as Name, " +
                         "created_at as CreatedAt, updated_at as UpdatedAt, " +
                         "deleted_at as DeletedAt, is_deleted as IsDeleted " +
                         "from public.role where is_deleted = false";
            ICollection<Role> result;
            using (var connection = _context.CreateConnection())
            {
                result = (await connection.QueryAsync<Role>(sql, new { })).ToList();
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return new List<Role>() ;
                }
            }
        }

        public async Task<Role> GetByIdAsync(long id)
        {
            string sql = "select id as Id, name as Name, created_at as CreatedAt, updated_at as UpdatedAt, deleted_at as DeletedAt from public.role where id = @id and is_deleted = false";
            Role result;
            using (var connection = _context.CreateConnection())
            {
                result = (await connection.QueryAsync<Role>(sql, new { id }).ConfigureAwait(false)).FirstOrDefault();
                if (result != null)
                {
                     return result;
                }
                else
                {
                    throw new EntityNotFound($"Role with id {id} doesnt exists");
                }
            }
        }

        public async Task<Role> UpdateAsync(Role entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            string sqlUpdate = "update public.role set name = @Name, updated_at = @UpdatedAt where id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var rowAffected = await connection.ExecuteAsync(sqlUpdate, entity);
                if (rowAffected > 0)
                {
                    return entity;
                }
                else
                {
                    throw new NoAffectedRows("Role couldn't be updated");
                }
            }
                
        }
    }
}
