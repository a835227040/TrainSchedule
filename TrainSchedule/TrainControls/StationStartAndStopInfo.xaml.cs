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
using TrainSchedule.Data.Model;

namespace TrainSchedule.TrainControls
{
    /// <summary>
    /// StationStartAndStopInfo.xaml 的交互逻辑
    /// </summary>
    public partial class StationStartAndStopInfo : UserControl
    {
        public StationStartAndStopInfo()
        {
            InitializeComponent();
            var c = Content;
            TrainStationModel model = DataContext as TrainStationModel;
            //tb_over_station.Text = model.over_station.name;
            //tb_start_station.Text = model.start_station.name;
        }
    }
}
