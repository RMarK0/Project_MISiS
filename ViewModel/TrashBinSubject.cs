using System;
using System.Collections.Generic;
using System.Text;
using Project_MISiS.Model;

namespace Project_MISiS.ViewModel
{
    public class TrashBinSubject : ISubject
    {
        public GamePage ActivePage;

        public TrashBinSubject(GamePage page)
        {
            ActivePage = page;
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

        public void Notify(TrashBin binObject)
        {
            foreach (IObserver observer in _observers)
            {
                ((TrashBinObserver)observer).Update(this, binObject);
            }
        }
        public void Notify()
        {
            
        }
    }
}
