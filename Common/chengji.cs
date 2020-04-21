using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public  class chengji
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
                excelSheet.Cells[1, 3] = "C#成绩";
                excelSheet.Cells[1, 4] = "SQL成绩";
                excelSheet.Cells[1, 5] = "录入时间";
                excelSheet.Cells[1, 6] = "班级名称";

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
