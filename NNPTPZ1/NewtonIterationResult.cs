using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    public class NewtonIterationResult
    {
        public NewtonIterationResult(ComplexNumber root, int iterationCount)
        {
            Root = root;
            IterationCount = iterationCount;
        }

        public ComplexNumber Root { get; }
        public int IterationCount { get; }
    }
}
