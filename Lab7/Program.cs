using System;

namespace Lab7
{
    class Program
    {
        static void Main(string[] args)
        {

            //Initialise a heap to some size
            int sizeOfHeap = 10;
            BinaryHeap<DemoClass> sampleHeap;
            sampleHeap = new BinaryHeap<DemoClass>(sizeOfHeap);
            Random rand = new Random();
            //populate a heap
            for (int i = 0; i < sizeOfHeap; i++)
            {
                DemoClass A = new DemoClass();
                A.name = "Object # " + i;
                A.priority = rand.Next(0, 100);
                sampleHeap.AddItem(A);
            }

            //Test recalculate UP

            

            //Print each  element in the heap

            foreach (DemoClass i in sampleHeap)
            {
                Console.WriteLine(i.ToString() + " ");
            }
            Console.WriteLine("\n Next we pop off the top element, a couple of times to illustrate what's happening");
            Console.WriteLine(sampleHeap.ExtractHead().ToString());
            Console.WriteLine(sampleHeap.ExtractHead().ToString());

            Console.WriteLine("The remaining Heap after two elements were removed");
            foreach (DemoClass i in sampleHeap)
            {
                if (i != null)
                    Console.WriteLine(i.ToString() + " ");
            }

            Console.WriteLine(sampleHeap.Peek(ref sampleHeap[0])

            Console.ReadKey(); // only necessary if your project autocloses
        }
    }
}
