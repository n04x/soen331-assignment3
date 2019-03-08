using System;
using System.Collections.Generic;

class BoundedPQProgram
{
    static void Main(string[] args)
    {
        Console.WriteLine("Begin Priority Queue Program");
        PriorityQueue <Element> pq = new PriorityQueue<Element>();

        Element e1 = new Element("Dog", 2.0f);
        Element e2 = new Element("Cat", 1.0f);
        Element e3 = new Element("Parrot", 3.0f);
        Element e4 = new Element("Bear", 3.5f);
        Element e5 = new Element("Penguin", 0.5f);
        pq.Insert(e1);
        pq.Insert(e2);
        pq.Insert(e3);
        pq.Insert(e4);
        pq.Insert(e5);

        Console.WriteLine(pq.ToString());
        Console.WriteLine("The element with the smallest key: " + pq.Min().ToString());
    }
    static void TestPQ(int numOps) {

    }
}
public class Element : IComparable<Element> {

    public string element;
    public float key;      // small value are more important.

    // constructor
    public Element(string el, float k) {
        this.element = el;
        this.key = k;
    } 

    public int CompareTo(Element other) {
        if(this.key < other.key) {
            return -1;
        } else if(this.key > other.key) {
            return 1;
        } else {
            return 0;
        }
    }
    public override string ToString() {
        return this.element + ":" + this.key;
    }
}
public class PriorityQueue <T> where T : IComparable <T> {
    private List<T> element;

    public PriorityQueue() {
        this.element = new List<T>();
    }
    public void Insert(T item) {
        // Console.WriteLine("Adding " + item.ToString() + " to PQ");
        element.Add(item);
        int child = element.Count - 1; // stores the child index at the end.
        while(child> 0) {
            int parent = (child -1) / 2;    // binary tree traversal
            if(element[child].CompareTo(element[parent]) >= 0) { // check the key if the child key is greater than parent or not.
                break;
            }
            T temp = element[child];
            element[child] = element[parent];
            element[parent] = temp;
            child = parent;
        }
    }
    public T Remove() {
        // assume it is not empty, will need to enforce that with Contracts.
        int last = element.Count - 1;
        T front_item = element[0];
        element[0] = element[last];
        element.RemoveAt(last);

        last--;
        int parent = 0; // parent index.
        while(true) {
            int left_child = parent * 2 + 1; // left child
            if(left_child > last) {
                break;
            }
            int right_child = left_child + 1;
            if(right_child <= last && element[right_child].CompareTo(element[left_child]) <= 0) { // if there is a right child and it is smaller than left child, use it instead
                left_child = right_child;
            }
            T temp = element[parent];
            element[parent] = element[left_child];
            element[left_child] = temp;
            parent = left_child;
        }
        return front_item;
    }
    public T Min() {
        T front = element[0];
        return front;
    }
    public override string ToString() {
        string str = "";
        str  += "Number of element in PQ: " + element.Count + "\n";
        foreach(T el in element) {
            str += el.ToString() + "\n";
        }
        return str;
    }
}

