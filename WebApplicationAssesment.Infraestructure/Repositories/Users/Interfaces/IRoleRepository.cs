﻿using WebApplicationAssesment.Domain.Users;
using WebApplicationAssesment.Infraestructure.Repositories.Common.Interfaces;

namespace WebApplicationAssesment.Infraestructure.Repositories.Users.Interfaces
{
    public interface IRoleRepository : IBaseRepositoryAsync<Role, long>
    {
    }
}
