
namespace DIContainer.Test
{
    /// <summary>
    /// Defines the <see cref="TestEnvironement" />.
    /// </summary>
    public abstract class TestEnvironement
    {
        /// <summary>
        /// Defines the <see cref="ISimpleTestingObject" />.
        /// </summary>
        protected interface ISimpleTestingObject
        {
            /// <summary>
            /// Gets or sets the TestStr.
            /// </summary>
            string TestStr { get; set; }

            /// <summary>
            /// Gets or sets the TestInt.
            /// </summary>
            int TestInt { get; set; }
        }

        /// <summary>
        /// Defines the <see cref="SimpleTestingObject" />.
        /// </summary>
        protected class SimpleTestingObject : ISimpleTestingObject
        {
            /// <summary>
            /// Gets or sets the TestStr.
            /// </summary>
            public string TestStr { get; set; }

            /// <summary>
            /// Gets or sets the TestInt.
            /// </summary>
            public int TestInt { get; set; }
        }

        /// <summary>
        /// Defines the <see cref="IParameterTestingObject" />.
        /// </summary>
        protected interface IParameterTestingObject
        {
            /// <summary>
            /// Gets the SimpleTestObj.
            /// </summary>
            ISimpleTestingObject SimpleTestObj { get; }

            /// <summary>
            /// Gets or sets the TestStr.
            /// </summary>
            string TestStr { get; set; }

            /// <summary>
            /// Gets or sets the TestInt.
            /// </summary>
            int TestInt { get; set; }
        }

        /// <summary>
        /// Defines the <see cref="InterfaceParameterTestingObject" />.
        /// </summary>
        protected class InterfaceParameterTestingObject : IParameterTestingObject
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="InterfaceParameterTestingObject"/> class.
            /// </summary>
            /// <param name="obj">The obj<see cref="ISimpleTestingObject"/>.</param>
            public InterfaceParameterTestingObject(ISimpleTestingObject obj) 
                => SimpleTestObj = obj;

            /// <summary>
            /// Gets the SimpleTestObj.
            /// </summary>
            public ISimpleTestingObject SimpleTestObj { get; }

            /// <summary>
            /// Gets or sets the TestStr.
            /// </summary>
            public string TestStr { get; set; }

            /// <summary>
            /// Gets or sets the TestInt.
            /// </summary>
            public int TestInt { get; set; }
        }

        /// <summary>
        /// Defines the <see cref="ClassParameterTestingObject" />.
        /// </summary>
        protected class ClassParameterTestingObject : InterfaceParameterTestingObject
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ClassParameterTestingObject"/> class.
            /// </summary>
            /// <param name="obj">The obj<see cref="SimpleTestingObject"/>.</param>
            public ClassParameterTestingObject(SimpleTestingObject obj)
                : base(obj)
            { }
        }

        /// <summary>
        /// Defines the <see cref="IParameterTestingObject2" />.
        /// </summary>
        protected interface IParameterTestingObject2
        {
            /// <summary>
            /// Gets or sets the ParamTestObj.
            /// </summary>
            IParameterTestingObject ParamTestObj { get; set; }

            /// <summary>
            /// Gets or sets the SimpleTestObj.
            /// </summary>
            ISimpleTestingObject SimpleTestObj { get; set; }
        }

        /// <summary>
        /// Defines the <see cref="InterfaceParameterTestingObject2" />.
        /// </summary>
        protected class InterfaceParameterTestingObject2 : IParameterTestingObject2
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="InterfaceParameterTestingObject2"/> class.
            /// </summary>
            /// <param name="paramTestObj">The paramTestObj<see cref="IParameterTestingObject"/>.</param>
            /// <param name="simpleTestObj">The simpleTestObj<see cref="ISimpleTestingObject"/>.</param>
            public InterfaceParameterTestingObject2(IParameterTestingObject paramTestObj, ISimpleTestingObject simpleTestObj)
            {
                ParamTestObj = paramTestObj;
                SimpleTestObj = simpleTestObj;
            }

            /// <summary>
            /// Gets or sets the ParamTestObj.
            /// </summary>
            public IParameterTestingObject ParamTestObj { get; set; }

            /// <summary>
            /// Gets or sets the SimpleTestObj.
            /// </summary>
            public ISimpleTestingObject SimpleTestObj { get; set; }
        }

        /// <summary>
        /// Defines the <see cref="ClassParameterTestingObject2" />.
        /// </summary>
        protected class ClassParameterTestingObject2 : InterfaceParameterTestingObject2
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ClassParameterTestingObject2"/> class.
            /// </summary>
            /// <param name="paramTestObj">The paramTestObj<see cref="ClassParameterTestingObject"/>.</param>
            /// <param name="simpleTestObj">The simpleTestObj<see cref="SimpleTestingObject"/>.</param>
            public ClassParameterTestingObject2(ClassParameterTestingObject paramTestObj, SimpleTestingObject simpleTestObj)
                : base(paramTestObj, simpleTestObj)
            { }
        }
    }
}
