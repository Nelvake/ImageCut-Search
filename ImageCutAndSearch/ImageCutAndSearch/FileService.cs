using ImageProcessor;
using ImageProcessor.Imaging.Formats;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace ImageCutAndSearch
{
    public class FileService
    {
        public static string rootDirectory { get; set; } = @"C:\New";

        /// <summary>
        /// Инициализация имени файла.
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        public static FileInfo InitFileName(FileInfo fileInfo)
        {
            fileInfo.FileName = $"[{fileInfo.Country}][{fileInfo.City}][{fileInfo.Tag}][{fileInfo.Coordinates}][{Guid.NewGuid()}]";
            return fileInfo;
        }

        /// <summary>
        /// Уменьшение размера файла.
        /// </summary>
        public static void ResizeImage(FileInfo fileInfo)
        {
            var isCorrect = false;
            if (SizeCorrection(fileInfo)) isCorrect = true;
            ISupportedImageFormat format = new JpegFormat { Quality = 70 };
            using (MemoryStream inStream = new MemoryStream(fileInfo.PhotoBytes))
            {
                using (FileStream outStream = File.Create($"{DirectoryFind(fileInfo)}{fileInfo.FileName}.{format.DefaultExtension}"))
                {
                    using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
                    {
                        if (isCorrect)
                        {
                            imageFactory.Load(inStream).Format(format).Save(outStream);
                        }
                        else
                        {
                            Size size = imageFactory.Load(inStream).Image.Size / 2;
                            imageFactory.Load(inStream).Resize(size).Format(format).Save(outStream);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Поиск фотографий.
        /// </summary>
        /// <param name="country"></param>
        /// <param name="city"></param>
        /// <param name="tag"></param>
        /// <param name="coordinates"></param>
        public static void SearchImage(string country, string city, string tag, string coordinates)
        {
            var path = DirectoryFind(new FileInfo() { Country = country, City = city, Tag = tag, Coordinates = coordinates }, false);
            try
            {
                var images = Directory.GetFiles(path);

                Console.WriteLine("Найденные фотографии:");
                foreach (var item in images)
                {
                    Console.WriteLine(item);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Фотографии не найдены!\n{ex.Message}");
            }

            
        }

        /// <summary>
        /// Проверка на корректонсть размерности файла.
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        private static bool SizeCorrection(FileInfo fileInfo)
        {
            var desiredValue = 5;
            var sizeInMB = fileInfo.PhotoBytes.Length / 1000 / 1024;
            return sizeInMB < desiredValue;
        }

        /// <summary>
        /// Проверка нахождений директорий.
        /// При отсутсвии создает директорию.
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="check"></param>
        /// <returns></returns>
        private static string DirectoryFind(FileInfo fileInfo, bool check = true)
        {
            var path = rootDirectory;
            if (!Directory.Exists($@"{path}\{fileInfo.Country}") && check)
            {
                Directory.CreateDirectory($@"{rootDirectory}\{fileInfo.Country}");
            }
            path += $@"\{fileInfo.Country}";
            if (!Directory.Exists($@"{path}\{fileInfo.City}") && check)
            {
                Directory.CreateDirectory($@"{path}\{fileInfo.City}");
            }
            path += $@"\{fileInfo.City}";
            if (!Directory.Exists($@"{path}\{fileInfo.Tag}") && check)
            {
                Directory.CreateDirectory($@"{path}\{fileInfo.Tag}");
            }
            path += $@"\{fileInfo.Tag}";
            if (!Directory.Exists($@"{path}\{fileInfo.Coordinates}") && check)
            {
                Directory.CreateDirectory($@"{path}\{fileInfo.Coordinates}");
            }
            path += $@"\{fileInfo.Coordinates}\";
            return path;
        }
    }
}
