using System;
using System.Collections.Generic;
using System.Linq;
using Test.Codes;
using Test.Models;
using Test.Services.IService;

namespace Test.Services.ServiceImplementation 
{
    public class ImageService : IImageService
    {
        private readonly TestDBContext context;

        public ImageService(TestDBContext context)
        {
            this.context = context;
        }

        public void DeleteImage(long id, out string ErrorMessage)
        {
            ErrorMessage = "";
            try
            {
                var image = context.Images.FirstOrDefault(p => p.Id == id);

                if (image != null)
                {
                    context.Images.Remove(image);
                    context.SaveChanges();
                }
                else
                {
                    ErrorMessage = ErrorCode.NotFindImage;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
            }
        }

        public Users FindUserByImage(byte[] ImageData, out string ErrorMessage)
        {
            ErrorMessage = "";
            try
            {
                //var images = context.Images.Where(p => p.ImageData.Length == ImageData.Length).ToList();
                var images = context.Images.ToList();

                long userId = CheckImage(ImageData, images, out ErrorMessage);

                if (userId != -1)
                {
                    var user = context.Users.FirstOrDefault(p => p.Id == userId);

                    if (user != null) { return user; }
                }
                ErrorMessage = ErrorCode.NotFindUser;
                return null;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
                return null;
            }
        }

        public IEnumerable<Images> GetImages(long UserID, out string ErrorMessage)
        {
            ErrorMessage = "";
            try
            {
                var images = context.Images.Where(p => p.UserId == UserID).ToList();
                return images;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
                return null;
            }
        }

        public void AddImage(long UserID,  byte[] ImageData, out string ErrorMessage)
        {
            ErrorMessage = "";
            try
            {
                if (CheckImage(ImageData, context.Images.ToList(), out ErrorMessage) == -1)
                {
                    context.Images.Add(new Images { UserId = UserID, ImageData = ImageData });
                    context.SaveChanges();
                }
                else
                {
                    ErrorMessage = ErrorCode.ImageIsAlreadyExist;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
            }
        }


        // Проверить изображение на уникальность.
        // Если фото есть, то вернет id человека
        private long CheckImage(byte[] ImageData, IEnumerable<Images> images, out string ErrorMessage)
        {
            ErrorMessage = "";
            try
            {
                long userId = -1;

                foreach (var image in images)
                {
                    for (long i = 0; i < ImageData.Length; i++)
                    {
                        if (ImageData[i] != image.ImageData[i]) { break; }
                        if (i == ImageData.Length - 1) { userId = image.UserId; }
                    }
                }
                return userId;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
                return -1;
            }
        }
    }
}
