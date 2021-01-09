using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatFood
{
    public class ClockFood
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public DateTime DayAndMonth { get; set; }
        public string Food { get; set; }
    }
}
