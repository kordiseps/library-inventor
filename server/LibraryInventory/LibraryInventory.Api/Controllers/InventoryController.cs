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

        public InventoryController(ILogger<InventoryController> logger, IConfiguration configuration)
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
            try
            {
                var insertSql = "INSERT INTO Book (Name,Author,PublishedIn,Lang) " +
                    "VALUES (@Name,@Author,@PublishedIn,@Lang)";
                sqlConnection.Execute(insertSql, book);
                return new InventoryOperationResponse { IsSuccess = true };

            }
            catch (System.Exception ex)
            {
                return new InventoryOperationResponse
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        [HttpPut]
        public InventoryOperationResponse Update([FromQuery] int id, [FromBody] Book book)
        {
            book.Id = id;
            try
            {
                var updateSql = "UPDATE Book SET Name = @Name, Author= @Author, PublishedIn = @PublishedIn, Lang= @Lang WHERE Id = @Id";
                sqlConnection.Execute(updateSql, book);
                return new InventoryOperationResponse { IsSuccess = true };

            }
            catch (System.Exception ex)
            {
                return new InventoryOperationResponse
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        [HttpDelete]
        public InventoryOperationResponse Remove(int id)
        {
            try
            {
                var deleteSql = "DELETE FROM Book WHERE Id=@Id";
                sqlConnection.Execute(deleteSql, new { Id = id });
                return new InventoryOperationResponse { IsSuccess = true };

            }
            catch (System.Exception ex)
            {
                return new InventoryOperationResponse
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }


    }

}
