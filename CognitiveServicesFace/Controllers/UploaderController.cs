using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CognitiveServicesFace.Data;
using CognitiveServicesFace.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CognitiveServicesFace.Controllers
{
    public class UploaderController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly FaceHelper _faceHelper;
        private readonly CognitiveServicesFaceContext _context;

        public UploaderController(IConfiguration config, IWebHostEnvironment webHostEnvironment, CognitiveServicesFaceContext context)
        {
            _config = config;
            _webHostEnvironment = webHostEnvironment;

            string key = _config.GetValue<string>("CognitiveServiceFaceKey");
            _faceHelper = new FaceHelper(key);

            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile file)
        {
            if (file?.Length > 0)
            {
                string folderPath = _config.GetValue<string>("UploadImageDirectory");

                var pictureName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName).ToLower();

                string webRootPath = _webHostEnvironment.WebRootPath;

                string route = Path.Combine(webRootPath, folderPath, pictureName);

                using (var stream = new FileStream(route, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                using (var fs = new FileStream(route, FileMode.Open))
                {
                    var picture = await _faceHelper.DetectAndExtractFaceAsync(fs);

                    picture.Name = file.FileName;
                    picture.Path = $"{folderPath}/{pictureName}";

                    _context.Pictures.Add(picture);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Details", "Pictures", new { Id = picture.Id });
                }
            }

            return View();
        }
    }
}
