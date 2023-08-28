namespace WebApplicationAssesment.Application.Common.Interfaces
{
    public interface IBaseServiceAsync<T, U, V>
    {
        Task<ICollection<T>> GetAllAsync();
        Task<T> GetByIdAsync(V id);
        Task<T> CreateAsync(U createDto);
        Task<T> UpdateAsync(T updateDto);
        Task<T> DeleteByIdAsync(V id);
    }
}
