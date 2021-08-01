using LibraryInventory.Api.Models;
using LibraryInventory.Api.Models.DTO;
using LibraryInventory.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace LibraryInventory.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {

        private readonly AuthorRepository authorRepository;
        private readonly BookRepository bookRepository;
        public AuthorController(IConfiguration configuration)
        {
            authorRepository = new AuthorRepository(configuration);
            bookRepository = new BookRepository(configuration);

        }
        [HttpGet]
        public IEnumerable<AuthorsPageDTO> Get()
        {
            var authorsFromDB = authorRepository.GetAll();
            List<AuthorsPageDTO> authors = new List<AuthorsPageDTO>();
            foreach (var item in authorsFromDB)
            {
                authors.Add(new AuthorsPageDTO
                {
                    Id = item.Id,
                    Country = item.Country,
                    Name = item.Name
                });
            }
            return authors;
        }

        [HttpGet("​{id}")]
        public AuthorsPageDTO GetSingle(int id)
        {
            var authorFromDb = authorRepository.GetSingle(id);
            var author = new AuthorsPageDTO
            {
                Id = authorFromDb.Id,
                Country = authorFromDb.Country,
                Name = authorFromDb.Name
            };
            return author;
        }

        [HttpPost]
        public OperationResponse Add(AddAuthorDTO author)
        {
            var addResult = authorRepository.Add(author.Name, author.Country);
            return new OperationResponse { IsSuccess = addResult };
        }

        [HttpPut]
        public OperationResponse Update([FromQuery] int id, [FromBody] UpdateAuthorDTO author)
        {
            var updateResult = authorRepository.Update(id, author.Name, author.Country);
            return new OperationResponse { IsSuccess = updateResult };
        }

        [HttpDelete]
        public OperationResponse Remove(int id)
        {
            var bookRemoveResult = bookRepository.RemoveAuhtorsBooks(id);
            if (bookRemoveResult)
            {
                var result = authorRepository.Remove(id);
                return new OperationResponse { IsSuccess = result };
            }
            else
            {
                return new OperationResponse { IsSuccess = false, ErrorMessage = "Books couldn't be deleted." };
            }

        }
    }
}
