using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DaleHackathon2020
{
    public static class Library
    {
        public static void CheckFolderPathAccess(string folderDescription, string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                throw new System.Exception($"Cannot find or access the {folderDescription} folder:  {folderPath}");
            }
        }
    }
}
