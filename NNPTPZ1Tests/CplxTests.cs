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
    public class CplxTests
    {

        [TestMethod()]
        public void AddTest()
        {
            Cplx a = new Cplx()
            {
                Re = 10,
                Im = 20
            };
            Cplx b = new Cplx()
            {
                Re = 1,
                Im = 2
            };

            Cplx actual = a.Add(b);
            Cplx shouldBe = new Cplx()
            {
                Re = 11,
                Im = 22
            };

            Assert.AreEqual(shouldBe, actual);

            var e2 = "(10 + 20i)";
            var r2 = a.ToString();
            Assert.AreEqual(e2, r2);
            e2 = "(1 + 2i)";
            r2 = b.ToString();
            Assert.AreEqual(e2, r2);

            a = new Cplx()
            {
                Re = 1,
                Im = -1
            };
            b = new Cplx() { Re = 0, Im = 0 };
            shouldBe = new Cplx() { Re = 1, Im = -1 };
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
            Poly poly = new Mathematics.Poly();
            poly.Coe.Add(new Cplx() { Re = 1, Im = 0 });
            poly.Coe.Add(new Cplx() { Re = 0, Im = 0 });
            poly.Coe.Add(new Cplx() { Re = 1, Im = 0 });
            Cplx result = poly.Eval(new Cplx() { Re = 0, Im = 0 });
            var expected = new Cplx() { Re = 1, Im = 0 };
            Assert.AreEqual(expected, result);
            result = poly.Eval(new Cplx() { Re = 1, Im = 0 });
            expected = new Cplx() { Re = 2, Im = 0 };
            Assert.AreEqual(expected, result);
            result = poly.Eval(new Cplx() { Re = 2, Im = 0 });
            expected = new Cplx() { Re = 5.0000000000, Im = 0 };
            Assert.AreEqual(expected, result);

            var r2 = poly.ToString();
            var e2 = "(1 + 0i) + (0 + 0i)x + (1 + 0i)xx";
            Assert.AreEqual(e2, r2);
        }
    }
}


