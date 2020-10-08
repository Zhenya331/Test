using System.Collections.Generic;
using Test.Models;

namespace Test.Services.IService
{
    public interface IImageService
    {
        // Добавить фото [HttpPost]
        void AddImage(long UserID,  byte[] ImageData, out string ErrorMessage);

        // Найти человека по фото [HttpPost]
        Users FindUserByImage(byte[] ImageData, out string ErrorMessage);

        // Получить все фото человека [HttpGet]
        IEnumerable<Images> GetImages(long UserID, out string ErrorMessage);

        //Удалить фото по его id [httpDelete]
        void DeleteImage(long id, out string ErrorMessage);
    }
}