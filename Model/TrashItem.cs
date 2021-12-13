using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Project_MISiS.Model
{
    /// <summary>
    /// Класс, определяющий взаимодействие и свойства объекта мусора
    /// </summary>
    public class TrashItem : IGameObject
    {
        public int CategoryId { get; set; }
        public Image Icon { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public TrashItem(int categoryId, Image icon) // конструктор для объекта мусора
        {
            CategoryId = categoryId;
            Icon = icon;
        }
    }

    /// <summary>
    /// Класс, отвечающий за появление объектов мусора на экране
    /// </summary>
    static class TrashFactory
    {
        private static readonly string ProjectUrl = Directory.GetParent(Environment.CurrentDirectory).FullName;
        // Путь к папке проекта
        public static TrashItem CreateTrashItem()
        {
            Random rnd = new Random();
            int categoryId = rnd.Next(1, 6);
            string category = CategoryTable.CategoryDictionary[categoryId];
            Image icon = new Image
            {
                Source = new BitmapImage(new Uri(ProjectUrl + $"/Resources/icon-{category}-{rnd.Next(1, 5)}.png")),
                Height = 150,
                Width = 150
            };

            return new TrashItem(categoryId, icon);
        }
    }
}
