using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Test.Models;
using Test.Services.IService;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        // GET: api/<UserController>
        // GetUsers
        [HttpGet]
        public IEnumerable<Users> Get([FromQuery] string LastName, [FromQuery] string FirstName, [FromQuery] string FatherName)
        {
            var result = userService.GetUsers(FirstName, LastName, FatherName, out string ErrorMessage);

            if (ErrorMessage == "") 
            {
                return result; 
            }
            Debug.WriteLine(ErrorMessage);
            return null;
        }

        // GET api/<UserController>/5
        // GetUserProfile
        [HttpGet("{id}")]
        public Users Get(long id)
        {
            var result = userService.GetUserProfile(id, out string ErrorMessage);

            if (ErrorMessage == "") { return result; }
            Debug.WriteLine(ErrorMessage);
            return null;
        }

        // POST api/<UserController>
        // AddUser
        [HttpPost("{lastname}/{firstname}/{fathername}")]
        public IActionResult Post(string LastName, string FirstName, string FatherName)
        {
            userService.AddUser(FirstName, LastName, FatherName, out string ErrorMessage);
            if (ErrorMessage != "") 
            { 
                Debug.WriteLine(ErrorMessage);
                return BadRequest(ErrorMessage);
            }
            ErrorMessage = $"Пользователь с ФИО: {LastName} {FirstName} {FatherName} добавлен успешно";
            return Ok(ErrorMessage);
        }

        // PUT api/<UserController>/5
        // UpdateUser
        [HttpPut("{id}/{lastname}/{firstname}/{fathername}")]
        public IActionResult Put(long id, string LastName, string FirstName, string FatherName)
        {
            userService.UpdateUser(id, FirstName, LastName, FatherName, out string ErrorMessage);
            if (ErrorMessage != "") 
            { 
                Debug.WriteLine(ErrorMessage);
                return BadRequest(ErrorMessage);
            }
            ErrorMessage = "ФИО изменено успешно";
            return Ok(ErrorMessage);
        }

        // DELETE api/<UserController>/5
        // DeleteUser
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            userService.DeleteUser(id, out string ErrorMessage);

            if (ErrorMessage != "") 
            { 
                Debug.WriteLine(ErrorMessage);
                return BadRequest(ErrorMessage);
            }

            ErrorMessage = "Пользователь удален успешно";
            return Ok(ErrorMessage);
        }
    }
}
