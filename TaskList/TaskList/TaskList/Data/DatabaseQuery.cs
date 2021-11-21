using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskList.Models;

namespace TaskList.Data
{
    public class DatabaseQuery
    {
        readonly SQLiteAsyncConnection _database;

        public DatabaseQuery(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);

            _database.CreateTableAsync<ToDo>().Wait();
        }

        #region CRUD
        public Task<List<ToDo>> GetToDoAsync()
        {
            return _database.Table<ToDo>().ToListAsync();
        }

        public Task<int> SaveUpdateToDoAsync(ToDo toDo)
        {
            if (toDo.Id != 0)
            {
                return _database.UpdateAsync(toDo);
            }
            else
            {
                return _database.InsertAsync(toDo);
            }
        }

        public Task<int> DeleteToDoAsync(ToDo toDo)
        {
            return _database.DeleteAsync(toDo);
        }
        #endregion
    }
}
