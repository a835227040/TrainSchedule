using System;

namespace TrainSchedule.Data.SQLite
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class SQLiteSerializationAttribute : Attribute
    {
        public string dbName { get; private set; }
        public string tabelName { get; private set; }
        public SQLiteSerializationAttribute(string dbName,string tabelName)
        {
            this.dbName = dbName;
            this.tabelName = tabelName;
        }
    }
}
