using System;
using System.Collections.Generic;
using System.Text;

namespace ImageCutAndSearch
{
    public class FileService
    {
        /// <summary>
        /// Инициализация имени файла
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        public static FileInfo InitFileName(FileInfo fileInfo)
        {
            fileInfo.FileName = $"[{fileInfo.Country}][{fileInfo.City}][{fileInfo.Tag}][{fileInfo.Coordinates}].";
            return fileInfo;
        }
    }
}
