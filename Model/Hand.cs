using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Project_MISiS.Model
{
    /// <summary>
    /// Класс, определяющий взаимодействие и свойства руки
    /// </summary>
    public class Hand
    {
        private static readonly string ProjectUrl = Directory.GetParent(Environment.CurrentDirectory).FullName;

        internal int X { get; set; }
        internal int Y { get; set; }
        public bool IsHoldingTrash { get; set; }
        public Image HandImage { get; set; }

        public Hand(int x, int y) // конструктор для объекта руки
        {
            HandImage = new Image()
            {
                Margin = new Thickness(500, 0, 0, 0),
                Width = 150,
                Height = 150,
                Source = new BitmapImage(new Uri(ProjectUrl + "/Resources/icon-hand-free.png"))
            };
            X = x;
            Y = y;
        }
    }
}
