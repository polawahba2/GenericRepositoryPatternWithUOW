using LibraryManagementSystem.Core;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.API.Controllers
{
    public class BooksController : BaseAPIController
    {
        private readonly IUnitOfWork _unitOfWork;

        public BooksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetBookByID")]
        public async Task<IActionResult> GetByIDAsync(int id)
        {
            var author = await _unitOfWork.Books.GetByIDAsync(
                id,
                ["Author"]
            );

            if (author == null) return NotFound("There Is No Book With This ID");

            return Ok(author);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _unitOfWork.Books.GetAllAsync(["Author"]));
        }

        [HttpGet("Search")]
        public async Task<IActionResult> FindByCriteriaAsync(string bookTitle, int? pageNumber = null, int? rowsCount = null)
        {
            var result = await _unitOfWork.Books.FindByCriteriaAsync(
                b => b.Title.Contains(bookTitle.Trim()),
                ["Author"],
                (pageNumber - 1) * rowsCount,
                rowsCount
                );
            if (result == null) return NotFound("There is No Book With this Title");
            return Ok(result);
        }

        [HttpPost("AddBooks")]
        public async Task<IActionResult> AddBookAsync(string bookTitle,int authorId)
        {
            var book = new Book
            {
                Title = bookTitle,
                AuthorId = authorId
            };
            var result = await _unitOfWork.Books.AddAsync(book);
            await _unitOfWork.SaveChangesAsync();
            return Ok(result);
        }
    }
}