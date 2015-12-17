namespace System.Immutable.Tests
{
    using TestClasses;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public sealed class ImmutableTests
    {
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "System.Immutable`1<System.Object>")]
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Unit Test")]
        [Test]
        public void GivenImmutableWhenConstructedAndConstructionDoesNotCreateNewInstanceThenArgumentExceptionThrown()
        {
            var objectInstance = new object();
            Action constructor = () => new Immutable<object>(() => objectInstance);

            constructor.ShouldThrow<ArgumentException>().WithMessage("Constructor does not create new instance of object");
        }

        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Unit Test")]
        [Test]
        public void GivenImmutableWhenPropertySetThenInvalidOperationExceptionThrown()
        {
            var immutableObject = new Immutable<ImmutableTest>(() => new ImmutableTest());

            Action propertySetAction = () => immutableObject.Object.Object = new object();

            propertySetAction.ShouldThrow<InvalidOperationException>();
        }

        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "System.Immutable`1<System.Object>")]
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Unit test")]
        [Test]
        public void GivenImmutableWhenObjectConstructorIsNullThenArgumentNullExceptionThrown()
        {
            Action constructor = () => new Immutable<object>(null);

            constructor.ShouldThrow<ArgumentNullException>().Where(x => x.ParamName == "objectConstructor");
        }

        [Test]
        public void GivenImmutableObjectWhenStateChangingMethodCalledThenStateRemainsUnchanged()
        {
            var testObject = new object();
            var immutableObject = new Immutable<ImmutableTest>(() => new ImmutableTest());

            immutableObject.Object.SetObject(testObject);
            immutableObject.Object.Object.Should().BeNull();
        }

        [Test]
        public void GivenImmutableObjectWhenMethodCalledThenReturnsValueAtExpectedState()
        {
            var testObject = new object();
            var immutableObject = new Immutable<ImmutableTest>(() => new ImmutableTest());

            var result = immutableObject.Object.SetObject(testObject);
            result.Should().Be(testObject);
        }
    }
} 
