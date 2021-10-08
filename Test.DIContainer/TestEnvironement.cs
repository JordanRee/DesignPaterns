
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
            ISimpleTestingObject SimpleTestObj { get; }
            string TestStr { get; set; }
            int TestInt { get; set; }
        }

        protected class InterfaceParameterTestingObject : IParameterTestingObject
        {
            public InterfaceParameterTestingObject(ISimpleTestingObject obj)
            {
                SimpleTestObj = obj;
            }

            public ISimpleTestingObject SimpleTestObj { get; }
            public string TestStr { get; set; }
            public int TestInt { get; set; }
        }

        protected class ClassParameterTestingObject : InterfaceParameterTestingObject
        {
            public ClassParameterTestingObject(SimpleTestingObject obj) 
                : base(obj)
            { }
        }

        protected interface IParameterTestingObject2
        {
            IParameterTestingObject ParamTestObj { get; set; }
            ISimpleTestingObject SimpleTestObj { get; set; }
        }

        protected class InterfaceParameterTestingObject2 : IParameterTestingObject2
        {
            public InterfaceParameterTestingObject2(IParameterTestingObject paramTestObj, ISimpleTestingObject simpleTestObj)
            {
                ParamTestObj = paramTestObj;
                SimpleTestObj = simpleTestObj;
            }

            public IParameterTestingObject ParamTestObj { get; set; }
            public ISimpleTestingObject SimpleTestObj { get; set; }
        }

        protected class ClassParameterTestingObject2 : InterfaceParameterTestingObject2
        {
            public ClassParameterTestingObject2(ClassParameterTestingObject paramTestObj, SimpleTestingObject simpleTestObj)
                : base(paramTestObj, simpleTestObj)
            { }
        }
    }
}
