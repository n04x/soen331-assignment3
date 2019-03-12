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
        
        while(!exit) {
            Console.WriteLine("Choose an action:");
            Console.WriteLine("Type 1 to Insert a new element");
            Console.WriteLine("Type 2 to Remove an element");
            Console.WriteLine("Type 3 to return the Minimum");
            Console.WriteLine("Type 4 to print the queue");
            Console.WriteLine("Type 5 to exit");
            int action = Convert.ToInt32(Console.ReadLine());
            switch(action) {
                case 1:
                    Console.WriteLine("Add a new element: ");
                    string element = Console.ReadLine();
                    Console.WriteLine("Add a key:");
                    float key = float.Parse(Console.ReadLine());
                    Element e = new Element(element, key);
                    pq.Insert(e);
                    break;
                case 2:
                    Console.WriteLine("We now remove the first element in the PQ");
                    Console.WriteLine("Element " + pq.Remove().ToString() + " has been removed!");
                    break;
                case 3:
                    Console.WriteLine("The element with the lowest key is: " + pq.Min().ToString());
                    break;
                case 4:
                    Console.WriteLine(pq.ToString());
                    break;
                case 5:
                    Console.WriteLine("Thank you for using BoundedPQ Program!");
                    exit = true;
                    break;
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
    public Element(string el, float k)
    {
        this.element = el;
        this.key = k;
    }

    [ContractInvariantMethod]
    public int CompareTo(Element other)
    {
        Contract.Invariant(this.key >= 0); //Invariant ensuring the key is >= 0
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
    // The contract for insert would be:
    // Precondition: item must be a type T.
    // Postcondition: if size >= capacity then replace the highest one.
    //                if size < capacity then add it to the end and perform sorting, using binary traversal
    //                size' = size.old + 1.

    [ContractInvariantMethod]
    public void Insert(T item)
    {

        int size = element.Count;
        Contract.Invariant(size <= capacity); //Invariant ensuring size is less or equal to capacity
        if (size >= capacity)
        {
            int max = Max();
            T temp = element[max];
            if (temp.CompareTo(item) >= 0)
            {
                element[max] = item;
                Contract.Ensures(size <= capacity); //post condition to ensure that after this operation, size is still less or equal to capacity
                Console.WriteLine("replace this: " + temp.ToString() + " with this: " + item.ToString());
            }
        }
        else
        {

            element.Add(item);
            Contract.Ensures(size <= capacity); //post condition to ensure that after this operation, size is still less or equal to capacity
        }
        // element.Add(item);
        int child = element.Count - 1; // stores the child index at the end.
        Contract.Requires(child > 0); //precondition ensuring that child is bigger than 0
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

            //Postcondition: New size should be size.old+1
            Contract.Ensures(element.Count == (size+1));
        }
    }
    // The contract for remove would be:
    // precondition: highest priority item must be at first.
    // postcondition: remove element at last.
    //                rebuild the binary heap.
    //                size' = size.old - 1
    public T Remove()
    {
        // assume it is not empty, will need to enforce that with Contracts.
        int last = element.Count - 1;
       Contract.Requires(last > 0);//precondition assuring last is bigger than 0
        T front_item = element[0];
        element[0] = element[last];
        element.RemoveAt(last);

        last--;
        int parent = 0; // parent index.
        while (true)
        {
            int left_child = parent * 2 + 1; // left child
            if (left_child > last)
            {
                break;
            }
            int right_child = left_child + 1;
            if (right_child <= last && element[right_child].CompareTo(element[left_child]) <= 0)
            { // if there is a right child and it is smaller than left child, use it instead
                left_child = right_child;
            }
            T temp = element[parent];
            element[parent] = element[left_child];
            element[left_child] = temp;
            parent = left_child;
        }

        //Postcondition: New sizw should be size.old - 1
        Contract.Ensures(element.Count == last);
        return front_item;
    }
    public int Max()
    {
        int index = 0;
        for (int i = 1; i < element.Count - 1; i++)
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
    // postcondition: front must be of type T.
    //                display element at position 0.
    public T Min()
    {
        T front = element[0];

        //Postcondition: Display element at the front
        Contract.Ensures(front.Equals(element[0]));
        // Contract.Ensures(front.element == element[0].element && front.key == element[0].key);
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
