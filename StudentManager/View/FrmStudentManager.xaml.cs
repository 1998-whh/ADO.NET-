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
using StudentManagerModel;
using StudentManagerModel.ObjExt;
using StudentManagerBLL;
using Common;
using System.IO;

namespace StudentManager.View
{
    /// <summary>
    /// FrmStudentManager.xaml 的交互逻辑
    /// </summary>
    public partial class FrmStudentManager : UserControl
    {
        StudentClassManager csm = new StudentClassManager();
        StudentManagerBLL.StudentManager sm = new StudentManagerBLL.StudentManager();
        List<StudentExt> students = null;
        //用来记录当前的选择的学员
        StudentExt selectStu = null;
        public FrmStudentManager()
        {
            InitializeComponent();
            List<StudentClass> classes = csm.GetClasses();
            smclassCmb.ItemsSource = classes;
            smclassCmb.DisplayMemberPath = "ClassName";//设置下拉框的显示文本
            smclassCmb.SelectedValuePath = "ClassID";//设置下拉框显示文本对应的value
            smclassCmb.SelectedIndex = 0;
            //给DataGrid进行数据绑定,需要针对DG中列进行绑定对应的数据列
            students = sm.GetStudents(Convert.ToInt32(smclassCmb.SelectedValue));
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
            List<StudentExt> liststu = sm.GetStudentsByIdOrname(target);
            smDgStudentLsit.ItemsSource = null;
            if (liststu.Count<=0)
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
            smDgStudentLsit.ItemsSource = null;
            smDgStudentLsit.ItemsSource = students;
        }

       

        List<int> IdList = new List<int>();
        List<ChakanStudentManage> frmList = new List<ChakanStudentManage>();
        //双击事件，查看学员信息
        private void smDgStudentLsit_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
        
            StudentExt stu = smDgStudentLsit.SelectedItem as StudentExt;
            if (stu==null)
            {
                return;
            }
            ///当这个学员的完整信息已经存在的话，证明已经打开了一个窗口
            ///除非是打开新的学员窗口，否则只能把之前的窗口给呈现出来
            if (IdList.Contains(stu.StudentID))
            {
                foreach (ChakanStudentManage item in frmList)
                {
                    if (item.StuId == stu.StudentID)
                    {
                        //激活窗口，
                        item.Activate();
                    }
                }
            }
            else
            {
                StudentExt obj = sm.GetStudentId(stu.StudentID);
                IdList.Add(stu.StudentID);
                View.ChakanStudentManage xiugai = new ChakanStudentManage(obj);
                xiugai.Closing += Xiugai_Closing;
                frmList.Add(xiugai);
                xiugai.Show();
            }
           
        }


        //关闭窗口的数据
        private void Xiugai_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int stuId = (sender as ChakanStudentManage).StuId;
            IdList.Remove(stuId);
            foreach (ChakanStudentManage item in frmList)
            {
                if (item.StuId == stuId)
                {
                    frmList.Remove(item);
                    return;
                }
            }

        }
        //修改信息
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            StudentExt selectStu = smDgStudentLsit.SelectedItem as StudentExt;
            
            if (selectStu == null)
            {
                MessageBox.Show("请选择要修改的学员！", "提示");
                return;
            }
            StudentExt objStu = sm.GetStudentId(selectStu.StudentID);
            XiugaiStudentManage updateStuInfor = new XiugaiStudentManage(objStu);
            updateStuInfor.ShowDialog();
            //刷新DG中这个学员的信息
            RefreshDG();
        }



        private void RefreshDG()
        {
            students = sm.GetStudents(Convert.ToInt32(smclassCmb.SelectedValue));
            smDgStudentLsit.ItemsSource = students;
        }


        /// <summary>
        /// 删除学员信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            StudentExt selectStu = smDgStudentLsit.SelectedItem as StudentExt;
           
            if (selectStu == null)
            {
                MessageBox.Show("请选择要删除的学员！", "提示");
                return;
            }
            StudentExt student = sm.GetStudentById(selectStu.StudentID);
            if (student != null)
            {
               
                
                MessageBoxResult mbr = MessageBox.Show("您确定要删除【" + student.StudentName + "】", "警告", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (mbr == MessageBoxResult.OK)
                {
                    if (sm.DeleteStudentById(student.StudentID))
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
        /// 打印学员信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dayin_Click(object sender, RoutedEventArgs e)
        {
            selectStu = smDgStudentLsit.SelectedItem as StudentExt;
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

    //声明比较器
    class StudentIdDESC : IComparer<StudentExt>
    {
        ///-1，0，1
        public StudentIdDESC(bool b)
        {
            Bo = b;
        }
        public bool Bo { get; set; }
        public int Compare(StudentExt x, StudentExt y)
        {
            if (Bo)
            {
                return x.StudentID.CompareTo(y.StudentID);
            }
            else
            {
                return y.StudentID.CompareTo(x.StudentID);
            }
        }
    }

    class StudentNameDESC : IComparer<StudentExt>
    {
        ///-1，0，1
        public StudentNameDESC(bool b)
        {
            Bo = b;
        }
        public bool Bo { get; set; }
        public int Compare(StudentExt x, StudentExt y)
        {
            if (Bo)
            {
                return y.StudentName.CompareTo(x.StudentName);
            }
            else
            {
                return x.StudentName.CompareTo(y.StudentName);
            }
        }
    }
}
