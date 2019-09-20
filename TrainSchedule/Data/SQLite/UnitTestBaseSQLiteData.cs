using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainSchedule.Data.SQLite
{
#if DEBUG
    [SQLiteSerialization("data","station_data")]
    public class UnitTestBaseSQLiteData :BaseSQLiteData
    {
        [SQLiteRow("abbreviation",SQLiteDataType.Text,isSelectWhere =true,isInsert =true)]
        public string abbreviation { get; set; }
        [SQLiteRow("name", SQLiteDataType.Text, isSelectWhere = true,isInsert =true)]
        public string name { get; set; }

        public UnitTestBaseSQLiteData(bool insertResult = false) : base(insertResult)
        {

        }
    }
#endif 
}
