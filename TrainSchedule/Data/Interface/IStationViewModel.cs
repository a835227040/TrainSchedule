using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainSchedule.Data.Interface
{
    public interface IStationViewModel
    {
        string selectedStationStr { get; set; }
        StationModel selectedStation { get; set; }
        string comboBoxText { get; set; }
        /// <summary>
        /// 备选的车站信息，这个信息会显示到ComboBox的Item中
        /// </summary>
        ObservableCollection<StationModel> optionalList { get;  }

        /// <summary>
        /// 表示从数据库中读取到的所有车站信息
        /// 这个信息是只读信息
        /// </summary>
        ObservableCollection<StationModel> list { get; }
    }
}
