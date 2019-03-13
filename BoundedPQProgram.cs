using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

class BoundedPQProgram
{
    static void Main(string[] args)
    {
        Console.WriteLine("Begin Priority Queue Program");
        PriorityQueue<Element> pq = new PriorityQueue<Element>(5);
    
        bool exit = false;
        pq.Insert(new Element("penguin", 0.5f));
        pq.Insert(new Element("dog", 3.0f));
        pq.Insert(new Element("cat", 1.5f));
        pq.Insert(new Element("parrot", 4.5f));
        while(!exit) {
            try
            {
                Console.WriteLine("Choose an action:");
                Console.WriteLine("Type 1 to Insert a new element");
                Console.WriteLine("Type 2 to Remove an element");
                Console.WriteLine("Type 3 to return the Minimum");
                Console.WriteLine("Type 4 to print the queue");
                Console.WriteLine("Type 5 to exit");
                int action = Convert.ToInt32(Console.ReadLine());
                switch (action)
                {
                    case 1:
                        bool done = false;
                        while (!done)
                        {
                            try
                            {
                                Console.WriteLine("Add a new element: ");
                                string element = Console.ReadLine();
                                Console.WriteLine("Add a key:");
                                float key = float.Parse(Console.ReadLine());
                                Element e = new Element(element, key);
                                pq.Insert(e);
                                Console.WriteLine("\n\n");
                                done = true;
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                        break;
                    case 2:
                        Console.WriteLine("We now remove the first element in the PQ");
                        Console.WriteLine("Element " + pq.Remove().ToString() + " has been removed!\n\n");
                        break;
                    case 3:
                        Console.WriteLine("The element with the lowest key is: " + pq.Min().ToString() + "\n\n");
                        break;
                    case 4:
                        Console.WriteLine(pq.ToString() + "\n\n");
                        break;
                    case 5:
                        Console.WriteLine("Thank you for using BoundedPQ Program!");
                        exit = true;
                        break;
                }
            } catch(Exception)
            {
                Console.WriteLine("Please, enter a valid option\n");
            }
        }
    }
    static void TestPQ(int numOps)
    {

    }
}
public class Element : IComparable<Element>
{

    public string element;
    public float key;      // small value are more important.

    // constructor
    //Precondition: Key must be positive
    public Element(string el, float k)
    {
        Contract.Requires(k >= 0, "Key is negative");
        this.element = el;
        this.key = k;
    }

    public int CompareTo(Element other)
    {
        if (this.key < other.key)
        {
            return -1;
        }
        else if (this.key > other.key)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
    public override string ToString()
    {
        return this.element + ": " + this.key;
    }
}
public class PriorityQueue<T> where T : IComparable<T>
{
    private List<T> element;
    private readonly int capacity;

    public PriorityQueue(int c)
    {
        this.element = new List<T>();
        this.capacity = c;
    }

    public int GetElementCount()
    {
        return this.element.Count;
    }

    public int GetCapacity()
    {
        return this.capacity;
    }

    // The contract for insert would be:
    // Precondition: Item must be a type T.
    //               PQ must not be full                 
    // Postcondition: count' = size.old + 1 or count' = count
    //                size' = size.old + 1.

    public void Insert(T item)
    {
        //Preconditions
        Contract.Requires(typeof(T) == item.GetType(), "Item must be of type T!");
        Contract.Requires(this.GetElementCount() <= this.GetCapacity(), "Element must be less or equal to capacity"); //Invariant ensuring size is less or equal to capacity
        //Postconditions
        Contract.Ensures(this.GetElementCount() == Contract.OldValue(this.GetElementCount()) + 1 || this.GetElementCount() == Contract.OldValue(this.GetElementCount()), "Element is not the proper size");
        int size = element.Count;
        if (size >= capacity)
        {
            int max = Max();
            T temp = element[max];
            Console.WriteLine(max);
            if (temp.CompareTo(item) >= 0)
            {
                element[max] = item;
                Console.WriteLine("Replace this: " + temp.ToString() + " with this: " + item.ToString());
            }
        }
        else
        {

            element.Add(item);

        }
        // element.Add(item);
        int child = element.Count - 1; // stores the child index at the end.
        while (child > 0)
        {
            int parent = (child - 1) / 2;    // binary tree traversal
            if (element[child].CompareTo(element[parent]) >= 0)
            { // check the key if the child key is greater than parent or not.
                break;
            }
            T temp = element[child];
            element[child] = element[parent];
            element[parent] = temp;
            child = parent;
        }
    }
    // The contract for remove would be:
    // precondition: The PQ must not be empty
    // postcondition: Remove min on PQ
    //                size' = size.old - 1  
    public T Remove()
    {
        //Precondition
        Contract.Requires(this.GetElementCount() > 0, "PQ must not be empty!");
        //Postconditions
        Contract.Ensures(Contract.OldValue(element[0]).Equals(Contract.Result<T>()), "Removed item must be the minimun!");
        Contract.Ensures(this.GetElementCount() == Contract.OldValue(this.GetElementCount()) - 1, "PQ size must be decremented by 1");

        // assume it is not empty, will need to enforce that with Contracts.
        int last = element.Count - 1;
        int size = element.Count;
        T front_item = element[0];
        element[0] = element[last];
        element.RemoveAt(last);
        last--;
        for(int i = 0; i < element.Count; i++) {
            if(front_item.CompareTo(element[i]) <= 0) {

            }
        }
        int parent = 0; // parent index.
        while (true)
        {
            int left_child = parent * 2 + 1; // left child
            if(left_child > last) {
                break;  // no more children.
            }
            int right_child = left_child + 1; // right child
             if (right_child <= last && element[right_child].CompareTo(element[left_child]) < 0)
            { // if there is a right child and it is smaller than left child, use it instead
                left_child = right_child;
            }
            if (element[parent].CompareTo(element[left_child]) <= 0) 
            {
                break;  // parent is smaller than the smallest child.
            }
           
            T temp = element[parent];
            element[parent] = element[left_child];
            element[left_child] = temp;
            parent = left_child;
        }

        return front_item;
    }
    public int Max()
    {
        int index = 0;
        for (int i = 1; i < element.Count; i++)
        {
            if (element[index].CompareTo(element[i]) <= 0)
            {
                index = i;
            }
        }
        return index;
    }
    // The contract for min would be:
    // precondtion: -
    // postcondition: Front must be of type T.
    //                display element at position 0.
    public T Min()
    {
        //Postconditions
        Contract.Ensures(Contract.Result<T>().GetType() == typeof(T), "Wrong type for the minimum!");
        Contract.Ensures(Contract.Result<T>().Equals(Contract.OldValue(element[0])));
        T front = element[0];
        
        return front;
    }
  
    public override string ToString()
    {
        string str = "";
        str += "Number of element in PQ: " + element.Count + "\n";
        foreach (T el in element)
        {
            str += el.ToString() + "\n";
        }
        return str;
    }
}
