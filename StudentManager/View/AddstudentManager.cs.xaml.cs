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
    /// AddstudentManager.xaml 的交互逻辑
    /// </summary>
    public partial class AddstudentManager : UserControl
    {
       
        StudentClassManager csm = new StudentClassManager();
        StudentManagerBLL.StudentManager manager = new StudentManagerBLL.StudentManager();
        common.BitmapImg image = null;
        public StudentExt student1 { get; set; }

        public AddstudentManager(StudentExt stu)
        {
            InitializeComponent();
            student1 = stu;
           
            if (string.IsNullOrEmpty(stu.StuImage))
            {
                stuImg.Source = new BitmapImage(new Uri("/img/bg/zw.jpg", UriKind.RelativeOrAbsolute));
            }
            else
            {

                image = SerializeObjectTostring.DeserializeObject(stu.StuImage) as common.BitmapImg;
                img.Buffer = image.Buffer;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = new MemoryStream(image.Buffer);
                bitmap.EndInit();
                stuImg.Source = bitmap;
            }
            List<StudentClass> classes = csm.GetClasses();
            cmbClassName.ItemsSource = classes;
            cmbClassName.DisplayMemberPath = "ClassName";
            cmbClassName.SelectedValuePath = "ClassID";
            //cmbClassName.SelectedIndex = stu.ClassID - 1;
            cmbClassName.SelectedValue = 1;
        }



        private void txtName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                System.Windows.MessageBox.Show("姓名不能为空！");
                txtName.Focus();
               
            }
           
        }

        private void txtStuNoId_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtStuNoId.Text))
            {
                System.Windows.MessageBox.Show("身份证号不能为空！");
                txtStuNoId.Focus();
                return;
            }
            else
            {
                datePkBirthday.Content = Common.GetValueById.GetBirthday(txtStuNoId.Text.Trim()).ToString("yyyy-MM-dd");

                txtAge.Content = Common.GetValueById.GetAge(Common.GetValueById.GetBirthday(txtStuNoId.Text.Trim()));
            }

        }

        private void txtCardNo_LostFocus(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(txtCardNo.Text))
            {
                System.Windows.MessageBox.Show("打卡号不能为空！");
                txtCardNo.Focus();
                return;
            }
            return;
        }

      

        private void txtPhoneNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPhoneNumber.Text))
            {
                System.Windows.MessageBox.Show("联系方式不能为空！");
                txtPhoneNumber.Focus();
                return;
            }

            else if (Common.DataValidate.IsPhone(txtPhoneNumber.Text))
            {
                System.Windows.MessageBox.Show("必须为11位正整数！");
                txtStuNoId.Focus();
                return;
            }


        }
        //本地上传
        common.BitmapImg img = new common.BitmapImg();
        private void btnUploadPic_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.Filter = "图像文件(*.jpg;*.jpeg;*.gif;*.png;*.bmp)|*.jpg;*.jpeg;*.gif;*.png,*.bmp";
            if (fileDialog.ShowDialog() == true)
            {
                string path = fileDialog.FileName;
                stuImg.Source = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
                stuImg.Stretch = Stretch.UniformToFill;
                img.Buffer = File.ReadAllBytes(path);
            }
        }
        public static string imgPath;
        private void btnOpenVideo_Click(object sender, RoutedEventArgs e)
        {
            StudentPhoto photo = new StudentPhoto();
            photo.ShowDialog();
            if (!string.IsNullOrEmpty(imgPath))
            {
                //照片刷新了之后对新照片进行序列化
                stuImg.Source = new BitmapImage(new Uri(imgPath, UriKind.RelativeOrAbsolute));
                img.Buffer = File.ReadAllBytes(imgPath);
            }
        }
      
        private void btnSureUpdate_Click(object sender, RoutedEventArgs e)
        {
            StudentClassManager csm = new StudentClassManager();
            List<StudentClass> classes = csm.GetClasses();
            //改变数据之前的最终验证
            if (CheckInfor())
            {
               
                student1.StudentidNo = txtStuNoId.Text;
                student1.Age = (int)txtAge.Content;
                student1.Birthday = (string)datePkBirthday.Content;
                student1.StudentName = txtName.Text;
                student1.StudentidNo = txtStuNoId.Text;
                student1.CardNo = txtCardNo.Text;
                student1.ClasslD = (int)cmbClassName.SelectedValue;
                student1.StudentSex = (radboy.IsChecked == true ? "男" : "女");
                student1.PhoneNumber = txtPhoneNumber.Text;
                student1.StudentAddress = (string.IsNullOrEmpty(txtAddress.Text) ? null : txtAddress.Text);
              
                //判断是否重新选择了Image
                if (stuImg.Source == new BitmapImage(new Uri("/img/bg/zw.jpg", UriKind.RelativeOrAbsolute)))
                {
                    student1.StuImage = null;
                }
                //判断数据库中的图片是否和目前的上传图片一样
                else
                {
                    //证明未修改图片,目前的图片和原来数据库中的一致
                    if (image != null && img.Buffer == image.Buffer)
                    {
                        student1.StuImage = Common.SerializeObjectTostring.SerializeObject(image);
                    }
                    else
                    {
                        student1.StuImage = Common.SerializeObjectTostring.SerializeObject(img);
                    }

                }
                if (manager.AddstudentManager(student1))
                {
                    System.Windows.MessageBox.Show("添加成功！", "提示");
                    this.Visibility = Visibility.Hidden;
                }
                else
                {
                    System.Windows.MessageBox.Show("添加失败，请稍后再试！", "提示");
                }
            }
        }

        private void btnCancle_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        bool CheckInfor()
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                System.Windows.MessageBox.Show("姓名不能为空！");
                txtName.Focus();
                return false;
            }
           
            if (string.IsNullOrEmpty(txtCardNo.Text))
            {
                System.Windows.MessageBox.Show("打卡号不能为空！");
                txtCardNo.Focus();
                return false;
            }
            
            if (string.IsNullOrEmpty(txtStuNoId.Text))
            {
                System.Windows.MessageBox.Show("身份证号不能为空！");
                txtStuNoId.Focus();
                return false;
            }
            
           
            if (string.IsNullOrEmpty(txtPhoneNumber.Text))
            {
                System.Windows.MessageBox.Show("联系方式不能为空！");
                txtPhoneNumber.Focus();
                return false;
            }

            if (Common.DataValidate.IsPhone(txtPhoneNumber.Text))
            {
                System.Windows.MessageBox.Show("必须为11位数！");
                txtStuNoId.Focus();
                return false;
            }

            return true;
        }

    }
}
