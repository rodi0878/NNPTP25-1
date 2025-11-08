﻿using Mathematics;
 using Microsoft.VisualStudio.TestTools.UnitTesting;

 namespace NNPTPZ1
{
    [TestClass]
    public class ComplexNumberTests
    {

        [TestMethod]
        public void AddTest()
        {
            ComplexNumber a = new ComplexNumber()
            {
                Real = 10,
                Imaginary = 20
            };
            ComplexNumber b = new ComplexNumber()
            {
                Real = 1,
                Imaginary = 2
            };

            ComplexNumber actual = a.Add(b);
            ComplexNumber shouldBe = new ComplexNumber()
            {
                Real = 11,
                Imaginary = 22
            };

            Assert.AreEqual(shouldBe, actual);

            var e2 = "(10 + 20i)";
            var r2 = a.ToString();
            Assert.AreEqual(e2, r2);
            e2 = "(1 + 2i)";
            r2 = b.ToString();
            Assert.AreEqual(e2, r2);

            a = new ComplexNumber()
            {
                Real = 1,
                Imaginary = -1
            };
            b = new ComplexNumber() { Real = 0, Imaginary = 0 };
            shouldBe = new ComplexNumber() { Real = 1, Imaginary = -1 };
            actual = a.Add(b);
            Assert.AreEqual(shouldBe, actual);

            e2 = "(1 + -1i)";
            r2 = a.ToString();
            Assert.AreEqual(e2, r2);

            e2 = "(0 + 0i)";
            r2 = b.ToString();
            Assert.AreEqual(e2, r2);
        }

        [TestMethod]
        public void AddTestPolynome()
        {
            Polynomial polynomial = new global::Mathematics.Polynomial();
            polynomial.Coefficients.Add(new ComplexNumber() { Real = 1, Imaginary = 0 });
            polynomial.Coefficients.Add(new ComplexNumber() { Real = 0, Imaginary = 0 });
            polynomial.Coefficients.Add(new ComplexNumber() { Real = 1, Imaginary = 0 });
            ComplexNumber result = polynomial.Eval(new ComplexNumber() { Real = 0, Imaginary = 0 });
            var expected = new ComplexNumber() { Real = 1, Imaginary = 0 };
            Assert.AreEqual(expected, result);
            result = polynomial.Eval(new ComplexNumber() { Real = 1, Imaginary = 0 });
            expected = new ComplexNumber() { Real = 2, Imaginary = 0 };
            Assert.AreEqual(expected, result);
            result = polynomial.Eval(new ComplexNumber() { Real = 2, Imaginary = 0 });
            expected = new ComplexNumber() { Real = 5.0000000000, Imaginary = 0 };
            Assert.AreEqual(expected, result);

            var r2 = polynomial.ToString();
            var e2 = "(1 + 0i) + (0 + 0i)x + (1 + 0i)xx";
            Assert.AreEqual(e2, r2);
        }
    }
}