using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace NNPTPZ1.Mathematics.Tests
{
    [TestClass()]
    public class PolynomialTests
    {
        private const double Tolerance = 1e-10;

        [TestMethod]
        public void Constructor_ShouldStoreCoefficientsInCorrectOrder()
        {
            var p = new Polynomial(
                new ComplexNumber(1),
                new ComplexNumber(2),
                new ComplexNumber(3)
            );

            var result = p.EvaluateAt(1);
            // 1 + 2x + 3x^2 at x=1 = 6
            Assert.AreEqual(6, result.RealPart, Tolerance);
        }

        [TestMethod]
        public void AddCoefficient_ShouldExtendPolynomial()
        {
            var p = new Polynomial(new ComplexNumber(1));
            p.AddCoefficient(new ComplexNumber(2)); // 1 + 2x

            var result = p.EvaluateAt(1);
            Assert.AreEqual(3, result.RealPart, Tolerance);
        }

        [TestMethod]
        public void EvaluateAt_ShouldHandleDoubleInput()
        {
            var p = new Polynomial(new ComplexNumber(1), new ComplexNumber(2)); // 1 + 2x
            var result = p.EvaluateAt(3.0);

            // f(3) = 1 + 2*3 = 7
            Assert.AreEqual(7, result.RealPart, Tolerance);
            Assert.AreEqual(0, result.ImaginaryPart, Tolerance);
        }

        [TestMethod]
        public void EvaluateAt_ShouldHandleComplexInput()
        {
            var p = new Polynomial(
                new ComplexNumber(1, 1),
                new ComplexNumber(2, 0)
            );

            var z = new ComplexNumber(1, 1);
            var result = p.EvaluateAt(z);

            // f(z) = (1 + i) + 2*(1 + i) = (1 + 2) + (1 + 2)i = 3 + 3i
            Assert.AreEqual(3, result.RealPart, Tolerance);
            Assert.AreEqual(3, result.ImaginaryPart, Tolerance);
        }

        [TestMethod]
        public void EvaluateAt_ShouldHandleZeroPolynomial()
        {
            var p = new Polynomial(ComplexNumber.Zero);
            var result = p.EvaluateAt(new ComplexNumber(5, 5));

            Assert.AreEqual(0, result.RealPart, Tolerance);
            Assert.AreEqual(0, result.ImaginaryPart, Tolerance);
        }

        [TestMethod]
        public void EvaluateAt_ShouldHandleHigherOrderPolynomial()
        {
            // p(x) = 1 + 2x + 3x^2 + 4x^3
            var p = new Polynomial(
                new ComplexNumber(1),
                new ComplexNumber(2),
                new ComplexNumber(3),
                new ComplexNumber(4)
            );

            var result = p.EvaluateAt(2); // 1 + 4 + 12 + 32 = 49
            Assert.AreEqual(49, result.RealPart, Tolerance);
        }

        [TestMethod]
        public void Differentiate_ShouldReturnCorrectDerivative()
        {
            // p(x) = 3 + 2x + x^2
            var p = new Polynomial(
                new ComplexNumber(3),
                new ComplexNumber(2),
                new ComplexNumber(1)
            );

            var derivative = p.Differentiate(); // p'(x) = 2 + 2x

            var result = derivative.EvaluateAt(1); // 2 + 2*1 = 4
            Assert.AreEqual(4, result.RealPart, Tolerance);
            Assert.AreEqual(0, result.ImaginaryPart, Tolerance);
        }

        [TestMethod]
        public void Differentiate_ConstantPolynomial_ShouldReturnZero()
        {
            var p = new Polynomial(new ComplexNumber(5));
            var derivative = p.Differentiate();

            var result = derivative.EvaluateAt(2);
            Assert.AreEqual(0, result.RealPart, Tolerance);
            Assert.AreEqual(0, result.ImaginaryPart, Tolerance);
        }

        [TestMethod]
        public void ToString_ShouldContainAllTermsInCorrectOrder()
        {
            var p = new Polynomial(
                new ComplexNumber(1),
                new ComplexNumber(2),
                new ComplexNumber(3)
            );

            var str = p.ToString();

            StringAssert.Contains(str, "x^2");
            StringAssert.Contains(str, "x^1");
            StringAssert.Contains(str, "1");
        }

        [TestMethod]
        public void ToString_ShouldWorkForSingleConstantPolynomial()
        {
            var p = new Polynomial(new ComplexNumber(5));
            Assert.AreEqual("(5 + 0i)", p.ToString());
        }

        [TestMethod]
        public void Differentiate_ShouldHandleComplexCoefficients()
        {
            // p(x) = (1 + i) + (2 - i)x + (3 + 2i)x^2
            var p = new Polynomial(
                new ComplexNumber(1, 1),
                new ComplexNumber(2, -1),
                new ComplexNumber(3, 2)
            );

            var derivative = p.Differentiate(); // (2 - i) + 2*(3 + 2i)x

            var result = derivative.EvaluateAt(1); // (2 - i) + 2*(3 + 2i) = (2 + 6) + (-1 + 4)i = 8 + 3i
            Assert.AreEqual(8, Math.Round(result.RealPart, 6));
            Assert.AreEqual(3, Math.Round(result.ImaginaryPart, 6));
        }
    }
}
