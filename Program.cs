/* ---------------------------------------------------------------------------
    Student code: E4886
    Module code: P175B118
-----------------------------------------------------------------------------*/
using System;
using System.IO;

namespace Question1
{
    /// <summary>
    /// Class for storing data about single worker
    /// </summary>
    class Worker
    {
        private string nameSurname;
        private int birthYear;
        private int producedComponents;

        // Default constructor
        public Worker()
        {

        }

        // Constructor with parameters 
        public Worker(string nameSurname, int birthYear, int producedComponents)
        {
            this.nameSurname = nameSurname;
            this.birthYear = birthYear;
            this.producedComponents = producedComponents;
        }

        //--------------------------------------------------------------------
        //                  Interface methods (set, get)
        //--------------------------------------------------------------------
        public string GetNameSurname() { return nameSurname; }
        public int GetBirthYear() { return birthYear; }
        public int GetProducedComponents() { return producedComponents; }

        // Print method for single worker
        public override string ToString()
        {
            return string.Format("{0, -30} {1, 6:d} {2, 6:d}", 
                                  nameSurname, birthYear, producedComponents);
        }

        // TODO: 
        // Implement overloaded operators >= and <=
        // Compare number of produced components and worker name, surname.
        public static bool operator <=(Worker wk1, Worker wk2)
        {
            if(wk1.GetProducedComponents() <= wk2.GetProducedComponents())
            {
                return true;
            }else if(wk1.GetProducedComponents() == wk2.GetProducedComponents() && wk1.GetNameSurname().CompareTo(wk2.GetNameSurname()) < 0)
            {
                return true;
            }
            return false;
        }

        public static bool operator >=(Worker wk1, Worker wk2)
        {
            if (wk1.GetProducedComponents() >= wk2.GetProducedComponents())
            {
                return true;
            }
            else if (wk1.GetProducedComponents() == wk2.GetProducedComponents() && wk1.GetNameSurname().CompareTo(wk2.GetNameSurname()) > 0)
            {
                return true;
            }
            return false;
        }



    }
    /// <summary>
    /// 1D container class
    /// </summary>
    class ArrayOfWorkers
    {
        const int CMax = 100;       // maximum number of workers
        private Worker[] Workers;   // array of worker data
        private int n;              // actual number of workers

        /// <summary>
        /// Default constructor (initialization)
        /// </summary>
        public ArrayOfWorkers()
        {
            n = 0;
            Workers = new Worker[CMax]; // memory allocation
        }

        //--------------------------------------------------------------------
        //                  Interface methods (set, get)
        //--------------------------------------------------------------------
        public Worker Get(int i) { return Workers[i]; }
        public int Count() { return n; }
        public void Add(Worker worker) { Workers[n++] = worker; }

        // TODO: implement method by the given header
        // Finds and returns the location (index) of worker object in sorted container
        private int LocationInSorted(Worker worker)
        {
            // TODO: implement method return value
            for(int i = 0; i < n; i++)
            {
                if (Workers[i] <= worker)
                {
                    if (Workers[i] == worker)
                    {
                        return i;
                    }
                    else
                    {
                        return -1;
                    }
                }
                     
            }
            return -1;
        }

        // TODO: implement method by the given header
        // Inserts data of each worker from container Brigade2 (unsorted) to sorted container
        // Use implemented overloaded operator in your solution
        // Use implemented method LocationInSorted in your solution
        public void InsertAll(ArrayOfWorkers Brigade2)
        {
            for (int i = 0; i < Brigade2.n; i++)
            {
                int j = 0;
                while(j < n && Brigade2.Get(i) <= Workers[j] )
                {
                    j++;
                }
                n++;
                for(int k = n; k > j; k--)
                {
                    Workers[k] = Workers[k - 1];
                }
                Workers[j] = Brigade2.Get(i);
                
            }
        }

        // TODO: implement method by the given header
        // Creates a new container  NewBrigade (deep copy) of workers who produced less components 
        // than the average of minimum and maximum numbers (value) of produced components 
        public void Create(ArrayOfWorkers NewBrigade)
        {
            int avg = (Workers[0].GetProducedComponents() + Workers[n - 1].GetProducedComponents())/2;
            for(int i = 0; i < n; i++)
            {
                if (Workers[i].GetProducedComponents() < avg)
                {
                    Worker temp = new Worker(Workers[i].GetNameSurname(), Workers[i].GetBirthYear(), Workers[i].GetProducedComponents());
                    NewBrigade.Add(temp);
                }
            }
        }
    }


    class Program
    {
        const string CFd1 = "Brigade1.txt";
        const string CFd2 = "Brigade2.txt";

        static void Main(string[] args)
        {
            ArrayOfWorkers Brigade1 = new ArrayOfWorkers();
            Read(CFd1, Brigade1);
            Print(Brigade1, "Initial data: Brigade1");
            ArrayOfWorkers Brigade2 = new ArrayOfWorkers();
            Read(CFd2, Brigade2);
            Print(Brigade2, "Initial data: Brigade2");

            // TODO:
            // Insert data from Brigade2 container to sorted container Brigade1.
            Brigade1.InsertAll(Brigade2);
           
            // Create a new container (deep copy) NewBrigade from Brigade1 (after insertion). 
            ArrayOfWorkers NewBrigade = new ArrayOfWorkers();
            Brigade1.Create(NewBrigade);

            // Print data of Brigade1 (after insertion)
            Print(Brigade1, "Brigade1 after insertion");

            // Print data of container NewBrigade to console.
            Print(NewBrigade, "NewBrigade after Insertion");
        }

        static void Read(string fv, ArrayOfWorkers Brigade)
        {
            using (StreamReader reader = new StreamReader(fv))
            {
                string line; // single line from text file
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(';'); // line fragments (parts)
                    string nameSurname = parts[0];
                    int birth = int.Parse(parts[1]);
                    int components = int.Parse(parts[2]);
                    Worker worker = new Worker(nameSurname, birth, components);
                    Brigade.Add(worker);
                }
            }
        }

        static void Print(ArrayOfWorkers Brigade, string header)
        {
            if (Brigade.Count() > 0)
            {
                const string top =
                    "----------------------------------------------------------\n" +
                    " No.      Surname and Name            Birth   Components\n" +
                    "----------------------------------------------------------";
                Console.WriteLine("\n " + header);
                Console.WriteLine(top);
                for (int i = 0; i < Brigade.Count(); i++)
                {
                    Worker worker = Brigade.Get(i);
                    Console.WriteLine("{0, 3}   {1}", i + 1, worker.ToString());
                }
                Console.WriteLine("---------------------------------------------------------\n");
            }
            else
            {
                Console.WriteLine("Container is empty.");
            }
        }
    }
}
