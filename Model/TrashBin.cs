using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Project_MISiS.Model
{
    public class TrashBin : IGameObject
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int CategoryId { get; set; }
        public Image Icon { get; set; }

        public TrashBin(int categoryId, Image icon)
        {
            CategoryId = categoryId;
            Icon = icon;
        }
    }

    public static class TrashBinFactory
    {
        private static readonly string ProjectUrl = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.FullName;

        public static TrashBin CreateTrashBin(int binNumber)
        {
            string category = CategoryTable.CategoryDictionary[binNumber + 1];

            Image icon = new Image
            {
                Source = new BitmapImage(new Uri(ProjectUrl + $"\\Resources\\bin-{category}.png")),
                Height = 150,
                Width = 200,
                Margin = new Thickness(binNumber * 200, 500, 0, 0)
            };
            return new TrashBin(binNumber + 1, icon) { X = binNumber * 200, Y = 0 };
        }
    }
}
