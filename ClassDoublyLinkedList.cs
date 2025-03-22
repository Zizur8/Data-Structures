using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleListTarea
{
    internal class ClassDoublyLinkedList<Type> where Type : IComparable<Type>,IEquatable<Type>
    {
        public ClassDoublyLinkedList()
        {
            _initialNode = null;
            _lastNode = null;
            _isOrderedList = true;
            _allowDuplicatedData = false;
        }
        public ClassDoublyLinkedList(bool blnOrderedList, bool blnallowDuplicateData)
        {
            _isOrderedList = blnOrderedList;
            _allowDuplicatedData = blnallowDuplicateData;
            _initialNode = null;
            _lastNode = null;
        }
        ~ClassDoublyLinkedList()
        {
            ClearList();
        }
        private bool _isOrderedList;
        private bool _allowDuplicatedData;

        private ClassNodeDoubleReference<Type>? _initialNode;
        public ClassNodeDoubleReference<Type>? InitialNode
        {
            get { return _initialNode; }
            private set { _initialNode = value; }
        }
        private ClassNodeDoubleReference<Type>? _lastNode;
        public ClassNodeDoubleReference<Type>? LastNode
        {
            get { return _lastNode; }
            private set { _lastNode = value; }
        }

        public bool NullList { get { return InitialNode == null; } }

        public IEnumerable<Type> Forward
        {
            get
            {
                ClassNodeDoubleReference<Type>? currentNode = InitialNode;
                while (currentNode != null)
                {
                    yield return currentNode.ObjectType;
                    currentNode = currentNode.PointerNext;
                }
            }
            
        }
        public IEnumerable<Type> Backwards
        {
            get
            {
                ClassNodeDoubleReference<Type>? currentNode = LastNode;
                while (currentNode != null)
                {
                    yield return currentNode.ObjectType;
                    currentNode = currentNode.PointerBack;
                }
            }
        }

        private bool IsDuplicateObject(Type newObject)
        {
            foreach (var i in Backwards) // Utiliza el enumerador para recorrer la lista
            {
                if (i.Equals(newObject))
                {
                    return true;
                }
            }
            return false;
        }

        public void InsertNode(Type newType)
        {
            ClassNodeDoubleReference<Type> newNode = new ClassNodeDoubleReference<Type>();
            newNode.ObjectType = newType;

            if (NullList)
            {
                //Insertar primer nodo
                newNode.PointerNext = null;
                newNode.PointerBack = null;
                InitialNode = newNode;
                LastNode = newNode;
                return;
            }

            if (!_isOrderedList && !_allowDuplicatedData)
            {
                if (IsDuplicateObject(newType))
                {
                    return;
                    //throw new Exception("Object Exist. Not Accept Data Duplicate");
                }

                if (LastNode != null)
                {
                    LastNode.PointerNext = newNode;
                }

                newNode.PointerNext = null;
                newNode.PointerBack = LastNode;
                LastNode = newNode;
                return;  // Termina aquí después de insertar
            }


            if (!_allowDuplicatedData)
            {
                if (IsDuplicateObject(newType))
                {
                    //throw new Exception("Object Exist. Not Accept Data Duplicate");
                    return;
                }

                
            }

            //if (_isOrderedList && newType.CompareTo(LastNode.ObjectType) > 0)
            //{
            //    LastNode.PointerNext = newNode;
            //    newNode.PointerNext = null;
            //    newNode.PointerBack = LastNode;
            //    LastNode = newNode;
            //}

            ClassNodeDoubleReference<Type>? currentNode = InitialNode;
            ClassNodeDoubleReference<Type>? previusNode = null;

            while (currentNode != null)
            {

                if (newType.CompareTo(currentNode.ObjectType) < 0)
                {
                    break;

                }
                previusNode = currentNode;
                currentNode = currentNode.PointerNext;
            }

            if (previusNode == null)
            {

                newNode.PointerNext = _initialNode;
                newNode.PointerBack = null;
                _initialNode!.PointerBack = newNode;
                _initialNode = newNode;
                return;
            }
            if (currentNode == null)
            {
                LastNode = newNode;  // Este es el nuevo último nodo
            }


            previusNode.PointerNext = newNode;
            newNode.PointerBack = previusNode;
            newNode.PointerNext = currentNode;

            if (currentNode != null)
            {
                currentNode.PointerBack = newNode;
            }
            else
            {
                LastNode = newNode;
            }


        }
        public Type RemoveNode(Type newType)
        {

            if (_initialNode == null)
            {
                throw new InvalidOperationException("Empty list. No items to remove.");
            }

            Type objectRemove;
            if (newType.Equals(_initialNode.ObjectType))
            {
                //Cuandp eliminmos el Primer Nodo.
                objectRemove = _initialNode.ObjectType;
                _initialNode = _initialNode.PointerNext;

                if (_initialNode != null) { _initialNode.PointerBack = null; } else { _lastNode = null; }

                return objectRemove;
            }
            if (_lastNode != null && newType.Equals(_lastNode.ObjectType))
            {
                objectRemove = _lastNode.ObjectType;
                _lastNode = _lastNode.PointerBack;
                if (_lastNode != null) { _lastNode.PointerNext = null; }
                return objectRemove;
            }


            ClassNodeDoubleReference<Type>? currentNode = LastNode;//Acuardate de cambiar por donde empezar
            ClassNodeDoubleReference<Type> previusNode = new ClassNodeDoubleReference<Type>();

            while (currentNode != null && !newType.Equals(currentNode.ObjectType))
            {
                //De Inicio a fin
                //previusNode = currentNode;
                //currentNode = currentNode.PointerNext;

                //De Fin a inicio
                previusNode = currentNode;
                currentNode = currentNode.PointerBack;
            }

            if (currentNode == null)
            {
                throw new InvalidOperationException("item not found Or Not Exist.");
            }

            objectRemove = currentNode.ObjectType;

            if (previusNode != null)
            {
                //De inicio a fin
                //previusNode.PointerNext = currentNode.PointerNext;
                //De fin a inicio
                previusNode.PointerBack = currentNode.PointerBack;
            }
            if (currentNode.PointerBack != null)//Cambiar el pointer
            {
                //inicio a fin
                //currentNode = currentNode.PointerNext;
                //currentNode.PointerBack = previusNode;
                //fin a inicio
                currentNode = currentNode.PointerBack;
                currentNode.PointerNext = previusNode;
            }

            return objectRemove;
        }

        public Type ToFindObject(Type newType)
        {
            foreach(Type i in Backwards)
            {
                if (i.Equals(newType))
                {
                    return i;
                }
            }
            //foreach (Type i in Forward)
            //{
            //    if (i.Equals(newType))
            //    {
            //        return i;
            //    }
            //}
            throw new InvalidOperationException("Object Doesn't Exist Or Cannot Be Found");
        }

        public void ClearList()
        {
            if (NullList)
            {
                throw new InvalidOperationException("List already empty.");
            }
            ClassNodeDoubleReference<Type>? current = LastNode;
            ClassNodeDoubleReference<Type>? previus = null;

            //while (current != null)
            //{
            //    De Inicio a fin
            //    previus = current;
            //    current = current.PointerNext;

            //    previus.PointerBack = null;
            //    previus.PointerNext = null;
            //    previus = null;
            //}

            while (current != null)
            {
                //De Fin a Inicio
                previus = current;
                current = current.PointerBack;
                previus.PointerBack = null;
                previus.PointerNext = null;
                previus = null;
            }

            InitialNode = null;
            LastNode = null;
            MessageBox.Show("All Nodes Have Been Deleted");
        }
        public void PrintList()
        {
            foreach (Type currentNode in Backwards)
            {
                MessageBox.Show($"{currentNode.ToString()}");
            }
        }
    }
}