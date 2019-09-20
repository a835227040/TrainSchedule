using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainSchedule.Data.Interface;
using TrainSchedule.Data.SQLite;

namespace TrainSchedule.Data
{
    [SQLiteSerialization("data", "station_data")]
    public class StationModel : BaseSQLiteData , IStationModel
    {
        /// <summary>
        /// 车站缩写
        /// </summary>
        [SQLiteRow("abbreviation", SQLiteDataType.Text, isSelectWhere = true, isInsert = true)]
        public string abbreviation { get; set; }
        /// <summary>
        /// 车站中文名
        /// </summary>
        [SQLiteRow("name", SQLiteDataType.Text, isSelectWhere = true, isInsert = true)]
        public string name { get; set; }
        /// <summary>
        /// 车站电码
        /// </summary>
        [SQLiteRow("code", SQLiteDataType.Text, isSelectWhere = true, isInsert = true)]
        public string code { get; set; }
        /// <summary>
        /// 车站数字码
        /// </summary>
        [SQLiteRow("code_number", SQLiteDataType.Integer, isSelectWhere = false, isInsert = true)]
        public int codeNumber { get; set; }
        /// <summary>
        /// 车站拼音
        /// </summary>
        [SQLiteRow("cn_name", SQLiteDataType.Text, isSelectWhere = true, isInsert = true)]
        public string cnName { get; set; }
        /// <summary>
        /// 创建一个简单的类
        /// </summary>
        public StationModel() : base(false)
        {

        }
        /// <summary>
        /// 创建一个查询过后的类
        /// </summary>
        /// <param name="insertResult"></param>
        public StationModel(bool insertResult = false) : base(insertResult)
        {

        }
        public StationModel(string[] stationData) : base(false)
        {
            SetStationDataFormString(stationData);
        }
        public void SetStationDataFormString(string[] stationData)
        {
            if (stationData == null || stationData.Length != 6)
            {
                throw new Exception("输入参数错误");
            }
            this.abbreviation = stationData[0];
            this.code = stationData[2];
            this.name = stationData[1];
            this.cnName = stationData[3];
            this.codeNumber = Convert.ToInt32(stationData[5]);
        }
        public override string ToString()
        {
            return this.name;
        }
    }
}
