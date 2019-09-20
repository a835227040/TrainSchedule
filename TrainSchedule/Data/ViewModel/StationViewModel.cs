using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using TrainSchedule.Data.Interface;

namespace TrainSchedule.Data.ViewModel
{
    public delegate bool StationViewModelIsNext();
    public class StationViewModel : BaseViewModel, IStationViewModel
    {
        /// <summary>
        /// 备选的车站信息，这个信息会显示到ComboBox的Item中
        /// </summary>
        public ObservableCollection<StationModel> optionalList { get; private set; } = new ObservableCollection<StationModel>();
        /// <summary>
        /// 表示从数据库中读取到的所有车站信息
        /// 这个信息是只读信息
        /// </summary>
        public ObservableCollection<StationModel> list
        {
            get => _list;
            private set
            {
                _list = value;
                NotifiPropertyChanged(nameof(list));
            }
        }

        public string comboBoxText
        {
            get => _comboBoxText;
            set
            {
                if(value == "TrainSchedule.Data.StationModel")
                {
                    //对于直接显示类名，直接返回，不做处理
                    comboBoxText = selectedStationStr;
                    return;
                }
                if (value == selectedStationStr)
                {
                    //选择正确的城市名，不做处理
                    return;
                }
                //表示要开始设置
                isComboBoxTextSeting = true;
                _comboBoxText = value;
                NotifiPropertyChanged(nameof(comboBoxText));
                if (value == null || value == string.Empty)
                {
                    optionalList.Clear();
                    return;
                }
                Task.Run(() =>
                {
                    //此时如需操作UI线程，需要Invoke
                    lock (comboBoxTextLock)
                    {
                        //表示进入同步状态。
                        isComboBoxTextSeting = false;
                        //改变备选
                        updataOptionalList(() =>
                        {
                            //这个值为false时，才继续更改
                            return !isComboBoxTextSeting;
                        });

                    }
                });
                Console.WriteLine(value + " |||| text");
            }
        }
        public StationModel selectedStation
        {
            get => _selectedStaion;
            set
            {
                if (value != null)
                {
                    _selectedStaion = value;
                    Console.WriteLine(value + " |||| select");
                    //comboBoxText = _selectedStaion.name;
                }
            }
        }

        public string selectedStationStr
        {
            get => _selectedStationStr;
            set
            {
                _selectedStationStr = value;
                Console.WriteLine(value + " ||| str");
            }
        }
        private string _selectedStationStr;
        private StationModel _selectedStaion;
        private void updataOptionalList(StationViewModelIsNext isNext)
        {
            var i = 0;
            while (isNext() && i < optionalList.Count)
            {
                Console.WriteLine(optionalList[i].name + " | " + selectedStationStr);
                if (optionalList[i].name != comboBoxText)
                {
                    Console.WriteLine("remove" + optionalList[i].name);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        optionalList.RemoveAt(i);
                    });
                }
                else
                {
                    ;
                }
                i++;
            }
            i = 0;
            while (isNext() && i < list.Count)
            {
                if(list[i].name == selectedStationStr)
                    continue;//已经有的选项不再继续添加~
                //回调中允许继续才会继续更新数据
                //这里是备选算法
                var t = CultureInfo.InvariantCulture.CompareInfo.IndexOf(list[i].cnName, comboBoxText, CompareOptions.IgnoreCase);
                ////1.拼音中包含
                //if (CultureInfo.InvariantCulture.CompareInfo.IndexOf(list[i].cnName, comboBoxText, CompareOptions.IgnoreCase) != -1)
                //{
                //    Application.Current.Dispatcher.Invoke(() =>
                //    {
                //        optionalList.Add(list[i]);
                //    });
                //    i++;
                //    continue;
                //}

                //2.中文车站名包含
                if (list[i].name.IndexOf(comboBoxText) != -1)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        optionalList.Add(list[i]);
                        //Console.WriteLine(list[i].name + " | " + comboBoxText);
                    });
                    i++;
                    continue;
                }
                //3.简称中包含
                if (list[i].abbreviation.IndexOf(comboBoxText) == 0)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        optionalList.Add(list[i]);
                        //Console.WriteLine(list[i].abbreviation + " | " + comboBoxText);
                    });
                    i++;
                    continue;
                }
                ////3.简称中包含
                //if (CultureInfo.InvariantCulture.CompareInfo.IndexOf(list[i].abbreviation, comboBoxText, CompareOptions.IgnoreCase) != -1)
                //{
                //    Application.Current.Dispatcher.Invoke(() =>
                //    {
                //        optionalList.Add(list[i]);
                //        Console.WriteLine(list[i].abbreviation + " | " + comboBoxText);
                //    });
                //    i++;
                //    continue;
                //}
                i++;

            }
        }

        private object comboBoxTextLock = new object();
        /// <summary>
        /// 指示comboBox是否在设置当中
        /// </summary>
        private bool isComboBoxTextSeting = false;
        private string _comboBoxText = string.Empty;
        private ObservableCollection<StationModel> _list;
        public StationViewModel()
        {
            list = new ObservableCollection<StationModel>((new StationModel()).Select<StationModel>());
        }
    }
}
