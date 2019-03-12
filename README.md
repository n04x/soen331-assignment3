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
We implement a List collection for storing our element in the priority queue. After each insertion at the end, we use **binary heap** to improve our priority queue performance. We use a min-heap structure to store the highest priority at root of our tree. Each node can only have at most 2 child nodes, either left or right. This structure allow us to quickly update our priority queue after modification such as `insert()` or `remove()`. The prority queue is the size of 4.    

We use a List collection to implement our Priority Queue since it is easier to manage a list for our binary heap.    

## How to use
Simply run it in command line, under Linux you simply move to the current folder and type `dotnet run` and it will run the program. In the program, there will be different actions proposed to the user such as Insert, Remove, Min, and etc. based on numerical input between 1 and 5 from the user. To exit it, simply press 5 and enter.    

### Use Insert
To use insert function, simply enter `1` and press `Enter`, it will prompt you a string elemen, find a nice name for it, then press `Enter` again, another prompt will appear asking you to assign a `key` to it which will be a `float` value. Once it is complete you press `Enter` and it will take you back to the Main page and the new element has been added to our priority queue.

### Use Remove
To use remove function, type `2` and press `Enter` in the Main menu. It will automatically remove the element with the highest priority (lowest key) from our list and return it on screen.
