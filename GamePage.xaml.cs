using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private readonly DispatcherTimer _gameTickTimer = new DispatcherTimer();

        public Hand MainHand;
        public TrashItem ActiveTrashItem;
        public TrashBin[] TrashBins = new TrashBin[6]; // создаем массив мусорок
        public Result[] Results = new Result[6];

        private readonly TrashMovementSubject _trashMovementSubject;
        private readonly TrashMovementObserver _trashMovementObserver = new TrashMovementObserver();

        private readonly HandMovementSubject _handMovementSubject;
        private readonly HandMovementObserver _handMovementObserver = new HandMovementObserver();

        public readonly TrashBinSubject TrashBinSubject;
        private readonly TrashBinObserver _trashBinObserver = new TrashBinObserver();

        public GamePage()
        {
            InitializeComponent();
            Startup.Start(this);

            _trashMovementSubject = new TrashMovementSubject(this);
            _handMovementSubject = new HandMovementSubject(this);
            TrashBinSubject = new TrashBinSubject(this);

            _trashMovementSubject.Attach(_trashMovementObserver);
            _handMovementSubject.Attach(_handMovementObserver);
            TrashBinSubject.Attach(_trashBinObserver);
        }

        private void GamePage_OnLoaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            if (window != null)
            {
                window.KeyDown += WindowOnKeyDown;
                window.KeyUp += WindowOnKeyUp;
            }

            _gameTickTimer.Tick += GameTickTimer_Tick;
            _gameTickTimer.Interval = TimeSpan.FromMilliseconds(5);
            _gameTickTimer.IsEnabled = true;
        }

        // эти 4 переменные отвечают за движение по осям X и Y
        private bool _moveRight;
        private bool _moveLeft;
        private bool _moveUp;
        private bool _moveDown;

        private void GameTickTimer_Tick(object sender, EventArgs e)
        {
            _handMovementSubject.UpdateMovementTriggers(_moveLeft, _moveRight, _moveUp, _moveDown);

            HandDebugTextBlock.Text = $"Hand: {MainHand.X} {MainHand.Y} {MainHand.HandImage.Margin}";
            if (ActiveTrashItem != null) 
                TrashDebugTextBlock.Text = $"Trash: {ActiveTrashItem.X} {ActiveTrashItem.Y} {ActiveTrashItem.Icon.Margin}";

            if (ActiveTrashItem == null)
            {
                ActiveTrashItem = TrashFactory.CreateTrashItem();
                ActiveTrashItem.Icon.HorizontalAlignment = HorizontalAlignment.Left;
                ActiveTrashItem.Icon.VerticalAlignment = VerticalAlignment.Bottom;
                ActiveTrashItem.Icon.Margin = new Thickness(100, 0, 0, 0);
                ActiveTrashItem.X = 200;
                ActiveTrashItem.Y = 600;

                GameCanvas.Children.Add(ActiveTrashItem.Icon);
            }

            if (MainHand.IsHoldingTrash && ActiveTrashItem != null) // если рука держит мусор, и если объект мусора существует
                _trashMovementSubject.UpdateTrashMovement(false, true); // рука двигает мусор и не бросает его
            string temp = "";
            foreach (Result res in Results)
            {
                temp += res.Value;
                temp += " ";
            }

            ResultsTestBlock.Text = temp;
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
    }
}