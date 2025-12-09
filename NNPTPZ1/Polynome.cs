using System.Collections.Generic;

namespace NNPTPZ1
{

    namespace Mathematics
    {
        public class Polynome
        {
            public List<ComplexNumber> Coefficients { get; private set; }

            public Polynome() => Coefficients = new List<ComplexNumber>();

            public void Add(ComplexNumber coefficient) =>
                Coefficients.Add(coefficient);

            /// <summary>
            /// Creates and returns the derivative of this polynomial.
            /// </summary>
            /// <returns>The derived polynomial</returns>
            public Polynome Derive()
            {
                Polynome derivedPolynome = new Polynome();
                for (int i = 1; i < Coefficients.Count; i++)
                {
                    derivedPolynome.Coefficients.Add(Coefficients[i].Multiply(new ComplexNumber() { RealPart = i }));
                }

                return derivedPolynome;
            }

            /// <summary>
            /// Evaluates the polynomial at a given real value.
            /// </summary>
            /// <param name="realValue">The input value.</param>
            /// <returns>The resulting complex value.</returns>
            public ComplexNumber Eval(double realValue)
            {
                return Eval(new ComplexNumber() { RealPart = realValue });
            }

            /// <summary>
            /// Evaluates the polynomial at a given complex value.
            /// </summary>
            /// <param name="complexValue">The complex input value.</param>
            /// <returns>The resulting complex value.</returns>
            public ComplexNumber Eval(ComplexNumber complexValue)
            {
                ComplexNumber result = ComplexNumber.Zero;

                for (int i = 0; i < Coefficients.Count; i++)
                {
                    ComplexNumber coefficient = Coefficients[i];
                    ComplexNumber powerValue = complexValue;
                    int power = i;

                    if (i > 0)
                    {
                        for (int j = 0; j < power - 1; j++)
                            powerValue = powerValue.Multiply(complexValue);

                        coefficient = coefficient.Multiply(powerValue);
                    }

                    result = result.Add(coefficient);
                }

                return result;
            }


            public override string ToString()
            {
                string result = "";

                for (int i = 0; i < Coefficients.Count; i++)
                {
                    result += Coefficients[i];
                    if (i > 0)
                    {
                        for (int j = 0; j < i; j++)
                        {
                            result += "x";
                        }
                    }
                    if (i + 1 < Coefficients.Count)
                        result += " + ";
                }
                return result;
            }
        }
    }
}
