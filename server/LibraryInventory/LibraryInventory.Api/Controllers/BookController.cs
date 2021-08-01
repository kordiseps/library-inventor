using LibraryInventory.Api.Models;
using LibraryInventory.Api.Models.DTO;
using LibraryInventory.Api.Models.Entity;
using LibraryInventory.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace LibraryInventory.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly BookRepository bookRepository;
        private readonly AuthorRepository authorRepository;

        public BookController(ILogger<BookController> logger, IConfiguration configuration)
        {
            _logger = logger;
            bookRepository = new BookRepository(configuration);
            authorRepository = new AuthorRepository(configuration);
        }

        private BooksPageDTO AsDto(Book book)
        {
            return new BooksPageDTO
            {
                Id = book.Id,
                AuthorId = book.AuthorId,
                AuthorName = authorRepository.GetSingle(book.AuthorId).Name,
                Name = book.Name,
                Lang = book.Lang,
                PublishedIn = book.PublishedIn
            };
        }

        [HttpGet]
        public IEnumerable<BooksPageDTO> Get()
        {
            List<BooksPageDTO> books = bookRepository.GetAll()
                .Select(AsDto)
                .ToList();
            return books;
        }

        [HttpGet("​{id}")]
        public BooksPageDTO GetSingle(int id)
        {
            var book = AsDto(bookRepository.GetSingle(id));
            return book;
        }

        [HttpPost]
        public OperationResponse Add(AddBookDTO book)
        {
            var addResult = bookRepository.Add(book.Name, book.AuthorId, book.PublishedIn, book.Lang);
            return new OperationResponse { IsSuccess = addResult };
        }

        [HttpPut]
        public OperationResponse Update([FromQuery] int id, [FromBody] UpdateBookDTO book)
        {
            var updateResult = bookRepository.Update(id, book.Name, book.PublishedIn, book.Lang);
            return new OperationResponse { IsSuccess = updateResult };
        }

        [HttpDelete]
        public OperationResponse Remove(int id)
        {
            var removeResult = bookRepository.Remove(id);
            return new OperationResponse { IsSuccess = removeResult };
        }


    }

}
