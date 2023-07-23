using BCrypt.Net;
using HardwareStoreRepository.DTO.Login;
using HardwareStoreRepository.DTO.Register;
using HardwareStoreRepository.Helper;
using HardwareStoreRepository.Models;
using HardwareStoreRepository.Repo.Customer;
using HardwareStoreRepository.Repo.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace HardwareStoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly JWTService  _jWTService;
        public AuthenticationController(IUserRepository userRepository,JWTService jWTService)
        {
            _userRepository = userRepository;
            _jWTService = jWTService;

        }

        /// <summary>
        /// Register a new user
        /// </summary> 
        /// /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Authentication/Register
        ///     {       
        ///       "Name": "Suhib Alhanafi",
        ///       "Email": "Suhib@yahoo.com",
        ///       "password": "qqzvxzvs1"        
        ///     }
        /// </remarks>
        [HttpPost("Register")]
        public IActionResult Register(RegisterDTOReq req)
        {
            var user = new User
            {
                Name = req.Name,
                Email =req .Email ,
                Password =  BCrypt.Net.BCrypt.HashPassword (req.Password) //hash

            };
           
            return Created ("Success", _userRepository.Create(user));
        }

        /// <summary>
        /// Authenticate and retrieve a JWT token.
        /// </summary> 
        /// /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Authentication/Login
        ///     {       
        ///       "Email": "Suhib@yahoo.com",
        ///       "password": "qqzvxzvs1"        
        ///     }
        /// </remarks>
        [HttpPost("Login")]
        public IActionResult Login(LoginDTOReq req)
        {
            var user = _userRepository.GetByEmail(req.Email);

            if(user ==null)
            {
                return BadRequest(new { message = "Invalid Credentials"});
            }

            if( !BCrypt.Net.BCrypt.Verify (req.Password ,user.Password))
            {
                return BadRequest(new { message = "Invalid Credentials" });

            }

            var jwt = _jWTService.Generate(user.Id);
            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true
            });

            return Ok(new { Message ="Success" });

        }

        [HttpGet("User")]
        public IActionResult User()
        {
            var jwt = Request.Cookies["jwt"];
            var token = _jWTService.Verify(jwt.ToString());

            int UserId = int.Parse((token.Issuer));
            
            var user= _userRepository.GetById(UserId);
            return Ok(user);
        }


        [HttpHead]
        public IActionResult Logout()
        {
            return Ok(new { Message = "Succuss" });
        }
    }
}
