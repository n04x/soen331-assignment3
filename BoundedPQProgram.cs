using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

class BoundedPQProgram
{
    static void Main(string[] args)
    {
        Console.WriteLine("Begin Priority Queue Program");
        PriorityQueue<Element> pq = new PriorityQueue<Element>(5);

        Element e1 = new Element("Dog", 2.0f);
        Element e2 = new Element("Cat", 3.0f);
        Element e3 = new Element("Elephant", 5.0f);
        Element e4 = new Element("Penguin", 0.5f);
        Element e5 = new Element("Tiger", 1.5f);

        pq.Insert(e1);
        pq.Insert(e2);
        pq.Insert(e3);
        pq.Insert(e4);
        pq.Insert(e5);

        bool exit = false;
        pq.Insert(new Element("penguin", 0.5f));
        pq.Insert(new Element("dog", 3.0f));
        pq.Insert(new Element("cat", 1.5f));
        pq.Insert(new Element("parrot", 4.5f));
        Element t1 = new Element("T-rex", -2.5f);
        pq.Insert(t1);
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
                    // System.Diagnostics.Debug.Assert(key > 0);
                    // Contract.Requires(key > 0);
                    Element e = new Element(element, key);
                    pq.Insert(e);
                    Console.WriteLine("\n\n");
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
        Contract.Requires(k >= 0, "key is negative");
        this.element = el;
        this.key = k;
    }

    // [ContractInvariantMethod]
    public int CompareTo(Element other)
    {
        // Contract.Invariant(this.key >= 0); //Invariant ensuring the key is >= 0
        Contract.Requires(this.key >= 0);
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

    // [ContractInvariantMethod]
    // [System.Diagnostics.Conditional("CONTRACT_FULL")]
    public void Insert(T item)
    {
        //Precondition: Item must be of type T
        Contract.Requires(typeof(T) == item.GetType(), "Item must be of type " + typeof(T));
        Contract.Requires(element.Count <= capacity, "element must be less or equal to capacity"); //Invariant ensuring size is less or equal to capacity
        Contract.Ensures(element.Count == Contract.OldValue(element.Count) + 1, "element is not the proper size");
        int size = element.Count;
        if (size >= capacity)
        {
            int max = Max();
            T temp = element[max];
            if (temp.CompareTo(item) >= 0)
            {
                element[max] = item;
                // Contract.Ensures(size <= capacity); //post condition to ensure that after this operation, size is still less or equal to capacity
                Console.WriteLine("replace this: " + temp.ToString() + " with this: " + item.ToString());
                //Postcondition: New size should be the same as size.old
                // Contract.Ensures(element.Count == Contract.OldValue(element.Count) + 1);
            }
        }
        else
        {

            element.Add(item);
            // Contract.Ensures(size <= capacity); //post condition to ensure that after this operation, size is still less or equal to capacity

            //Postcondition: New size should be size.old+1
            // Contract.Ensures(element.Count == (size+1));
            // Contract.Ensures(element.Count == Contract.OldValue(element.Count) + 1);

        }
        // element.Add(item);
        int child = element.Count - 1; // stores the child index at the end.
        // Contract.Requires(child > 0); //precondition ensuring that child is bigger than 0
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
    // precondition: highest priority item must be at first.
    // postcondition: remove element at last.
    //                rebuild the binary heap.
    //                size' = size.old - 1
    // [System.Diagnostics.Conditional("CONTRACT_FULL")]    
    public T Remove()
    {
        Contract.Requires(element.Count > 0, "PQ must not be empty!");
        Contract.Ensures(element.Count == Contract.OldValue(element.Count) - 1, "PQ size must be decremented by 1");
        // assume it is not empty, will need to enforce that with Contracts.
        int last = element.Count - 1;
        int size = element.Count;
        // Contract.Requires(last > 0);//precondition assuring last is bigger than 0
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

        //Postcondition: New size should be size.old - 1
        // Contract.Ensures(element.Count == last);
        // Contract.Ensures(element.Count == (size - 1));

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
        Contract.Ensures(Contract.Result<T>().Equals(typeof(T)), "wrong type for the minimum!");
        T front = element[0];

        //Postcondition: Display element at the front
        // Contract.Ensures(front.Equals(element[0]));
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
