using StudentManagerModel;
using StudentManagerModel.ObjExt;
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
    /// AddScoreManager.xaml 的交互逻辑
    /// </summary>
    public partial class AddScoreManager : UserControl
    {
        public AddScoreManager()
        {
            InitializeComponent();
        }
        ScoreList score = new ScoreList();
        StudentManagerBLL.ScoreListManager Score = new StudentManagerBLL.ScoreListManager();
        /// <summary>
        /// 添加成绩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_score_Click(object sender, RoutedEventArgs e)
        {
           
                score.StudentlD = Convert.ToInt32(xueshengID.Text);
                score.CSharp = Convert.ToInt32(Cname.Text);
                score.SQLServerDB = Convert.ToInt32(Sqlname.Text);
            if (Score.AddScoreManager(score))
            {
                System.Windows.MessageBox.Show("添加成功！", "提示");
                this.Visibility = Visibility.Hidden;
            }
            else
            {
                System.Windows.MessageBox.Show("添加失败，请稍后再试！", "提示");
            }

        }

       

        /// <summary>
        /// C#成绩验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cname_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Cname.Text))
            {
                System.Windows.MessageBox.Show("C#成绩不能为空！");
                Cname.Focus();

            }
        }
        /// <summary>
        /// SQL成绩验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sqlname_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Sqlname.Text))
            {
                System.Windows.MessageBox.Show("SQl成绩不能为空！");
                Sqlname.Focus();

            }
        }
        /// <summary>
        /// 学生ID不能为空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xueshengID_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(xueshengID.Text))
            {
                System.Windows.MessageBox.Show("学生ID不能为空！");
                Cname.Focus();

            }
        }
      
      
    }
}
