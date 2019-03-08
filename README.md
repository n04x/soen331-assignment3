# soen331-assignment3
Group assignment for SOEN331, build a Priority Queue in C#

## Authors
Thomas Backs - 27554524    
Angel Chung - 40047737    
Michael Mansy - 40004472    
Antoine Betenjaneh - 27161956    

## Assignment #3
Due date is **March 13** by 23:59.    
Implementation of bounded Priority Queue ADT with contactual specifications. The language we chose for our project is C# and the contractual specification used is the native one called Code Contracts by Microsoft.    

## Bounded Priority Queue
We implement a List collection for storing our element in the priority queue. After each insertion at the end, we use **binary heap** to improve our priority queue performance. We use a min-heap structure to store the highest priority at root of our tree. Each node can only have at most 2 child nodes, either left or right. This structure allow us to quickly update our priority queue after modification such as `insert()` or `remove()`.    

We use a List collection to implement our Priority Queue since it is easier to manage a list for our binary heap.

