using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCreator
{
    class Creator
    {
        public Creator()
        {

        }

        public bool Create(string pProjectName)
        {
            string name = pProjectName.Trim();

            string origin = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "..\\..\\..\\AppDemo";
            string target = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "..\\..\\..\\" + name;
            if(!CopyProject(origin, target))
            {
                return false;
            }

            Rename(pProjectName);

            return true;
        }

        private bool CopyProject(string origin, string target)
        {
            bool result = true;

            if (!origin.EndsWith("\\"))
            {
                origin += "\\";
            }
            if (!target.EndsWith("\\"))
            {
                target += "\\";
            }

            DirectoryInfo info = new DirectoryInfo(origin);
            if (!Directory.Exists(target))
            {
                Directory.CreateDirectory(target, info.GetAccessControl());//创建目录,访问权限
            }

            FileInfo[] fileList = info.GetFiles();
            DirectoryInfo[] dirList = info.GetDirectories();
            foreach (FileInfo fi in fileList)
            {
                 File.Copy(fi.FullName, target + fi.Name, true);
            }
            foreach (DirectoryInfo di in dirList)
            {
                //CopyDir(origin + "\\" + di.Name, target + "\\" + di.Name);
                CopyProject(di.FullName, target + "\\" + di.Name);
            }
            //设置目录属性和访问权限
            DirectoryInfo tmp = new DirectoryInfo(target);
            tmp.Attributes = info.Attributes;
            tmp.SetAccessControl(info.GetAccessControl());

            return result;
        }

        private void Rename(string pProjectName)
        {
            string directoryPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "..\\..\\..\\" + pProjectName;
            DirectoryInfo directory = new DirectoryInfo(directoryPath);
            FileInfo[] fileList = directory.GetFiles();

            for (int i = 0; i < fileList.Length; i++)
            {
                string filename = fileList[i].FullName;
                string content = File.ReadAllText(filename);
                content = content.Replace("AppDemo", pProjectName);
                File.WriteAllText(filename, content);
            }

            for (int i = 0; i < fileList.Length; i++)
            {
                if (fileList[i].Name.IndexOf("AppDemo") != -1)
                {
                    string oldname = fileList[i].FullName;
                    string newname = fileList[i].FullName.Replace("AppDemo", pProjectName);
                    fileList[i].MoveTo(newname);
                    File.Delete(oldname);
                }
            }
        }
    }
}
