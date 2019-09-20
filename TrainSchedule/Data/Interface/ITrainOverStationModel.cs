using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainSchedule.Data.Interpreter;

namespace TrainSchedule.Data.Interface
{
    /// <summary>
    /// 列车到达站信息
    /// </summary>
    public interface ITrainOverStationModel
    {
        /// <summary>
        /// 大于1的序号
        /// </summary>
        int train_no { get; set; }
        /// <summary>
        /// 车站
        /// </summary>
        StationModel station { get; set; }
        /// <summary>
        /// 结构体去掉年月日即为列车到达时间，
        /// 次日/当日到达以结构体的年月日做为判断依据，1970年1月1日为当日到达、1月2日为次日到达以此类推
        /// </summary>
        DateTime over_time { get; set; }
        /// <summary>
        /// 结构体去掉年月日即为列车触发时间，
        /// 次日/当日触发以结构体的年月日做为判断依据，1970年1月1日为当日到达、1月2日为次日到达以此类推
        /// </summary>
        DateTime start_time { get; }
        /// <summary>
        /// 该值为0则为始发车站/终点站
        /// </summary>
        int stop_time { get; set; }
    }
}
