using Dapper;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;

namespace TestProject.Repositories
{
    public class DataRepository : IDataRepository
    {
        public async Task CreateAsync(int item1, string item2)
        {
            string sql = "INSERT INTO Data (Item1, Item2) VALUES (@Item1, @Item2);";
            object[] parameters = { new { Item1 = item1, Item2 = item2 } };

            using (var connection = new SQLiteConnection(Constants.ConnectionString))
            {
                await connection.ExecuteAsync(sql, parameters);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SQLiteConnection(Constants.ConnectionString))
            {
                var sql = $"DELETE FROM Data WHERE Id = {id}";
                await connection.ExecuteAsync(sql);
            }
        }

        public async Task<IList<Data>> GetAsync(int start, int end)
        {
            using (var connection = new SQLiteConnection(Constants.ConnectionString))
            {
                var sql = $"SELECT * FROM Data WHERE Id >= {start} AND Id <= {end}";
                var data = await connection.QueryAsync<Data>(sql);
                return data.ToList();
            }
        }
    }
}
