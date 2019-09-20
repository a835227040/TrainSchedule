using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainSchedule.Data.Interpreter
{
    /// <summary>
    /// 出发或到达时间解释器
    /// </summary>
    public class TimeInterpreter
    {
        public int day => _dateTime.Day - BASE_DATE_TIME.Day;
        private readonly DateTime BASE_DATE_TIME = new DateTime(2010, 1, 1);
        private DateTime _dateTime = new DateTime(2010, 1, 1);
        /// <summary>
        /// 从字符串构造一个解释器
        /// </summary>
        /// <param name="formatText"></param>
        /// <param name="day">第几天到达，当日到达为0，次日到达为1</param>
        public TimeInterpreter(string formatText, int day = 0)
        {
            _dateTime = _dateTime.AddDays(day);
            var temp = DateTime.Parse(formatText);
            _dateTime = _dateTime.AddHours(temp.Hour).AddMinutes(temp.Minute).AddSeconds(temp.Second);
        }
        /// <summary>
        /// 从DateTime结构构造一个解释器
        /// </summary>
        /// <param name="time"></param>
        public TimeInterpreter(DateTime time)
        {
            _dateTime = time;
        }
        public override string ToString()
        {
            return _dateTime.ToString("HH:mm");
        }
    }
}
