﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
using Laba5.Models;
using Laba5.ViewModels;
using Timer = System.Timers.Timer;

namespace Laba5.View
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        private int sortIndex;

        public UserControl1()
        {
            InitializeComponent();
            DataContext = new ProcViewModel();
            Grid.Sorting += Grid_Sorting;
            Timer timer = new Timer(5000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Grid_Sorting(object sender, DataGridSortingEventArgs e)
        {
             sortIndex = e.Column.DisplayIndex;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(new Action(() => { DataContext = new ProcViewModel(sortIndex); }));
            Debug.WriteLine("Timer!");
        }

        private void BtnKill_OnClick(object sender, RoutedEventArgs e)
        {
            var sel = (Proc)Grid.SelectedItem;
            var proc = Process.GetProcessById(sel.ProcessId);
            proc.Kill();
        }

        private void Proc_Click(object sender, RoutedEventArgs e)
        {
            var sel = (Proc)Grid.SelectedItem;
            var proc = Process.GetProcessById(sel.ProcessId);
            ProcessThreadCollection sel1 = proc.Threads;
            string text = "";
            int i = 0;
            foreach (ProcessThread thread in sel1)
                {
                i++;
                text += i + ". " + thread.Id.ToString() + " " + thread.StartTime.ToString() + " " + thread.PriorityLevel.ToString() + "\r\n" + "\r\n";
                    
                }
            Proc_Block.Text = text;



        }

        private void Modules_Click(object sender, RoutedEventArgs e)
        {
            var sel = (Proc)Grid.SelectedItem;
            var proc = Process.GetProcessById(sel.ProcessId);
            ProcessModuleCollection sel2 = proc.Modules;

            string text = "";
            int i = 0;
            foreach (ProcessModule module in sel2)
            {
                i++;
                text += i + ". " + module.ModuleName.ToString() + " " + module.FileName.ToString() + "\r\n" + "\r\n";

            }
            Mod_Block.Text = text;
        }
    }
}
