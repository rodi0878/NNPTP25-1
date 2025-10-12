using System.Collections.Generic;

namespace NNPTPZ1
{

    namespace Mathematics
    {
        public class Polynome
        {
            /// <summary>
            /// Coefficients
            /// </summary>
            public List<Complex> Coefficients { get; set; }

            /// <summary>
            /// Constructor
            /// </summary>
            public Polynome() => Coefficients = new List<Complex>();

            /// <summary>
            /// Derives this polynomial and creates new one
            /// </summary>
            /// <returns>Derivated polynomial</returns>
            public Polynome Derive()
            {
                Polynome derivedPolynomial = new Polynome();
                for (int i = 1; i < Coefficients.Count; i++)
                    derivedPolynomial.Coefficients.Add(Coefficients[i].Multiply(new Complex() { RealPart = i }));

                return derivedPolynomial;
            }

            /// <summary>
            /// Evaluates polynomial at given point
            /// </summary>
            /// <param name="x">point of evaluation</param>
            /// <returns>y</returns>
            public Complex Eval(double x) => Eval(new Complex() { RealPart = x, ImaginaryPart = 0 });

            /// <summary>
            /// Evaluates polynomial at given point
            /// </summary>
            /// <param name="x">point of evaluation</param>
            /// <returns>y</returns>
            public Complex Eval(Complex x)
            {
                Complex result = Complex.Zero;
                for (int i = 0; i < Coefficients.Count; i++)
                {
                    Complex coefficient = Coefficients[i];
                    Complex powerValue = x;
                    int power = i;

                    if (i > 0)
                    {
                        for (int j = 0; j < power - 1; j++)
                            powerValue = powerValue.Multiply(x);

                        coefficient = coefficient.Multiply(powerValue);
                    }

                    result = result.Add(coefficient);
                }

                return result;
            }

            /// <summary>
            /// ToString
            /// </summary>
            /// <returns>String repr of polynomial</returns>
            public override string ToString()
            {
                string output = "";
                
                for (int i = 0; i < Coefficients.Count; i++)
                {
                    output += Coefficients[i];
                    if (i > 0)
                        for (int j = 0; j < i; j++)
                            output += "x";
                    
                    if (i + 1 < Coefficients.Count)
                        output += " + ";
                }
                return output;
            }
        }
    }
}
