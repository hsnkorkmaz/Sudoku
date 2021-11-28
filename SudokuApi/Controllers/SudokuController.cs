﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SudokuApi.Dtos;
using SudokuApi.Interfaces;
using SudokuApi.Models;
using SudokuApi.Services;

namespace SudokuApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SudokuController : ControllerBase
    {
        private readonly ISudokuService _sudokuService;
        private readonly ISudokuGeneratorService _sudokuGeneratorService;

        public SudokuController(ISudokuService sudokuService, ISudokuGeneratorService sudokuGeneratorService)
        {
            _sudokuService = sudokuService;
            _sudokuGeneratorService = sudokuGeneratorService;
        }

        [HttpGet("GenerateBoard")]
        public async Task<ActionResult> GenerateBoard(int difficulty)
        {
            var cancellation = new CancellationTokenSource();
            
            var sudokuTask = _sudokuGeneratorService.GenerateRandomBoard(difficulty, cancellation.Token);
            
            if (!sudokuTask.Wait(1000))
            {
                cancellation.Cancel();

                var newBoard = new Board
                {
                    Name = "Hardest Sudoku",
                    //worlds hardest sudoku
                    Numbers = new List<int>()
                    {
                        8,0,0,0,0,0,0,0,0,
                        0,0,3,6,0,0,0,0,0,
                        0,7,0,0,9,0,2,0,0,
                        0,5,0,0,0,7,0,0,0,
                        0,0,0,0,4,5,7,0,0,
                        0,0,0,1,0,0,0,3,0,
                        0,0,1,0,0,0,0,6,8,
                        0,0,8,5,0,0,0,1,0,
                        0,9,0,0,0,0,4,0,0
                    }
                };

                return Ok(newBoard);
            };

            return Ok(sudokuTask.Result);
        }

        [HttpPost("SolveBoard")]
        public async Task<ActionResult> SolveBoard(Board board)
        {
            var solvedBoard = await Task.Run(() => _sudokuService.SolveGivenBoard(board));

            return Ok(solvedBoard);
        }

        [HttpPost("ValidateBoard")]
        public async Task<ActionResult> ValidateBoard(Board board)
        {
            var response = new ValidateBoardResponseDto() { IsValid = true };

            for (int i = 0; i < board.Numbers.Count; i++)
            {
                var tempNumbers = new List<int>();
                tempNumbers.AddRange(board.Numbers);
                tempNumbers[i] = 0;
                if (!_sudokuService.IsPossible(tempNumbers,i,board.Numbers[i]))
                {
                    response.IsValid = false;
                    break;
                }
            }
          
            return Ok(response);
        }

        [HttpPost("IsPossible")]
        public async Task<ActionResult> CheckIsPossible(IsPossibleRequestDto request)
        {
            bool result = await Task.Run(() => _sudokuService.IsPossible(request.Numbers, request.Index, request.Value));

            var response = new IsPossibleResponseDto()
            {
                Index = request.Index, 
                Value = request.Value, 
                IsPossible = result
            };

            return Ok(response);
        }
    }
}
