using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace NNPTPZ1.Mathematics.Tests
{
    [TestClass()]
    public class ComplexNumbersTests
    {
        private const double Tolerance = 1e-10;

        [TestMethod]
        public void Constructor_ShouldInitializeCorrectly()
        {
            var c = new ComplexNumber(3, -4);

            Assert.AreEqual(3, c.RealPart);
            Assert.AreEqual(-4, c.ImaginaryPart);
        }

        [TestMethod]
        public void FromDouble_ShouldCreateComplexWithZeroImaginaryPart()
        {
            var c = ComplexNumber.FromDouble(5.5);

            Assert.AreEqual(5.5, c.RealPart, Tolerance);
            Assert.AreEqual(0, c.ImaginaryPart, Tolerance);
        }

        [TestMethod]
        public void Add_ShouldReturnCorrectSum()
        {
            var a = new ComplexNumber(2, 3);
            var b = new ComplexNumber(1, -1);

            var result = a.Add(b);

            Assert.AreEqual(3, result.RealPart, Tolerance);
            Assert.AreEqual(2, result.ImaginaryPart, Tolerance);
        }

        [TestMethod]
        public void AddDouble_ShouldReturnCorrectSum()
        {
            var a = new ComplexNumber(2, 3);
            var b = 1;

            var result = a.Add(b);

            Assert.AreEqual(3, result.RealPart, Tolerance);
            Assert.AreEqual(3, result.ImaginaryPart, Tolerance);
        }

        [TestMethod]
        public void Subtract_ShouldReturnCorrectDifference()
        {
            var a = new ComplexNumber(5, 4);
            var b = new ComplexNumber(2, 1);

            var result = a.Subtract(b);

            Assert.AreEqual(3, result.RealPart, Tolerance);
            Assert.AreEqual(3, result.ImaginaryPart, Tolerance);
        }

        [TestMethod]
        public void SubtractDouble_ShouldReturnCorrectDifference()
        {
            var a = new ComplexNumber(5, 4);
            var b = 2;

            var result = a.Subtract(b);

            Assert.AreEqual(3, result.RealPart, Tolerance);
            Assert.AreEqual(4, result.ImaginaryPart, Tolerance);
        }

        [TestMethod]
        public void Multiply_ShouldReturnCorrectProduct()
        {
            var a = new ComplexNumber(3, 2);
            var b = new ComplexNumber(1, 4);

            var result = a.Multiply(b);

            Assert.AreEqual(-5, Math.Round(result.RealPart, 6));
            Assert.AreEqual(14, Math.Round(result.ImaginaryPart, 6));
        }

        [TestMethod]
        public void MultiplyByDouble_ShouldReturnCorrectProduct()
        {
            var a = new ComplexNumber(3, 2);
            var b = 4;

            var result = a.Multiply(b);

            Assert.AreEqual(12, Math.Round(result.RealPart, 6));
            Assert.AreEqual(8, Math.Round(result.ImaginaryPart, 6));
        }

        [TestMethod]
        public void Multiply_ByZero_ShouldReturnZero()
        {
            var a = new ComplexNumber(3, 2);
            var result = a.Multiply(0);

            Assert.AreEqual(0, result.RealPart, Tolerance);
            Assert.AreEqual(0, result.ImaginaryPart, Tolerance);
        }

        [TestMethod]
        public void Divide_ShouldReturnCorrectQuotient()
        {
            var a = new ComplexNumber(3, 2);
            var b = new ComplexNumber(4, -1);

            var result = a.Divide(b);

            Assert.AreEqual(0.588, Math.Round(result.RealPart, 3));
            Assert.AreEqual(0.647, Math.Round(result.ImaginaryPart, 3));
        }

        [TestMethod]
        public void DivideByDouble_ShouldReturnCorrectQuotient()
        {
            var a = new ComplexNumber(3, 2);
            var b = 2;

            var result = a.Divide(b);

            Assert.AreEqual(1.5, Math.Round(result.RealPart, 3));
            Assert.AreEqual(1, Math.Round(result.ImaginaryPart, 3));
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException), "Expected divide by zero to throw an exception.")]
        public void Divide_ByZero_ShouldThrow()
        {
            var a = new ComplexNumber(1, 2);
            var zero = ComplexNumber.Zero;

            // The class currently doesn’t throw, but if refactored to do so, this test will ensure it’s enforced.
            var _ = a.Divide(zero);
        }

        [TestMethod]
        public void Power_ShouldComputeCorrectly_ForPositiveExponent()
        {
            var c = new ComplexNumber(1, 1);

            var result = c.Power(3);

            Assert.AreEqual(-2, Math.Round(result.RealPart, 6));
            Assert.AreEqual(2, Math.Round(result.ImaginaryPart, 6));
        }

        [TestMethod]
        public void Power_ZeroExponent_ShouldReturnOne()
        {
            var c = new ComplexNumber(5, 6);

            var result = c.Power(0);

            Assert.AreEqual(1, result.RealPart, Tolerance);
            Assert.AreEqual(0, result.ImaginaryPart, Tolerance);
        }

        [TestMethod]
        public void Power_NegativeExponent_ShouldReturnInversePower()
        {
            var c = new ComplexNumber(2, 0);

            var result = c.Power(-2);

            Assert.AreEqual(0.25, Math.Round(result.RealPart, 6));
            Assert.AreEqual(0, Math.Round(result.ImaginaryPart, 6));
        }

        [TestMethod]
        public void GetMagnitudeSquared_ShouldReturnCorrectValue()
        {
            var c = new ComplexNumber(3, 4);

            Assert.AreEqual(25, c.GetMagnitudeSquared(), Tolerance);
        }

        [TestMethod]
        public void GetMagnitude_ShouldReturnCorrectMagnitude()
        {
            var c = new ComplexNumber(3, 4);

            Assert.AreEqual(5, c.GetMagnitude(), Tolerance);
        }

        [TestMethod]
        public void GetConjugate_ShouldNegateImaginaryPart()
        {
            var c = new ComplexNumber(3, 4);
            var conj = c.GetConjugate();

            Assert.AreEqual(3, conj.RealPart, Tolerance);
            Assert.AreEqual(-4, conj.ImaginaryPart, Tolerance);
        }

        [TestMethod]
        public void GetAngleInRadians_ShouldReturnCorrectAngle()
        {
            var c = new ComplexNumber(1, 1);

            Assert.AreEqual(Math.PI / 4, c.GetAngleInRadians(), 1e-6);
        }

        [TestMethod]
        public void GetAngleInDegrees_ShouldReturnCorrectAngle()
        {
            var c = new ComplexNumber(1, 1);

            Assert.AreEqual(45, c.GetAngleInDegrees(), 1e-6);
        }

        [TestMethod]
        public void GetEuclideanDistanceTo_ShouldReturnCorrectEuclideanDistance()
        {
            var a = new ComplexNumber(1, 2);
            var b = new ComplexNumber(4, 6);

            Assert.AreEqual(5, a.GetEuclideanDistanceTo(b), Tolerance);
        }

        [TestMethod]
        public void Equals_ShouldReturnTrueForEqualComplexNumbers()
        {
            var a = new ComplexNumber(2.5, -1.2);
            var b = new ComplexNumber(2.5, -1.2);

            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(a.Equals((object)b));
        }

        [TestMethod]
        public void Equals_ShouldReturnFalseForDifferentComplexNumbers()
        {
            var a = new ComplexNumber(2.5, -1.2);
            var b = new ComplexNumber(2.5, -1.3);

            Assert.IsFalse(a.Equals(b));
        }

        [TestMethod]
        public void GetHashCode_ShouldBeConsistentForEqualNumbers()
        {
            var a = new ComplexNumber(2.5, -1.2);
            var b = new ComplexNumber(2.5, -1.2);

            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [TestMethod]
        public void Zero_ShouldBeComplexZero()
        {
            Assert.AreEqual(0, ComplexNumber.Zero.RealPart, Tolerance);
            Assert.AreEqual(0, ComplexNumber.Zero.ImaginaryPart, Tolerance);
        }

        [TestMethod]
        public void One_ShouldBeComplexOne()
        {
            Assert.AreEqual(1, ComplexNumber.One.RealPart, Tolerance);
            Assert.AreEqual(0, ComplexNumber.One.ImaginaryPart, Tolerance);
        }

        [TestMethod]
        public void ToString_ShouldContainRealAndImaginaryParts()
        {
            var c = new ComplexNumber(3, -4);
            var s = c.ToString();

            StringAssert.Contains(s, "(3 - 4i)");
        }
    }
}


