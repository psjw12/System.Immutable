namespace System
{
    using System.Immutable;

    using Castle.DynamicProxy;

    public sealed class Immutable<T> where T : class
    {
        private readonly Func<T> objectConstructor;

        public T Object { get; private set; }

        public Immutable(Func<T> objectConstructor)
        {
            if (objectConstructor == null)
            {
                throw new ArgumentNullException(nameof(objectConstructor));
            }

            this.objectConstructor = objectConstructor;

            this.VerifyObjectConstructorCreatesNewInstance();
            this.CreateProxy();
        }

        private void VerifyObjectConstructorCreatesNewInstance()
        {
            if (object.ReferenceEquals(this.objectConstructor.Invoke(), this.objectConstructor.Invoke()))
            {
                throw new ArgumentException($"Constructor does not create new instance of {typeof(T).Name}");
            }
        }

        private void CreateProxy()
        {
            var proxyGenerator = new ProxyGenerator();

            this.Object = proxyGenerator.CreateClassProxy<T>(
                ProxyGenerationOptions.Default,
                new ImmutableInceptor(this.objectConstructor));
        }
    }
}
