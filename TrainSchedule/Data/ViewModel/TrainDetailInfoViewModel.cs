using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainSchedule.Data.Interface;
using TrainSchedule.Data.Model;

namespace TrainSchedule.Data.ViewModel
{
    public class TrainDetailInfoViewModel : ITrainDetailInfoViewModel
    {
        public const string NOT_SEAT_DATE = "--";
        public const string NOT_SEAT_NONE = "无";
        public const string NOT_SEAT_YOU = "有";
        /// <summary>
        /// 出发站是不是始发站
        /// </summary>
        public bool is_start => false;
        /// <summary>
        /// 到达站是不是终点站
        /// </summary>
        public bool is_end => false;
        /// <summary>
        /// 车次
        /// </summary>
        public string train_no => _model.train_no.ToUpper();
        /// <summary>
        /// 出发站/到达站
        /// </summary>
        public ITrainStationModel station => _station;
        /// <summary>
        /// 出发时间/到达时间
        /// </summary>
        public ITrainRunTimeModel time => _model.run_time;
        /// <summary>
        /// 历时
        /// </summary>
        public string run_time => $"{_model.time.ToString("hh\\:mm")}";
        /// <summary>
        /// 商务座/特等座(仅限高铁)
        /// </summary>
        public string A1
        {
            get{
                if (_model.td != 0)
                    return seatCountToString(_model.td);
                else
                    return NOT_SEAT_DATE;
            }
        }
            
        /// <summary>
        /// 一等座/软座
        /// </summary>
        public string A2
        {
            get
            {
                if (_model.d1 != 0)
                    return seatCountToString(_model.d1);
                else if (_model.rz != 0)
                    return seatCountToString(_model.rz);
                else
                    return NOT_SEAT_DATE;
            }
        }
        /// <summary>
        /// 二等座/硬座
        /// </summary>
        public string A3
        {
            get
            {
                if (_model.d2 != 0)
                    return seatCountToString(_model.d2);
                else if (_model.yz != 0)
                    return seatCountToString(_model.yz);
                else
                    return NOT_SEAT_DATE;
            }
        }
        /// <summary>
        /// 高级软卧/动卧
        /// </summary>
        public string A4
        {
            get
            {
                if (_model.gjrw != 0)
                    return seatCountToString(_model.gjrw);
                else if (_model.dw != 0)
                    return seatCountToString(_model.dw);
                else
                    return NOT_SEAT_DATE;
            }
        }
        /// <summary>
        /// 硬卧
        /// </summary>
        public string A5
        {
            get
            {
                if (_model.yw != 0) return seatCountToString(_model.yw);
                else return NOT_SEAT_DATE;
            }
        }
        /// <summary>
        /// 软卧
        /// </summary>
        public string A7 => seatCountToString(_model.rw);
        /// <summary>
        /// 无座
        /// </summary>
        public string A6
        {
            get
            {
                if (_model.wz != 0) return _model.wz.ToString();
                else return NOT_SEAT_DATE;
            }
        }
        private TrainDetailInfoModel _model;
        private TrainStationModel _station;
        private string seatCountToString(int count)
        {
            if (count == int.MinValue)
                return NOT_SEAT_NONE;
            else if (count == int.MaxValue)
                return NOT_SEAT_YOU;
            else
                return count.ToString();
        }
        public TrainDetailInfoViewModel(TrainDetailInfoModel model)
        {
            _model = model;
            _station = new TrainStationModel(_model.train_start_station, _model.train_end_station);
        }
    }
}
