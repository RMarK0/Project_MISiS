using System;
using System.Collections.Generic;
using System.Text;
using Project_MISiS.Model;

namespace Project_MISiS.ViewModel
{
    class TrashBinObserver : IObserver
    {
        public void Update(ISubject subject)
        {
            // empty
        }
        public void Update(ISubject subject, TrashBin binObject)
        {
            if (binObject.CategoryId == ((TrashBinSubject)subject).ActivePage.ActiveTrashItem.CategoryId)
            {
                foreach (Result result in ((TrashBinSubject)subject).ActivePage.Results)
                {
                    if (result.CategoryId == binObject.CategoryId)
                    {
                        result.Value++;
                        break;
                    }
                }
            }
            else
            {
                /* if not correct category */
            }
        }
    }
}
