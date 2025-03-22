using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleListTarea
{
    internal class ClassBinaryTree<Type> where Type : IComparable<Type>, IEquatable<Type>
    {
        //C:\Program Files\Graphviz\bin

        public bool NullABB { get { return _rootNode == null; } }
        private ClassNodeDoubleReference<Type>? _rootNode;

        //Propiedad Asimetrica.
        public ClassNodeDoubleReference<Type>? RootNode { get { return _rootNode; } private set { _rootNode = value; } }

        public void InsertNode(Type newObject)
        {
            if (NullABB)
            {
                _rootNode = new ClassNodeDoubleReference<Type> { ObjectType = newObject };
            }
            else
            {
                AddChild(RootNode!, newObject);
            }
        }

        private void AddChild(ClassNodeDoubleReference<Type> currentNode, Type newObject)
        {
            if (newObject.Equals(currentNode.ObjectType))
                return; // O lanza una excepción si no deseas datos duplicados.

            var targetPointer = newObject.CompareTo(currentNode.ObjectType) < 0
                                ? currentNode.PointerBack
                                : currentNode.PointerNext;

            if (targetPointer != null)
            {
                AddChild(targetPointer, newObject);
            }
            else
            {
                var newNode = new ClassNodeDoubleReference<Type> { ObjectType = newObject };
                if (newObject.CompareTo(currentNode.ObjectType) < 0)
                    currentNode.PointerBack = newNode;
                else
                    currentNode.PointerNext = newNode;
            }
        }


        public IEnumerable<Type> PreOrderTraversal()
        {
            return PreOrderTraversal(_rootNode);
        }

        private IEnumerable<Type> PreOrderTraversal(ClassNodeDoubleReference<Type> currentNode)
        {
            if (currentNode == null)
                yield break;

            yield return currentNode.ObjectType;

            foreach (Type i in PreOrderTraversal(currentNode.PointerBack))
                yield return i;

            foreach (Type i in PreOrderTraversal(currentNode.PointerNext))
                yield return i;
        }

        public IEnumerable<Type> InOrderTraversal()
        {
            return InOrderTraversal(_rootNode);
        }

        private IEnumerable<Type> InOrderTraversal(ClassNodeDoubleReference<Type> currentNode)
        {
            if (currentNode == null)
                yield break;

            foreach (Type i in InOrderTraversal(currentNode.PointerBack))
                yield return i;

            yield return currentNode.ObjectType;

            foreach (Type i in InOrderTraversal(currentNode.PointerNext))
                yield return i;
        }

        public IEnumerable<Type> PostOrderTraversal()
        {
            return PostOrderTraversal(_rootNode);
        }

        private IEnumerable<Type> PostOrderTraversal(ClassNodeDoubleReference<Type> currentNode)
        {
            if (currentNode == null)
                yield break;

            foreach (Type i in PostOrderTraversal(currentNode.PointerBack))
                yield return i;

            foreach (Type i in PostOrderTraversal(currentNode.PointerNext))
                yield return i;

            yield return currentNode.ObjectType;
        }




        //public Type RemoveNode(Type removeObject)
        //{
        //    ClassNodeDoubleReference<Type> currentNode = _rootNode;
        //    ClassNodeDoubleReference<Type>? previusNode = null;

        //    if (NullABB)
        //        throw new InvalidOperationException("El árbol está vacío.");

        //    while (currentNode != null)
        //    {
        //        if (currentNode.TypeObject!.Equals(removeObject))
        //        {
        //            Type removedObject = currentNode.TypeObject;

        //            if (currentNode.NodePointerBack != null && currentNode.NodePointerNext != null)
        //            {
        //                var successor = FindNodeMin(currentNode.NodePointerNext);

        //                Type successorValue = successor.TypeObject;
        //            }
        //            else
        //            {
        //                ClassNodeDoubleReference<Type>? childNode = currentNode.NodePointerBack ?? currentNode.NodePointerNext;

        //                if (previusNode == null)
        //                {
        //                    _rootNode = childNode;
        //                }
        //                else if (previusNode.NodePointerBack == currentNode)
        //                {
        //                    previusNode.NodePointerBack = childNode;
        //                }
        //                else
        //                {
        //                    previusNode.NodePointerNext = childNode;
        //                }
        //            }

        //            return removedObject;
        //        }
        //        else
        //        {
        //            previusNode = currentNode;
        //            currentNode = (removeObject.CompareTo(currentNode.TypeObject) < 0) ? currentNode.NodePointerBack : currentNode.NodePointerNext;
        //        }
        //    }

        //    throw new Exception("Nodo no encontrado.");
        //}

        //private ClassNodeDoubleReference<Type> FindNodeMin(ClassNodeDoubleReference<Type> currentNode)
        //{
        //    while (currentNode.NodePointerBack != null)
        //    {
        //        currentNode = currentNode.NodePointerBack;
        //    }
        //    return currentNode;
        //}


        public Type RemoveNode(Type removeObject)
        {
            ClassNodeDoubleReference<Type> currentNode = _rootNode;
            ClassNodeDoubleReference<Type> previusNode = null;
            if (NullABB)
            {
                throw new InvalidOperationException("Null ABB");
            }


            while (currentNode != null)
            {


                if (currentNode.ObjectType.Equals(removeObject))
                {
                    Type removedObject = currentNode.ObjectType;
                    if (currentNode.PointerBack != null && currentNode.PointerNext != null)
                    {

                        ClassNodeDoubleReference<Type> currentMinNode = currentNode.PointerNext;
                        ClassNodeDoubleReference<Type> fatherMinNode = currentNode;
                        while (currentMinNode.PointerBack != null)
                        {
                            fatherMinNode = currentMinNode;
                            currentMinNode = currentMinNode.PointerBack;
                        }
                        currentNode.ObjectType = currentMinNode.ObjectType;

                        if (fatherMinNode.PointerBack == currentMinNode)
                        {
                            fatherMinNode.PointerBack = currentMinNode.PointerNext;
                        }
                        else
                        {
                            fatherMinNode.PointerNext = currentMinNode.PointerNext;
                        }
                        MessageBox.Show($"fatherminnode: {fatherMinNode.ObjectType}\ncurrentMinNode: {currentMinNode.ObjectType}");


                        return removedObject;

                    }



                    if (currentNode.PointerBack == null && currentNode.PointerNext == null)
                    {
                        if (previusNode.PointerBack == currentNode)
                        {
                            previusNode.PointerBack = null;

                        }
                        else
                        {
                            previusNode.PointerNext = null;
                        }
                        currentNode = null;

                        return removedObject;
                    }
                    if (currentNode.PointerBack == null && currentNode.PointerNext != null)
                    {

                        if (previusNode.PointerBack == currentNode)
                        {
                            previusNode = currentNode.PointerNext;
                        }
                        else
                        {
                            previusNode = currentNode.PointerBack;
                        }

                        return removedObject;

                    }
                    else
                    {

                        if (previusNode.PointerBack == currentNode)
                        {
                            previusNode = currentNode.PointerNext;
                        }
                        else
                        {
                            previusNode = currentNode.PointerBack;
                        }

                        return removedObject;
                    }

                }
                else
                {
                    previusNode = currentNode;
                    if (removeObject.CompareTo(currentNode.ObjectType) < 0)
                    {
                        currentNode = currentNode.PointerBack;
                    }
                    else { currentNode = currentNode.PointerNext; }
                }




            }
            throw new InvalidOperationException();
        }

        private ClassNodeDoubleReference<Type> FindNodeMin(ClassNodeDoubleReference<Type> currentNode)
        {
            ClassNodeDoubleReference<Type> previusNode = null;
            while (currentNode.PointerBack != null)
            {
                currentNode = currentNode.PointerBack;
            }

            previusNode.PointerBack = null;
            return currentNode;

        }
        public Type SearchNode(Type searchNode)
        {
            ClassNodeDoubleReference<Type> FoundedNode = new ClassNodeDoubleReference<Type>();

            foreach (Type currentType in this.PreOrderTraversal())
            {
                if (searchNode.Equals(currentType))
                {
                    return currentType;
                }
            }

            throw new Exception("Error. Node Not Founded.");

        }

        public void Clear()
        {

            if (NullABB)
            {
                return;
            }
            _rootNode = null;
            MessageBox.Show("Binary Tree is empty","Clear Data", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        public ClassNodeDoubleReference<Type> EncontrarNodoMinSubArbolDerecho(Type myObject)
        {
            if (RootNode == null)
            {
                MessageBox.Show("BST Empty");
            }
            ClassNodeDoubleReference<Type> CurrentNode = RootNode;

            while (CurrentNode != null)
            {
                if (CurrentNode.ObjectType.Equals(myObject))
                {
                    break;
                }
                if(myObject.CompareTo(CurrentNode.ObjectType) < 0)
                {
                    CurrentNode = CurrentNode.PointerBack;
                }
                else
                {
                    CurrentNode = CurrentNode.PointerNext;
                }
                
            }
            if (CurrentNode == null)
            {
                throw new Exception("Nodo No encontrado");
            }
            if (CurrentNode.PointerNext == null)
            {
                throw new Exception("El nodo no cuenta con hijos");
            }

            CurrentNode = CurrentNode.PointerNext;

            while (CurrentNode.PointerBack != null)
            {
                CurrentNode = CurrentNode.PointerBack;
            }

            return CurrentNode;

        }
    }
}
