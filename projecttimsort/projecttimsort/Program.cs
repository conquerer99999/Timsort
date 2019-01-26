using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p1 = new Program();
            Random rnd = new Random();
            /*Declaring the size of the array and initializing it*/
            int size = 67;
            int[] array = new int[size];
            /*Generating a random array*/
            for (int i = 0; i < size; i++)
            {
                array[i] = rnd.Next(1, 70);
            }
            Console.WriteLine("The randomly generated array is : \n");
            /*Printing the randomly generated array*/
            for (int i = 0; i < size; i++)
            {
                Console.Write("{0} ", array[i]);
            }
            Console.WriteLine();
            p1.TimSort(array);
        }
        /*Declaring the size of the minimum part of the array that will be soeted using Insertion Sort*/
        /*It is chosen from 32 to 64 since Insertion Sort works fastest on small arrays*/
        public int Min_Run = 32;
        public void TimSort(int[] array)
        {
            int size = array.Length;
            int temp_high = 0;
            /*This loop will call the Insertion Sort function and will sort small parts of the array*/
            /*The parts will be of the size Min_Run*/
            for (int z = 0; z < size; z += Min_Run)
            {
                /*Math.Min() funstion returns the smaller value between the two arguments being passed*/
                /*int high stores the upper bound of the chunk that is to be sorted , we have to use the*/
                /*Math.Min() function here because in some cases the size of the array may be divisible by*/
                /*the Min_Run i.e all the chunks being forme are of equal size but if there is a chunk*/
                /*being left at the end smaller than the size of the Min_Run the if conditions after that will help*/
                /*us identify that using high and then we can set our upper bound in case accordingly*/
                int high = Math.Min(size - z, Min_Run);
                if (high == Min_Run)
                {
                    temp_high = temp_high + Min_Run;
                    array = InsertionSort(array, z, temp_high);
                }
                else
                    array = InsertionSort(array, z, size);
            }
            /*This conditions check if the size of the array is smaller than or equal to the Min_Run it terminates*/
            /*Tim Sort after printing the array that has been sorted in the Insertion Sort above*/
            if (size <= Min_Run)
            {
                for (int y = 0; y < size; y++)
                {
                    Console.Write("{0} ", array[y]);
                }
                return;
            }
            Console.WriteLine();
            /*Printing the array after sorting RUNS of size Min_Run using Insertion Sort*/
            Console.WriteLine("The array after sorting the RUNS of size {0} by using Insertion Sort : \n", Min_Run);
            for (int p = 0; p < size; p++)
            {
                Console.Write("{0} ", array[p]);
            }
            /*Preparing for Merge Sort*/
            /*Making a temporary array of size Min_Run since we know that the size of the size run will be equal to Min_Run*/
            /*if the code gets this far as if the size was smaller we would have exited after Insertion Sort*/
            int[] temp = new int[Min_Run];
            int[] temp2;
            int i;
            int j;
            /*Copying the first run into the temporary array of size Min_Run*/
            for (i = 0; i < Min_Run; i++)
            {
                temp[i] = array[i];
            }
            /*Checking to see if the size of the second run is smaller than Min_run this will occur if the size of the array*/
            /*is not divisible by Min_Run*/
            if (size - Min_Run < Min_Run)
                temp2 = new int[size - Min_Run];
            /*This else consition will run if the size of the second run is also equal to Min_Run*/
            else
                temp2 = new int[Min_Run];
            /*Figuring out the number of elements we need to copy into temp2*/
            int less = Math.Min(size - Min_Run, Min_Run);
            /*Copying less number of items into temp2 , onwards from where our first run ended */
            Console.WriteLine(less);
            for (j = 0; j < less; j++)
            {
                temp2[j] = array[i];
                i++;
            }
            int c;
            int[] temp3;
            /*Merging these two arrays or RUNS */
            int[] newarray = MergeSort(temp, temp2);
            /*If there are more than two RUNS this loop will sort them using Merge Sort*/
            for (int a = less + Min_Run; a < size; a += Math.Min(Min_Run, size - a))
            {
                /*Calculating a new less for the new temp3 array which will be made to merge RUNS from*/
                /*RUN number 3 and onwards*/
                int less1 = Math.Min(Min_Run, size - a);
                temp3 = new int[less1];
                for (c = 0; c < less1; c++)
                {
                    temp3[c] = array[i];
                    i++;
                }
                /*This will merge the new RUN with the array containing the merge of all the previous RUNS*/
                newarray = MergeSort(newarray, temp3);
            }
            Console.WriteLine();
            /*Printing the sorted array after performing Tim Sort*/
            Console.WriteLine("Sorted Array after performing Tim Sort : \n");
            for (int b = 0; b < newarray.Length; b++)
            {
                Console.Write("{0} ", newarray[b]);
            }
        }
        /*This merge sort function takes two arguments i.e the arrays to be merged together*/
        public int[] MergeSort(int[] array1, int[] array2)
        {
            int i = 0;
            int j = 0;
            int k = 0;
            int m = array1.Length;
            int n = array2.Length;
            /*Creating a new array of the size that is the sum of both the sizes of the arrays to be merged*/
            int[] merge = new int[m + n];
            while ((i < m) && (j < n))
            {
                /*Checking if the element 'i' of array1 is lesser than the element 'j' of array2 , it will be*/
                /*copied in the new array that we made above and the counters will be incremented*/
                if (array1[i] < array2[j])
                {
                    merge[k] = array1[i];
                    i++;
                    k++;
                }
                /*if element 'j' of array2 is lesser element 'i' of array1 then it will be copied into the new array*/
                else
                {
                    merge[k] = array2[j];
                    k++;
                    j++;
                }
            }
            /*Copying the remaing elements of array1 into merge array if any*/
            while (i < m)
            {
                merge[k] = array1[i];
                k++;
                i++;
            }
            /*Copying the remaing elements of array2 into merge array if any*/
            while (j < n)
            {
                merge[k] = array2[j];
                k++;
                j++;
            }
            /*Returning the merge of both the arrays*/
            return merge;
        }
        /*Performing Insertion Sort*/
        public int[] InsertionSort(int[] array, int low, int high)
        {
            int j;
            for (int i = low; i < high; i++)
            {
                /*Storing first element of the RUN into temp*/
                int temp = array[i];
                j = i;
                /*Checking if j>low and temp is lesser than the element before it we will save element */
                /*at j-1 into j and decrement j until j=low and then we will know where to insert temp*/
                while ((j > low) && (temp < array[j - 1]))
                {
                    array[j] = array[j - 1];
                    j--;
                }
                /*We have figured out the index to insert temp and we will insert it there*/
                array[j] = temp;
            }
            /*Returning the sorted RUN using Insertion Sort*/
            return array;
        }
    }
}
