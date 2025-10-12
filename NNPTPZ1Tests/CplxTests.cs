using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NNPTPZ1.Mathematics.Tests
{
    [TestClass()]
    public class CplxTests
    {

        [TestMethod()]
        public void AddTest()
        {
            Complex complexNumberFirst = new Complex()
            {
                RealPart = 10,
                ImaginaryPart = 20
            };
            Complex complexNumberSecond = new Complex()
            {
                RealPart = 1,
                ImaginaryPart = 2
            };

            Complex actual = complexNumberFirst.Add(complexNumberSecond);
            Complex shouldBe = new Complex()
            {
                RealPart = 11,
                ImaginaryPart = 22
            };

            Assert.AreEqual(shouldBe, actual);

            var expected2 = "(10 + 20i)";
            var result2 = complexNumberFirst.ToString();
            Assert.AreEqual(expected2, result2);
            expected2 = "(1 + 2i)";
            result2 = complexNumberSecond.ToString();
            Assert.AreEqual(expected2, result2);

            complexNumberFirst = new Complex()
            {
                RealPart = 1,
                ImaginaryPart = -1
            };
            complexNumberSecond = new Complex() { RealPart = 0, ImaginaryPart = 0 };
            shouldBe = new Complex() { RealPart = 1, ImaginaryPart = -1 };
            actual = complexNumberFirst.Add(complexNumberSecond);
            Assert.AreEqual(shouldBe, actual);

            expected2 = "(1 + -1i)";
            result2 = complexNumberFirst.ToString();
            Assert.AreEqual(expected2, result2);

            expected2 = "(0 + 0i)";
            result2 = complexNumberSecond.ToString();
            Assert.AreEqual(expected2, result2);
        }

        [TestMethod()]
        public void AddTestPolynome()
        {
            Polynome polynomial = new Polynome();
            polynomial.Coefficients.Add(new Complex() { RealPart = 1, ImaginaryPart = 0 });
            polynomial.Coefficients.Add(new Complex() { RealPart = 0, ImaginaryPart = 0 });
            polynomial.Coefficients.Add(new Complex() { RealPart = 1, ImaginaryPart = 0 });
            Complex result = polynomial.Eval(new Complex() { RealPart = 0, ImaginaryPart = 0 });
            var expected = new Complex() { RealPart = 1, ImaginaryPart = 0 };
            Assert.AreEqual(expected, result);
            result = polynomial.Eval(new Complex() { RealPart = 1, ImaginaryPart = 0 });
            expected = new Complex() { RealPart = 2, ImaginaryPart = 0 };
            Assert.AreEqual(expected, result);
            result = polynomial.Eval(new Complex() { RealPart = 2, ImaginaryPart = 0 });
            expected = new Complex() { RealPart = 5.0000000000, ImaginaryPart = 0 };
            Assert.AreEqual(expected, result);

            var result2 = polynomial.ToString();
            var expected2 = "(1 + 0i) + (0 + 0i)x + (1 + 0i)xx";
            Assert.AreEqual(expected2, result2);
        }
    }
}
