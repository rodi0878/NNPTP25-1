using System.Collections.Generic;

namespace NNPTPZ1
{
    public class Polynomial
    {
        /// <summary>
        /// Coeficient
        /// </summary>
        public List<ComplexNumber> Coeficients { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Polynomial() => Coeficients = new List<ComplexNumber>();

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>String repr of polynomial</returns>
        public override string ToString()
        {
            string output = "";
            for (int i = 0; i < Coeficients.Count; i++)
            {
                output += Coeficients[i];
                for (int j = 0; j < i; j++)
                    output += "x";
                if (i < Coeficients.Count - 1)
                    output += " + ";
            }
            return output;
        }

        /// <summary>
        /// Adds coeficient to polynom, which will reise its degree.
        /// Added coeficient is with one degree higher then degree of this polynom.
        /// </summary>
        /// <param name="coeficient">Added coeficient</param>
        /// <returns>y</returns>
        public void Add(ComplexNumber coeficient) =>
            Coeficients.Add(coeficient);

        /// <summary>
        /// Derives this polynomial and creates new one
        /// </summary>
        /// <returns>Derivated polynomial</returns>
        public Polynomial Derive()
        {
            Polynomial output = new Polynomial();
            for (int i = 1; i < Coeficients.Count; i++)
            {
                output.Coeficients.Add(Coeficients[i].Multiply(new ComplexNumber() { RealPart = i, ImaginariPart = 0 }));
            }
            return output;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="argument">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Eval(double argument)
        {
            return Eval(new ComplexNumber() { RealPart = argument, ImaginariPart = 0 });
        }
        
        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="argument">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Eval(ComplexNumber argument)
        {
            ComplexNumber sum = ComplexNumber.Zero;
            for (int power = 0; power < Coeficients.Count; power++)
            {
                ComplexNumber coef = Coeficients[power];
                ComplexNumber term = new ComplexNumber() { RealPart = 1, ImaginariPart = 0 };
                for (int j = 0; j < power; j++)
                    term = term.Multiply(argument);
                sum = sum.Add(Coeficients[power].Multiply(term));
            }
            return sum;
        }
    }
}
