using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.VisualBasic;
using SudokuApi.Interfaces;
using SudokuApi.Models;

namespace SudokuApi.Services
{
    public class SudokuService : ISudokuService
    {
        private readonly int _gridSize = 9;
        private readonly int _boxSize = 3;
        private readonly int _emptyCell = 0;

        private Board _boardToSolve;

        public Board SolveGivenBoard(Board board)
        {
            if (board.Numbers.Count != _gridSize * _gridSize)
            {
                return board;
            }

            _boardToSolve = new Board();
            _boardToSolve.Name = board.Name;
            _boardToSolve.Numbers.AddRange(board.Numbers);

            var isSolved = Solve();

            if (!isSolved) return board;

            _boardToSolve.IsSolved = true;
            return _boardToSolve;
        }
        

        private bool Solve()
        {
            for (int i = 0; i < _boardToSolve.Numbers.Count; i++)
            {
                if (_boardToSolve.Numbers[i] != _emptyCell) continue;

                return TryDifferentNumbers(i);
            }

            return true;
        }


        private bool TryDifferentNumbers(int index)
        {
            for (int j = 1; j <= _gridSize; j++)
            {
                if (!IsPossible(_boardToSolve.Numbers, index, j)) continue;
                _boardToSolve.Numbers[index] = j;

                if (Solve())
                {
                    return true;
                }

                _boardToSolve.Numbers[index] = _emptyCell;
            }

            return false;
        }

        public bool IsPossible(List<int> numbers, int index, int possibleNumber)
        {
            return CheckRow(numbers, index, possibleNumber) &&
                   CheckColumn(numbers, index, possibleNumber) &&
                   CheckBox(numbers, index, possibleNumber);
        }

        private bool CheckColumn(List<int> numbers, int index, int possibleNumber)
        {
            var colIndex = index % _gridSize;

            for (int i = 0; i < _gridSize; i++)
            {
                int numberIndex = colIndex + (i * _gridSize);
                if (numbers[numberIndex] == possibleNumber)
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckRow(List<int> numbers, int index, int possibleNumber)
        {
            int rowIndex = index / _gridSize;

            for (int i = 0; i < _gridSize; i++)
            {
                int numberIndex = rowIndex * _gridSize + i;
                if (numbers[numberIndex] == possibleNumber)
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckBox(List<int> numbers, int index, int possibleNumber)
        {
            int localBoxRowIndex = index / _gridSize / _boxSize * _boxSize;
            int localBoxColIndex = index % _gridSize / _boxSize * _boxSize;
            int localBoxStartIndex = (localBoxRowIndex * _gridSize) + localBoxColIndex;

            for (int i = 0; i < _boxSize; i++)
            {
                //first row
                if (numbers[localBoxStartIndex + i] == possibleNumber)
                {
                    return false;
                }
                //second row
                if (numbers[localBoxStartIndex + i + _gridSize] == possibleNumber)
                {
                    return false;
                }
                //third row
                if (numbers[localBoxStartIndex + i + _gridSize * 2] == possibleNumber)
                {
                    return false;
                }
            }
            return true;
        }
    }
}


