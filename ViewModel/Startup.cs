using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Project_MISiS.Model;

namespace Project_MISiS.ViewModel
{
    public static class Startup
    {
        public static void Start(GamePage page)
        {
            page.MainHand = new Hand(600, 600);
            
            page.GameCanvas.Children.Add(page.MainHand.HandImage);
            
            Panel.SetZIndex(page.MainHand.HandImage, 1);

            for (int i = 0; i < 5; i++) // этот цикл отвечает за размещение мусорок
            {
                page.TrashBins[i] = TrashBinFactory.CreateTrashBin(i);
                page.GameCanvas.Children.Add(page.TrashBins[i].Icon);
            }

            for (int i = 0; i < 5; i++)
                page.Results[i] = new Result(i + 1);
            
        }
    }
}
