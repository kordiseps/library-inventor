using Dapper;
using LibraryInventory.Api.Models.Entity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryInventory.Api.Repositories
{
    public class AuthorRepository
    {
        private readonly SqlConnection sqlConnection;
        public AuthorRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = configuration.GetConnectionString("MyDefault");
        }

        public IEnumerable<Author> GetAll()
        {
            var author = sqlConnection.Query<Author>("SELECT * FROM Author");
            return author;
        }

        public Author GetSingle(int authorId)
        {
            var author = sqlConnection.
                Query<Author>("SELECT * FROM Author WHERE Id = @Id", new { Id = authorId }).
                FirstOrDefault();
            return author;
        }

        public bool Add(string name, string country)
        {
            try
            {
                var author = new
                {
                    Name = name,
                    Country = country
                };
                var insertSql = "INSERT INTO Author (Name,Country) VALUES (@Name,@Country)";
                sqlConnection.Execute(insertSql, author);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Update(int authorId, string name, string country)
        {
            try
            {
                var author = new
                {
                    Name = name,
                    Country = country,
                    Id = authorId
                };
                var updateSql = "UPDATE Author SET Name = @Name, Country= @Country WHERE Id = @Id";
                sqlConnection.Execute(updateSql, author);
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
                var deleteSql = "DELETE FROM Author WHERE Id=@Id";
                sqlConnection.Execute(deleteSql, new { Id = id });
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
