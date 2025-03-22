using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleListTarea
{
    internal class ClassNodeDoubleReference<Type>
    {
        public ClassNodeDoubleReference()
        {
            _pointerBack = null;
            _pointerNext = null;

        }
        private Type _objectType = default!;

        public Type ObjectType
        {
            get { return _objectType; }
            set { _objectType = value; }
        }

        private ClassNodeDoubleReference<Type>? _pointerNext;

        public ClassNodeDoubleReference<Type>? PointerNext
        {
            get { return _pointerNext; }
            set { _pointerNext = value; }
        }
        private ClassNodeDoubleReference<Type>? _pointerBack;

        public ClassNodeDoubleReference<Type>? PointerBack
        {
            get { return _pointerBack; }
            set { _pointerBack = value; }
        }

        public override string ToString()
        {
            return ObjectType.ToString();

        }
    }
}
