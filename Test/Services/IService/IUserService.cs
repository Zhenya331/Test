using System.Collections.Generic;
using Test.Models;

namespace Test.Services.IService
{
    public interface IUserService
    {
        // Добавить человека [HttpPost]
        void AddUser(string FirstName, string LastName, string FatherName, out string ErrorMessage);

        // Получить список людей [HttpGet]
        IEnumerable<Users> GetUsers(string FirstName, string LastName, string FatherName, out string ErrorMessage);
        
        // Удалить человека [HttpDelete]
        void DeleteUser(long id, out string ErrorMessage);

        // Изменить ФИО человека [HttpPut]
        void UpdateUser(long id, string FirstName, string LastName, string FatherName, out string ErrorMessage);

        // Получить информацию о человеке [HttpGet]
        Users GetUserProfile(long id, out string ErrorMessage);
    }
}