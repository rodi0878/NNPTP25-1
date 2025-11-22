using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1.Mathematics;
using System;

namespace NNPTPZ1.Mathematics.Tests
{
    [TestClass]
    public class ComplexNumberTests
    {
        [TestMethod]
        public void AddTest()
        {
            ComplexNumber a = new ComplexNumber
            {
                Real = 10,
                Imaginary = 20
            };
            ComplexNumber b = new ComplexNumber
            {
                Real = 1,
                Imaginary = 2
            };

            ComplexNumber actual = a.Add(b);
            ComplexNumber expected = new ComplexNumber
            {
                Real = 11,
                Imaginary = 22
            };

            Assert.AreEqual(expected, actual);

            string expectedString = "(10 + 20i)";
            string actualString = a.ToString();
            Assert.AreEqual(expectedString, actualString);

            expectedString = "(1 + 2i)";
            actualString = b.ToString();
            Assert.AreEqual(expectedString, actualString);

            a = new ComplexNumber
            {
                Real = 1,
                Imaginary = -1
            };
            b = new ComplexNumber
            {
                Real = 0,
                Imaginary = 0
            };
            expected = new ComplexNumber
            {
                Real = 1,
                Imaginary = -1
            };
            actual = a.Add(b);
            Assert.AreEqual(expected, actual);

            expectedString = "(1 + -1i)";
            actualString = a.ToString();
            Assert.AreEqual(expectedString, actualString);

            expectedString = "(0 + 0i)";
            actualString = b.ToString();
            Assert.AreEqual(expectedString, actualString);
        }

        [TestMethod]
        public void AddTestPolynomial()
        {
            Polynomial polynomial = new Polynomial();
            polynomial.Coefficients.Add(new ComplexNumber { Real = 1, Imaginary = 0 });
            polynomial.Coefficients.Add(new ComplexNumber { Real = 0, Imaginary = 0 });
            polynomial.Coefficients.Add(new ComplexNumber { Real = 1, Imaginary = 0 });

            ComplexNumber result = polynomial.Evaluate(new ComplexNumber { Real = 0, Imaginary = 0 });
            ComplexNumber expected = new ComplexNumber { Real = 1, Imaginary = 0 };
            Assert.AreEqual(expected, result);

            result = polynomial.Evaluate(new ComplexNumber { Real = 1, Imaginary = 0 });
            expected = new ComplexNumber { Real = 2, Imaginary = 0 };
            Assert.AreEqual(expected, result);

            result = polynomial.Evaluate(new ComplexNumber { Real = 2, Imaginary = 0 });
            expected = new ComplexNumber { Real = 5.0000000000, Imaginary = 0 };
            Assert.AreEqual(expected, result);

            string expectedPolynomialString = "(1 + 0i) + (0 + 0i)x + (1 + 0i)xx";
            string actualPolynomialString = polynomial.ToString();
            Assert.AreEqual(expectedPolynomialString, actualPolynomialString);
        }
    }
}