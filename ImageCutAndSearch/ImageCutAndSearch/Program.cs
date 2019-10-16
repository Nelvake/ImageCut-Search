using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace ImageCutAndSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            // Тестовый комментарий
            // Приходящий лист по типу FileInfo
            List<FileInfo> images = new List<FileInfo>(5);

            #region Использую для инициализации листа
            var rootDirectory = @"C:\New\";
            List<string> filesPath = Directory.GetFiles(rootDirectory).ToList();

            FileService.InitOfName(images, filesPath);
            //ManualInput(images, filesPath);
            //AutoInit(images, filesPath);
            #endregion


            #region Уменьшение размера фотографии и одновременное распределение по иерархии папок
            FileService.ResizeImage(images);

            // Два варианта с перегрузкой.

            //foreach (var image in images)
            //{
            //    FileService.ResizeImage(image);
            //}
            #endregion

            // Поиск фотографии по заданым полям.
            FileService.SearchImage("Россия", "Москва", "Река", "288485");
        }

        /// <summary>
        /// Ручной ввод полей файла.
        /// Используется для тестов.
        /// </summary>
        /// <param name="images"></param>
        /// <param name="filesPath"></param>
        private static void ManualInput(List<FileInfo> images, List<string> filesPath)
        {
            foreach (var file in filesPath)
            {
                images.Add(new FileInfo(file)
                {
                    Country = Console.ReadLine(),
                    City = Console.ReadLine(),
                    Tag = Console.ReadLine(),
                    Coordinates = "327285"
                });
                FileService.InitFileName(images.Last());
            }
        }

        /// <summary>
        /// Автоматический инициализация полей
        /// с заранее заготовленными данными.
        /// Используется для тестов
        /// </summary>
        /// <param name="images"></param>
        /// <param name="filesPath"></param>
        private static void AutoInit(List<FileInfo> images, List<string> filesPath)
        {
            images.Add(new FileInfo(filesPath[0])
            {
                Country = "Казахстан",
                City = "Астана",
                Tag = "Парк",
                Coordinates = "12843930"
            });
            FileService.InitFileName(images.Last());
            images.Add(new FileInfo(filesPath[1])
            {
                Country = "Казахстан",
                City = "Костанай",
                Tag = "Парк",
                Coordinates = "384020843"
            });
            FileService.InitFileName(images.Last());
        }
    }
}
