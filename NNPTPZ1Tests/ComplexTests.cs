using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NNPTPZ1.Mathematics.Tests
{
    [TestClass()]
    public class ComplexTests
    {

        [TestMethod()]
        public void AddTest()
        {
            Complex a = new Complex(10, 20);
            Complex b = new Complex(1, 2);

            Complex actual = a.Add(b);
            Complex shouldBe = new Complex(11, 22);

            Assert.AreEqual(shouldBe, actual);

            var e2 = "(10 + 20i)";
            var r2 = a.ToString();
            Assert.AreEqual(e2, r2);
            e2 = "(1 + 2i)";
            r2 = b.ToString();
            Assert.AreEqual(e2, r2);

            a = new Complex(1, -1);
            b = new Complex(0, 0);
            shouldBe = new Complex(1, -1);
            actual = a.Add(b);
            Assert.AreEqual(shouldBe, actual);

            e2 = "(1 + -1i)";
            r2 = a.ToString();
            Assert.AreEqual(e2, r2);

            e2 = "(0 + 0i)";
            r2 = b.ToString();
            Assert.AreEqual(e2, r2);
        }

        [TestMethod()]
        public void AddTestPolynome()
        {
            Polynomial poly = new Polynomial();
            poly.Coefficients.Add(new Complex(1, 0));
            poly.Coefficients.Add(new Complex(0, 0));
            poly.Coefficients.Add(new Complex(1, 0));
            Complex result = poly.Eval(new Complex(0, 0));
            var expected = new Complex(1, 0);
            Assert.AreEqual(expected, result);
            result = poly.Eval(new Complex(1, 0));
            expected = new Complex(2, 0);
            Assert.AreEqual(expected, result);
            result = poly.Eval(new Complex(2, 0));
            expected = new Complex(5.0000000000, 0);
            Assert.AreEqual(expected, result);

            var r2 = poly.ToString();
            var e2 = "(1 + 0i) + (0 + 0i)x + (1 + 0i)xx";
            Assert.AreEqual(e2, r2);
        }
    }
}


