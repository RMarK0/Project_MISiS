using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Project_MISiS.Model;

namespace Project_MISiS.ViewModel
{
    public class HandMovementObserver : IObserver
    {
        private readonly int _speed = 5;

        public void Update(ISubject sbj)
        {
            if (((HandMovementSubject)sbj).MoveDownTrigger)
            {
                if (!(((HandMovementSubject)sbj).ActiveHand.Y - _speed < 0))
                {
                    Thickness newMargin = ((HandMovementSubject)sbj).ActiveHand.HandImage.Margin;
                    newMargin.Top += _speed;
                    ((HandMovementSubject)sbj).ActiveHand.HandImage.Margin = newMargin;
                    ((HandMovementSubject)sbj).ActiveHand.Y -= _speed;
                }
            }

            if (((HandMovementSubject)sbj).MoveUpTrigger)
            {
                if (!(((HandMovementSubject)sbj).ActiveHand.Y + _speed > 700))
                {
                    Thickness newMargin = ((HandMovementSubject)sbj).ActiveHand.HandImage.Margin;
                    newMargin.Top -= _speed;
                    ((HandMovementSubject)sbj).ActiveHand.HandImage.Margin = newMargin;
                    ((HandMovementSubject)sbj).ActiveHand.Y += _speed;
                }
            }

            if (((HandMovementSubject)sbj).MoveLeftTrigger)
            {
                if (!(((HandMovementSubject)sbj).ActiveHand.X - _speed < 0))
                {
                    Thickness newMargin = ((HandMovementSubject)sbj).ActiveHand.HandImage.Margin;
                    newMargin.Left -= _speed;
                    ((HandMovementSubject)sbj).ActiveHand.HandImage.Margin = newMargin;
                    ((HandMovementSubject)sbj).ActiveHand.X -= _speed;
                }
            }

            if (((HandMovementSubject)sbj).MoveRightTrigger)
            {
                if (!(((HandMovementSubject)sbj).ActiveHand.X + _speed > 1200))
                {
                    Thickness newMargin = ((HandMovementSubject)sbj).ActiveHand.HandImage.Margin;
                    newMargin.Left += _speed;
                    ((HandMovementSubject)sbj).ActiveHand.HandImage.Margin = newMargin;
                    ((HandMovementSubject)sbj).ActiveHand.X += _speed;
                }
            }
        }
    }
}
