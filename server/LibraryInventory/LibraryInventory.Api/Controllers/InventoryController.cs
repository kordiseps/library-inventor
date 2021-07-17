using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace LibraryInventory.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;

        public InventoryController(ILogger<InventoryController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Book> Get()
        {
            _logger.Log(LogLevel.Information, "get started");
            return books;
        }

        [HttpGet("​{id}")]
        public Book GetSingle(int id)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            return book;
        }

        [HttpPost]
        public GeneralResponse Add(Book book)
        {
            book.Id = books.Max(x => x.Id) + 1;
            books.Add(book);
            return new GeneralResponse { IsSuccess = true };
        }

        [HttpPut()]
        public GeneralResponse Update([FromQuery] int id, [FromBody] Book book)
        {
            Remove(id);
            book.Id = id;
            books.Add(book);
            return new GeneralResponse { IsSuccess = true };
        }

        [HttpDelete()]
        public GeneralResponse Remove(int id)
        {
            books.RemoveAll(x => x.Id == id);
            return new GeneralResponse { IsSuccess = true };
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
