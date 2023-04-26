using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using UserServiceAPI.Model;
using UserServiceAPI.Repository;

namespace UserServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            
            var users = await _userRepository.GetUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            using var activity = DiagnosticsConfig.ActivitySource.StartActivity();
            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            
            using (Log.Logger.BeginTimedOperation("Running GetUserById method"))
            {
                Random rnd = new Random();
                Thread.Sleep(rnd.Next(200, 1000));
            };
            Log.Logger.Debug("Finding user with ID #{id}", id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            using var activity = DiagnosticsConfig.ActivitySource.StartActivity();
            
            await _userRepository.AddUserAsync(user);
            
            Log.Logger.Debug("Adding user with ID #" + user.Id);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            using var activity = DiagnosticsConfig.ActivitySource.StartActivity();
            if (id != user.Id)
            {
                return BadRequest();
            }
            await _userRepository.UpdateUserAsync(user);
            Log.Logger.Debug("Updated user {user.Name} with ID #{user.Id}", user.Name, user.Id);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            using var activity = DiagnosticsConfig.ActivitySource.StartActivity();
            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            await _userRepository.DeleteUserAsync(user);
            Log.Logger.Debug("Deleted user with ID #{id}", id);
            
            return NoContent();
        }
    }
}
