using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NNPTPZ1.Mathematics.Tests
{
    [TestClass]
    public class ComplexNumberTests
    {
        private ComplexNumber a;
        private ComplexNumber b;
        private ComplexNumber expected;
        private string expectedString;
        private string actualString;

        [TestInitialize]
        public void TestInitialize()
        {
            a = new ComplexNumber { Real = 10, Imaginary = 20 };
            b = new ComplexNumber { Real = 1, Imaginary = 2 };
        }

        [TestMethod]
        public void AddComplexNumbersTest()
        {
            ComplexNumber result = a.Add(b);
            expected = new ComplexNumber { Real = 11, Imaginary = 22 };

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ComplexNumberToStringTest_A()
        {
            expectedString = "(10 + 20i)";
            actualString = a.ToString();

            Assert.AreEqual(expectedString, actualString);
        }

        [TestMethod]
        public void ComplexNumberToStringTest_B()
        {
            expectedString = "(1 + 2i)";
            actualString = b.ToString();

            Assert.AreEqual(expectedString, actualString);
        }

        [TestMethod]
        public void AddComplexNumbersWithZeroImaginaryTest()
        {
            a = new ComplexNumber { Real = 1, Imaginary = -1 };
            b = new ComplexNumber { Real = 0, Imaginary = 0 };

            ComplexNumber result = a.Add(b);
            expected = new ComplexNumber { Real = 1, Imaginary = -1 };

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ComplexNumberToStringTest_C()
        {
            a = new ComplexNumber { Real = 1, Imaginary = -1 };
            b = new ComplexNumber { Real = 0, Imaginary = 0 };

            expectedString = "(1 + -1i)";
            actualString = a.ToString();

            Assert.AreEqual(expectedString, actualString);
        }

        [TestMethod]
        public void ComplexNumberToStringTest_D()
        {
            a = new ComplexNumber { Real = 1, Imaginary = -1 };
            b = new ComplexNumber { Real = 0, Imaginary = 0 };

            expectedString = "(0 + 0i)";
            actualString = b.ToString();

            Assert.AreEqual(expectedString, actualString);
        }
    }
}