namespace System.Immutable.Tests.TestClasses
{
    public class ImmutableTest
    {
        public virtual object Object { get; set; }

        public virtual object SetObject(object @object)
        {
            this.Object = @object;
            return this.Object;
        }
    }
}
