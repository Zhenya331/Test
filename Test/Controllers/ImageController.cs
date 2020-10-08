using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Test.Models;
using Test.Services.IService;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService imageService;

        public ImageController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        // POST: api/<ImageController>
        // FindUserByImage
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Post()
        {
            var file = Request.Form.Files[0];

            if (file.Length > 0)
            {
                using (var reader = new BinaryReader(file.OpenReadStream()))
                {
                    //string contentAsString = reader.ReadToEnd();
                    //byte[] ImageData = new byte[contentAsString.Length * sizeof(char)];
                    //Buffer.BlockCopy(contentAsString.ToCharArray(), 0, ImageData, 0, ImageData.Length);
                    byte[] ImageData = null;
                    ImageData = reader.ReadBytes((int)file.Length);

                    var result = imageService.FindUserByImage(ImageData, out string ErrorMessage);

                    if (ErrorMessage == "")
                    {
                        ErrorMessage = "ok";
                        return Ok(new { result.Id, result.LastName, result.FirstName, result.FatherName, ErrorMessage });
                    }
                    return Ok(new { Id = -1, LastName = "-", FirstName = "-", FatherName = "-", ErrorMessage });
                }
            }

            return BadRequest();
        }

        // GET api/<ImageController>/5
        // GetImages
        [HttpGet("{id}")]
        public IEnumerable<Images> Get(long id)
        {
            var result = imageService.GetImages(id, out string ErrorMessage);

            if (ErrorMessage == "")
            { 
                return result; 
            }

            Debug.WriteLine(ErrorMessage);
            return null;
        }

        // POST api/<ImageController>
        // AddImage
        [HttpPost("{UserId}"), DisableRequestSizeLimit]
        public IActionResult Post(long UserId)
        {
            var file = Request.Form.Files[0];
            
            if (file.Length > 0)
            {
                using (var reader = new BinaryReader(file.OpenReadStream()))
                {
                    //string contentAsString = reader.ReadToEnd();
                    //byte[] ImageData = new byte[contentAsString.Length * sizeof(char)];
                    //Buffer.BlockCopy(contentAsString.ToCharArray(), 0, ImageData, 0, ImageData.Length);
                    byte[] ImageData = null;
                    ImageData = reader.ReadBytes((int)file.Length);

                    imageService.AddImage(UserId, ImageData, out string ErrorMessage);

                    if (ErrorMessage != "")
                    {
                        Debug.WriteLine(ErrorMessage);
                        return BadRequest(ErrorMessage);
                    }

                    ErrorMessage = "Изображение добавлено успешно";
                    return Ok(ErrorMessage);
                }
            }
            return BadRequest();
        }

        // DELETE api/<ImageController>/5
        // DeleteImage
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            imageService.DeleteImage(id, out string ErrorMessage);

            if (ErrorMessage != "") 
            {
                Debug.WriteLine(ErrorMessage);
                return BadRequest(ErrorMessage);
            }
            ErrorMessage = "Изображение удалено успешно";
            return Ok(ErrorMessage);
        }
    }
}
