
namespace DIContainer.Test
{
    public abstract class TestEnvironement
    {
        protected interface ISimpleTestingObject
        {
            string TestStr { get; set; }
            int TestInt { get; set; }
        }

        protected class SimpleTestingObject : ISimpleTestingObject
        {
            public string TestStr { get; set; }
            public int TestInt { get; set; }
        }

        protected interface IParameterTestingObject
        {
            ISimpleTestingObject TestObj { get; }
            string TestStr { get; set; }
            int TestInt { get; set; }
        }

        protected class InterfaceParameterTestingObject : IParameterTestingObject
        {
            public InterfaceParameterTestingObject(ISimpleTestingObject obj)
            {
                TestObj = obj;
            }

            public ISimpleTestingObject TestObj { get; }
            public string TestStr { get; set; }
            public int TestInt { get; set; }
        }

        protected class ClassParameterTestingObject : InterfaceParameterTestingObject
        {
            public ClassParameterTestingObject(SimpleTestingObject obj) : base(obj)
            { }
        }
    }
}
