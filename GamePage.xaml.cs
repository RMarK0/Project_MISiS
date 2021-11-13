using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Project_MISiS.Model;
using Project_MISiS.ViewModel;

namespace Project_MISiS
{
    public partial class GamePage : Page
    {
        private readonly DispatcherTimer gameTickTimer = new DispatcherTimer();

        public Hand MainHand;
        public TrashItem ActiveTrashItem;
        public TrashBin[] TrashBins = new TrashBin[6]; // создаем массив мусорок

        private readonly TrashMovementSubject _trashMovementSubject;
        private readonly Observers.TrashMovementObserver _trashMovementObserver = new Observers.TrashMovementObserver();
        private readonly HandMovementSubject _handMovementSubject;
        private readonly Observers.HandMovementObserver _handMovementObserver = new Observers.HandMovementObserver();

        public GamePage()
        {
            InitializeComponent();
            
            MainHand = new Hand(500, 500);
            GameCanvas.Children.Add(MainHand.HandImage);
            MainHand.HandImage.VerticalAlignment = VerticalAlignment.Bottom;
            MainHand.HandImage.HorizontalAlignment = HorizontalAlignment.Left;
            MainHand.HandImage.Margin = new Thickness(500, 0, 0, 500);
            MainHand.HandImage.Width = 150;
            MainHand.HandImage.Height = 150;
            Panel.SetZIndex(MainHand.HandImage, 1);
            

            
            for (int i = 0; i < 6; i++) // этот цикл отвечает за размещение мусорок
            {
                TrashBins[i] = TrashBinFactory.CreateTrashBin(i);
                GameCanvas.Children.Add(TrashBins[i].Icon);
            }

            _trashMovementSubject = new TrashMovementSubject(this);
            _handMovementSubject = new HandMovementSubject(this);

            _trashMovementSubject.Attach(_trashMovementObserver);
            _handMovementSubject.Attach(_handMovementObserver);
        }

        private void GamePage_OnLoaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            if (window != null)
            {
                window.KeyDown += WindowOnKeyDown;
                window.KeyUp += WindowOnKeyUp;
            }

            gameTickTimer.Tick += GameTickTimer_Tick;
            gameTickTimer.Interval = TimeSpan.FromMilliseconds(5);
            gameTickTimer.IsEnabled = true;
        }

        // эти 4 переменные отвечают за движение по осям X и Y
        private bool _moveRight;
        private bool _moveLeft;
        private bool _moveUp;
        private bool _moveDown;
        
        private void GameTickTimer_Tick(object sender, EventArgs e)
        {
            HandDebugTextBlock.Text = $"Hand: {MainHand.X} {MainHand.Y}";
            if (ActiveTrashItem != null) 
                TrashDebugTextBlock.Text = $"Trash: {ActiveTrashItem.X} {ActiveTrashItem.Y}";

            if (ActiveTrashItem == null)
            {
                ActiveTrashItem = TrashFactory.CreateTrashItem();
                
                ActiveTrashItem.Icon.HorizontalAlignment = HorizontalAlignment.Left;
                ActiveTrashItem.Icon.VerticalAlignment = VerticalAlignment.Bottom;
                ActiveTrashItem.Icon.Margin = new Thickness(100, 0, 0, 400);
                ActiveTrashItem.X = 100;
                ActiveTrashItem.Y = 400;

                GameCanvas.Children.Add(ActiveTrashItem.Icon);
            }

            try
            {
                _handMovementSubject.UpdateMovementTriggers(_moveLeft, _moveRight, _moveUp, _moveDown);
                if (MainHand.IsHoldingTrash && ActiveTrashItem != null) // если рука держит мусор, и если объект мусора существует
                {
                    _trashMovementSubject.UpdateTrashMovement(false, true); // рука двигает мусор и не бросает его
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        private void WindowOnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right) // если отжата стрелка вправо
                _moveRight = false;

            if (e.Key == Key.Left) // если отжата стрелка влево
                _moveLeft = false;

            if (e.Key == Key.Up) // если отжата стрелка вверх
                _moveUp = false;

            if (e.Key == Key.Down) // если отжата стрелка вниз
                _moveDown = false;

            if (e.Key == Key.Enter && ActiveTrashItem != null) // обработка захвата мусора рукой
                _trashMovementSubject.UpdateTrashMovement(MainHand.IsHoldingTrash, false);
        }

        private void WindowOnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right) // если нажата стрелка вправо
                _moveRight = true;

            if (e.Key == Key.Left) // если нажата стрелка влево
                _moveLeft = true;

            if (e.Key == Key.Up) // если нажата стрелка вверх
                _moveUp = true;

            if (e.Key == Key.Down) // если нажата стрелка вниз
                _moveDown = true;
        }
    }
}
