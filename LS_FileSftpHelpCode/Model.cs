using System;
using System.Collections.Generic;
using System.Text;

namespace LS_FileSftpHelpCode
{
    public class HeaderdataModel
    {
        /// <summary>
        /// 客户订单指定
        /// </summary>
        public string order_name { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public string mailing_date { get; set; }
        /// <summary>
        /// 例如。GKA 1000专用1000
        /// </summary>
        public string mailing_place { get; set; }
        /// <summary>
        /// 产品定义的发货单ID-Post AT提供的规范
        /// </summary>
        public string dispatch_note_id { get; set; }
        /// <summary>
        /// 您在Post AT上的客户编号或客户的客户卡号
        /// </summary>
        public string client_number { get; set; }
        /// <summary>
        /// 联系人的电子邮件地址
        /// </summary>
        public string client_contact_email { get; set; }
        /// <summary>
        /// 物品和容器数据的文件名
        /// </summary>
        public string titleitemfile { get; set; }
        /// <summary>
        /// Post AT收到的唯一PVM用户ID
        /// </summary>
        public string pvm_user { get; set; }
        /// <summary>
        /// 0 =没有文件，1 =发货单，2 =收件人列表，3 = 1 + 2
        /// </summary>
        public string documents_flag { get; set; }
        /// <summary>
        /// 0 =总订单，1 =每袋订单
        /// </summary>
        public string bags_split { get; set; }
    }
    public class AddressdataModel
    {
        /// <summary>
        /// 项目参考号码
        /// </summary>
        public string Kundenreferenz { get; set; }
        /// <summary>
        /// 项识别
        /// </summary>
        public string Sendungs_ID { get; set; }
        /// <summary>
        /// 
        /// </summary>克/ 2或3位小数 小数点前两位，小数点后两位
        public string Bruttogewicht_der { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Format_der_Sendung { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Bestimmungsland { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Empfängername { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Empfänger_PLZ { get; set; }
        /// <summary>
        ///
        /// </summary>
        public string Bestimmungsort { get; set; }
        /// <summary>
        /// Adresszeile 1(Empfänger)
        /// </summary>
        public string Adresszeile { get; set; }
        /// <summary>
        /// 接收器电话号码
        /// </summary>
        public string EmpfängerTelefonnummer { get; set; }
        /// <summary>
        /// 服务
        /// </summary>
        public string Versandart { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Wareninhalt { get; set; }
    }

    public class CustomdataMode
    {
        /// <summary>
        /// 号
        /// </summary>
        public string Kundenreferenz { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Art_der_Sendung_Text { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Wert_der_Sendung { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Währung { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name_des_Absenders { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Straßenname_Nummer { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Ort_des_Absenders { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Aufgabeland { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Stückzahl_je_Artikel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Beschreibung_des_Artikels { get; set; }
    }
}
