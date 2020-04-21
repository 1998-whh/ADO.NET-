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
using System.Windows.Shapes;

namespace StudentManager.View
{
    /// <summary>
    /// ZhucezhaoghaoManager.xaml 的交互逻辑
    /// </summary>
    public partial class ZhucezhaoghaoManager : Window
    {
        public ZhucezhaoghaoManager()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btncancel_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
          
        }
        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnconfirm_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
