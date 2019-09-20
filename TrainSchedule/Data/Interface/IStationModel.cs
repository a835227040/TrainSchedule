using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainSchedule.Data.SQLite;

namespace TrainSchedule.Data.Interface
{
    public interface IStationModel
    {
        /// <summary>
        /// 车站缩写
        /// </summary>
        string abbreviation { get; set; }
        /// <summary>
        /// 车站中文名
        /// </summary>
        string name { get; set; }
        /// <summary>
        /// 车站电码
        /// </summary>
        string code { get; set; }
        /// <summary>
        /// 车站数字码
        /// </summary>
        int codeNumber { get; set; }
        /// <summary>
        /// 车站拼音
        /// </summary>
        string cnName { get; set; }
    }
}
