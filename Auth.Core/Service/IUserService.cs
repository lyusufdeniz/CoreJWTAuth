using Auth.Core.Entities;
using SharedLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Core.Service
{
    public interface IUserService
    {
        Task<ResponseDTO<UserDTO>> CreateUserAsync(CreateUserDTO userDTO);
        Task<ResponseDTO<UserDTO>> GetUserByName(string name);

    }
}
