using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test22
{
    internal class solvableOrNot
    {
        //this function determine Solvability
        public static bool solvable(List<int> puz, int[,] puzzle, int N) //O(N POWER 2)
        {
            int space = 0;
            int inversions = 0;


            if (N % 2 == 0)
            {
                for (int r1 = 0; r1 < N; r1++)  //O(N)
                {
                    for (int c1 = 0; c1 < N; c1++)  //O(N)
                    {
                        if (puzzle[r1, c1] == 0)
                        {
                            space = N - r1;
                            break;

                        }
                    }
                    if (space != 0)
                    {

                        break;
                    }
                }

                for (int r = 0; r < N * N; r++)   //O(N POWER 2)
                {
                    if (r == (N * N) - 1)
                        break;

                    for (int c = r + 1; c < N * N; c++)  //O(N POWER 2)
                    {
                        if (puz[r] == 0)
                        {
                            break;
                        }
                        if (puz[r] > puz[c] && puz[c] != 0)
                        {
                            inversions++;
                            continue;

                        }

                    }
                }
                if (inversions % 2 != 0 && space % 2 == 0 || inversions % 2 == 0 && space % 2 != 0)
                {
                    return true;

                }
            }
            else
            {
                for (int r = 0; r < N * N; r++)   //O(N POWER 2)
                    for (int c = r + 1; c < N * N; c++)   //O(N POWER 2)
                    {
                        if (r == (N * N) - 1)
                        { break; }
                        if (puz[r] == 0)
                        {
                            break;
                        }
                        if (puz[r] > puz[c] && puz[c] != 0)
                        {
                            inversions++;
                            continue;
                        }
                    }

                if (inversions % 2 == 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
