using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainSchedule.Data.Model;

namespace TrainSchedule.Data.Interface
{
    public interface ITrainDetailInfoViewModel
    {
        /// <summary>
        /// 出发站是不是始发站
        /// </summary>
         bool is_start {get;}
        /// <summary>
        /// 到达站是不是终点站
        /// </summary>
         bool is_end {get;}
        /// <summary>
        /// 车次
        /// </summary>
         string train_no {get;}
        /// <summary>
        /// 出发站/到达站 station
        /// </summary>
        ITrainStationModel station {get;}
        /// <summary>
        /// 出发时间/到达时间
        /// </summary>
        ITrainRunTimeModel time {get;}
        /// <summary>
        /// 历时
        /// </summary>
        string run_time {get;}
        /// <summary>
        /// 商务座/特等座(仅限高铁)
        /// </summary>
         string A1 {get;}

        /// <summary>
        /// 一等座/软座
        /// </summary>
         string A2 {get;}
        /// <summary>
        /// 二等座/硬座
        /// </summary>
         string A3 {get;}
        /// <summary>
        /// 高级软卧/动卧
        /// </summary>
         string A4 {get;}
        /// 硬卧
        /// </summary>
         string A5 {get;}
        /// <summary>
        /// 无座
        /// </summary>
         string A6 {get;}
    }
}
