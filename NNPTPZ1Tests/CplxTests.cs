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

        [TestMethod]
        public void AddTest()
        {
            Cplx a = new Cplx { Real = 10, Imaginary = 20 };
            Cplx b = new Cplx { Real = 1, Imaginary = 2 };

            Cplx actual = a.Add(b);
            Cplx expected = new Cplx { Real = 11, Imaginary = 22 };
            Assert.AreEqual(expected, actual);

            Assert.AreEqual("(10 + 20i)", a.ToString());
            Assert.AreEqual("(1 + 2i)", b.ToString());

            a = new Cplx { Real = 1, Imaginary = -1 };
            b = new Cplx { Real = 0, Imaginary = 0 };
            expected = new Cplx { Real = 1, Imaginary = -1 };
            actual = a.Add(b);
            Assert.AreEqual(expected, actual);

            Assert.AreEqual("(1 + -1i)", a.ToString());
            Assert.AreEqual("(0 + 0i)", b.ToString());
        }


        [TestMethod]
        public void AddTestPolynome()
        {
            Poly poly = new Poly();
            poly.Coefficients.Add(new Cplx { Real = 1, Imaginary = 0 });
            poly.Coefficients.Add(new Cplx { Real = 0, Imaginary = 0 });
            poly.Coefficients.Add(new Cplx { Real = 1, Imaginary = 0 });

            Cplx result = poly.Eval(new Cplx { Real = 0, Imaginary = 0 });
            Cplx expected = new Cplx { Real = 1, Imaginary = 0 };
            Assert.AreEqual(expected, result);

            result = poly.Eval(new Cplx { Real = 1, Imaginary = 0 });
            expected = new Cplx { Real = 2, Imaginary = 0 };
            Assert.AreEqual(expected, result);

            result = poly.Eval(new Cplx { Real = 2, Imaginary = 0 });
            expected = new Cplx { Real = 5, Imaginary = 0 };
            Assert.AreEqual(expected, result);

            string expectedStr = "(1 + 0i) + (0 + 0i)x + (1 + 0i)xx";
            string actualStr = poly.ToString();
            Assert.AreEqual(expectedStr, actualStr);
        }
    }
}


