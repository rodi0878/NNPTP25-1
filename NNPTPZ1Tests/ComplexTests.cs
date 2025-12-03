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
    [TestClass()]
    public class ComplexTests
    {

        [TestMethod()]
        public void AddTest()
        {
            Complex a = new Complex()
            {
                Real = 10,
                Imaginary = 20
            };
            Complex b = new Complex()
            {
                Real = 1,
                Imaginary = 2
            };

            Complex actual = a.Add(b);
            Complex shouldBe = new Complex()
            {
                Real = 11,
                Imaginary = 22
            };

            Assert.AreEqual(shouldBe, actual);

            var expectedString = "(10 + 20i)";
            var resultString = a.ToString();
            Assert.AreEqual(expectedString, resultString);
            expectedString = "(1 + 2i)";
            resultString = b.ToString();
            Assert.AreEqual(expectedString, resultString);

            a = new Complex()
            {
                Real = 1,
                Imaginary = -1
            };
            b = new Complex() { Real = 0, Imaginary = 0 };
            shouldBe = new Complex() { Real = 1, Imaginary = -1 };
            actual = a.Add(b);
            Assert.AreEqual(shouldBe, actual);

            expectedString = "(1 + -1i)";
            resultString = a.ToString();
            Assert.AreEqual(expectedString, resultString);

            expectedString = "(0 + 0i)";
            resultString = b.ToString();
            Assert.AreEqual(expectedString, resultString);
        }

        [TestMethod()]
        public void AddTestPolynomial()
        {
            Polynomial polynomial = new Polynomial();
            polynomial.Coefficients.Add(new Complex() { Real = 1, Imaginary = 0 });
            polynomial.Coefficients.Add(new Complex() { Real = 0, Imaginary = 0 });
            polynomial.Coefficients.Add(new Complex() { Real = 1, Imaginary = 0 });
            Complex result = polynomial.Evaluate(new Complex() { Real = 0, Imaginary = 0 });
            var expected = new Complex() { Real = 1, Imaginary = 0 };
            Assert.AreEqual(expected, result);
            result = polynomial.Evaluate(new Complex() { Real = 1, Imaginary = 0 });
            expected = new Complex() { Real = 2, Imaginary = 0 };
            Assert.AreEqual(expected, result);
            result = polynomial.Evaluate(new Complex() { Real = 2, Imaginary = 0 });
            expected = new Complex() { Real = 5.0000000000, Imaginary = 0 };
            Assert.AreEqual(expected, result);

            var resultString = polynomial.ToString();
            var expectedString = "(1 + 0i) + (0 + 0i)x + (1 + 0i)xx";
            Assert.AreEqual(expectedString, resultString);
        }
    }
}


