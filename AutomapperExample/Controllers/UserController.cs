using AutoMapper;
using AutomapperExample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutomapperExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        public UserController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public UserInfoDto Get()
        {
            var user = new User
            {
                Id = 0,
                FirstName = "Ali",
                LastName = "Nayman",
                Status = 1,
            };

            var userInfo = _mapper.Map<UserInfoDto>(user);
            return userInfo;
        }
    }
}

