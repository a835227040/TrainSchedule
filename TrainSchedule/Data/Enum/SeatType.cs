using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainSchedule.Data.Enum
{
    public static partial class EnumUtil
    {
        public const string SEAT_TYPE_ORDER = "其他";
        public const string SEAT_TYPE_WZ = "无座";
        public const string SEAT_TYPE_YZ = "硬座";
        public const string SEAT_TYPE_RZ = "软座";
        public const string SEAT_TYPE_YW = "硬卧";
        public const string SEAT_TYPE_RW = "软卧";
        public const string SEAT_TYPE_GJRW = "高级软卧";
        public const string SEAT_TYPE_2D = "二等座";
        public const string SEAT_TYPE_1D = "一等座";
        public const string SEAT_TYPE_TD = "特等座";
        public const string SEAT_TYPE_SW = "商务座";
        /// <summary>
        /// 将铺位/座位号枚举转化成字符串
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ToString(SeatType type)
        {
            switch (type)
            {
                case SeatType.A1: return SEAT_TYPE_YZ;
                case SeatType.A2: return SEAT_TYPE_RZ;
                case SeatType.A3: return SEAT_TYPE_YW;
                case SeatType.A4: return SEAT_TYPE_RW;
                case SeatType.A6: return SEAT_TYPE_GJRW;
                case SeatType.A9: return SEAT_TYPE_SW;
                case SeatType.M: return SEAT_TYPE_1D;
                case SeatType.MIN: return SEAT_TYPE_ORDER;
                case SeatType.O: return SEAT_TYPE_2D;
                case SeatType.P: return SEAT_TYPE_TD;
                case SeatType.WZ: return SEAT_TYPE_WZ;
                default: return string.Empty;
            }
        }
        /// <summary>
        /// 将座位号字符串(中文)转化成枚举
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static SeatType FromSeatType(string text)
        {
            switch (text)
            {
                case SEAT_TYPE_YZ: return SeatType.A1;
                case SEAT_TYPE_RZ: return SeatType.A2;
                case SEAT_TYPE_YW: return SeatType.A3;
                case SEAT_TYPE_RW: return SeatType.A4;
                case SEAT_TYPE_GJRW: return SeatType.A6;
                case SEAT_TYPE_SW: return SeatType.A9;
                case SEAT_TYPE_1D: return SeatType.M;
                case SEAT_TYPE_ORDER: return SeatType.MIN;
                case SEAT_TYPE_2D: return SeatType.O;
                case SEAT_TYPE_TD: return SeatType.P;
                case SEAT_TYPE_WZ: return SeatType.WZ;
                default: return 0;
            }
        }
    }
    /// <summary>
    /// 铺位/座位类型
    /// </summary>
    public enum SeatType
    {
        /// <summary>
        /// 其它座
        /// </summary>
        MIN,
        /// <summary>
        /// 无座
        /// </summary>
        WZ,
        /// <summary>
        /// 硬座
        /// </summary>
        A1,
        /// <summary>
        /// 软座
        /// </summary>
        A2,
        /// <summary>
        /// 硬卧
        /// </summary>
        A3,
        /// <summary>
        /// 软卧
        /// </summary>
        A4,
        /// <summary>
        /// 高级软卧
        /// </summary>
        A6,
        /// <summary>
        /// 商务座
        /// </summary>
        A9,
        /// <summary>
        /// 二等座
        /// </summary>
        O,
        /// <summary>
        /// 一等座
        /// </summary>
        M,
        /// <summary>
        /// 特等座
        /// </summary>
        P,
    }
}
