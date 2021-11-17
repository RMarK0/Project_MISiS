using System;
using System.Collections.Generic;
using System.Text;

namespace Project_MISiS.Model
{
    /// <summary>
    /// Класс, определяющий функционал и свойства объекта результата
    /// </summary>
    public class Result
    {
        public int Value { get; set; }
        public int CategoryId { get; set; }

        public Result(int categoryId)
        {
            CategoryId = categoryId;
            Value = 0;
        }

    }
}
