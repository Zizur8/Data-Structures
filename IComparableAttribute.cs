using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleListTarea
{
    internal interface IComparableAttribute<Type>
    {
        abstract int ComparableAttribute(Type x, Type y);
    }
}
