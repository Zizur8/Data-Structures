using System;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleListTarea
{
    internal class ClassQueue<Type> where Type: IEquatable<Type>
    {

        public ClassQueue()
        {
            _InitialNode = null;
            _LastNode = null;

        }

        private ClassNode<Type>? _InitialNode;

        public ClassNode<Type>? InitialNode
        {
            get { return _InitialNode; }
            set { _InitialNode = value; }
        }

        private ClassNode<Type>? _LastNode;

        public ClassNode<Type>? LastNode
        {
            get { return _LastNode; }
            set { _LastNode = value; }
        }


        public bool NullList { get { return _InitialNode == null; } }

        public void Enqueue(Type newType)
        {
            ClassNode<Type> newNode = new ClassNode<Type>();
            newNode.ObjectType = newType;

            if (NullList)
            {
                InitialNode = newNode;
                LastNode = newNode;
                return;
            }


            newNode.PointerNext = LastNode;
            LastNode = newNode;



        }
        public Type Dequeue()
        {
            if (NullList)
            {
                throw new Exception("Empty Queue.");
            }


            ClassNode<Type>? currentNode = LastNode;
            Type dequeueObject = InitialNode!.ObjectType;

            if (currentNode!.PointerNext == null)
            {
                dequeueObject = InitialNode.ObjectType;
                InitialNode = null;
                LastNode = null;
                return dequeueObject;
            }

            while (currentNode!.PointerNext != InitialNode)
            {

                currentNode = currentNode.PointerNext;
            }



            dequeueObject = InitialNode.ObjectType;


            currentNode.PointerNext = null;
            InitialNode = currentNode;
            
            return dequeueObject;


        }

        public Type Dequeue(Type dequeueType)
        {
            if (NullList)
            {
                throw new Exception("Empty Queue.");
            }


            if (dequeueType.Equals(InitialNode!.ObjectType))
            {

                return Dequeue();
            }

            ClassNode<Type>? currentNode = LastNode!;
            ClassNode<Type>? previusNode = null;

            while (currentNode != null)
            {
                if (currentNode.ObjectType.Equals(dequeueType))
                {
                    
                    break;
                }
                previusNode = currentNode;
                currentNode = currentNode.PointerNext;
            }

            if(currentNode == null) { throw new InvalidOperationException("Item Not Found Or Not Exist"); }

            if (previusNode == null)
            {
                
                LastNode = LastNode!.PointerNext;
                
            }
            else
            {
                
                previusNode.PointerNext = currentNode.PointerNext;
            }

            Type removeObject = currentNode.ObjectType;
            currentNode.PointerNext = null;
            currentNode = null;
            return removeObject;
        }


        public Type Peek()
        {
            if (NullList)
            {
                throw new Exception("Empty Queue.");
            }
            return InitialNode!.ObjectType;
        }
        public Type Rear()
        {
            if (NullList)
            {
                throw new Exception("Empty Queue.");
            }
            return LastNode!.ObjectType;
        }

        public void Clear()
        {
            if (NullList)
            {
                throw new Exception("Empty Queue.");
            }
            ClassNode<Type>? currentNode = LastNode;
            ClassNode<Type>? previusNode;
            while (currentNode != null)
            {
                previusNode = currentNode;
                previusNode.PointerNext = null;
                previusNode = null;
                currentNode = currentNode.PointerNext;
            }

            InitialNode = null;
            LastNode = null;

        }

        public Type ToFindObject(Type newType)
        {
            if (NullList) { throw new InvalidOperationException(); }
            foreach (Type item in GetEnumerator())
            {
                if (item.Equals(newType))
                {
                    return item;
                }
            }
            throw new Exception("Data Not Found Or Not Exist.");


        }

        public IEnumerable<Type> GetEnumerator()
        {
            if (!NullList)
            {
                ClassNode<Type>? currentNode = LastNode!;
                while (currentNode != null)
                {
                    yield return currentNode.ObjectType;
                    currentNode = currentNode.PointerNext;
                }
            }

        }

    }
}
