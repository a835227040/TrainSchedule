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
using TrainSchedule.Data.Interpreter;
using TrainSchedule.Data.ViewModel;

namespace TrainSchedule.TrainControls
{
    /// <summary>
    /// StationSearchComboBox.xaml 的交互逻辑
    /// </summary>
    public partial class StationSearchComboBox : UserControl
    {
        private StationViewModel _viewModel;
        public StationSearchComboBox()
        {
            InitializeComponent();
        }

        private async void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this)) return;
            comboBox.Text = "正在加载城市数据";
            comboBox.IsEnabled = false;
            StationDataInterpreter sdi = new StationDataInterpreter();
            var isSync = await sdi.IsCreateAsync();
            if (isSync)
            {
                await sdi.ExecuteSyncAsync();
                await sdi.CreateDataAsync();
            }
            await Task.Run(() =>
            {
                _viewModel = new StationViewModel();
            });
            comboBox.Text = "";
            this.DataContext = _viewModel;
            comboBox.IsEnabled = true;
        }
        private void ComboBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (comboBox.Text != null || comboBox.Text != string.Empty)
            {
                comboBox.IsDropDownOpen = true;
            }
            else
                comboBox.IsDropDownOpen = false;
        }
    }
}
