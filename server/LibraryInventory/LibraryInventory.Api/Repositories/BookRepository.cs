using Dapper;
using LibraryInventory.Api.Models.Entity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace LibraryInventory.Api.Repositories
{
    public class BookRepository
    {
        private readonly SqlConnection sqlConnection;
        public BookRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = configuration.GetConnectionString("MyDefault");
        }

        public IEnumerable<Book> GetAll()
        {
            var booksFromDB = sqlConnection.Query<Book>("SELECT * FROM Book");
            return booksFromDB;
        }

        public Book GetSingle(int bookId)
        {
            var booksFromDB = sqlConnection.Query<Book>("SELECT * FROM Book WHERE Id = @Id", new { Id = bookId }).FirstOrDefault();
            return booksFromDB;
        }


        public IEnumerable<Book> GetAuhtorsBooks(int authorId)
        {
            var booksFromDB = sqlConnection.Query<Book>("SELECT * FROM Book WHERE AuthorId = @Id", new { Id = authorId });
            return booksFromDB;
        }

        public bool Add(string name, int authorId, string publishedIn, string lang)
        {
            try
            {
                var book = new
                {
                    Name = name,
                    AuthorId = authorId,
                    PublishedIn = publishedIn,
                    Lang = lang
                };
                var insertSql = "INSERT INTO Book (Name,AuthorId,PublishedIn,Lang) VALUES (@Name,@AuthorId,@PublishedIn,@Lang)";
                sqlConnection.Execute(insertSql, book);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Update(int bookId, string name,  string publishedIn, string lang)
        {
            try
            {
                var bookSqlParameters = new
                {
                    Id = bookId,
                    Name = name,
                    PublishedIn = publishedIn,
                    Lang = lang
                };

                var updateSql = "UPDATE Book SET Name = @Name, PublishedIn = @PublishedIn, Lang= @Lang WHERE Id = @Id";
                sqlConnection.Execute(updateSql, bookSqlParameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Remove(int id)
        {
            try
            {
                var deleteSql = "DELETE FROM Book WHERE Id=@Id";
                sqlConnection.Execute(deleteSql, new { Id = id });
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public bool RemoveAuhtorsBooks(int authorId)
        {
            try
            {
                var deleteSql = "DELETE FROM Book WHERE AuthorId = @Id";
                sqlConnection.Execute(deleteSql, new { Id = authorId });
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
