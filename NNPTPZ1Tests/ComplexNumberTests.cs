using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1.Mathematics.Tests
{
    [TestClass]
    public class ComplexNumberTests
    {
        [TestMethod]
        public void AddShouldReturnSum()
        {
            ComplexNumber firstNumber = new ComplexNumber(10, 20);
            ComplexNumber secondNumber = new ComplexNumber(1, 2);

            ComplexNumber actual = firstNumber.Add(secondNumber);
            ComplexNumber expected = new ComplexNumber(11, 22);

            Assert.AreEqual(expected, actual);
            Assert.AreEqual("(10 + 20i)", firstNumber.ToString());
            Assert.AreEqual("(1 + 2i)", secondNumber.ToString());
        }

        [TestMethod]
        public void AddZeroShouldNotChangeValue()
        {
            ComplexNumber original = new ComplexNumber(1, -1);
            ComplexNumber zero = ComplexNumber.Zero();
            ComplexNumber result = original.Add(zero);

            Assert.AreEqual(original, result);
            Assert.AreEqual("(1 + -1i)", original.ToString());
            Assert.AreEqual("(0 + 0i)", zero.ToString());
        }

        [TestMethod]
        public void PolynomialEvaluationShouldMatchExpectedValues()
        {
            Polynomial polynomial = new Polynomial();
            polynomial.AddCoefficient(new ComplexNumber(1, 0));
            polynomial.AddCoefficient(ComplexNumber.Zero());
            polynomial.AddCoefficient(new ComplexNumber(1, 0));

            ComplexNumber result = polynomial.Evaluate(ComplexNumber.Zero());
            ComplexNumber expected = new ComplexNumber(1, 0);
            Assert.AreEqual(expected, result);

            result = polynomial.Evaluate(new ComplexNumber(1, 0));
            expected = new ComplexNumber(2, 0);
            Assert.AreEqual(expected, result);

            result = polynomial.Evaluate(new ComplexNumber(2, 0));
            expected = new ComplexNumber(5.0, 0);
            Assert.AreEqual(expected, result);

            string actualDescription = polynomial.ToString();
            string expectedDescription = "(1 + 0i) + (0 + 0i)x + (1 + 0i)xx";
            Assert.AreEqual(expectedDescription, actualDescription);
        }
    }
}
