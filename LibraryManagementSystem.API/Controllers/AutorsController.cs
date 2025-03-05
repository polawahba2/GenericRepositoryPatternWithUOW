using LibraryManagementSystem.Core;
using LibraryManagementSystem.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.API.Controllers
{
    public class AuthorsController : BaseAPIController
    {

        private readonly IUnitOfWork _unitOfWork;
        public AuthorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var author = await _unitOfWork.Authors.GetByIDAsync(id);
            if (author == null) return NotFound("There Is No Author With This ID");
            return Ok(author);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() => Ok(await _unitOfWork.Authors.GetAllAsync());


        [HttpGet("Search{authorName}")]
        public async Task<IActionResult> FindByCriteriaAsync(string authorName)
        {
            var result = await _unitOfWork.Authors.FindByCriteriaAsync(a => a.Name.Contains(authorName.Trim()));
            if (result == null) return NotFound("There is No Author With this Name");
            return Ok(result);
        }

        [HttpPost("AddAuthor")]
         public async Task<IActionResult> AuthorAsync(string name)
        {
            var Author = new Author
            {
                Name = name
            };
            var result = await _unitOfWork.Authors.AddAsync(Author);
            await _unitOfWork.SaveChangesAsync();
            return Ok(result);
        }
    }
}