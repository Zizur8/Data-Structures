using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleListTarea
{
    internal class ClassSinglyLinkedList<Type> where Type : IComparable<Type>, IEquatable<Type>
    {
        public ClassSinglyLinkedList()
        {

            _isOrdered = true;
            _allowDuplicate = false;
        }
        public ClassSinglyLinkedList(bool blnOrderedList)
        {

            _isOrdered = blnOrderedList;
            _allowDuplicate = false;
        }
        public ClassSinglyLinkedList(bool blnOrderedList, bool blnDuplicateData)
        {
            _isOrdered = blnOrderedList;
            _allowDuplicate = blnDuplicateData;
        }
        ~ClassSinglyLinkedList()
        {
            ClearList();
        }

        private bool _isOrdered;
        private bool _allowDuplicate;

        private ClassNode<Type>? _initialNode = null;
        public ClassNode<Type>? InitialNode
        {
            get { return _initialNode; }
            private set { _initialNode = value; }
        }



        public bool NullList
        {
            get { return _initialNode == null; }
        }
        public void InsertNode(Type newObject)
        {
            if (!_allowDuplicate && !_isOrdered)
            {
                if (IsDuplicateObject(newObject))
                {
                    return;
                    //throw new InvalidOperationException("Object Exist. Not Accept Data Duplicate");
                }
            }

            ClassNode<Type> newNode = new ClassNode<Type>();
            newNode.ObjectType = newObject;

            if (_initialNode == null)
            {
                // Inserta el primer nodo
                _initialNode = newNode;
                return;
            }

            if (_isOrdered && newNode.ObjectType.CompareTo(_initialNode.ObjectType) < 0)
            {
                newNode.PointerNext = _initialNode;
                _initialNode = newNode;
                return;
            }

            ClassNode<Type>? currentNode = _initialNode;
            ClassNode<Type>? previousNode = null;

            while (currentNode != null)
            {
                if (_isOrdered && newNode.ObjectType.CompareTo(currentNode.ObjectType) <= 0)
                {
                    if (newNode.ObjectType.Equals(currentNode.ObjectType) && !_allowDuplicate)
                    {
                        return;
                        //throw new InvalidOperationException("Object Exist. Not Accept Data Duplicate");
                    }
                    break;
                }

                previousNode = currentNode;
                currentNode = currentNode.PointerNext;
            }

            // Inserta el nuevo nodo
            if (previousNode == null)
            {
                _initialNode = newNode;
            }
            else
            {
                previousNode.PointerNext = newNode;
            }
            newNode.PointerNext = currentNode;
        }

        private bool IsDuplicateObject(Type newObject)
        {
            foreach (var i in this.GetEnumerator())
            {
                if (i.Equals(newObject))
                {
                    return true;
                }
            }
            return false;
        }
        public Type RemoveNode(Type newObject)
        {
            if (_initialNode == null)
            {
                throw new InvalidOperationException("Empty list. No items to remove.");
            }

            if (_isOrdered && newObject.CompareTo(_initialNode.ObjectType) < 0)
            {
                throw new InvalidOperationException("Item not found. Not Exist.");
            }

            if (newObject.Equals(_initialNode.ObjectType))
            {
                _initialNode = _initialNode.PointerNext;
                return newObject;
            }

            ClassNode<Type>? currentNode = _initialNode;
            ClassNode<Type>? previousNode = null;

            while (currentNode != null && !currentNode.ObjectType.Equals(newObject))
            {
                previousNode = currentNode;
                currentNode = currentNode.PointerNext;
            }

            if (currentNode == null)
            {
                throw new InvalidOperationException("Item not found.");
            }

            previousNode!.PointerNext = currentNode.PointerNext;
            return newObject;
        }

        public Type ToFindObject(Type newObject)
        {
            if (NullList)
            {
                throw new InvalidOperationException("Empty list. No items to search.");
            }


            ClassNode<Type>? currentNode = _initialNode;

            while (currentNode != null && !newObject.Equals(currentNode.ObjectType))
            {
                currentNode = currentNode.PointerNext;
            }

            if (currentNode == null)
            {
                throw new InvalidOperationException("item not found.");
                
            }

            return currentNode.ObjectType;


        }
        public void ClearList()
        {
            if (NullList)
            {
                throw new InvalidOperationException("List already empty.");
            }

            ClassNode<Type>? currentNode = InitialNode;
            ClassNode<Type>? previusNode = null;

            while (currentNode != null)
            {
                previusNode = currentNode;
                currentNode = currentNode.PointerNext;
                previusNode.PointerNext = null;
                previusNode = null;
            }

            InitialNode = null;
            MessageBox.Show("All nodes have been removed.");
        }
        public IEnumerable<Type> GetEnumerator()
        {
            ClassNode<Type>? currentNode = _initialNode;
            while (currentNode != null)
            {
                
                yield return currentNode.ObjectType;
                currentNode = currentNode.PointerNext;
            }
        }

    }
}
