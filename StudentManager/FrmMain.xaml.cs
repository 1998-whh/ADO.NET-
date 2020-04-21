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
using System.Windows.Threading;
using System.Configuration;
using StudentManagerModel.ObjExt;
using StudentManager.View;
using System.IO;

namespace StudentManager
{
    /// <summary>
    /// FrmMain.xaml 的交互逻辑
    /// </summary>
    public partial class FrmMain : Window
    {
        public FrmMain()
        {
            InitializeComponent();
            #region//登录窗体验证
            frmLogin login = new frmLogin();
            login.ShowDialog();
            if (login.DialogResult != true)
            {
                Environment.Exit(0);
            }
            DispatcherTimer timer = null;
            #endregion
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += Timer_Tick;
            timer.Start();
            try
            {
                statusAdminLb.Content = "操作管理员【" + App.CurrentAdmin.AdminName + "】";
                statusVersionLb.Content = "版本号：" + ConfigurationManager.AppSettings["version"].ToString();
            }
            catch (Exception)
            {

                throw;
            }

            //清除photos中的照片
            //List<string> files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "../Debug/photos/").ToList();
            //if (files.Count > 0)
            //{
            //    foreach (string item in files)
            //    {
            //        File.Delete(item);
            //    }
            //}
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            string week = "星期天";
            switch (now.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    week = "星期天";
                    break;
                case DayOfWeek.Monday:
                    week = "星期一";
                    break;
                case DayOfWeek.Tuesday:
                    week = "星期二";
                    break;
                case DayOfWeek.Wednesday:
                    week = "星期三";
                    break;
                case DayOfWeek.Thursday:
                    week = "星期四";
                    break;
                case DayOfWeek.Friday:
                    week = "星期五";
                    break;
                case DayOfWeek.Saturday:
                    week = "星期六";
                    break;
                default:
                    break;
            }
            statusTimeLb.Content = string.Format("{0}年{1}月{2}日    {3}:{4}:{5}    {6}", now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, week);
        }
        //窗口关闭
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //窗口最小化
        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
      
        //usercontrolName.Visibility = Visibility.Collapsed; // 隐藏不占用空间
        //usercontrolName.Visibility = Visibility.Hidden; // 隐藏
        //usercontrolName.Visibility = Visibility.Visible; // 显示
       
      
        //在线帮助
        private void inlinehMenu_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.yltedu.com/");
        }
        //官网
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.yltedu.com/");
        }
        //退出系统
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //修改密码
        private void updatePwd_Click_1(object sender, RoutedEventArgs e)
        {
            GirdContent.Children.Clear();
            View.FrmChangePassword Password = new View.FrmChangePassword();
            GirdContent.Children.Add(Password);
        }

        private void exitMenu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //添加学员
        private void addsMenu_Click(object sender, RoutedEventArgs e)
        {
            GirdContent.Children.Clear();
            StudentExt stu = new StudentExt();
            View.AddstudentManager add = new View.AddstudentManager(stu);
            GirdContent.Children.Add(add);

        }
        //信息管理
        private void smMenu_Click(object sender, RoutedEventArgs e)
        {
            GirdContent.Children.Clear();
            View.FrmStudentManager frmStudent = new View.FrmStudentManager();
            GirdContent.Children.Add(frmStudent);
          
        }
        //成绩录入
        private void writesMenu_Click(object sender, RoutedEventArgs e)
        {
            GirdContent.Children.Clear();
            View.AddScoreManager Score = new View.AddScoreManager();
            GirdContent.Children.Add(Score);
        }
        //成绩分析
        private void checksMenu_Click(object sender, RoutedEventArgs e)
        {
            GirdContent.Children.Clear();
            View.ChengjiGuanli Chengji = new View.ChengjiGuanli();
            GirdContent.Children.Add(Chengji);
        }
        //成绩查询
        private void selectsMenu_Click(object sender, RoutedEventArgs e)
        {
            GirdContent.Children.Clear();
            View.ChengjiGuanli Chengji = new View.ChengjiGuanli();
            GirdContent.Children.Add(Chengji);
        }
        //考勤查询
        private void queryaMenu_Click(object sender, RoutedEventArgs e)
        {
            GirdContent.Children.Clear();
            View.KaoqinGuanli kaoqin = new View.KaoqinGuanli();
            GirdContent.Children.Add(kaoqin);
        }
        //考勤打卡
        private void adakaMenu_Click(object sender, RoutedEventArgs e)
        {
            GirdContent.Children.Clear();
            View.KaoqinGuanli kaoqin = new View.KaoqinGuanli();
            GirdContent.Children.Add(kaoqin);
        }
        /// <summary>
        /// 快捷键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Escape)
                {
                    this.WindowState = WindowState.Minimized;
                }
                else if (e.Key == Key.S)
                {
                    menuxitong.IsSubmenuOpen = true;
                }
                else if (e.Key == Key.J)
                {
                    scoreStuMan.IsSubmenuOpen = true;
                }
                else if (e.Key == Key.A)
                {
                    queryaMenuMan.IsSubmenuOpen = true;
                }
                else if (e.Key == Key.M)
                {
                    HelpMenuMan.IsSubmenuOpen = true;
                }
                else if (e.Key == Key.Z)
                {
                    smMenu_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btndaoru_Click(object sender, RoutedEventArgs e)
        {
            GirdContent.Children.Clear();
            View.DaorustudentManager Data = new View.DaorustudentManager();
            GirdContent.Children.Add(Data);
        }
        /// <summary>
        /// 账号切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void zhanghaoqiehuan_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            frmLogin login = new frmLogin();
            login.ShowDialog();
            if (login.DialogResult != true)
            {
                Environment.Exit(0);
               
            }
            
            this.Visibility = Visibility.Visible;
        }
    }
}
