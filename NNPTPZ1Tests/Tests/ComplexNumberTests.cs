using MathCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests;

/// <summary>
/// Unit tests for the ComplexNumber struct.
/// Checks basic arithmetic and string representation.
/// </summary>
[TestClass]
public class ComplexNumberTests
{
    /// Adding two non-zero complex numbers should produce the correct sum.
    /// Also verifies the string representation format.
    [TestMethod]
    public void Add_Works()
    {
        var a = new ComplexNumber(10, 20);
        var b = new ComplexNumber(1, 2);
        Assert.AreEqual(new ComplexNumber(11, 22), a + b);

        // Check ToString() formatting
        Assert.AreEqual("(10 + 20i)", a.ToString());
        Assert.AreEqual("(1 + 2i)", b.ToString());
    }

    [TestMethod]
    public void Add_WithZero_IsIdentity()
    {
        var a = new ComplexNumber(1, -1);
        var zero = ComplexNumber.Zero;

        // Adding zero must return the same number
        Assert.AreEqual(new ComplexNumber(1, -1), a + zero);
         // Check ToString() formatting
        Assert.AreEqual("(1 + -1i)", a.ToString());
        Assert.AreEqual("(0 + 0i)", zero.ToString());
    }
}