using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace NNPTPZ1.Tests
{
    [TestClass()]
    public class CplxTests
    {

        [TestMethod()]
        public void AddTest()
        {
            ComplexNumber a = new ComplexNumber()
            {
                RealPart = 10,
                ImaginariPart = 20
            };
            ComplexNumber b = new ComplexNumber()
            {
                RealPart = 1,
                ImaginariPart = 2
            };

            ComplexNumber actual = a.Add(b);
            ComplexNumber shouldBe = new ComplexNumber()
            {
                RealPart = 11,
                ImaginariPart = 22
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
                RealPart = 1,
                ImaginariPart = -1
            };
            b = new ComplexNumber() { RealPart = 0, ImaginariPart = 0 };
            shouldBe = new ComplexNumber() { RealPart = 1, ImaginariPart = -1 };
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
            poly.Coeficients.Add(new ComplexNumber() { RealPart = 1, ImaginariPart = 0 });
            poly.Coeficients.Add(new ComplexNumber() { RealPart = 0, ImaginariPart = 0 });
            poly.Coeficients.Add(new ComplexNumber() { RealPart = 1, ImaginariPart = 0 });
            ComplexNumber result = poly.Eval(new ComplexNumber() { RealPart = 0, ImaginariPart = 0 });
            var expected = new ComplexNumber() { RealPart = 1, ImaginariPart = 0 };
            Assert.AreEqual(expected, result);
            result = poly.Eval(new ComplexNumber() { RealPart = 1, ImaginariPart = 0 });
            expected = new ComplexNumber() { RealPart = 2, ImaginariPart = 0 };
            Assert.AreEqual(expected, result);
            result = poly.Eval(new ComplexNumber() { RealPart = 2, ImaginariPart = 0 });
            expected = new ComplexNumber() { RealPart = 5.0000000000, ImaginariPart = 0 };
            Assert.AreEqual(expected, result);

            var r2 = poly.ToString();
            var e2 = "(1 + 0i) + (0 + 0i)x + (1 + 0i)xx";
            Assert.AreEqual(e2, r2);
        }
    }
}


