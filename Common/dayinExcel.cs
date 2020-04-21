using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace Common
{
    /// <summary>
    /// 将数据进行Excel工作表的导入导出
    /// </summary>
    public class dayinExcel
    {

        /// <summary>
        /// 导出数据到Excel工作簿
        /// </summary>
        /// <param name="dataTable">数据源</param>
        /// <param name="path">目标地址</param>
        /// <returns></returns>
        public static bool ExportToExcel(System.Data.DataTable dataTable, string path)
        {
            //先创建一个Excel连接，实例化一个从当前应用程序到Office办公软件中Excel的一个接口
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            //创建一个空的工作簿
            Workbook excelWb = excelApp.Workbooks.Add(System.Type.Missing);
            //创建一个工作表
            Worksheet excelSheet = excelWb.Sheets[1];
            try
            {
                excelSheet.Name = "学生信息表";
                excelSheet.Cells[1, 1] = "学号";
                excelSheet.Cells[1, 2] = "姓名";
                excelSheet.Cells[1, 3] = "性别";
                excelSheet.Cells[1, 4] = "出生日期";
                excelSheet.Cells[1, 5] = "身份证号";
                excelSheet.Cells[1, 6] = "考勤卡号";
                excelSheet.Cells[1, 7] = "年龄";
                excelSheet.Cells[1, 8] = "电话号码";
                excelSheet.Cells[1, 9] = "家庭住址";
                excelSheet.Cells[1, 10] = "班级名称";
                excelSheet.Cells[1, 11] = "打卡时间";

                //取消科学计数法
                excelSheet.Cells.NumberFormat = "@";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        excelSheet.Cells[i + 2, j + 1] = dataTable.Rows[i][j].ToString();
                    }
                }
                excelSheet.Columns.AutoFit();
                excelWb.SaveAs(path);
                excelWb.Close();
                excelApp.Quit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        
        
    }   
}
