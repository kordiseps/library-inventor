using Dapper;
using LibraryInventory.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace LibraryInventory.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;
        private readonly SqlConnection sqlConnection;

        public InventoryController(ILogger<InventoryController> logger,IConfiguration configuration)
        {
            _logger = logger;
            sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = configuration.GetConnectionString("MyDefault");
        }

        [HttpGet]
        public IEnumerable<Book> Get()
        {
            var booksFromDB = sqlConnection.Query<Book>("SELECT * FROM Book");
            return booksFromDB;
        }

        [HttpGet("​{id}")]
        public Book GetSingle(int id)
        {
            var book = sqlConnection.Query<Book>("SELECT * FROM Book WHERE Id = @Id", new { Id = id }).FirstOrDefault();
            return book;
        }

        [HttpPost]
        public InventoryOperationResponse Add(Book book)
        {
            if (books.Count > 10)
            {
                return new InventoryOperationResponse
                {
                    ErrorMessage = "Limit exceed",
                    IsSuccess = false
                };
            }
            book.Id = books.Max(x => x.Id) + 1;
            books.Add(book);
            return new InventoryOperationResponse { IsSuccess = true };
        }

        [HttpPut]
        public InventoryOperationResponse Update([FromQuery] int id, [FromBody] Book book)
        {
            Remove(id);
            book.Id = id;
            books.Add(book);
            return new InventoryOperationResponse { IsSuccess = true };
        }

        [HttpDelete]
        public InventoryOperationResponse Remove(int id)
        {
            books.RemoveAll(x => x.Id == id);
            return new InventoryOperationResponse { IsSuccess = true };
        }

        static List<Book> books = new List<Book>
        {
            new Book { Id= 1,   Name= "War and Peace",        Author= "Tolstoy",               PublishedIn= "1869",       Lang= "Russian" },
            new Book { Id= 2,   Name= "Anna Karenina",        Author= "Tolstoy",               PublishedIn= "1877",       Lang= "Russian" },
            new Book { Id= 3,   Name= "Crime And Punishment", Author= "Dostoyevski",           PublishedIn= "1866",       Lang= "Russian" },
            new Book { Id= 4,   Name= "The Miserables",       Author= "Victor Hugo",           PublishedIn= "1862",       Lang= "French"  },
            new Book { Id= 5,   Name= "Mind at Peace",        Author= "Ahmet Hamdi Tanpınar",  PublishedIn= "1949",       Lang= "Turkish" },
            new Book { Id= 6,   Name= "The Speech",           Author= "Mustafa Kemal Ataturk", PublishedIn= "1927",       Lang= "Turkish" },
            new Book { Id= 7,   Name= "The Waves",            Author= "Virginia Woolf",        PublishedIn= "1931",       Lang= "English" },
        };

    }

}
