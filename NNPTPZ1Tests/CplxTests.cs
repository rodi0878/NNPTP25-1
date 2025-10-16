using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NNPTPZ1;
using Mathematics;

namespace NNPTPZ1.Mathematics.Tests
{
    [TestClass()]
    public class CplxTests
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

            string e2 = "(10 + 20i)";
            string r2 = a.ToString();
            Assert.AreEqual(e2, r2);
            e2 = "(1 + 2i)";
            r2 = b.ToString();
            Assert.AreEqual(e2, r2);

            a = new Complex()
            {
                Real = 1,
                Imaginary = -1
            };
            b = new Complex() { Real = 0, Imaginary = 0 };
            shouldBe = new Complex() { Real = 1, Imaginary = -1 };
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
            poly.Coefficients.Add(new Complex() { Real = 1, Imaginary = 0 });
            poly.Coefficients.Add(new Complex() { Real = 0, Imaginary = 0 });
            poly.Coefficients.Add(new Complex() { Real = 1, Imaginary = 0 });
            Complex result = poly.Eval(new Complex() { Real = 0, Imaginary = 0 });
            Complex expected = new Complex() { Real = 1, Imaginary = 0 };
            Assert.AreEqual(expected, result);
            result = poly.Eval(new Complex() { Real = 1, Imaginary = 0 });
            expected = new Complex() { Real = 2, Imaginary = 0 };
            Assert.AreEqual(expected, result);
            result = poly.Eval(new Complex() { Real = 2, Imaginary = 0 });
            expected = new Complex() { Real = 5.0000000000, Imaginary = 0 };
            Assert.AreEqual(expected, result);

            string r2 = poly.ToString();
            string e2 = "(1 + 0i) + (0 + 0i)x + (1 + 0i)xx";
            Assert.AreEqual(e2, r2);
        }
    }
}


