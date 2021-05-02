using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LocalApp.Models;
using SQLite;

namespace LocalApp.Services
{
    public class serviceSQLite
    {
        private readonly SQLiteAsyncConnection dbconnection;
        private async Task CreateTables()
        {
            await dbconnection.CreateTableAsync<Tasks>();
        }
        private async Task initializeData()
        {

        }
        public async Task<List<T>>GetItems<T>()where T :new()
        {
            return await dbconnection.Table<T>().ToListAsync();
        }
        public async Task<bool> AddItem<T>(T item) where T:new()
        {
            int rows = await dbconnection.InsertAsync(item);
            return (rows == 1);
        }
        public async Task<bool>UdateItems<T>(T item )where T : new()
        {
            int rows =await dbconnection.UpdateAsync(item);
            return (rows == 1);
        }
        public async Task<bool>DeleteItem<T>(T item )where T :new()
        {
            int rows = await dbconnection.DeleteAsync(item);
            return (rows == 1);
        }
        public serviceSQLite(string path)
        {
            dbconnection = new SQLiteAsyncConnection(path);
            Task.Run(() => CreateTables()).Wait();
            Task.Run(() => initializeData()).Wait();
        }
    }
}
