using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskList.Models
{
    public class ToDo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
    }
}
