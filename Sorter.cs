using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleListTarea
{
    abstract class Sorter<Type> where Type : IComparable<Type>, IEquatable<Type>, IComparableAttribute<Type>
    {
        public delegate int CriterioDeOrdenamiento(Type x, Type y, ComparableAttribute attribute);
        public delegate int ComparableAttribute(CoffeeShop x, CoffeeShop y);


        public static void Sort(ClassSinglyLinkedList<Type> list, CriterioDeOrdenamiento orden, ComparableAttribute ComparableAttribute)
        {

        }
        //public static bool AscendingOrder(Type x, Type y, ComparableAttribute attribute)
        //{
        //    if (attribute(x, y) > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        //public static bool DescendingOrder(Type x, Type y, ComparableAttribute attribute);
    }
}
