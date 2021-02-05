using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace LS_FileSftpHelpCode
{
    public class CvsFileHelp
    {
        public void WriteCVS(string fileName, int id, double data)
        {
            if (!File.Exists(fileName)) //当文件不存在时创建文件
            {
                //创建文件流(创建文件)
                FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                //创建流写入对象，并绑定文件流
                StreamWriter sw = new StreamWriter(fs);
                //实例化字符串流
                StringBuilder sb = new StringBuilder();
                //将数据添加进字符串流中（如果数据标题有变更，修改此处）
                sb.Append("林立康").Append(",").Append("666").Append(",");
                //将字符串流数据写入文件
                sw.WriteLine(sb);
                //刷新文件流
                sw.Flush();
                sw.Close();
                fs.Close();
            }

            //将数据写入文件

            //实例化文件写入对象
            StreamWriter swd = new StreamWriter(fileName, true, Encoding.Default);
            StringBuilder sbd = new StringBuilder();
            //将需要保存的数据添加到字符串流中
            sbd.Append(id).Append(",").Append(data).Append(",");
            swd.WriteLine(sbd);
            swd.Flush();
            swd.Close();
        }
        public void WriteCVS(string fileName, int id, double data, int data1, string data2, bool flag)
        {
            if (!File.Exists(fileName)) //当文件不存在时创建文件
            {
                //创建文件流(创建文件)
                FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                //创建流写入对象，并绑定文件流
                StreamWriter sw = new StreamWriter(fs);
                //实例化字符串流
                StringBuilder sb = new StringBuilder();
                //将数据添加进字符串流中（如果数据标题有变更，修改此处）
                sb.Append("ID").Append(",").Append("Data").Append(",").Append("Data1").Append(",").Append("Data2").Append(",").Append("Flag").Append(",");
                //将字符串流数据写入文件
                sw.WriteLine(sb);
                //刷新文件流
                sw.Flush();
                sw.Close();
                fs.Close();
            }
            //将数据写入文件
            //实例化文件写入对象
            StreamWriter swd = new StreamWriter(fileName, true, Encoding.Default);
            StringBuilder sbd = new StringBuilder();
            //将需要保存的数据添加到字符串流中
            sbd.Append(id).Append(",").Append(data).Append(",").Append(data1).Append(",").Append(data2).Append(",").Append(flag).Append(",");
            swd.WriteLine(sbd);
            swd.Flush();
            swd.Close();
        }
        /// <summary>
        /// filePath 路径 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lst"></param>
        /// <param name="filePath"></param>
        public  void SaveListtoCsv<T>(List<T> lst, string filePath)
        {
            //StreamWriter sw = new StreamWriter("成功记录.csv", false, UnicodeEncoding.GetEncoding("GB2312"));
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.GetEncoding("utf-8")))
            {
                // 生成列
                var type = typeof(T);
                PropertyInfo[] props = type.GetProperties();
                StringBuilder strColumn = new StringBuilder();
                for (var index = 0; index < props.Length; index++)
                {
                    PropertyInfo item = props[index];
                    strColumn.Append("\"" + item.Name + "\"");
                    if (index != props.Length - 1)
                    {
                        strColumn.Append(";");
                    }
                }
                sw.WriteLine(strColumn);

                // 写入数据
                StringBuilder strValue = new StringBuilder();
                foreach (var dr in lst)
                {
                    strValue.Clear();
                    for (var index = 0; index < props.Length; index++)
                    {
                        PropertyInfo item = props[index];
                        var value = "";
                        if (item.GetValue(dr, null) != null)
                        {
                            value = item.GetValue(dr, null).ToString().Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                        }
                        strValue.Append("\"" + value + "\"");
                        if (index != props.Length - 1)
                        {
                            strValue.Append(";");
                        }
                    }
                    sw.WriteLine(strValue);
                }
            }

        }
    }
}
