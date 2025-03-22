using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleListTarea
{
    internal class ClassStack<Type> where Type : IEquatable<Type>
    {
        private ClassNode<Type>? _TopNode;

        public ClassNode<Type>? TopNode
        {
            get { return _TopNode; }
            set { _TopNode = value; }
        }

        public ClassStack()
        {

            _TopNode = null;

        }
        public bool NullStack { get { return TopNode == null; } }
        public void Push(Type newType)
        {
            if (NullStack)
            {
                TopNode = new ClassNode<Type>();
                TopNode.ObjectType = newType;
                TopNode.PointerNext = null;
                return;
            }
            ClassNode<Type> newNode = new ClassNode<Type>();
            newNode.ObjectType = newType;
            newNode.PointerNext = TopNode;
            TopNode = newNode;

        }
        public Type Pop()
        {
            if (TopNode == null)
            {
                throw new Exception("Empty Stack.");
            }
            Type typePop = TopNode.ObjectType;
            TopNode = TopNode.PointerNext;
            return typePop;
        }
        public Type Pop(Type typePop)
        {
            if (TopNode == null)
            {
                throw new InvalidOperationException();

            }
            Type removeType;
            ClassNode<Type>? currentNode = TopNode;
            ClassNode<Type>? previusNode = null;
            
            while (currentNode != null)
            {
                if (currentNode.ObjectType.Equals(typePop))
                {

                    break;
                }
                previusNode = currentNode;
                currentNode = currentNode.PointerNext;
            }
            if (currentNode == null) { throw new InvalidOperationException("Item Not Exist."); }
            if (previusNode != null) { previusNode.PointerNext = currentNode.PointerNext;  } else { return Pop(); }
            removeType = currentNode.ObjectType;
            return removeType;
            

        }
        public Type ToFindObject(Type newType)
        {
            if (NullStack)
            {
                throw new Exception("Empty Stack.");
            }
            foreach (Type  item in GetEnumerator())
            {
                if (item.Equals(newType))
                {
                    return item;
                }
            }

            throw new InvalidOperationException("Object Not Found Or Not Exist.");
        }
        public Type Peek()
        {

            if (TopNode != null)
            {
                return TopNode.ObjectType;
            }

            throw new Exception("Empty Stack.");
        }

        public IEnumerable<Type> GetEnumerator()
        {

            ClassNode<Type>? currentNode = TopNode;
            ClassNode<Type> previusNode;
            while (currentNode != null)
            {
                previusNode = currentNode;
                currentNode = currentNode.PointerNext;
                yield return previusNode.ObjectType;

            }
        }

        public void Clear()
        {
            if (NullStack)
            {
                throw new Exception("Empty Stack.");
            }
            ClassNode<Type>? currentNode = TopNode;
            ClassNode<Type>? previusNode;
            while (currentNode != null)
            {
                previusNode = currentNode;
                previusNode.PointerNext = null;
                previusNode = null;
                currentNode = currentNode.PointerNext;
            }
            TopNode = null;
        }

    }
}
