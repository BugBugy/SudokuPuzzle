using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuTest
{
    class SudokuPuzzleValidator
    {
        static void Main(string[] args)
        {
            int[][] goodSudoku1 = {
                new int[] {7,8,4,  1,5,9,  3,2,6},
                new int[] {5,3,9,  6,7,2,  8,4,1},
                new int[] {6,1,2,  4,3,8,  7,5,9},

                new int[] {9,2,8,  7,1,5,  4,6,3},
                new int[] {3,5,7,  8,4,6,  1,9,2},
                new int[] {4,6,1,  9,2,3,  5,8,7},

                new int[] {8,7,6,  3,9,4,  2,1,5},
                new int[] {2,4,3,  5,6,1,  9,7,8},
                new int[] {1,9,5,  2,8,7,  6,3,4}
            };


            int[][] goodSudoku2 = {
                new int[] {1,4, 2,3},
                new int[] {3,2, 4,1},

                new int[] {4,1, 3,2},
                new int[] {2,3, 1,4}
            };

            int[][] badSudoku1 =  {
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},

                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},

                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9}
            };

            int[][] badSudoku2 = {
                new int[] {1,2,3,4,5},
                new int[] {1,2,3,4},
                new int[] {1,2,3,4},
                new int[] {1}
            };


            Console.WriteLine(ValidateSudoku(goodSudoku1));
            Console.WriteLine(ValidateSudoku(goodSudoku2));
            Console.WriteLine(ValidateSudoku(badSudoku1));
            Console.WriteLine(ValidateSudoku(badSudoku2));

            Debug.Assert(ValidateSudoku(goodSudoku1), "This is supposed to validate! It's a good sudoku!");
            Debug.Assert(ValidateSudoku(goodSudoku2), "This is supposed to validate! It's a good sudoku!");
            Debug.Assert(!ValidateSudoku(badSudoku1), "This isn't supposed to validate! It's a bad sudoku!");
            Debug.Assert(!ValidateSudoku(badSudoku2), "This isn't supposed to validate! It's a bad sudoku!");
        }

        static bool ValidateSudoku(int[][] puzzle)
        {
            int N = puzzle.Length;
            int maxSum = 0;
            for (int i = N; i > 0; i--)
                maxSum += i;
            for (int i = 0; i < N; i++) {
                if (puzzle[i].Length == N) // NxN Validation
                {
                    // check for sum (this check is not necessary but can invalidate most of the wrong sudoko's faster)
                    if (puzzle[i].Sum() != maxSum || puzzle[i].Distinct().Count() != N || !puzzle[i].All(x => x > 0 && x <N+1))
                    {
                        return false;
                    }
                }
                else
                    return false;
                int[] tempCol = new int[N];
                for (int j = 0; j < N; j++)
                {
                    tempCol[j] = puzzle[j][i];
                }

                if (tempCol.Sum() != maxSum || tempCol.Distinct().Count() != N || !tempCol.All(x => x > 0 && x < N+1))
                    return false;

            }

            //Validate inner box's
            int size = 0;
            // calculate inner box/sub grid size
            for (int i = 2; i < N; i++)
            {
                if (N % i == 0)
                {
                    size = N / i;
                }
            }
            for (int row = 0; row < N; row++)
            {
                for (int i = 0; i < N; i += size)
                {
                    var test = puzzle[row].Skip(i).Take(size).ToArray();
                    if (test.Distinct().Count() != size)
                        return false;
                }
                List<int> ints = new List<int>();
                for (int i = 0; i < N; i++)
                {
                    ints.Add(puzzle[i][row]);
                    if (ints.Count == size)
                    {
                        if (ints.Distinct().Count() != size)
                        {
                            return false;
                        }
                        else
                            ints.Clear();
                    }
                }
            }
            return true;
        }
    }
}