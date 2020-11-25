using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace UndoRedo
{
    [Serializable]
    class DoubleLinkedList
    {
        //create doublelinkedlist
        private int data;
        private DoubleLinkedList next;
        private DoubleLinkedList prev;

        public DoubleLinkedList()
        {
            data = 0;
            next = null;
            prev = null;
        }
        public DoubleLinkedList(int value)
        {
            data = value;
            next = null;
            prev = null;
        }
        /*basic methods: insert, sort, reverse*/
        DoubleLinkedList head;
        //insert a node at the end of the list
        public void Insertion(int value)
        {
            DoubleLinkedList node = new DoubleLinkedList(value);
            DoubleLinkedList tail = head;

            node.next = null;// this insert node is last one

            //if the list is empty, then let the node as head
            if (head == null)
            {
                node.prev = null;
                head = node;
                return;
            }
            //insertion 
            while (tail.next != null)
            {
                tail = tail.next;
            }
            tail.next = node;
            node.prev = tail;
        }
        // sort the list
        public DoubleLinkedList Sort(DoubleLinkedList head)
        {
            int swapped;
            DoubleLinkedList temp;
            DoubleLinkedList tail = null;
            if (head == null)
            {
                return null;
            }
            do
            {
                swapped = 0;
                temp = head;
                while (temp.next != tail)
                {
                    if (temp.data > temp.next.data)
                    {
                        int t = temp.data;
                        temp.data = temp.next.data;
                        temp.next.data = t;
                        swapped = 1;
                    }
                    temp = temp.next;
                }
                tail = temp;
            }
            while (swapped != 0);
            {
                return head;
            }

        }
        //reverse the list
        public void Reverse()
        {
            DoubleLinkedList temp = null;
            DoubleLinkedList cur = head;

            //for current node, swap cur.next with cur.prev
            while (cur != null)
            {
                temp = cur.prev;
                cur.prev = cur.next;
                cur.next = temp;
                cur = cur.prev;
            }

            //if only last one node
            if (temp != null)
            {
                head = temp.prev;
            }

        }

        //print the list
        public void PrintList(DoubleLinkedList node)
        {
            while (node != null)
            {
                Console.Write(node.data + " ");
                node = node.next;
            }
            Console.WriteLine();
        }
 
      
        static void Main(string[] args)
        {
            DoubleLinkedList dll = new DoubleLinkedList();
            IUndoable<DoubleLinkedList> test = new Undoable<DoubleLinkedList>(dll);
            IUndoState<DoubleLinkedList> state = new UndoState<DoubleLinkedList>(dll);
            DoubleLinkedList stateList (DoubleLinkedList node)
            {
                DoubleLinkedList res = new DoubleLinkedList();
                while(node != null)
                {
                    res.Insertion(node.data);
                    node = node.next;
                }
                return res;
            }
            //1: initial double linkedlist with {5,3,2,1,4,8} and print it out
            dll.Insertion(5);
            dll.Insertion(3);
            dll.Insertion(2);
            dll.Insertion(1);
            dll.Insertion(4);
            dll.Insertion(8);
            test.Value = stateList(dll.head);
            test.SaveState();
            Console.Write("The initial list is: ");
            dll.PrintList(dll.head);
            Console.Write("The current state is: ");
            state.printState();


            //2: bubble sort the original list and print it out
            dll.Sort(dll.head);
            test.Value = stateList(dll.head);
            Console.Write("\nThe sorted list is: ");
            dll.PrintList(dll.head);
            test.SaveState();
            Console.Write("The current state is: ");
            state.printState();

            //3: reverse the list
            dll.Reverse();
            test.Value = stateList(dll.head);
            Console.Write("\nThe reverse list is: ");
            dll.PrintList(dll.head);
            test.SaveState();
            Console.Write("The current state is: ");
            state.printState();

            //4: add 9 to the end of the list
            dll.Insertion(9);
            test.Value = stateList(dll.head);
            Console.Write("\nInsert 9 into list: ");
            dll.PrintList(dll.head);
            test.SaveState();




            //5: Undo back to before you reversed the list
            test.Undo();
            test.Undo();
            Console.Write("\nUndo back to before you reversed the list: ");
            dll.PrintList(dll.head);


            //6: Add a 9 to the end of the list again
            test.Redo();
            test.Redo();
            Console.Write("\nRedo add 9 to the end of list: " );
            dll.PrintList(dll.head);


            //7: Undo back to the original list
            test.Undo();
            test.Undo();
            test.Undo();
            Console.Write("\nUndo back to the original list: ");
            dll.PrintList(dll.head);
        }
    }
}
