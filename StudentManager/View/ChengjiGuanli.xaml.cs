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
    /// ChengjiGuanli.xaml 的交互逻辑
    /// </summary>
    public partial class ChengjiGuanli : UserControl
    {
        StudentClassManager csm = new StudentClassManager();
        StudentManagerBLL.StudentManager sm1 = new StudentManagerBLL.StudentManager();
        StudentManagerBLL.ScoreListManager sm = new StudentManagerBLL.ScoreListManager();
        List<ScoreExt> students = null;
        //用来记录当前的选择的学员
        //ScoreExt selectStu = null;
        public ChengjiGuanli()
        {
            InitializeComponent();
            List<StudentClass> classes = csm.GetClasses();
            smclassCmb.ItemsSource = classes;
            smclassCmb.DisplayMemberPath = "ClassName";//设置下拉框的显示文本
            smclassCmb.SelectedValuePath = "ClassID";//设置下拉框显示文本对应的value
            smclassCmb.SelectedIndex = 0;
            //给DataGrid进行数据绑定,需要针对DG中列进行绑定对应的数据列
            students = sm.GetScoreList(Convert.ToInt32(smclassCmb.SelectedValue));
            smDgStudentLsit.ItemsSource = students;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// 输入学号、姓名查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectBySIN_Click(object sender, RoutedEventArgs e)
        {

            smclassCmb.SelectedIndex = -1;
            string target = mstxtIdorName.Text.Trim();
            List<ScoreExt> liststu = sm.GetStudentByldOrName(target);
            smDgStudentLsit.ItemsSource = null;
            if (liststu.Count <= 0)
            {
                MessageBox.Show("根据条件没有查询到条件信息！", "提示");
                mstxtIdorName.Focus();
                mstxtIdorName.SelectAll();
                return;
            }
            students = liststu;
            smDgStudentLsit.ItemsSource = students;
        }
        /// <summary>
        /// 根据班级查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectByCId_Click(object sender, RoutedEventArgs e)
        {
            if (smclassCmb.SelectedIndex < 0)
            {
                MessageBox.Show("请选择班级！", "提示");
                return;
            }
            RefreshDG();
        }

        private void btnGroupBySid_Click(object sender, RoutedEventArgs e)
        {
            if (smDgStudentLsit.ItemsSource == null)
            {
                return;
            }
            //倒序：从大到小
            if ((sender as Button).Tag.ToString() == "True")
            {
                students.Sort(new ScoreDESC(true));
                groupBySidImg.Source = new BitmapImage(new Uri("/img/ico/up.ico", UriKind.RelativeOrAbsolute));
                (sender as Button).Tag = "False";
            }
            else if ((sender as Button).Tag.ToString() == "False")
            {
                students.Sort(new ScoreDESC(false));
                groupBySidImg.Source = new BitmapImage(new Uri("/img/ico/down.ico", UriKind.RelativeOrAbsolute));
                (sender as Button).Tag = "True";
            }
            smDgStudentLsit.ItemsSource = null;
            smDgStudentLsit.ItemsSource = students;

        }

        private void btnGroupBySName_Click(object sender, RoutedEventArgs e)
        {
            if (smDgStudentLsit.ItemsSource == null)
            {
                return;
            }
            if ((sender as Button).Tag.ToString() == "True")
            {
                students.Sort(new ScoreDESC(true));
                groupBySNameImg.Source = new BitmapImage(new Uri("/img/ico/jiang.ico", UriKind.RelativeOrAbsolute));
                (sender as Button).Tag = "False";
            }
            else if ((sender as Button).Tag.ToString() == "False")
            {
                students.Sort(new ScoreDESC(false));
                groupBySNameImg.Source = new BitmapImage(new Uri("/img/ico/sheng.ico", UriKind.RelativeOrAbsolute));
                (sender as Button).Tag = "True";
            }
            smDgStudentLsit.ItemsSource = null;
            smDgStudentLsit.ItemsSource = students;
        }

        //修改信息
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            ScoreExt selectStu = smDgStudentLsit.SelectedItem as ScoreExt;

            if (selectStu == null)
            {
                MessageBox.Show("请选择要修改的学员！", "提示");
                return;
            }

            StudentExt objStu = sm1.GetStudentById(selectStu.StudentlD);
            XiugaiStudentManage updateStuInfor = new XiugaiStudentManage(objStu);
            updateStuInfor.ShowDialog();
            //刷新DG中这个学员的信息
            RefreshDG();
        }



        private void RefreshDG()
        {
            students = sm.GetScoreList(Convert.ToInt32(smclassCmb.SelectedValue));
            smDgStudentLsit.ItemsSource = students;
        }


        /// <summary>
        /// 删除学员信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            ScoreExt selectStu = smDgStudentLsit.SelectedItem as ScoreExt;

            if (selectStu == null)
            {
                MessageBox.Show("请选择要删除的学员！", "提示");
                return;
            }
            ScoreExt student = sm.GetStudentById(selectStu.StudentlD);
            if (student != null)
            {


                MessageBoxResult mbr = MessageBox.Show("您确定要删除【" + student.StudentName + "】", "警告", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (mbr == MessageBoxResult.OK)
                {
                    if (sm.DeleteStudentById(student.StudentlD))
                    {
                        MessageBox.Show("删除成功！", "提示");
                    }
                    else
                    {
                        MessageBox.Show("删除失败请稍后再试！", "提示");
                    }
                }
                else
                {
                    MessageBox.Show("删除失败请稍后再试！", "提示");
                }
            }

        }
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcel_Click(object sender, RoutedEventArgs e)
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
                if (Common.chengji.ExportToExcel(table, path))
                {
                    MessageBox.Show("导出数据完成！", "提示");
                }
                else
                {
                    MessageBox.Show("导出数据失败，请稍后再试！", "提示");
                }
            }
        }

       
    }
    class ScoreDESC : IComparer<ScoreExt>
    {
        ///-1，0，1
        public ScoreDESC(bool b)
        {
            Bo = b;
        }
        public bool Bo { get; set; }
    
        public int Compare(ScoreExt x, ScoreExt y)
        {
            if (Bo)
            {
                return x.StudentlD.CompareTo(y.StudentlD);
            }
            else
            {
                return y.StudentlD.CompareTo(x.StudentlD);
            }
        }
    }


}

