using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TrainSchedule.Data.Interface;

namespace TrainSchedule.TrainControls
{
    /// <summary>
    /// TrainRunTime.xaml 的交互逻辑
    /// </summary>
    public partial class TrainRunTime : UserControl
    {
        private readonly Brush currert_over_brush = new SolidColorBrush(Color.FromRgb(0x39, 0x78, 0xff));
        private readonly Brush order_over_brush = new SolidColorBrush(Color.FromRgb(0xc9, 0x39, 0xff));
        private ITrainRunTimeModel _run_time;
        public TrainRunTime()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _run_time = DataContext as ITrainRunTimeModel;
            if (_run_time == null) throw new ObjectDisposedException("run_time参数不能为空");

            //处理出发时间
            if(_run_time.start_time.day == 0)
            {
                start.Background = currert_over_brush;
                tb_start.Text = "当日";
                start.ToolTip = "当日出发";
            }
            else
            {
                start.Background = order_over_brush;
                tb_start.Text = $"第{_run_time.start_time.day}天";
                start.ToolTip = $"第{_run_time.start_time.day}日出发";
            }
            tb_start_time.Text = $"{_run_time.start_time}";
            //处理到达时间
            if (_run_time.end_time.day == 0)
            {
                over.Background = currert_over_brush;
                tb_over.Text = "当日";
                over.ToolTip = "当日到达";
            }
            else
            {
                over.Background = order_over_brush;
                tb_over.Text = $"第{_run_time.end_time.day}天";
                over.ToolTip = $"第{_run_time.end_time.day}日到达";
            }
            tb_stop_time.Text = _run_time.end_time.ToString();
        }
    }
}
