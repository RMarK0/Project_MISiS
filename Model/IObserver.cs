using System;
using System.Collections.Generic;
using System.Text;

namespace Project_MISiS.Model
{
    public interface IObserver
    {
        void Update(ISubject subject);
    }
}
