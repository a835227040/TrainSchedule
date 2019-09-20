using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainSchedule.Data.Interface;
using TrainSchedule.Data.Interpreter;
using TrainSchedule.Data.SQLite;

namespace TrainSchedule.Data.Model
{
    [SQLiteSerialization("data", "train_data")]
    public class TrainModel : BaseSQLiteData, ITrainDataTabel
    {
        [SQLiteRow("start_station", SQLiteDataType.Text, isInsert = true,isSelectWhere = true,isSelectIgnoreNULL = true)]
        public string start_station
        {
            get => _start_station;
            set => _start_station = value;
        }
        [SQLiteRow("over_station", SQLiteDataType.Text, isInsert = true, isSelectWhere = true, isSelectIgnoreNULL = true)]
        public string over_station
        {
            get => _over_station;
            set => _over_station = value;
        }
        [SQLiteRow("train_type", SQLiteDataType.Text, isInsert = true)]
        /// <summary>
        /// 这个也是经Model改写过的数据
        /// </summary>
        public TrainType train_type
        {
            get => _train_type;
        }
        /// <summary>
        /// 这个是经Model改写过的数据，使用原始数据调用构造函数以创建它。[关键字段]
        /// </summary>
        [SQLiteRow("train_no", SQLiteDataType.Text, isInsert = true, isSerializationFromDB = false, isSelectIgnoreNULL = true)]
        public TrainNumberInterpreter train_no
        {
            get => _train_no;
        }
        /// <summary>
        /// 这个是输入的原始数据
        /// </summary>
        [SQLiteRow("station_train_code", SQLiteDataType.Text, isInsert = true, isSerializationFromDB = false)]
        public string station_train_code
        {
            get => _station_train_code;
        }
        [SQLiteRow("train_type", SQLiteDataType.Text, isInsert = false)]
        public string train_type_str
        {
            get => train_type.ToString();
            set
            {
                _train_type = (TrainType)System.Enum.Parse(typeof(TrainType), value);
            }
        }
        [SQLiteRow("train_no", SQLiteDataType.Text, isInsert = false, isSelectWhere = true)]
        public string train_no_str
        {
            get => _train_no.ToString();
            set
            {
                _train_no = new TrainNumberInterpreter(value);
            }
        }
        private string _start_station;
        private string _over_station;
        private TrainNumberInterpreter _train_no;
        private string _station_train_code;
        private TrainType _train_type;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">指经过12306爬取下来的数据分析而成的实体类</param>
        public TrainModel(TrainData data) : base(false)
        {
            //使用输入的json实体类和构造函数构造一个车次解析器
            _train_no = new TrainNumberInterpreter(data.train_no);
            _station_train_code = data.code;
            _train_type = _train_no.type;
            _start_station = data.start_station;
            _over_station = data.over_station;
        }
        /// <summary>
        /// 给基类的构造函数
        /// </summary>
        /// <param name="insertResult"></param>
        public TrainModel(bool insertResult=false) : base(insertResult)
        {

        }
    }
}
