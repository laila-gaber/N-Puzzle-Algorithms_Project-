using DotNetty.Common.Utilities;
using Lucene.Net.Util;
using NetTopologySuite.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Test22;

namespace Test22
{
    internal class Node
    {
        public static int N;
        public int[,] puzz;
        public int size = N;
        public int level = 0;
        public int H, M;
        public string hashp;
        public Node parent;
        public static Queue<Node> Final_queue = new Queue<Node>();
        public int Total_F;
        public int Total_F_manhaten;

        //this function detects which is the next node to generate children from
        public Node Next_puzzle(int choice)
        {
            Node smallest_puzzle = new Node(); 
            while (Program.p.Count > 0)   //O(Mlog(N))
            {

                smallest_puzzle = Program.p.Dequeue(); //O(1)
                Program.puzzle_list.Add(smallest_puzzle.hashp);    //O(1)
                if (choice == 1)
                {
                    //Base Case
                    if (smallest_puzzle.H == 0)  //O(1)
                    {
                        return smallest_puzzle;
                    }
                    else
                    {
                            generate_child(smallest_puzzle,choice);//O(N POWER 2)

                    }
                }
                else if(choice==21)
                {
                    //Base Case
                    if (smallest_puzzle.M == 0)  //O(1)
                    {
                        return smallest_puzzle;
                    }
                    else
                    {
                        generate_child(smallest_puzzle, choice);//O(N POWER 2)

                    }
                }
                
            }
                return smallest_puzzle;
        }

        //this function generate children from the smallest heuristc value node
        public void generate_child(Node smallest_puzzle,int choice)   //O(N POWER 2)
        {
            int row = blank_postion(smallest_puzzle)[0];   //O(N POWER 2)
            int column = blank_postion(smallest_puzzle)[1]; //O(N POWER 2)
            //left
            if (row >= 0 && column - 1 >= 0)
            {

                //adding child information
                Node left = new Node();
                left.puzz = (int[,])smallest_puzzle.puzz.Clone();
                left.level = smallest_puzzle.level;
                left.parent = smallest_puzzle;
                left.level++;

                //retrive the next swaped num after zero position in adjacent array 
                int[] adjacent = new int[2];
                adjacent[0] = row;
                adjacent[1] = column - 1;
                int newData;
                int[,] puzzleCopy;
                puzzleCopy = (int[,])left.puzz.Clone();

                //holding the next swaped num in newData
                newData = puzzleCopy[adjacent[0], adjacent[1]];
                puzzleCopy[adjacent[0], adjacent[1]] = puzzleCopy[row, column];
                puzzleCopy[row, column] = newData;
                left.puzz = puzzleCopy;
                
               //creates the puzzle string 
                left.hashp = puzzle_string(left);   //O(N POWER 2)
                if (choice == 1) 
                {
                    //calculate the hamming for the puzzle
                    int hamm = Hamming(left);   //O(N POWER 2)
                    left.H = hamm;
                    left.Total_F = hamm + left.level;
                    if(!Program.puzzle_list.Contains(left.hashp))
                        Program.p.Enqueue(left, left.Total_F);  //LOG(N)
                }
                else if (choice == 21)
                {
                    //calculate the Manhattan for the puzzle
                    int man = ManHattan(left);    //O(N POWER 2)
                    left.M = man;
                    left.Total_F_manhaten = man + left.level;
                    if (!Program.puzzle_list.Contains(left.hashp))  //O(1)
                        Program.p.Enqueue(left, left.Total_F_manhaten);  //LOG(N)
                } 

            }

            
            //right
            if (row >= 0 && column + 1 != N)
            {
                //adding child information
                Node right = new Node();
                right.puzz = (int[,])smallest_puzzle.puzz.Clone(); ;
                right.level = smallest_puzzle.level;
                right.parent = smallest_puzzle; 
                right.level++;

                //retrive the next swaped num after zero position in adjacent array 
                int[] adjacent = new int[2];
                adjacent[0] = row;
                adjacent[1] = column + 1;

                //swap variabels
                int newData;
                int[,] puzzleCopy;

                //add the source puzzle in puzzlecopy
                puzzleCopy = (int[,])right.puzz.Clone();

                //holding the next swaped num in newData
                newData = puzzleCopy[adjacent[0], adjacent[1]];
                puzzleCopy[adjacent[0], adjacent[1]] = puzzleCopy[row, column];
                puzzleCopy[row, column] = newData;
                right.puzz = puzzleCopy;

                //creates the puzzle string  
                right.hashp = puzzle_string(right);  //O(N POWER 2)
                if (choice == 1)
                {
                    //calculate the hamming for the puzzle
                    int hamm = Hamming(right);      //O(N POWER 2)
                    right.H = hamm;
                    right.Total_F = hamm + right.level;
                    if (!Program.puzzle_list.Contains(right.hashp)) //O(1)
                        Program.p.Enqueue(right, right.Total_F);     //LOG(N)
                }
                else if (choice == 21)
                {
                    //calculate the Manhattan for the puzzle
                    int man = ManHattan(right);       //O(N POWER 2)
                    right.M = man;
                    right.Total_F_manhaten = man + right.level;
                    if (!Program.puzzle_list.Contains(right.hashp))  //O(1)
                        Program.p.Enqueue(right, right.Total_F_manhaten);  //LOG(N)
                }
       
               
            }
        
            //up
            if (row - 1 >= 0 && column >= 0)
            {
                //adding child information
                Node up = new Node();
                up.puzz = (int[,])smallest_puzzle.puzz.Clone(); ;
                up.level = smallest_puzzle.level;
                up.parent = smallest_puzzle;
                up.level++;

                //retrive the next swaped num after zero position in adjacent array 
                int[] adjacent = new int[2];
                adjacent[0] = row - 1;
                adjacent[1] = column;

                //swap variabels
                int newData;
                int[,] puzzleCopy;

                //add the source puzzle in puzzlecopy
                puzzleCopy = (int[,])up.puzz.Clone();

                //holding the next swaped num in newData
                newData = puzzleCopy[adjacent[0], adjacent[1]];
                puzzleCopy[adjacent[0], adjacent[1]] = puzzleCopy[row, column];
                puzzleCopy[row, column] = newData;
                up.puzz = puzzleCopy;

                //creates the puzzle string  
                up.hashp = puzzle_string(up);   //O(N POWER 2)
                if (choice == 1)
                {
                    //calculate the hamming for the puzzle
                    int hamm = Hamming(up);      //O(N POWER 2)
                    up.H = hamm;
                    up.Total_F = hamm + up.level;
                    if (!Program.puzzle_list.Contains(up.hashp))   //O(1)
                        Program.p.Enqueue(up, up.Total_F);     //LOG(N)
                }
                else if (choice == 21)
                {
                    //calculate the Manhattan for the puzzle
                    int man = ManHattan(up);      //O(N POWER 2)
                    up.M = man;
                    up.Total_F_manhaten = man + up.level;
                    if (!Program.puzzle_list.Contains(up.hashp))  //O(1)
                        Program.p.Enqueue(up, up.Total_F_manhaten);   //LOG(N)
                }
           

            }
            //down
            if (row + 1 != N && column >= 0)
            {
                //adding child information
                Node down = new Node();
                down.puzz = (int[,])smallest_puzzle.puzz.Clone(); ;
                down.level = smallest_puzzle.level;
                down.parent = smallest_puzzle;
                down.level++;

                //retrive the next swaped num after zero position in adjacent array 
                int[] adjacent = new int[2];
                adjacent[0] = row + 1; 
                adjacent[1] = column;
                //swap variables
                int newData;
                int[,] puzzleCopy;
                //add the source puzzle in puzzlecopy
                puzzleCopy = (int[,])down.puzz.Clone();

                //holding the next swaped num in newData
                newData = puzzleCopy[adjacent[0], adjacent[1]];
                puzzleCopy[adjacent[0], adjacent[1]] = puzzleCopy[row, column];
                puzzleCopy[row, column] = newData;
                down.puzz = puzzleCopy;

                //creating the puzzle string
                down.hashp = puzzle_string(down);    //O(N POWER 2)
                if (choice == 1)
                {
                    //calculate the hamming for the puzzle
                    int hamm = Hamming(down);          //O(N POWER 2)
                    down.H = hamm;
                    down.Total_F = hamm + down.level;
                    if (!Program.puzzle_list.Contains(down.hashp))   //O(1)
                        Program.p.Enqueue(down, down.Total_F);  //LOG(N)
                }
                else if (choice == 21)
                {
                    //calculate the Manhattan for the puzzle
                    int man = ManHattan(down);        //O(N POWER 2)
                    down.M = man;
                    down.Total_F_manhaten = man + down.level;
                    if (!Program.puzzle_list.Contains(down.hashp))   //O(1)
                        Program.p.Enqueue(down, down.Total_F_manhaten);  //LOG(N)
                }
             
            }
        }

        //this function creats the goal puzzle
        public static int[,] Goal_puzzle(Node intial) //O(N power 2)
        {
            int z = 0;  //O(1)
            int N = intial.size; //O(1)
            int[,] goal = new int[N+1, N+1];  //O(1)
            for (int i = 0; i < N; i++)  //O(N)
            {
                for (int j = 0; j < N; j++) //O(N)
                {
                    z++;             //O(1)
                    goal[i, j] = z;  //O(1)
                }
            }
            goal[N - 1, N - 1] = 0;  //O(1)
            return goal;        //O(1)
        }

        //this function calculates hamming distance
        public static int Hamming(Node intial) //O(N power 2)
        {

            int N = intial.size;         //O(1)
            int[,] goal = new int[N, N];  //O(1)
            goal = Goal_puzzle(intial);  //O(1)

            int h = 0;    //O(1)
            for (int i = 0; i < N; i++)  //O(N)
            {

                for (int j = 0; j < N; j++)  //O(N)
                {
                    if (intial.puzz[i, j] != goal[i, j]) //O(1)
                    {
                        if (intial.puzz[i, j] != 0) //O(1)
                        {
                            h++; //O(1)
                        }

                    }
                }
            }
            intial.H = h;     //O(1)
            return intial.H; //O(1)
        }

        //this function calculates Manhattan distance
        public static int ManHattan(Node intial) //O(N power 2)
        {

            int value = 0;   //O(1)
            int sum = 0;    //O(1)

            int targetRow, targetCol, dx, dy; //O(1)
            int N = intial.size;  //O(1)
            for (int i = 0; i < N; i++)  //O(N)
            {
                for (int j = 0; j < N; j++)  //O(N)
                {
                    value = intial.puzz[i, j]; //O(1)
                    if (value != 0)     //O(1)
                    {

                        targetRow = (value - 1) / N;  //O(1)
                        targetCol = (value - 1) % N;  //O(1)
                        dx = i - targetRow;   //O(1)
                        dy = j - targetCol;  //O(1)
                        sum += Math.Abs(dx) + Math.Abs(dy); //O(1)
                    }
                }
            }
            intial.M = sum;  //O(1)
            return intial.M; //O(1)
        } 

        //this function detects the balnk postion
        public static int[] blank_postion(Node intial)
        {
            int[] pos = new int[2];   //O(1)
            for (int i = 0; i < intial.size; i++)  //O(N)
            {
                for (int j = 0; j < intial.size; j++)  //O(N)
                {
                    if (intial.puzz[i, j] == 0)  //O(1)
                    {
                        pos[0] = i;   //O(1)
                        pos[1] = j;  //O(1)
                        break;
                    }
                }
            }
            return pos;  //O(1)
        }
        //this function converts the puzzle To string to add it in the HashSet
        public string puzzle_string(Node puzzle) //O(N power 2)
        {
            string sb;         //O(1)
            string total = ""; //O(1)
            int z = 0;        //O(1)
            for (var i = 0; i < puzzle.size; i++)  //O(N)
            {
                for (var j = 0; j < puzzle.size; j++) //O(N)
                {
                    sb = puzzle.puzz[i, j].ToString(); //O(N)
                    total += sb; //O(1)
                    z++;   //O(1)
                }
            }
            return total; //O(1)
        }
    }
}

