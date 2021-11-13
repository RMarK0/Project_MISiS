using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Project_MISiS.Model;

namespace Project_MISiS.ViewModel
{
    class Observers
    {
        public class HandMovementObserver : IObserver
        {
            private readonly int _speed = 3;

            public void Update(ISubject sbj)
            {
                if (((HandMovementSubject)sbj).MoveDownTrigger)
                {
                    Thickness newMargin = ((HandMovementSubject)sbj).ActiveHand.HandImage.Margin;
                    newMargin.Top += _speed;
                    ((HandMovementSubject)sbj).ActiveHand.HandImage.Margin = newMargin;
                    ((HandMovementSubject)sbj).ActiveHand.Y -= _speed;
                }

                if (((HandMovementSubject)sbj).MoveLeftTrigger)
                {
                    Thickness newMargin = ((HandMovementSubject)sbj).ActiveHand.HandImage.Margin;
                    newMargin.Left -= _speed;
                    ((HandMovementSubject)sbj).ActiveHand.HandImage.Margin = newMargin;
                    ((HandMovementSubject)sbj).ActiveHand.X -= _speed;
                }

                if (((HandMovementSubject)sbj).MoveRightTrigger)
                {
                    Thickness newMargin = ((HandMovementSubject)sbj).ActiveHand.HandImage.Margin;
                    newMargin.Left += _speed;
                    ((HandMovementSubject)sbj).ActiveHand.HandImage.Margin = newMargin;
                    ((HandMovementSubject)sbj).ActiveHand.X += _speed;
                }

                if (((HandMovementSubject)sbj).MoveUpTrigger)
                {
                    Thickness newMargin = ((HandMovementSubject)sbj).ActiveHand.HandImage.Margin;
                    newMargin.Top -= _speed;
                    ((HandMovementSubject)sbj).ActiveHand.HandImage.Margin = newMargin;
                    ((HandMovementSubject)sbj).ActiveHand.Y += _speed;
                }
            }
        }

        public class TrashMovementObserver : IObserver
        {
            private bool DetectCollision(Hand hand, TrashItem trash)
            {
                RectangleF handRect = new RectangleF(hand.X, hand.Y, (float)hand.HandImage.Width / 2,
                    (float)hand.HandImage.Height / 2);
                RectangleF trashRect = new RectangleF(trash.X, trash.Y, (float)trash.Icon.Width / 2,
                    (float)trash.Icon.Height / 2);

                if (handRect.IntersectsWith(trashRect))
                    return true;
                return false;
            }

            private bool DetectCollision(TrashItem trash, TrashBin bin)
            {
                RectangleF binRect = new RectangleF(bin.X, bin.Y, (float)bin.Icon.Width / 2,
                    (float)bin.Icon.Height / 2);
                RectangleF trashRect = new RectangleF(trash.X, trash.Y, (float)trash.Icon.Width / 2,
                    (float)trash.Icon.Height / 2);

                if (binRect.IntersectsWith(trashRect))
                    return true;
                return false;
            }

            public void Update(ISubject subj)
            {
                try
                {
                    TrashMovementSubject subject = (TrashMovementSubject)subj;
                    Hand activeHand = subject.GamePage.MainHand;

                    if (subject.IsMovingTrash)
                    {
                        Thickness trashItemMargin = subject.GamePage.MainHand.HandImage.Margin;
                        trashItemMargin.Left -= 20;
                        subject.GamePage.ActiveTrashItem.Icon.Margin = trashItemMargin;
                        subject.GamePage.ActiveTrashItem.X = subject.GamePage.MainHand.X - 20;
                        subject.GamePage.ActiveTrashItem.Y = subject.GamePage.MainHand.Y;
                    }
                    else
                    {
                        if (!subject.IsThrowingTrash)
                        {
                            /*hand intersects with trashitem*/
                            if (DetectCollision(subject.GamePage.MainHand, subject.GamePage.ActiveTrashItem))
                            {
                                string ProjectUrl = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.FullName;
                                subject.GamePage.ActiveTrashItem.X = activeHand.X;
                                subject.GamePage.ActiveTrashItem.Y = activeHand.Y;

                                subject.GamePage.ActiveTrashItem.Icon.Margin = activeHand.HandImage.Margin;

                                subject.GamePage.MainHand.HandImage.Source = new BitmapImage(new Uri(ProjectUrl + "\\Resources\\icon-hand-pitched.png"));
                                subject.GamePage.MainHand.IsHoldingTrash = true;
                            }
                        }
                        else
                        {
                            string ProjectUrl = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.FullName;

                            subject.GamePage.MainHand.IsHoldingTrash = false;
                            subject.GamePage.MainHand.HandImage.Source = new BitmapImage(new Uri(ProjectUrl + "\\Resources\\icon-hand-free.png"));
                            // 
                            foreach (TrashBin bin in subject.GamePage.TrashBins)
                            {
                                if (DetectCollision(subject.GamePage.ActiveTrashItem, bin))
                                {
                                    subject.GamePage.GameCanvas.Children.Remove(subject.GamePage.ActiveTrashItem.Icon);
                                    subject.GamePage.ActiveTrashItem = null;
                                    subject.GamePage.MainHand.IsHoldingTrash = false;

                                    break;
                                }

                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}
