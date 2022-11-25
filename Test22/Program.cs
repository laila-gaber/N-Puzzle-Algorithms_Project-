using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

//using Test22;

namespace Test22
{
    class Program
    {
        public static PriorityQueue<Node, int> p = new PriorityQueue<Node, int>();
        public static HashSet<string> puzzle_list = new HashSet<string>();
        static void Main(string[] args)
        {
            Node obj = new Node();

            FileStream file;
            int cases;
            StreamReader sr;
            string line;
            int wrongAnswers;
            TextReader origConsole = Console.In;
            Console.WriteLine("N Puzzle:\n[1] Sample test cases\n[2] Complete testing\n");
            Console.Write("\nEnter your choice [1-2]: ");
            char choice = Console.ReadLine()[0];

            switch (choice)
            {
                case '1':
                    #region SAMPLE CASES
                    file = new FileStream("C://Users//Lenovo//Desktop//Test22//Test22//sample.txt", FileMode.Open, FileAccess.Read);
                    sr = new StreamReader(file);
                    wrongAnswers = 0;
                    for (int i = 0; i < 11; i++)  //O(1)
                    {

                        line = sr.ReadLine();
                        int N = int.Parse(line);
                        Node.N = N;
                        int[,] puzzle = new int[N, N];
                        List<int> puz = new List<int>();

                        for (int r = 0; r < N; r++)  //O(N)
                        {
                            string[] lineNumbers = sr.ReadLine().Split(' ');
                            for (int c = 0; c < N; c++)  //O(N)
                            {
                                puz.Add(Convert.ToInt32(lineNumbers[c]));
                                puzzle[r, c] = Convert.ToInt32(lineNumbers[c]);

                            }
                        }
                        //Start the Time
                        Stopwatch stopwatch = new Stopwatch();
                       stopwatch.Start();
                        
                      
                        if (solvableOrNot.solvable(puz, puzzle, N) == true)  //O(N POWER 2)
                        {
                            obj.puzz = puzzle;
                            obj.size = N;
                            obj.parent = null;
                            int h = Node.Hamming(obj); //O(N POWER 2)
                            obj.Total_F = h + 0;
                            obj.hashp = obj.puzzle_string(obj); //O(N POWER 2)
                            p.Enqueue(obj,obj.Total_F); //O(LOG(N))

                            Node last_step = new Node();
                            Node Nodes_steps = new Node();
                            last_step = obj.Next_puzzle(1); //O(NLOG(M))
      
                            int L = last_step.level;
                            // Stop the Time
                           stopwatch.Stop();
                            int level = last_step.level;
                            Stack<int[,]> res = new Stack<int[,]>();
                            while(last_step.parent != null)//O(M)
                            {
                                res.Push(last_step.puzz);
                                last_step = last_step.parent;
                            }
                            res.Push(last_step.puzz);

                            while (res.Count > 0) //O(M)
                            {
                                for(int k = 0; k <N; k++) //O(N)
                                {
                                    for(int j = 0; j <N; j++) //O(N)
                                    {
                                       
                                        Console.Write(res.Peek()[k,j] + " ");
                                    }
                                    Console.WriteLine();
                                }
                                Console.WriteLine();
                                res.Pop();
                            }
                            Console.WriteLine("steps:");
                            Console.WriteLine(level);
                            Console.WriteLine(stopwatch.Elapsed);
                            Console.WriteLine();
                            // Show the time
                        }
                        else
                        {
                            Console.WriteLine("UnSolvable");
                        }

                        Stopwatch sw = Stopwatch.StartNew();
                        
                        puzzle_list.Clear();

                        sw.Stop();
                        p.Clear();

                    }
                    sr.Close();
                    file.Close();
                    break;
                #endregion
                case '2':
                    #region COMPLETE CASES
                    CompleteTest:
                    Console.WriteLine("\nComplete Testing is running now...");
                    Console.WriteLine("N Puzzle:\n[1] Manhattan Only\n[2] Manhattan & Hamming\n[3] UnSolvable & Vlarg\n");
                    Console.Write("\nEnter your choice [1-2-3]: ");
                    char choiceComplete = Console.ReadLine()[0];
                    switch (choiceComplete)
                    {
                        case '1':
                            #region Manhattan
                            file = new FileStream("C://Users//Lenovo//Desktop//Test22//Test22//Manhattan.txt", FileMode.Open, FileAccess.Read);
                            sr = new StreamReader(file);
                            wrongAnswers = 0;
                            for (int i = 0; i < 4; i++)//O(1)
                            {
                                line = sr.ReadLine();
                                int N = int.Parse(line);
                                Node.N = N;
                                int[,] puzzle = new int[N, N];
                                List<int> puz = new List<int>();

                                for (int r = 0; r < N; r++)
                                {
                                    string[] lineNumbers = sr.ReadLine().Split(' ');
                                    for (int c = 0; c < N; c++)
                                    {
                                        puz.Add(Convert.ToInt32(lineNumbers[c]));
                                        puzzle[r, c] = Convert.ToInt32(lineNumbers[c]);
                                    }
                                }

                                //Start the Time
                                Stopwatch stopwatch1 = new Stopwatch();
                                stopwatch1.Start();


                                if (solvableOrNot.solvable(puz, puzzle, N) == true)//O(N POWER 2)
                                {
                                    obj.puzz = puzzle;
                                    
                                    obj.size = N;
                                    obj.parent = null;
                                    int m = Node.ManHattan(obj); //O(N POWER 2)
                                    obj.Total_F_manhaten = m + 0;

                                    obj.hashp = obj.puzzle_string(obj); //O(N POWER 2)
                                    p.Enqueue(obj, obj.Total_F_manhaten); //O(LOG(N))

                                    Node last_step = new Node();
                                    Node Nodes_steps = new Node();
                                    last_step = obj.Next_puzzle(21); //O(M LOG(N))

                                    int L = last_step.level;
                                    // Stop the Time
                                    stopwatch1.Stop();
                                    int level = last_step.level;
                                    Stack<int[,]> res = new Stack<int[,]>();
                                    while (last_step.parent != null) //O(M)
                                    {
                                        res.Push(last_step.puzz);
                                        last_step = last_step.parent;
                                    }
                                    res.Push(last_step.puzz);

                                    while (res.Count > 0) //O(M)
                                    {
                                        for (int k = 0; k < N; k++) //O(N)
                                        {
                                            for (int j = 0; j < N; j++) //O(N)
                                            {

                                                Console.Write(res.Peek()[k, j] + " ");
                                            }
                                            Console.WriteLine();
                                        }
                                        Console.WriteLine();
                                        res.Pop();
                                    }
                                    Console.WriteLine("steps:");
                                    Console.WriteLine(level);
                                    Console.WriteLine(stopwatch1.Elapsed);
                                    Console.WriteLine();
                                    // Show the time
                                }
                                else
                                {
                                    Console.WriteLine("UnSolvable");
                                }

                                Stopwatch sw = Stopwatch.StartNew();

                                puzzle_list.Clear();

                                sw.Stop();
                                p.Clear();
                            }

                                sr.Close();
                            file.Close();

                            break;
                        #endregion
                        case '2':
                            #region Manhattan & Hamming

                            file = new FileStream("C://Users//Lenovo//Desktop//Test22//Test22//Manhattan&Hamming.txt", FileMode.Open, FileAccess.Read);

                            sr = new StreamReader(file);

                            wrongAnswers = 0;
                            for (int i = 0; i < 4; i++) //O(1)
                            {
                                line = sr.ReadLine();
                                int N = int.Parse(line);
                                Node.N = N;
                                int[,] puzzle = new int[N, N];
                                List<int> puz = new List<int>();

                                for (int r = 0; r < N; r++)
                                {
                                    string[] lineNumbers = sr.ReadLine().Split(' ');
                                    for (int c = 0; c < N; c++)
                                    {
                                        puz.Add(Convert.ToInt32(lineNumbers[c]));
                                        puzzle[r, c] = Convert.ToInt32(lineNumbers[c]);
                                    }
                                }
  
                                Stopwatch sw = new Stopwatch();
                                if (solvableOrNot.solvable(puz, puzzle, N) == true) //O(N POWER 2)
                                { 
                                    
                                    
                                    void display(int choice)//O(N POWER 2)
                                    {
                                        puzzle_list.Clear();
                                        p.Clear();
                                        sw.Start();
                                        Node last_step = new Node();
                                        Node Nodes_steps = new Node();
                                        if (choice==1)
                                        {
                                            Console.WriteLine("hamming");
                                            Console.WriteLine();
                                            obj.puzz = puzzle;
                                            obj.size = N;
                                            obj.parent = null;
                                            int h = Node.Hamming(obj);//O(N POWER 2)
                                            obj.Total_F = h + 0;

                                            obj.hashp = obj.puzzle_string(obj);//O(N POWER 2)
                                            p.Enqueue(obj, obj.Total_F); //O(LOG(N))

                                            //Program.Hashset.Add(obj.puzz.ToString());

                                            last_step = obj.Next_puzzle(1); //O(M LOG(N))
                                            int L = last_step.level;
                                        }
                                        else if(choice==2)
                                        {
                                            Console.WriteLine("manhatten");
                                            Console.WriteLine();
                                            obj.puzz = puzzle;

                                            obj.size = N;
                                            obj.parent = null;
                                            int m = Node.ManHattan(obj); //O(N POWER 2)
                                            obj.Total_F_manhaten = m + 0;

                                            obj.hashp = obj.puzzle_string(obj); //O(N POWER 2)
                                            p.Enqueue(obj, obj.Total_F_manhaten); //O(LOG(N))

                                            //Program.Hashset.Add(obj.puzz.ToString());

                                            last_step = obj.Next_puzzle(21); //O(M LOG(N))

                                            int L = last_step.level;
                                        }
                                        sw.Stop();
                                        int level = last_step.level;
                                        Stack<int[,]> res = new Stack<int[,]>();
                                        while (last_step.parent != null) //O(M)
                                        {
                                            res.Push(last_step.puzz);
                                            last_step = last_step.parent;
                                        }
                                        res.Push(last_step.puzz);

                                        /*while (res.Count > 0)
                                        {
                                            for (int k = 0; k < N; k++)
                                            {
                                                for (int j = 0; j < N; j++)
                                                {

                                                    Console.Write(res.Peek()[k, j] + " ");
                                                }
                                                Console.WriteLine();
                                            }
                                            Console.WriteLine();
                                            res.Pop();
                                        }*/
                                        Console.WriteLine("steps:");
                                        Console.WriteLine(level);
                                        Console.WriteLine(sw.Elapsed);
                                        Console.WriteLine();
                                        // Show the time

                                    }

                                    display(1); //O(N POWER 2)
                                    display(2); //O(N POWER 2)

                                }
                                else
                                {
                                    Console.WriteLine("UnSolvable");
                                }
                                
                                puzzle_list.Clear();
                                sw.Stop();
                                p.Clear();
                            }

                            sr.Close();
                            file.Close();

                            break;
                        #endregion
                        case '3':
                            #region UnSolvable


                            file = new FileStream("C://Users//Lenovo//Desktop//Test22//Test22//complete.txt", FileMode.Open, FileAccess.Read);

                            sr = new StreamReader(file);
                            wrongAnswers = 0;
                            for (int i = 0; i < 5; i++)
                            {
                                line = sr.ReadLine();
                                int N = int.Parse(line);
                                Node.N = N;
                                int[,] puzzle = new int[N, N];
                                List<int> puz = new List<int>();

                                for (int r = 0; r < N; r++)//O(N)
                                {
                                    string[] lineNumbers = sr.ReadLine().Split(' ');
                                    for (int c = 0; c < N; c++) //O(N)
                                    {
                                        puz.Add(Convert.ToInt32(lineNumbers[c]));
                                        puzzle[r, c] = Convert.ToInt32(lineNumbers[c]);
                                    }
                                }
                                Stopwatch sw = new Stopwatch();
                                if (solvableOrNot.solvable(puz, puzzle, N) == true)
                                {
                                    void display(int choice)//O(N POWER 2)
                                    {
                                        puzzle_list.Clear();
                                        p.Clear();
                                        sw.Start();
                                        Node last_step = new Node();
                                        Node Nodes_steps = new Node();
                                        if (choice == 2)
                                        {
                                            Console.WriteLine("Vlarge");
                                            Console.WriteLine();
                                            obj.puzz = puzzle;

                                            obj.size = N;
                                            obj.parent = null;
                                            int m = Node.ManHattan(obj);//O(N POWER 2)
                                            obj.Total_F_manhaten = m + 0;

                                            obj.hashp = obj.puzzle_string(obj);//O(N POWER 2)
                                            p.Enqueue(obj, obj.Total_F_manhaten); //O(LOG(N))
                                            last_step = obj.Next_puzzle(21);//O(M LOG(N))

                                            int L = last_step.level;
                                        }

                                        sw.Stop();
                                        int level = last_step.level;
                                        Stack<int[,]> res = new Stack<int[,]>();
                                        while (last_step.parent != null) //O(M)
                                        {
                                            res.Push(last_step.puzz);
                                            last_step = last_step.parent;
                                        }
                                        res.Push(last_step.puzz);

                                        /*while (res.Count > 0)
                                        {
                                            for (int k = 0; k < N; k++)
                                            {
                                                for (int j = 0; j < N; j++)
                                                {

                                                    Console.Write(res.Peek()[k, j] + " ");
                                                }
                                                Console.WriteLine();
                                            }
                                            Console.WriteLine();
                                            res.Pop();
                                        }*/
                                        Console.WriteLine("steps:");
                                        Console.WriteLine(level);
                                        Console.WriteLine(sw.Elapsed);
                                        Console.WriteLine();
                                        // Show the time

                                    }
                                    display(2);//O(N POWER 2)
                                }
                                else
                                {
                                    Console.WriteLine("UnSolvable");
                                }
                                
                                sw.Stop();

                            }

                            sr.Close();
                            file.Close();

                            break;
                            #endregion
                    }

                    break;
                    #endregion/*

            }
        }   
        
    }
}
