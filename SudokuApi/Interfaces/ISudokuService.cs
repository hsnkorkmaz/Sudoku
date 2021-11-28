using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SudokuApi.Models;

namespace SudokuApi.Interfaces
{
    public interface ISudokuService
    {
        Board SolveGivenBoard(Board board);
        bool IsPossible(List<int> numbers, int index, int possibleNumber);
    }
}
