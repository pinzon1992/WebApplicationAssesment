using Microsoft.AspNetCore.Mvc;

namespace WebApplicationAssesment.Common.Interfaces
{
    public interface IBaseActionsAsync<T, U, V>
    {
        Task<IActionResult> GetAll();
        Task<IActionResult> Get(V id);
        Task<IActionResult> Create(U createDto);
        Task<IActionResult> Update(T dto);
        Task<IActionResult> Delete(V id);
    }
}
