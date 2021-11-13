using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Project_MISiS.Model
{
    /// <summary>
    /// Класс, определяющий взаимодействие и свойства руки
    /// </summary>
    public class Hand
    {
        private static readonly string ProjectUrl = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.FullName;

        internal int X { get; set; }
        internal int Y { get; set; }
        public bool IsHoldingTrash { get; set; }
        public Image HandImage { get; set; }

        public Hand(int x, int y) // конструктор для объекта руки
        {
            HandImage = new Image();

            X = x;
            Y = y;

            HandImage.Source = new BitmapImage(new Uri(ProjectUrl + "\\Resources\\icon-hand-free.png"));
        }
    }
}
