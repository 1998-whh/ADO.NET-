using Common;
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

namespace StudentManager.View
{
    /// <summary>
    /// FrmChangePassword.xaml 的交互逻辑
    /// </summary>
    public partial class FrmChangePassword : UserControl
    {
        public FrmChangePassword()
        {
            InitializeComponent();
        }
        //确认
        private void btnconfirm_Click(object sender, RoutedEventArgs e)
        {
            //if (Originalpassword.Text.Trim().Length == 0)
            //{
            //    MessageBox.Show("请输入登录账号！", "登录提示");
            //    Originalpassword.Focus();
            //    return;
            //}
            //if (DataValidate.IsInteger(Originalpassword.Text.Trim()) == false)
            //{
            //    MessageBox.Show("请输入正确账号！(纯数字格式)", "登录提示");
            //    Originalpassword.Focus();
            //    return;
            //}
            //if (txtLogPwd.Password.Trim().Length == 0)
            //{
            //    MessageBox.Show("请输入登录密码！", "登录提示");
            //    txtLogPwd.Focus();
            //    return;
            //}
            ////输入的账号密码
            //Admins admin = new Admins()
            //{
            //    Loginld = Convert.ToInt32(txtLogId.Text.Trim()),
            //    //LoginPwd = txtLogPwd.Password
            //};
            ////和后台交互查询，判断登录信息是否正确
            //try
            //{
            //    Admins mainuse = new AdminManager().GetAdmins(admin);
            //    if (mainuse == null)
            //    {
            //        MessageBox.Show("用户账号不存在！", "提示信息");
            //        txtLogId.Focus();
            //    }
            //    else
            //    {
            //        if (mainuse.LoginPwd == txtLogPwd.Password)
            //        {
            //            //保存登录信息
            //            App.CurrentAdmin = mainuse;
            //            this.DialogResult = true;
            //            this.Close();
            //        }
            //        else
            //        {
            //            MessageBox.Show("用户密码错误！", "提示信息");
            //            txtLogPwd.Focus();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("服务器连接异常，登录失败！请检查您的网络！");
            //}
        }
        //取消
        private void btncancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
