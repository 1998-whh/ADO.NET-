using Common;
using StudentManagerBLL;
using StudentManagerModel;
using StudentManagerModel.ObjExt;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace StudentManager.View
{
    /// <summary>
    /// KaoqinGuanli.xaml 的交互逻辑
    /// </summary>
    public partial class KaoqinGuanli : UserControl
    {
        StudentClassManager csm = new StudentClassManager();
        StudentManagerBLL.StudentAttendManager sm = new StudentManagerBLL.StudentAttendManager();
        List<StudentExt> students = null;
        //用来记录当前的选择的学员
        StudentExt selectStu = null;
        public KaoqinGuanli()
        {
            InitializeComponent();
           List<StudentClass> classes = csm.GetClasses();
            smclassCmb.ItemsSource = classes;
            smclassCmb.DisplayMemberPath = "ClassName";//设置下拉框的显示文本
            smclassCmb.SelectedValuePath = "ClassID";//设置下拉框显示文本对应的value
            smclassCmb.SelectedIndex = 0;
            //给DataGrid进行数据绑定,需要针对DG中列进行绑定对应的数据列
            students = sm.GetStudents(Convert.ToInt32(smclassCmb.SelectedValue));
            smDgkapqinLsit.ItemsSource = students;
        }
        /// <summary>
        /// 考勤查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnkaoqinchaxun_Click(object sender, RoutedEventArgs e)
        {
            if (smclassCmb.SelectedIndex < 0)
            {
                MessageBox.Show("请选择班级！", "提示");
                return;
            }
            RefreshDG();
        }

        private void RefreshDG()
        {
            students = sm.GetStudents(Convert.ToInt32(smclassCmb.SelectedValue));
            smDgkapqinLsit.ItemsSource = students;
        }



        /// <summary>
        /// 学号排列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnxuehao_Click(object sender, RoutedEventArgs e)
        {
            if (smDgkapqinLsit.ItemsSource == null)
            {
                return;
            }
            //倒序：从大到小
            if ((sender as Button).Tag.ToString() == "True")
            {
                students.Sort(new StudentIdDESC(true));
                groupBySidImg.Source = new BitmapImage(new Uri("/img/ico/up.ico", UriKind.RelativeOrAbsolute));
                (sender as Button).Tag = "False";
            }
            else if ((sender as Button).Tag.ToString() == "False")
            {
                students.Sort(new StudentIdDESC(false));
                groupBySidImg.Source = new BitmapImage(new Uri("/img/ico/down.ico", UriKind.RelativeOrAbsolute));
                (sender as Button).Tag = "True";
            }
            smDgkapqinLsit.ItemsSource = null;
            smDgkapqinLsit.ItemsSource = students;
        }
        /// <summary>
        /// 姓名排列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnName_Click(object sender, RoutedEventArgs e)
        {
            if (smDgkapqinLsit.ItemsSource == null)
            {
                return;
            }
            if ((sender as Button).Tag.ToString() == "True")
            {
                students.Sort(new StudentNameDESC(true));
                groupBySNameImg.Source = new BitmapImage(new Uri("/img/ico/jiang.ico", UriKind.RelativeOrAbsolute));
                (sender as Button).Tag = "False";
            }
            else if ((sender as Button).Tag.ToString() == "False")
            {
                students.Sort(new StudentNameDESC(false));
                groupBySNameImg.Source = new BitmapImage(new Uri("/img/ico/sheng.ico", UriKind.RelativeOrAbsolute));
                (sender as Button).Tag = "True";
            }
            smDgkapqinLsit.ItemsSource = null;
            smDgkapqinLsit.ItemsSource = students;
        }
        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// 查询考勤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectkaoqin_Click(object sender, RoutedEventArgs e)
        {
            smclassCmb.SelectedIndex = -1;
            string target = mstxtIdorName.Text.Trim();
            List<StudentExt> liststu = sm.GetStudentsByIdOrname(target);
            smDgkapqinLsit.ItemsSource = null;
            if (liststu.Count <= 0)
            {
                MessageBox.Show("根据条件没有查询到条件信息！", "提示");
                mstxtIdorName.Focus();
                mstxtIdorName.SelectAll();
                return;
            }
            students = liststu;
            smDgkapqinLsit.ItemsSource = students;
        }
        /// <summary>
        /// 刷新考勤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdatekaoqin_Click(object sender, RoutedEventArgs e)
        {
            smclassCmb.SelectedIndex = -1;
            string target = mstxtIdorName.Text.Trim();
            List<StudentExt> liststu = sm.GetStudentsByIdOrname(target);
            smDgkapqinLsit.ItemsSource = null;
            if (liststu.Count <= 0)
            {
                MessageBox.Show("根据条件没有查询到条件信息！", "提示");
                mstxtIdorName.Focus();
                mstxtIdorName.SelectAll();
                return;
            }
            students = liststu;
            smDgkapqinLsit.ItemsSource = students;
        }
        /// <summary>
        /// 考勤导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnkaoqindaochu_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog fileDialog = new Microsoft.Win32.SaveFileDialog();
            fileDialog.Filter = "Excel工作簿(*.xlsx;*.xls)|*.xlsx;*.xls";
            fileDialog.FileName = "学生信息表.xlsx";
            fileDialog.Title = "导出到Excel表";
            if (fileDialog.ShowDialog() == true)
            {
                string path = fileDialog.FileName;
                System.Data.DataTable table = sm.GetDataTable((int)smclassCmb.SelectedValue);
                if (table.Rows.Count <= 0)
                {
                    MessageBox.Show("该班级暂无学生数据！", "提示");
                    return;
                }
                if (Common.dayinExcel.ExportToExcel(table, path))
                {
                    MessageBox.Show("导出数据完成！", "提示");
                }
                else
                {
                    MessageBox.Show("导出数据失败，请稍后再试！", "提示");
                }
            }
        }
        /// <summary>
        /// 打印考勤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dayinkaoqin_Click(object sender, RoutedEventArgs e)
        {
            selectStu = smDgkapqinLsit.SelectedItem as StudentExt;
            if (selectStu == null)
            {
                MessageBox.Show("请选择您要打印的学员", "提示");
                return;
            }
            common.BitmapImg image = null;
            if (string.IsNullOrEmpty(selectStu.StuImage))
            {
                selectStu.ImgPath = "/img/bg/zw.jpg";
            }
            else
            {
                image = SerializeObjectTostring.DeserializeObject(selectStu.StuImage) as common.BitmapImg;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = new MemoryStream(image.Buffer);
                bitmap.EndInit();
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));
                long sc = DateTime.Now.Ticks;
                using (MemoryStream stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    byte[] buffer = stream.ToArray();
                    File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "/photos/" + sc + ".png", buffer);
                    stream.Close();
                }
                selectStu.ImgPath = AppDomain.CurrentDomain.BaseDirectory + "/photos/" + sc + ".png";
            }
            View.DayinxinxiManger frmPrint = new DayinxinxiManger("DayinModel.xaml", selectStu);
            frmPrint.ShowInTaskbar = false;
            frmPrint.ShowDialog();
        }


       
    }
}
