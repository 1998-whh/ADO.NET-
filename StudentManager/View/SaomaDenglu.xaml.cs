﻿using System;
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
using System.Windows.Shapes;

namespace StudentManager.View
{
    /// <summary>
    /// SaomaDenglu.xaml 的交互逻辑
    /// </summary>
    public partial class SaomaDenglu : Window
    {
        public SaomaDenglu()
        {
            InitializeComponent();
        }

        private void fanhui_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
