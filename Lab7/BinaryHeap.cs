using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7

{
    // If you want a complete implementation of binary heaps for ints only https://egorikas.com/max-and-min-heap-implementation-with-csharp/
    class BinaryHeap<T> : IEnumerable where T : IComparable
    {
        private T[] array;
        private int count; // count is initialised in the constructor (below) and incremented in Additem
        public BinaryHeap(int size)
        {
            array = new T[size];
            count = 0;
        }
        // Get Item should really be private (needs to be public in the lab for demo purposes)
        public T GetItem(int index)
        {
            return array[index];
        }
        private void SetItem(int index, T value)
        {
            while (index >= array.Length) //note this is a while loop not an if, which fixes a bug with earlier SetItem implementations
                Grow(array.Length * 2);

            array[index] = value;

        }
        private void Grow(int newsize)
        {
            Array.Resize(ref array, newsize);
        }

        // Indices of left and right children
        // "Has" methods to determine if the indices exist
        private int LeftChildIndex(int pos) { return 2 * pos + 1; }
        private int RightChildIndex(int pos) { return 2 * pos + 2; }
        private int GetParentIndex(int pos) => (pos - 1) / 2;

        private T GetRightChild(int pos) => array[RightChildIndex(pos)];
        private T GetLeftChild(int pos) => array[LeftChildIndex(pos)];
        private T GetParent(int pos) => array[GetParentIndex(pos)];
        private bool HasLeftChild(int pos)
        {
            if (LeftChildIndex(pos) < count)
                return true;
            else
                return false;
        }
        private bool HasRightChild(int pos)
        {
            if (RightChildIndex(pos) < count)
                return true;
            else
                return false;
        }
        private bool IsRoot(int pos) => pos == 0; // (true if element is root)

        // Swap, uh, swaps two values given two indicies. This should be private but I originally had it public for some reason.   
        private void Swap(int position1, int position2)
        {
            T first = array[position1];
            array[position1] = array[position2];
            array[position2] = first;
        }


        //iterator so you can see how they work
        //This just prints all of the elements in the array in order, it's up to you to reconstruct the tree by hand. 
        // 
        public IEnumerator GetEnumerator()
        {
            for (int index = 0; index < array.Length; index++)
            {
                // Yield each element 

                yield return array[index];
            }
        }
        public void AddItem(T value)
        {
            // THis is part of your lab 
            // you need to make this code actually insert in a tree like fashion, not just this crap
            //   SetItem(currentIndex, value);
            //   currentIndex++;

            int index = 0;

            //if the next avlue is null, then you need to grow
            if(array[index + 1] == default)
            {
                Grow(array.Length * 2);
            }
          

            //Insert Logic
            // If the tree is empty, insert at the bottom (it does that already)
            // if not, insert at the end, 
            // From the end you either need to swap to the root, and keep minheapify
            // or, you should probably implement move up
            // for that you run a while loop, check if the current position both isn't the root, and is higher priority than a parent (in this case that probably means it's a lower value)
            // if it is, swap with parent, and keep doing that until it's its in the

            array[count] = value;
            count++;
            ReCalculateUp();


        }

        //ExtractRoot (which is the same as extract min in our case)
        public T ExtractHead() // (This could also be called 'pop')
        {
            // check to make sure the heap isn't empty, if it is, return a 'null' or at least, default object
            if (count <= 0) // change to count in assignment if you use that
            {
                System.Console.WriteLine("Tried to extract from an empty heap");
                return default(T);

            }

            // this should get the head
            T head = array[0];
            array[0] = array[count - 1];
            array[count - 1] = default(T);
            count--;
            ReCalculateDown();


            return head;

        }
        //

        private void ReCalculateDown()
        {
            //CompareTo  
            //this.CompareTo(value) returns < 0 if this < value
            //this.CompareTo(value) returns >0 if this > value


            int index = 0;
            while (HasLeftChild(index))
            {
                var smallerIndex = LeftChildIndex(index);
                if (HasRightChild(index) && (GetRightChild(index).CompareTo(GetLeftChild(index)) < 0)) //there's a set of ( ) around the right expression that are redundant but hopefully easier to read
                {
                    smallerIndex = RightChildIndex(index);
                }

                if (array[smallerIndex].CompareTo(array[index]) > 0) //If array[smallerindex]>= array[index] 

                {
                    break;
                }

                Swap(smallerIndex, index);
                index = smallerIndex;
            }
        }

        //implement and test this for task 4.
        private void ReCalculateUp()
        {
            int index = 0;
            //Loop through to the end
            while (GetParentIndex(index) != default)
            {
                //compare current node with parent node


                var greaterIndex = GetParentIndex(index);
                if (GetParentIndex(index).CompareTo(GetLeftChild(index)) > 0)
                {
                    //SWAP the two indeces
                    Swap(index, greaterIndex);
                }


                /*
                 *
                 * may remove
                if (array[smallerIndex + 1] == default) // checks to see if at the first index (end of tree)
                {
                    index = smallerIndex;
                    break;
                }

            */

            }
            //Loop back up through loop to see if parent is less than the child

            while (HasLeftChild(index))
            {
                var greaterIndex = LeftChildIndex(index);
                if (HasRightChild(index) && (GetRightChild(index).CompareTo(GetLeftChild(index)) < 0)) //there's a set of ( ) around the right expression that are redundant but hopefully easier to read
                {
                    greaterIndex = RightChildIndex(index);
                }

                if (array[greaterIndex].CompareTo(array[index]) > 0) //If array[smallerindex]>= array[index] 

                {
                    break;
                }

                Swap(greaterIndex, index);
                index = greaterIndex;
            }
        }

        //PEEK METHOD
        public T Peek(ref T refParent)
        {
            refParent = array[0];


            SetItem(0, (T)Convert.ChangeType(20, typeof(T)));




            return refParent;


        }



        //get the index of the last item
        //loop through the list, comparing the child to the parent.
        //if the parent is less than the child, swap them.
        //if the parent is greater than or equal to the child, stop.
        //set our index to that of the parent and repeat.

    }//end ReCalculateUp()
    


    /*
        public void HeapSort()
        {
            // This one is for your assignments
            // 
        }


    }
    */

    /*
     * private void bubbleUp()
    {
        int index = length;
        if (min)
        {
            while (hasParent(index) && (parent(index).compareTo(heap[index]) > 0))
            {
                swap(index, parentIndex(index));
                index = parentIndex(index);
            }	
        }
        else
        {
            while (hasParent(index) && (parent(index).compareTo(heap[index]) < 0))
            {
                swap(index, parentIndex(index));
                index = parentIndex(index);
            }	

        }
    }
    */
}
