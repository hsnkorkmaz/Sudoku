using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SudokuApi.Models
{
    public class Board
    {
        public string Name { get; set; }
        public bool IsSolved { get; set; }
        public int FilledCount { get; set; }
        public List<int> Numbers { get; set; } = new List<int>();
    }
}
