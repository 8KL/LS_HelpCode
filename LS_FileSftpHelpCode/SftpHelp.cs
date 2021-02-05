using Renci.SshNet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LS_FileSftpHelpCode
{
    //首先，服务器返回的错误是“权限被拒绝”，应该相信这一点。这意味着以下之一：
    //1）您的代码可以正常运行-它连接到正确的服务器，登录到正确的帐户，并传递了正确的目录路径，但是远程SFTP用户帐户或远程目录权限使您没有创建目录的权限。解决方案在服务器端-意味着SSH服务器管理员应验证用户帐户是否具有权限，验证HOME目录以及验证远程  文件系统上的目录权限。
    //2）未连接到正确的服务器，或未登录到正确的用户帐户（或至少未使用您想要的服务器和/或用户帐户
    //3）HOME目录与您期望的有所不同。也许将IPSwitch客户端工具设置为在登录后立即自动切换到其他目录？添加对RealPath的调用以找到HOME目录的确切路径：字符串homePath = sftp.RealPath（“。”，“”）
    //5）也许目录“ xyz”已经存在，并且“权限被拒绝”错误是由这一事实引起的。 No
    //6) 供应商提供软件信息，是供应商自己的还是**  需要确认  走的是什么语言！ 可以提供一个可以连接上的dome
    public class SftpHelp
    {
        private SftpClient sftpClient;

        /// <summary>
        /// 连接状态
        /// </summary>+
        public bool Connected
        {
            get { return sftpClient.IsConnected; }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ip">ip</param>
        /// <param name="port">端口</param>
        /// <param name="user">账号</param>
        /// <param name="pwd">密码</param>
        public SftpHelp(string ip, string port, string user, string pwd)
        {
            sftpClient = new SftpClient(ip, Int32.Parse(port), user, pwd);
        }
        /// <summary>
        /// 连接sftp
        /// </summary>
        /// <returns></returns>
        public bool Connect(string path)
        {
            try
            {
                if (!Connected)
                {
                    sftpClient.Connect();
                }
                return true;
            }
            catch (Exception ex)
            {
                string absolutePath = System.IO.Path.GetFullPath(path);
                System.IO.File.Delete(absolutePath);
                throw new Exception(string.Format("连接sftp失败，原因：{0}", ex.Message));
            }
        }
        /// <summary>
        /// 断开sftp
        /// </summary>
        public void Disconnrct()
        {
            try
            {
                if (sftpClient != null && Connected)
                {
                    sftpClient.Disconnect();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(string.Format("断开SFTP失败，原因：{0}", ex.Message));
            }
        }
        /// <summary>
        /// sftp上传文件
        /// </summary>
        /// <param name="localPath"></param>
        /// <param name="remotePath"></param>
        /// <returns></returns>
        public int Put(string localPath, string remotePath)
        {
            try
            {

                using (var file = File.OpenRead(localPath))
                {
                    Connect(null);
                    sftpClient.UploadFile(file, remotePath);
                    Disconnrct();
                }
                return 1;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("SFTP文件上传失败，原因：{0}", ex.Message));
            }
        }
        /// <summary>
        /// sftp获取文件
        /// </summary>
        /// <param name="remotePath">远程地址</param>
        /// <param name="localPath">本地地址</param>
        public void Get(string remotePath, string localPath)
        {
            try
            {
                Connect(null);
                var byt = sftpClient.ReadAllBytes(remotePath);
                Disconnrct();
                File.WriteAllBytes(localPath, byt);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("sftp文件获取失败：{0}", ex.Message));
            }
        }
        /// <summary>
        /// 删除sftp文件 
        /// </summary>
        /// <param name="remoteFile">远程路径</param>
        public void Delete(string remoteFile)
        {
            try
            {
                Connect(null);
                sftpClient.Delete(remoteFile);
                Disconnrct();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("sftp文件删除失败，原因：{0}", ex.Message));
            }
        }
        /// <summary>
        /// 获取sftp文件列表
        /// </summary>
        /// <param name="remotePath">远程目录</param>
        /// <param name="fileSuffix">文件后缀</param>
        /// <returns></returns>
        public ArrayList GetFileList(string remotePath, string fileSuffix)
        {
            try
            {
                Connect(null);
                var files = sftpClient.ListDirectory(remotePath);
                Disconnrct();
                var objList = new ArrayList();
                foreach (var file in files)
                {
                    string name = file.Name;
                    if (name.Length > (fileSuffix.Length + 1) && fileSuffix == name.Substring(name.Length - fileSuffix.Length))
                    {
                        objList.Add(name);
                    }
                }
                return objList;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("sftp文件列表获取失败，原因：{0}", ex.Message));
            }
        }
        /// <summary>
        /// 移动sftp文件
        /// </summary>
        /// <param name="oldRemotePath">旧远程路径</param>
        /// <param name="newRemotePath">新远程路径</param>
        public void Move(string oldRemotePath, string newRemotePath)
        {
            try
            {
                Connect(null);
                sftpClient.RenameFile(oldRemotePath, newRemotePath);
                Disconnrct();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("sftp文件移动失败，原因：{0}", ex.Message));
            }
        }
    }
}
