using System.Collections.Generic;
using Project_MISiS.Model;

namespace Project_MISiS.ViewModel
{
    public class TrashMovementSubject : ISubject
    {
        internal bool IsMovingTrash;
        internal bool IsThrowingTrash;
        internal GamePage GamePage;
        public TrashMovementSubject(GamePage page)
        {
            GamePage = page;
        }

        public void UpdateTrashMovement(bool isThrowingTrash, bool isMovingTrash)
        {
            IsMovingTrash = isMovingTrash;
            IsThrowingTrash = isThrowingTrash;
            Notify();
        }

        // Ниже идет часть кода, отвечающая за реализацию паттерна Observer
        private readonly List<IObserver> _observers = new List<IObserver>();

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (IObserver observer in _observers)
            {
                observer.Update(this);
            }
        }
    }
    public class HandMovementSubject : ISubject
    {
        public readonly Hand ActiveHand;
        public GamePage ActivePage;

        public bool MoveLeftTrigger;
        public bool MoveRightTrigger;
        public bool MoveUpTrigger;
        public bool MoveDownTrigger;

        public HandMovementSubject(GamePage page)
        {
            ActivePage = page;
            ActiveHand = ActivePage.MainHand;
        }

        public void UpdateMovementTriggers(bool moveLeft, bool moveRight, bool moveUp, bool moveDown)
        {
            MoveDownTrigger = moveDown;
            MoveLeftTrigger = moveLeft;
            MoveRightTrigger = moveRight;
            MoveUpTrigger = moveUp;

            Notify();
        }

        // Ниже идет часть кода, отвечающая за реализацию паттерна Observer
        private readonly List<IObserver> _observers = new List<IObserver>();

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (IObserver observer in _observers)
            {
                observer.Update(this);
            }
        }
    }
}
