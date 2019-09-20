using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainSchedule.Data.Enum
{
    public static partial class EnumUtil
    {
        /// <summary>
        /// 工厂方法，将指定的枚举值转化成字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">要输入的数据</param>
        /// <returns></returns>
        public static string ToString<T>(T data){
            if (!typeof(T).IsEnum) throw new Exception("只能是枚举类型");

            var type = typeof(T);
            if (type == typeof(SeatType))
            {
                return ToString(data);
            }
            return null;
        }
        /// <summary>
        /// 工厂方法，将指定的枚举值转化成字符串
        /// </summary>
        /// <param name="type"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToString(Type type,object data)
        {
            if (!type.IsEnum) throw new Exception("只能是枚举类型");

            if (type == typeof(SeatType))
            {
                return ToString(data);
            }
            return null;
        }
    }
}
