using System.Text;

namespace MathCore;

/// Represents a polynomial of the form:
public class Polynomial
{
    /// List of coefficients. Index = power of z.
    public List<ComplexNumber> Coefficients { get; } = new();

   
    public Polynomial Derivative()
    {
        var d = new Polynomial();
        for (int i = 1; i < Coefficients.Count; i++)
            d.Coefficients.Add(Coefficients[i] * i); // multiply coefficient by its power
        return d;
    }

    /// <summary>
    ///  Evaluate polynomial at complex number z.
    /// </summary>
    /// <param name="z"></param>
    /// <returns></returns>
    public ComplexNumber Evaluate(ComplexNumber z)
    {
        if (Coefficients.Count == 0) return ComplexNumber.Zero;

        var acc = Coefficients[^1];
        for (int i = Coefficients.Count - 2; i >= 0; i--)
            acc = acc * z + Coefficients[i];

        return acc;
    }

    /// Evaluate polynomial at a real value (x treated as z = x + 0i).
    public ComplexNumber Evaluate(double x) => Evaluate(new ComplexNumber(x, 0));

    public override string ToString()
    {
        if (Coefficients.Count == 0) return "0";
        var parts = new List<string>();
        for (int i = 0; i < Coefficients.Count; i++)
        {
            StringBuilder sb = new StringBuilder(Coefficients[i].ToString());
            sb.Append(new string('x', i));
            parts.Add(sb.ToString());
        }
        return string.Join(" + ", parts);
    }
}