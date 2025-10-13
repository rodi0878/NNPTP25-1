using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1Tests
{
    [TestClass]
    public class PolynomialTests
    {
        private readonly Polynomial _polynomial;

        public PolynomialTests()
        {
            _polynomial = new Polynomial();
            _polynomial.Coefficients.Add(new ComplexNumber { Real = 1, Imaginary = 0 });
            _polynomial.Coefficients.Add(new ComplexNumber { Real = 0, Imaginary = 0 });
            _polynomial.Coefficients.Add(new ComplexNumber { Real = 1, Imaginary = 0 });
        }

        [TestMethod]
        public void PolynomeEvaluateTest01()
        {
            ComplexNumber actual = _polynomial.Evaluate(new ComplexNumber { Real = 0, Imaginary = 0 });
            ComplexNumber expected = new ComplexNumber { Real = 1, Imaginary = 0 };

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PolynomeEvaluateTest02()
        {
            ComplexNumber actual = _polynomial.Evaluate(new ComplexNumber { Real = 1, Imaginary = 0 });
            ComplexNumber expected = new ComplexNumber { Real = 2, Imaginary = 0 };

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PolynomeEvaluateTest03()
        {
            ComplexNumber actual = _polynomial.Evaluate(new ComplexNumber { Real = 2, Imaginary = 0 });
            ComplexNumber expected = new ComplexNumber { Real = 5.0000000000, Imaginary = 0 };

            Assert.AreEqual(expected, actual);
        }
        
        [TestMethod]
        public void PolynomeToStringTest()
        {
            var actualString = _polynomial.ToString();
            var expectedString = "(1 + 0i) + (0 + 0i)x + (1 + 0i)xx";
            
            Assert.AreEqual(expectedString, actualString);
        }
    }
}