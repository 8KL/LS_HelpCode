using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace LS_FileSftpHelpCode
{
    class Program
    {
        /// <summary>
        /// 文件操作和SFTP
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            SftpHelp sFTPHelper = new SftpHelp("filetransfertest.post.at", "22", "data_0023053590_test", "7s9gGfnn");
            var path = "Aviso_0021876895_" + DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss").Replace("/", "-").Replace(" ", "").Replace(":", "").Replace("-", "");  //文件名
            if (Directory.Exists(path))
            {
                Console.WriteLine("创建失败");
            }
            else
            {
                var datetime = Convert.ToString(DateTime.Now.ToString("yy-MM-dd HH:mm:ss ffff")).Replace("-", "").Replace(" ", "").Replace(":", "") + ".csv";
                Directory.CreateDirectory(path);//创建本地文件名字
                var customsPath = "customs_0021876895_" + datetime;
                var addresslistPath = "addresslist_0023053590_" + datetime;
                var headerdataPath = "headerdata_0023053590_" + datetime;
                var address =  HeaderModel();
                var header = AddressModel();
                var customs = CustomModel();
                new CvsFileHelp().SaveListtoCsv(header, path + "/" + headerdataPath);
                new CvsFileHelp().SaveListtoCsv(address, path + "/" + addresslistPath);
                new CvsFileHelp().SaveListtoCsv(customs, path + "/" + customsPath);
                var xinname = path + ".zip";
                FileHelp.CompressDirectory(path, xinname, 9, false);//要压缩的文件夹名字    文件夹新名字
                sFTPHelper.Connect(xinname);
                sFTPHelper.Put(xinname, @"/incoming/" + xinname);//本地路径  远程路径
                string absolutePath = System.IO.Path.GetFullPath(xinname);
                //if (!Directory.Exists(absolutePath))//本地压缩文件的名字
                //{
                //    //Directory.Delete(xinname, true);//删除文件夹
                //    System.IO.File.Delete(absolutePath);
                //    Console.WriteLine("删除成功");
                //}
                //else
                //{
                //    Console.WriteLine("删除失败");
                //}
            }
            Console.WriteLine("1");
            Console.ReadKey();     
        }
        public static List<HeaderdataModel> HeaderModel()
        {
            try
            {
                string strdata = "[{'order_name':'Yw0000001Test','mailing_date':'2020-01-06','mailing_place':'1000','dispatch_note_id':'4404','client_number':'0123456789','client_contact_email':'12346@11.com','titleitemfile':'1321231','pvm_user':'12345','documents_flag':'0','bags_split':'1'}]";
                HeaderdataModel exeModel = new HeaderdataModel();
                string s = JsonConvert.SerializeObject(exeModel);
                //JavaScriptSerializer Serializer = new JavaScriptSerializer();
                List<HeaderdataModel> dhldata = JsonConvert.DeserializeObject<List<HeaderdataModel>>(strdata);
                return dhldata;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<AddressdataModel> AddressModel()
        {
            try
            {
                string strdata = "[{'Kundenreferenz':'Yw0000001Test','Sendungs_ID':'S10','Bruttogewicht_der':'0.1','Format_der_Sendung':'1','Bestimmungsland':'core','Empfängername':'abbre','Empfänger-PLZ':'1','Bestimmungsort':'1','Adresszeile':'dizhiadd','EmpfängerTelefonnummer':'123456789','Versandart':'2020','Wareninhalt':'true'}]";
                AddressdataModel exeModel = new AddressdataModel();
                string s = JsonConvert.SerializeObject(exeModel);
                //JavaScriptSerializer Serializer = new JavaScriptSerializer();
                List<AddressdataModel> dhldata = JsonConvert.DeserializeObject<List<AddressdataModel>>(strdata);
                return dhldata;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<CustomdataMode> CustomModel()
        {
            try
            {
                string strdata = "[{'Kundenreferenz':'Yw0000001Test','Art_der_Sendung_Text':'2020-01-06','Wert_der_Sendung':'1000','Währung':'4404','Name_des_Absenders':'0123456789','Straßenname_Nummer':'12346@11.com','Ort_des_Absenders':'1321231','Aufgabeland':'12345','Stückzahl_je_Artikel':'0','Beschreibung_des_Artikels':'1'}]";
                HeaderdataModel exeModel = new HeaderdataModel();
                string s = JsonConvert.SerializeObject(exeModel);
                //JavaScriptSerializer Serializer = new JavaScriptSerializer();
                List<CustomdataMode> dhldata = JsonConvert.DeserializeObject<List<CustomdataMode>>(strdata);
                return dhldata;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
