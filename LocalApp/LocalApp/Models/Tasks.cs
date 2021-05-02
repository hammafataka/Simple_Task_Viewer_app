using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace LocalApp.Models
{
    [Table("Tasks")]
    public class Tasks
    {
        [PrimaryKey,AutoIncrement]
        public int id { get; set; }
        public string name { get; set; }
        public bool status { get; set; }
        public string image { get; set; }
    }
}
