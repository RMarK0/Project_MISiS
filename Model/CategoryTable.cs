using System;
using System.Collections.Generic;
using System.Text;

namespace Project_MISiS.Model
{
    public static class CategoryTable
    {
        public static Dictionary<int, string> CategoryDictionary = new Dictionary<int, string>()
        {
            {1, "glass"},
            {2, "plastic"},
            {3, "paper"},
            {4, "metal"},
            {5, "other"},
            {6, "bio"}
        };
    }
}
