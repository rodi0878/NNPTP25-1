using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NNPTPZ1;

namespace NNPTPZ1.Mathematics.Tests
{
    [TestClass]
    public class ComplexNumberTests
    {
        [TestMethod]
        public void AddTest()
        {
            var a = new ComplexNumber { Real = 10, Imag = 20 };
            var b = new ComplexNumber { Real = 1, Imag = 2 };

            var actual = a.Add(b);
            var expected = new ComplexNumber { Real = 11, Imag = 22 };

            Assert.AreEqual(expected, actual);

            var e2 = "(10 + 20i)";
            var r2 = a.ToString();
            Assert.AreEqual(e2, r2);

            e2 = "(1 + 2i)";
            r2 = b.ToString();
            Assert.AreEqual(e2, r2);

            a = new ComplexNumber { Real = 1, Imag = -1 };
            b = new ComplexNumber { Real = 0, Imag = 0 };
            expected = new ComplexNumber { Real = 1, Imag = -1 };

            actual = a.Add(b);
            Assert.AreEqual(expected, actual);

            e2 = "(1 + -1i)";
            r2 = a.ToString();
            Assert.AreEqual(e2, r2);

            e2 = "(0 + 0i)";
            r2 = b.ToString();
            Assert.AreEqual(e2, r2);
        }

        [TestMethod]
        public void PolynomialEvalTest()
        {
            // poly: 1 + 0*x + 1*x^2
            var poly = new Polynomial();
            poly.Coefficients.Add(new ComplexNumber(1, 0));
            poly.Coefficients.Add(new ComplexNumber(0, 0));
            poly.Coefficients.Add(new ComplexNumber(1, 0));

            var result = poly.Eval(new ComplexNumber(0, 0));
            var expected = new ComplexNumber(1, 0);
            Assert.AreEqual(expected, result);

            result = poly.Eval(new ComplexNumber(1, 0));
            expected = new ComplexNumber(2, 0);
            Assert.AreEqual(expected, result);

            result = poly.Eval(new ComplexNumber(2, 0));
            expected = new ComplexNumber(5.0, 0);
            Assert.AreEqual(expected, result);

            var r2 = poly.ToString();
            var e2 = "(1 + 0i) + (0 + 0i)x + (1 + 0i)xx";
            Assert.AreEqual(e2, r2);
        }
    }
}


