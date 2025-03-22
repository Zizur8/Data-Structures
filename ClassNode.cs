using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleListTarea
{
    internal class ClassNode<Type>
    {
        
        ~ClassNode()
        {
            _pointerNext = null;
        }

        private Type _objectType = default(Type)!;
        public Type ObjectType
        {
            get { return _objectType; }
            set { _objectType = value; }
        }
        private ClassNode<Type>? _pointerNext;

        public ClassNode<Type>? PointerNext
        {
            get { return _pointerNext; }
            set { _pointerNext = value; }
        }
        public override string ToString()
        {
            return $"{ObjectType}";
        }
    }
}
