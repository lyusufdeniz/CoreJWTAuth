using Auth.Core.Entities;
using Auth.Core.Service;
using AutoMapper;
using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Identity;
using SharedLibrary.DTO;

namespace Auth.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;



        public UserService(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<ResponseDTO<UserDTO>> CreateUserAsync(CreateUserDTO userDTO)
        {
            var user = new User { Email = userDTO.EMail, UserName = userDTO.UserName };
            var result = await _userManager.CreateAsync(user, userDTO.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return ResponseDTO<UserDTO>.Fail(400, new ErrorDTO(errors, true));
            }
            return ResponseDTO<UserDTO>.Succes(_mapper.Map<UserDTO>(user), 200);
        }

        public async Task<ResponseDTO<UserDTO>> GetUserByName(string name)
        {
            var user=await _userManager.FindByNameAsync(name);
            if (user == null) ResponseDTO<UserDTO>.Fail(404, "User Not FOund",true);
            return ResponseDTO<UserDTO>.Succes(_mapper.Map<UserDTO>(user), 200);
        }
    }
}
