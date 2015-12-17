using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Immutable
{
    using Castle.DynamicProxy;

    internal sealed class ImmutableInceptor : IInterceptor
    {
        private readonly Func<object> immutableObjectConstructor;

        public ImmutableInceptor(Func<object> immutableObjectConstructor)
        {
            this.immutableObjectConstructor = immutableObjectConstructor;
        }

        public void Intercept(IInvocation invocation)
        {
            var newImmutableObject = this.immutableObjectConstructor.Invoke();
            invocation.ReturnValue = invocation.Method.Invoke(newImmutableObject, invocation.Arguments);
        }
    }
}
