using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1.Mathematics.Tests
{
    [TestClass]
    public class PolynomialTests
    {
        private Polynomial polynomial;
        private ComplexNumber expectedResult;

        [TestInitialize]
        public void TestInitialize()
        {
            polynomial = new Polynomial();
            polynomial.Coefficients.Add(new ComplexNumber { Real = 1, Imaginary = 0 });
            polynomial.Coefficients.Add(new ComplexNumber { Real = 0, Imaginary = 0 });
            polynomial.Coefficients.Add(new ComplexNumber { Real = 1, Imaginary = 0 });

            expectedResult = new ComplexNumber { Real = 1, Imaginary = 0 };
        }

        [TestMethod]
        public void PolynomialEvaluateAtZeroTest()
        {
            ComplexNumber result = polynomial.Evaluate(new ComplexNumber { Real = 0, Imaginary = 0 });
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void PolynomialEvaluateAtOneTest()
        {
            ComplexNumber result = polynomial.Evaluate(new ComplexNumber { Real = 1, Imaginary = 0 });
            expectedResult = new ComplexNumber { Real = 2, Imaginary = 0 };
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void PolynomialEvaluateAtTwoTest()
        {
            ComplexNumber result = polynomial.Evaluate(new ComplexNumber { Real = 2, Imaginary = 0 });
            expectedResult = new ComplexNumber { Real = 5.0000000000, Imaginary = 0 };
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void PolynomialToStringTest()
        {
            string expectedPolynomialString = "(1 + 0i) + (0 + 0i)x + (1 + 0i)xx";
            string actualPolynomialString = polynomial.ToString();
            Assert.AreEqual(expectedPolynomialString, actualPolynomialString);
        }
    }
}