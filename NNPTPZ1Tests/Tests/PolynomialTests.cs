using MathCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests;
/// <summary>
/// Unit tests for the Polynomial class.
/// Covers evaluation, string representation, and derivative calculation.
/// </summary>
[TestClass]
public class PolynomialTests
{
    /// Test evaluating a polynomial at different points and checking its ToString().
    [TestMethod]
    public void Evaluate_And_ToString_Works()
    {
        // Define polynomial: p(x) = 1 + 0x + 1x^2
        var p = new Polynomial();
        p.Coefficients.AddRange(new[]
        {
            new ComplexNumber(1,0), // constant term
            new ComplexNumber(0,0), // coefficient for x
            new ComplexNumber(1,0)  // coefficient for x²
        });

        // Evaluate polynomial at different points
        Assert.AreEqual(new ComplexNumber(1,0), p.Evaluate(new ComplexNumber(0,0))); // p(0) = 1
        Assert.AreEqual(new ComplexNumber(2,0), p.Evaluate(new ComplexNumber(1,0))); // p(1) = 2
        Assert.AreEqual(new ComplexNumber(5,0), p.Evaluate(new ComplexNumber(2,0))); // p(2) = 5

        // Check string representation
        Assert.AreEqual("(1 + 0i) + (0 + 0i)x + (1 + 0i)xx", p.ToString());
    }


    /// Test that derivative is computed correctly.
    [TestMethod]
    public void Derivative_Works()
    {
        // Define polynomial: p(x) = 3 + 2x + 5x²
        var p = new Polynomial();
        p.Coefficients.AddRange(new[]
        {
            new ComplexNumber(3,0), // constant term
            new ComplexNumber(2,0), // coefficient for x
            new ComplexNumber(5,0) // coefficient for x²
        });

        // Derivative should be: 2 + 10x
        var d = p.Derivative();
        Assert.AreEqual(2, d.Coefficients.Count);
        Assert.AreEqual(new ComplexNumber(2,0), d.Coefficients[0]);  // coefficient for x^0
        Assert.AreEqual(new ComplexNumber(10,0), d.Coefficients[1]); // coefficient for x^1
    }
}