using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1.Mathematics;
using System;
using System.Collections.Generic;

namespace NNPTPZ1.Mathematics.Tests
{
    [TestClass]
    public class CplxTests
    {
        private const double Tolerance = 1e-10;

        private void AssertCplxEqual(Cplx expected, Cplx actual)
        {
            Assert.AreEqual(expected.Real, actual.Real, Tolerance, "Real parts differ");
            Assert.AreEqual(expected.Imaginary, actual.Imaginary, Tolerance, "Imaginary parts differ");
        }

        [TestMethod]
        public void AddTest()
        {
            Cplx a = new Cplx() { Real = 10, Imaginary = 20 };
            Cplx b = new Cplx() { Real = 1, Imaginary = 2 };
            Cplx expected = new Cplx() { Real = 11, Imaginary = 22 };
            Cplx actual = a.Add(b);
            AssertCplxEqual(expected, actual);

            a = new Cplx() { Real = 1, Imaginary = -1 };
            b = new Cplx() { Real = 0, Imaginary = 0 };
            expected = new Cplx() { Real = 1, Imaginary = -1 };
            actual = a.Add(b);
            AssertCplxEqual(expected, actual);
        }

        [TestMethod]
        public void SubtractTest()
        {
            Cplx a = new Cplx() { Real = 5, Imaginary = 3 };
            Cplx b = new Cplx() { Real = 2, Imaginary = 1 };
            Cplx expected = new Cplx() { Real = 3, Imaginary = 2 };
            Cplx actual = a.Subtract(b);
            AssertCplxEqual(expected, actual);
        }

        [TestMethod]
        public void MultiplyTest()
        {
            Cplx a = new Cplx() { Real = 2, Imaginary = 3 };
            Cplx b = new Cplx() { Real = 4, Imaginary = -1 };
            Cplx expected = new Cplx() { Real = 11, Imaginary = 10 }; 
            Cplx actual = a.Multiply(b);
            AssertCplxEqual(expected, actual);
        }

        [TestMethod]
        public void ToStringTest()
        {
            Cplx a = new Cplx() { Real = 10, Imaginary = 20 };
            string s = a.ToString();
            Assert.IsTrue(s.Contains("10") && s.Contains("20") && s.Contains("i"));

            a = new Cplx() { Real = 1, Imaginary = -1 };
            s = a.ToString();
            Assert.IsTrue(s.Contains("1") && s.Contains("-1") && s.Contains("i"));
        }

        [TestMethod]
        public void PolyEvaluateTest()
        {
            Poly poly = new Poly();
            poly.Coefficients.Add(new Cplx() { Real = 1, Imaginary = 0 });
            poly.Coefficients.Add(new Cplx() { Real = 0, Imaginary = 0 });
            poly.Coefficients.Add(new Cplx() { Real = 1, Imaginary = 0 });

            Cplx result = poly.Evaluate(new Cplx() { Real = 0, Imaginary = 0 });
            Cplx expected = new Cplx() { Real = 1, Imaginary = 0 };
            AssertCplxEqual(expected, result);

            result = poly.Evaluate(new Cplx() { Real = 1, Imaginary = 0 });
            expected = new Cplx() { Real = 2, Imaginary = 0 };
            AssertCplxEqual(expected, result);

            result = poly.Evaluate(new Cplx() { Real = 2, Imaginary = 0 });
            expected = new Cplx() { Real = 5, Imaginary = 0 };
            AssertCplxEqual(expected, result);

            string polyString = poly.ToString();
            Assert.IsTrue(polyString.Contains("1") && polyString.Contains("x"));
        }
    }
}
