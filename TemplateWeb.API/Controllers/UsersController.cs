using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TemplateWeb.BL;
using TemplateWeb.DAL;

namespace TemplateWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserManager __userManager;
        private readonly IConfiguration _configuration;

        public UsersController(UserManager<User> userManager, IConfiguration configuration, IUserManager userManager_)
        {
            _userManager = userManager;
            __userManager = userManager_;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<string>> Register(RegisterDTO registerDTO)
        {
            var existingUser = await _userManager.FindByNameAsync(registerDTO.username!);
            if (existingUser != null)
            {
                return BadRequest("Username is already taken.");
            }

            var newUser = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = registerDTO.username,
                firstName = registerDTO.firstName,
                lastName = registerDTO.lastName,
                address = registerDTO.address,
                birthday = registerDTO.birthday,
                role=registerDTO.role,
                PhoneNumber= registerDTO.phone,
                Email = registerDTO.email,
                creationDate = DateTime.Now.ToString(),
                updatedDate = DateTime.Now.ToString()
            };

            var creationResult = await _userManager.CreateAsync(newUser, registerDTO.password!);

            if (!creationResult.Succeeded)
            {
                return BadRequest(creationResult.Errors);
            }

            // Fetch the user from the database
            var createdUser = await _userManager.FindByNameAsync(newUser.UserName!);
            if (createdUser == null)
            {
                return BadRequest("User was not found after creation");
            }

            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, newUser.UserName!),
            };

            // Now add the claims
            var claimsResult = await _userManager.AddClaimsAsync(newUser, userClaims);
            if (!claimsResult.Succeeded)
            {
                // If adding the claims fails, delete the user to avoid orphaned users
                await _userManager.DeleteAsync(newUser);
                return BadRequest(claimsResult.Errors);
            }

            // Use UserManager to add additional functionality
            return Ok(newUser);
        }


        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<TokenDTO>> Login(LoginDTO credintials)
        {
            var user = await _userManager.FindByNameAsync(credintials.Username!);
            if (user == null)
            {
                return BadRequest("User Not Found");
            }
            if (await _userManager.IsLockedOutAsync(user))
            {
                return BadRequest("Try Again Later, Your Profile is locked!");
            }
            var userClaims = await _userManager.GetClaimsAsync(user);
            bool isAuthenticated = await _userManager.CheckPasswordAsync(user, credintials.Password!);
            if (!isAuthenticated)
            {
                await _userManager.AccessFailedAsync(user);
                return Unauthorized("Wrong Username or Password");
            }
            var exp = DateTime.Now.AddMinutes(750);
            var secretKey = _configuration.GetValue<string>("SecretKey");
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new ArgumentNullException("The secret key cannot be null or empty.");
            }
            var secretKeyBytes = Encoding.ASCII.GetBytes(secretKey);
            var Key = new SymmetricSecurityKey(secretKeyBytes);
            var methodGeneratingToken = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature);
            var jwt = new JwtSecurityToken(
                claims: userClaims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(120)
            );
            var tokenHandler = new JwtSecurityTokenHandler();
            string tokenString = tokenHandler.WriteToken(jwt);
            return new TokenDTO
            {
                Token = tokenString,
                ExpiryDate = exp,
                Username = user.UserName,
                User_id = user.Id,
            };
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var Users = __userManager.GetAllUsers();
            return Ok(Users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(string id)
        {
            var User = __userManager.GetUserById(id);
            if (User == null)
            {
                return BadRequest("User not found");
            }
            return Ok(User);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(string id)
        {
            UserDTO? User = __userManager.GetUserById(id);
            if (User == null)
            {
                return NotFound("User not found");
            }
            __userManager.DeleteUser(id);
            return Ok("User " + id + " has been deleted successfully");
        }

        [HttpPut("{id}")]
        public ActionResult<User> UpdateUser(string id, UserDTO User)
        {

            if (id != User.UserID)
            {
                return BadRequest();
            }
            __userManager.UpdateUser(User);
            return NoContent();
        }

    }
}
