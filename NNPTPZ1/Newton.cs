using Mathematics;

namespace NNPTPZ1;

public class Newton
{
    public static int FindRoot(Polynomial polynome, Polynomial polynomeDerived, ref ComplexNumber complexPosition)
    {
        int maxIterations = 30;
        // find solution of equation using newton's iteration
        int iterations = 0;
        for (int i = 0; i< maxIterations; i++)
        {
            var difference = polynome.Eval(complexPosition).Divide(polynomeDerived.Eval(complexPosition));
            complexPosition = complexPosition.Subtract(difference);

            if (Math.Pow(difference.Real, 2) + Math.Pow(difference.Imaginary, 2) >= 0.5)
            {
                i--;
            }
            iterations++;
        }
        return iterations;
    }
    
    public static int AddUniqueRoot(List<ComplexNumber> roots, ComplexNumber complexPosition)
    {
        // find solution root number
        var known = false;
        var index = 0;
        for (int w = 0; w <roots.Count;w++)
        {
            if (Math.Pow(complexPosition.Real- roots[w].Real, 2) + Math.Pow(complexPosition.Imaginary - roots[w].Imaginary, 2) <= 0.01)
            {
                known = true;
                index = w;
            }
        }
        if (!known)
        {
            roots.Add(complexPosition);
            index = roots.Count;
        }

        return index;
    }
}