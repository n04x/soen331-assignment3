﻿using System;
using System.Collections.Generic;

class BoundedPQProgram
{
    static void Main(string[] args)
    {
        Console.WriteLine("Begin Priority Queue Program");
        PriorityQueue <Element> pq = new PriorityQueue<Element>(5);

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
        
        Console.WriteLine("We remove: " + pq.Remove().ToString());
        // Below is new stuff in order to test our Bounded way.
        // Element b1 = new Element("T-Rex", 3.1f);
        // Element b2 = new Element("Velociraptor", 0.75f);
        // Element b3 = new Element("Triceratops", 4.0f); // this should never be added.
        // pq.Insert(b1);
        // pq.Insert(b2);
        // pq.Insert(b3);

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
    private int capacity;

    public PriorityQueue(int c) {
        this.element = new List<T>();
        this.capacity = c;
    }
    // The contract for insert would be:
    // Precondition: item must be a type T.
    // Postcondition: if size >= capacity then replace the highest one.
    //                if size < capacity then add it to the end and perform sorting, using binary traversal
    //                size' = size.old + 1.
    public void Insert(T item) {
        int size = element.Count;
        if(size  >= capacity) {
            int max = Max();
            T temp = element[max];
            if(temp.CompareTo(item) >= 0) {
                element[max] = item;
                Console.WriteLine("replace this: " + temp.ToString() + " with this: " + item.ToString());
            }
        } else {
            element.Add(item);
        }
        // element.Add(item);
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
    // The contract for remove would be:
    // precondition: highest priority item must be at first.
    // postcondition: remove element at last.
    //                rebuild the binary heap.
    //                size' = size.old - 1
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
    public int Max() {
        int index = 0;
        for(int i = 1; i < element.Count - 1; i++) {
            if(element[index].CompareTo(element[i]) <= 0) {
                index = i;
            }
        }
        return index;
    }
    // The contract for min would be:
    // precondtion: -
    // postcondition: front must be of type T.
    //                display element at position 0.
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

