using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainSchedule.Data.Interface
{
    public interface ITrainWindowViewModel
    {
        /// <summary>
        /// 筛选器 - 高铁
        /// </summary>
        bool? is_filter_gc { get; set; }
        bool? is_filter_d { get; set; }
        bool? is_filter_z { get; set; }
        bool? is_filter_t { get; set; }
        bool? is_filter_k { get; set; }
        bool? is_filter_all { get; set; }
        DateTime? start_filter_start_time { get; set; }
        DateTime? start_filter_end_time { get; set; }
        DateTime? over_filter_start_time { get; set; }
        DateTime? over_filter_end_time { get; set; }
        ObservableCollection<ITrainDetailInfoViewModel> train_table { get; set; }
        ObservableCollection<ITrainDetailInfoViewModel> all_train_table { get; set; }
    }
}
