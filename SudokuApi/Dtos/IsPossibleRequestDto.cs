using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SudokuApi.Dtos
{
    public class IsPossibleRequestDto
    {
        public List<int> Numbers { get; set; }
        public int Index { get; set; }
        public int Value { get; set; }
    }
}
