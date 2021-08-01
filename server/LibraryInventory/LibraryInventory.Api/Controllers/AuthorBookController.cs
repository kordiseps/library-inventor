using Dapper;
using LibraryInventory.Api.Models;
using LibraryInventory.Api.Models.DTO;
using LibraryInventory.Api.Models.Entity;
using LibraryInventory.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace LibraryInventory.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorBookController : ControllerBase
    {

        private readonly BookRepository bookRepository;
        public AuthorBookController(IConfiguration configuration)
        {
            bookRepository = new BookRepository(configuration);
        }
        [HttpGet]
        public IEnumerable<AuthorsBookDTO> Get(int id)
        {
            var books = bookRepository.GetAuhtorsBooks(id).
                Select(b => new AuthorsBookDTO
                {
                    Id = b.Id,
                    Name = b.Name,
                    Lang = b.Lang,
                    PublishedIn = b.PublishedIn
                }).
                ToList();
            return books;
        }
        [HttpDelete]
        public OperationResponse Remove(int bookId, int authorId)
        {
            try
            {
                return new OperationResponse { IsSuccess = true };
            }
            catch (System.Exception ex)
            {
                return new OperationResponse
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
