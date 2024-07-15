using Application.Contracts.RecipeContracts;
using Application.Contracts.UserContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Recipe.Pages
{
    [Authorize]
    public class UserRecipesModel : PageModel
    {
        public List<RecipeViewModel> recipes { get; set; }
        private readonly IRecipeApplication _recipeApplication;
        private readonly IUserApplication _userApplication;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public UserRecipesModel(IRecipeApplication recipeApplication,
            IUserApplication userApplication, IWebHostEnvironment hostingEnvironment)
        {
            _recipeApplication = recipeApplication;
            _userApplication = userApplication;
            _hostingEnvironment = hostingEnvironment;
        }
        public void OnGet()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value); //todo: verificar erro de autenticação
            recipes = _recipeApplication.SelectAllRecipes(u => u.AuthorId == userId);

            foreach (var item in recipes)
            {
                if (!String.IsNullOrEmpty(item.Image))
                {
                    
                    var decodedImage = DecodePic(item.Image, item.Title);
                    item.Image = SaveImage(decodedImage);
                }
            }
        }
        private IFormFile DecodePic(string base64String, string fileName)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            var memoryStream = new MemoryStream(imageBytes);
            IFormFile formFile = new FormFile(memoryStream, 0, imageBytes.Length, fileName, fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpg"
            };
            return formFile;
        }
        private string SaveImage(IFormFile image)
        {
            var rootPath = _hostingEnvironment.WebRootPath;
            var filename = Guid.NewGuid().ToString();
            var uploadPath = Path.Combine(rootPath, @"img\Recipes");
            var extension = ".jpg";
            var filePath = Path.Combine(uploadPath, filename + extension);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }
            var imageAddress = "img/Recipes/" + filename + extension;
            return imageAddress;
        }
    }
}
