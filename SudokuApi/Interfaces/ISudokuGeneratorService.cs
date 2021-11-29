using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SudokuApi.Models;

namespace SudokuApi.Services
{
    public interface ISudokuGeneratorService
    {
        Task<Board> GenerateRandomBoard(int difficulty, CancellationToken cancellationToken);
        List<int> MakeRandomEmpty(List<int> numbers, int count);
    }
}
