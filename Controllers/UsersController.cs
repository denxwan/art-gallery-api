using System.Windows.Input;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using art_gallery_api.Persistence;
using art_gallery_api.Models;

namespace art_gallery_api.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    // Dependency Injection

    private readonly IUserDataAccess _usersRepo;
    public UsersController(IUserDataAccess userRepo)
    {
        _usersRepo = userRepo;
    }

    // Endpoints here

    // -- SELECT COMMANDS --

    [HttpGet()]
    public IEnumerable<User> GetUsers()
    {
        return _usersRepo.GetUsers();
    }

    [HttpGet("admin")]
    public IEnumerable<User> GetAdminUsersOnly()
    {
        return _usersRepo.GetAdminUsersOnly();
    }

    [HttpGet("member")]
    public IEnumerable<User> GetMemebersOnly()
    {
        return _usersRepo.GetMemebersOnly();
    }

    [HttpGet("{id}", Name = "GetUsers")]
    public IActionResult GetUserById(int id)
    {
        foreach (var user in _usersRepo.GetUsers())
        {
            if (user.UserId == id)
            {
                return Ok(user);
            }
        }
        return NotFound();
    }

    // -- INSERT COMMANDS --

    [HttpPost()]
    public IActionResult AddUser(User newUser)
    {
        if(newUser == null)
        {
            return BadRequest();
        }

        foreach (var user in _usersRepo.GetUsers())
        {
            if(newUser.Firstname.Equals(user.Firstname) && (newUser.Lastname.Equals(user.Lastname)) && (newUser.Email.Equals(user.Email)))
            {
                return Conflict();
            }
        }

        User result = _usersRepo.InsertUser(newUser);

        return Created("GetUser", result);
    }

    // -- UPDATE COMMANDS --

    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, User updatedUser)
    {
        foreach (var user in _usersRepo.GetUsers())
        {
            if (user.UserId == id)
            {
                User result;

                try
                {
                    result = _usersRepo.UpdateUser(user, updatedUser);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    return BadRequest();
                }

                return Ok(result);
            }
        }
        return NotFound();
    }

    // -- DELETE COMMANDS --

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        foreach (var user in _usersRepo.GetUsers())
        {
            if (user.UserId == id)
            {
                _usersRepo.RemoveUser(id);
                return Ok();
            }
        }
        return NotFound();
    }
}
