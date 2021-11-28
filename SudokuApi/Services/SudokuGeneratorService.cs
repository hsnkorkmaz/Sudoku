using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SudokuApi.Interfaces;
using SudokuApi.Models;

namespace SudokuApi.Services
{
    public class SudokuGeneratorService : ISudokuGeneratorService
    {
        private readonly ISudokuService _sudokuService;

        private readonly Random _randomCreator = new Random();
        private readonly int _gridSize = 9;
        private readonly int _emptyCell = 0;
        private readonly int _randomNumberCount = 10;
        private Board _randomBoard = new Board() { Name = "Random Board" };
        private Board _solvedBoard;

        public SudokuGeneratorService(ISudokuService sudokuService)
        {
            _sudokuService = sudokuService;
        }

        public Task<Board> GenerateRandomBoard(int difficulty, CancellationToken cancellationToken)
        {
            return Task.Run(() => {

                _randomBoard.Numbers = FillEmpty();

                for (int i = 0; i < _randomNumberCount; i++)
                {
                    SetRandomNumber();
                }

                _solvedBoard.Numbers = MakeRandomEmpty(_solvedBoard.Numbers, difficulty);

                return _solvedBoard;

            }, cancellationToken);
            
        }
        
        public List<int> MakeRandomEmpty(List<int> numbers, int count)
        {
            var randomIndexes = Enumerable.Range(0, numbers.Count).OrderBy(x => _randomCreator.Next()).Take(count).ToList();
            for (int i = 0; i < randomIndexes.Count; i++)
            {
                numbers[randomIndexes[i]] = _emptyCell;
            }

            return numbers;
        }
        
        public List<int> FillEmpty()
        {
            var tempNumbers = new List<int>();
            for (int i = 0; i < _gridSize * _gridSize; i++)
            {
                tempNumbers.Add(_emptyCell);
            }

            return tempNumbers;
        }
        
        private bool SetRandomNumber()
        {
            var possibleValues = IsRandomPossible();
            _randomBoard.Numbers[possibleValues[0]] = possibleValues[1];
            
            var isSolvedBoard = _sudokuService.SolveGivenBoard(_randomBoard);
            if (!isSolvedBoard.IsSolved)
            {
                _randomBoard.Numbers[possibleValues[0]] = _emptyCell;
                if (SetRandomNumber())
                {
                    return true;
                }

                return false;
            }

            _solvedBoard = isSolvedBoard;
            return true;
        }


        private List<int> IsRandomPossible()
        {
            var possibleValues = new List<int>();
            possibleValues.Add(GenerateRandomIndex());
            possibleValues.Add(GenerateRandomNumber());


            bool isPossible = _sudokuService.IsPossible(_randomBoard.Numbers, possibleValues[0], possibleValues[1]);
            if (isPossible)
            {
                return possibleValues;
            }

            return IsRandomPossible();
        }

        private int GenerateRandomIndex()
        {
            var emptyIndexes = new List<int>();
            for (int i = 0; i < _randomBoard.Numbers.Count; i++)
            {
                if (_randomBoard.Numbers[i] == _emptyCell)
                {
                    emptyIndexes.Add(i);
                }
            }

            int emptyIndex = _randomCreator.Next(emptyIndexes.Count - 1);

            return emptyIndexes[emptyIndex];
        }

        private int GenerateRandomNumber()
        {
            return _randomCreator.Next(1, 9);
        }
    }
}
