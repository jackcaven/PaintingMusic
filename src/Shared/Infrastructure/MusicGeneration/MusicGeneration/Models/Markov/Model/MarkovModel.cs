using MusicGeneration.Models.Markov.Helpers;

namespace MusicGeneration.Models.Markov.Model
{
    internal class MarkovModel
    {
        private const int matrixDimension = 127;
        private readonly double[,] transitions;

        public MarkovModel(string csvResourceName)
        {
            transitions = new double[matrixDimension, matrixDimension];
            SetupModel(csvResourceName);

            if (transitions.GetLength(0) != matrixDimension)
            {
                throw new ArgumentException("Markov file was not uploaded correctly");
            }
        }

        public List<int> GenerateNotes(int initialNote, int numberOfNotesToGenerate)
        {
            List<int> notes = [initialNote];

            for (int i = 1; i < numberOfNotesToGenerate; i++)
            {
                notes.Add(GetNextState(notes[i - 1]));
            }

            return notes;
        }

        private int GetNextState(int currentState)
        {
            if (currentState < 0 || currentState > matrixDimension)
                throw new ArgumentOutOfRangeException(nameof(currentState), "Invalid state index provided");

            double roll = new Random().NextDouble();
            double cumulative = 0.0;

            for (int nextState = 0; nextState < matrixDimension; nextState++)
            {
                cumulative += transitions[currentState, nextState];
                if (roll <= cumulative)
                    return nextState;
            }

            return currentState;
        }

        private void SetupModel(string csvResourceName)
        {
            try
            {
                var rawRows = CsvLoader.LoadCsv(csvResourceName);

                for (int i = 0; i < matrixDimension; i++)
                {
                    for (int j = 0; j < matrixDimension; j++)
                    {
                        transitions[i, j] = double.Parse(rawRows[i][j]);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Failed to load csv for Markov Model", e);
            }
        }
    }
}
