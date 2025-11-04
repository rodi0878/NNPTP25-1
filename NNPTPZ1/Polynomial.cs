using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1
{
    public class Polynomial
    {
        /// <summary>
        /// ListOfCoefficients
        /// </summary>
        public List<ComplexNumber> ListOfCoefficients { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Polynomial() => ListOfCoefficients = new List<ComplexNumber>();

        public void Add(ComplexNumber coefficient) =>
            ListOfCoefficients.Add(coefficient);

        /// <summary>
        /// Derives this polynomial and creates new one
        /// </summary>
        /// <returns>Derivated polynomial</returns>
        public Polynomial Derive()
        {
            Polynomial derivedPolynomial = new Polynomial();
            for (int i = 1; i < ListOfCoefficients.Count; i++)
            {
                derivedPolynomial.ListOfCoefficients.Add(ListOfCoefficients[i].Multiply(new ComplexNumber() { RealPart = i }));
            }

            return derivedPolynomial;
        }

        /// <summary>
        /// Evaluates polynomial at real point
        /// </summary>
        /// <param name="realPoint">point of evaluation</param>
        public ComplexNumber EvaluateAtRealPoint(double realPoint) =>
            EvaluateAtComplexPoint(new ComplexNumber() { RealPart = realPoint, ImaginaryPart = 0 });


        /// <summary>
        /// Evaluates polynomial at complex point
        /// </summary>
        /// <param name="complexPoint">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber EvaluateAtComplexPoint(ComplexNumber complexPoint)
        {
            ComplexNumber result = ComplexNumber.ZeroValue;
            for (int i = 0; i < ListOfCoefficients.Count; i++)
            {
                ComplexNumber coefficiens = ListOfCoefficients[i];
                ComplexNumber bx = complexPoint;
                int power = i;

                if (i > 0)
                {
                    for (int j = 0; j < power - 1; j++)
                        bx = bx.Multiply(complexPoint);

                    coefficiens = coefficiens.Multiply(bx);
                }

                result = result.Add(coefficiens);
            }

            return result;
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>String repr of polynomial</returns>
        public override string ToString()
        {
            string s = "";
            int i = 0;
            for (; i < ListOfCoefficients.Count; i++)
            {
                s += ListOfCoefficients[i];
                if (i > 0)
                {
                    int j = 0;
                    for (; j < i; j++)
                    {
                        s += "x";
                    }
                }
                if (i + 1 < ListOfCoefficients.Count)
                    s += " + ";
            }
            return s;
        }
    }
}
