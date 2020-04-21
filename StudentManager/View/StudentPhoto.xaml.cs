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
using System.Windows.Shapes;
using WPFMediaKit.DirectShow.Controls;

namespace StudentManager.View
{
    /// <summary>
    /// StudentPhoto.xaml 的交互逻辑
    /// </summary>
    public partial class StudentPhoto : Window
    {
        public StudentPhoto()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //检测系统连接的摄像头
            if (MultimediaUtil.VideoInputNames.Length > 0)
            {
                //当前的拍照设备采用默认摄像头
                picture.VideoCaptureSource = MultimediaUtil.VideoInputNames[0];
            }
            else
            {
                MessageBox.Show("您的设备暂无摄像设备链接！", "提示");
                this.Close();
            }
        }
        RenderTargetBitmap bmp = null;
        private void btnClickPhoto_Click(object sender, RoutedEventArgs e)
        {
            bmp = new RenderTargetBitmap((int)picture.Width, (int)picture.Height, 96, 96, PixelFormats.Default);
            //将摄像头捕获的区域显示到照片纸上
            bmp.Render(picture);
            //图片预览
            pictrueYulan.Source = bmp;
            //预览图显示，摄像头隐藏
            pictrueYulan.Visibility = Visibility.Visible;
            picture.Visibility = Visibility.Hidden;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            picture.Visibility = Visibility.Visible;
            pictrueYulan.Visibility = Visibility.Hidden;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //如果照片纸还是NULL则是未拍照
            if (bmp == null)
            {
                MessageBox.Show("请重新拍照！", "提示");
                picture.Visibility = Visibility.Visible;
                pictrueYulan.Visibility = Visibility.Hidden;
                return;
            }
            //选择路径保存照片
            Microsoft.Win32.SaveFileDialog fileDialog = new Microsoft.Win32.SaveFileDialog();
            fileDialog.Filter = "图像文件(*.jpg;*.jpeg;*.gif;*.png;*.bmp)|*.jpg;*.jpeg;*.gif;*.png;*.bmp";
            fileDialog.Title = "保存照片";
            fileDialog.FileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
            if (fileDialog.ShowDialog() == true)
            {
                //将刚才的照片以流的方式进行保存
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bmp));
                using (MemoryStream stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    byte[] buffer = stream.ToArray();
                    File.WriteAllBytes(fileDialog.FileName, buffer);
                    MessageBox.Show("照片保存成功！", "提示");
                    //刷新修改界面的照片
                    XiugaiStudentManage.imgPath = fileDialog.FileName;
                    picture.Visibility = Visibility.Visible;
                    pictrueYulan.Visibility = Visibility.Hidden;
                }
            }
        }
    }
    
}
