using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1Tests
{
    [TestClass]
    public class ComplexNumberTests
    {
        [TestMethod]
        public void ComplexNumberToStringTest01()
        {
            ComplexNumber a = new ComplexNumber
            {
                Real = 10,
                Imaginary = 20
            };

            var expectedString = "(10 + 20i)";
            var actualString = a.ToString();
            Assert.AreEqual(expectedString, actualString);
        }

        [TestMethod]
        public void ComplexNumberToStringTest02()
        {
            ComplexNumber b = new ComplexNumber
            {
                Real = 1,
                Imaginary = 2
            };

            string expectedString = "(1 + 2i)";
            string actualString = b.ToString();
            Assert.AreEqual(expectedString, actualString);
        }

        [TestMethod]
        public void ComplexNumberToStringTest03()
        {
            ComplexNumber a = new ComplexNumber
            {
                Real = 1,
                Imaginary = -1
            };

            string expectedString = "(1 + -1i)";
            string actualString = a.ToString();
            Assert.AreEqual(expectedString, actualString);
        }

        [TestMethod]
        public void ComplexNumberToStringTest04()
        {
            ComplexNumber b = new ComplexNumber
            {
                Real = 0,
                Imaginary = 0
            };

            string expectedString = "(0 + 0i)";
            string actualString = b.ToString();
            Assert.AreEqual(expectedString, actualString);
        }

        [TestMethod]
        public void ComplexNumberAddTest01()
        {
            ComplexNumber a = new ComplexNumber
            {
                Real = 10,
                Imaginary = 20
            };
            ComplexNumber b = new ComplexNumber
            {
                Real = 1,
                Imaginary = 2
            };

            ComplexNumber actualNumber = a.Add(b);
            ComplexNumber expectedNumber = new ComplexNumber
            {
                Real = 11,
                Imaginary = 22
            };

            Assert.AreEqual(expectedNumber, actualNumber);
        }

        [TestMethod]
        public void ComplexNumberAddTest02()
        {
            ComplexNumber a = new ComplexNumber
            {
                Real = 1,
                Imaginary = -1
            };
            ComplexNumber b = new ComplexNumber
            {
                Real = 0,
                Imaginary = 0
            };

            ComplexNumber actualNumber = a.Add(b);
            ComplexNumber expectedNumber = new ComplexNumber
            {
                Real = 1, 
                Imaginary = -1
            };

            Assert.AreEqual(expectedNumber, actualNumber);
        }
    }
}