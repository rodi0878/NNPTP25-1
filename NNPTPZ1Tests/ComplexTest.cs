using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1.Mathematics;
using System;
using System.Collections.Generic;

namespace NNPTPZ1.Mathematics.Tests
{
    [TestClass]
    public class ComplexTest
    {
        private const double Tolerance = 1e-10;

        private void AssertComplexEqual(Complex expected, Complex actual)
        {
            Assert.AreEqual(expected.Real, actual.Real, Tolerance, "Real parts differ");
            Assert.AreEqual(expected.Imaginary, actual.Imaginary, Tolerance, "Imaginary parts differ");
        }

        [TestMethod]
        public void AddTest()
        {
            Complex a = new Complex() { Real = 10, Imaginary = 20 };
            Complex b = new Complex() { Real = 1, Imaginary = 2 };
            Complex expected = new Complex() { Real = 11, Imaginary = 22 };
            Complex actual = a.Add(b);
            AssertComplexEqual(expected, actual);

            a = new Complex() { Real = 1, Imaginary = -1 };
            b = new Complex() { Real = 0, Imaginary = 0 };
            expected = new Complex() { Real = 1, Imaginary = -1 };
            actual = a.Add(b);
            AssertComplexEqual(expected, actual);
        }

        [TestMethod]
        public void SubtractTest()
        {
            Complex a = new Complex() { Real = 5, Imaginary = 3 };
            Complex b = new Complex() { Real = 2, Imaginary = 1 };
            Complex expected = new Complex() { Real = 3, Imaginary = 2 };
            Complex actual = a.Subtract(b);
            AssertComplexEqual(expected, actual);
        }

        [TestMethod]
        public void MultiplyTest()
        {
            Complex a = new Complex() { Real = 2, Imaginary = 3 };
            Complex b = new Complex() { Real = 4, Imaginary = -1 };
            Complex expected = new Complex() { Real = 11, Imaginary = 10 }; 
            Complex actual = a.Multiply(b);
            AssertComplexEqual(expected, actual);
        }

        [TestMethod]
        public void ToStringTest()
        {
            Complex a = new Complex() { Real = 10, Imaginary = 20 };
            string s = a.ToString();
            Assert.IsTrue(s.Contains("10") && s.Contains("20") && s.Contains("i"));

            a = new Complex() { Real = 1, Imaginary = -1 };
            s = a.ToString();
            Assert.IsTrue(s.Contains("1") && s.Contains("-1") && s.Contains("i"));
        }

        [TestMethod]
        public void PolynomialEvaluateTest()
        {
            Polynomial poly = new Polynomial();
            poly.Coefficients.Add(new Complex() { Real = 1, Imaginary = 0 });
            poly.Coefficients.Add(new Complex() { Real = 0, Imaginary = 0 });
            poly.Coefficients.Add(new Complex() { Real = 1, Imaginary = 0 });

            Complex result = poly.Evaluate(new Complex() { Real = 0, Imaginary = 0 });
            Complex expected = new Complex() { Real = 1, Imaginary = 0 };
            AssertComplexEqual(expected, result);

            result = poly.Evaluate(new Complex() { Real = 1, Imaginary = 0 });
            expected = new Complex() { Real = 2, Imaginary = 0 };
            AssertComplexEqual(expected, result);

            result = poly.Evaluate(new Complex() { Real = 2, Imaginary = 0 });
            expected = new Complex() { Real = 5, Imaginary = 0 };
            AssertComplexEqual(expected, result);

            string polyString = poly.ToString();
            Assert.IsTrue(polyString.Contains("1") && polyString.Contains("x"));
        }
    }
}
