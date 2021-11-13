using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Project_MISiS.Model
{
    interface IGameObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Image Icon { get; set; }
        int CategoryId { get; set; }
    }
}
