namespace MusicGeneration.Models.Som.Model
{
    class Som
    {
        [Serializable]
        public class SOM(int rows, int cols, int steps, double learningRate)
        {
            public int Rows { get; set; } = rows;
            public int Cols { get; set; } = cols;
            public int Dimensions { get; set; }
            public int Steps { get; set; } = steps;
            public double LearningRate { get; set; } = learningRate;
            public double[][][]? Map { get; set; }
            public double[][]? Dataset { get; set; }
            public double[][]? TrainingData { get; set; } = null;
            private int bmuRow;
            private int bmuCol;

            public void SelfOrganisingMap()
            {
                if (TrainingData == null)
                {
                    // create random data for training since the dataset is empty (no training data given)
                    Dimensions = 10;
                    int datasetSize = 1000;

                    TrainingData = GenerateRandomDataset(datasetSize, Dimensions);
                }
                else
                {
                    // use the given training data

                }

                Map = InitializeMap(Rows, Cols, TrainingData[0].Length);
                Console.WriteLine("Training starting...");
                TrainSOM(Map, TrainingData, Rows, Cols, TrainingData[0].Length, Steps, LearningRate);

                Console.WriteLine("Training complete!");
            }

            private double[][][] InitializeMap(int rows, int cols, int dimensions)
            {
                Random rnd = new Random();
                double[][][] map = new double[rows][][];

                for (int i = 0; i < rows; i++)
                {
                    map[i] = new double[cols][];
                    for (int j = 0; j < cols; j++)
                    {
                        map[i][j] = new double[dimensions];
                        for (int k = 0; k < dimensions; k++)
                        {
                            map[i][j][k] = rnd.NextDouble();
                        }
                    }
                }

                return map;
            }

            private double[][] GenerateRandomDataset(int size, int dimensions)
            {
                Random rnd = new Random();
                double[][] dataset = new double[size][];

                for (int i = 0; i < size; i++)
                {
                    dataset[i] = new double[dimensions];
                    for (int j = 0; j < dimensions; j++)
                    {
                        dataset[i][j] = rnd.NextDouble();
                    }
                }

                return dataset;
            }

            private void TrainSOM(double[][][] map, double[][] dataset, int rows, int cols, int dimensions, int steps, double learningRate)
            {
                Random rnd = new Random();

                for (int step = 0; step < steps; step++)
                {
                    int dataIndex = rnd.Next(dataset.Length);
                    double[] dataPoint = dataset[dataIndex];

                    (int bmuRow, int bmuCol) = FindBMU(map, dataPoint, rows, cols, dimensions);

                    UpdateWeights(map, dataPoint, bmuRow, bmuCol, rows, cols, dimensions, learningRate * (1 - step / (double)steps));
                }
            }

            private (int, int) FindBMU(double[][][] map, double[] dataPoint, int rows, int cols, int dimensions)
            {
                int bmuRow = 0;
                int bmuCol = 0;
                double minDistance = double.MaxValue;

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        double distance = 0;
                        for (int k = 0; k < dimensions; k++)
                        {
                            distance += Math.Pow(dataPoint[k] - map[i][j][k], 2);
                        }
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            bmuRow = i;
                            bmuCol = j;
                        }
                    }
                }

                return (bmuRow, bmuCol);
            }

            private void UpdateWeights(double[][][] map, double[] dataPoint, int bmuRow, int bmuCol, int rows, int cols, int dimensions, double learningRate)
            {
                for (int i = Math.Max(0, bmuRow - 1); i <= Math.Min(rows - 1, bmuRow + 1); i++)
                {
                    for (int j = Math.Max(0, bmuCol - 1); j <= Math.Min(cols - 1, bmuCol + 1); j++)
                    {
                        for (int k = 0; k < dimensions; k++)
                        {
                            map[i][j][k] += learningRate * (dataPoint[k] - map[i][j][k]);
                        }
                    }
                }
            }

            public double[] GetBMUFeatureValues(double[] dataPoint)
            {
                (bmuRow, bmuCol) = FindBMU(Map, dataPoint, Rows, Cols, Dimensions);
                return Map[bmuRow][bmuCol];
            }
            public (double[] bmuFeatures, double[] variances) GetBMUFeatureValuesAndVariances(double[] dataPoint)
            {
                (int bmuRow, int bmuCol) = FindBMU(Map, dataPoint, Rows, Cols, Dimensions);
                double[] bmuFeatures = Map[bmuRow][bmuCol];

                List<double[]> neighborhoodFeatures = new List<double[]>();
                for (int i = Math.Max(0, bmuRow - 1); i <= Math.Min(Rows - 1, bmuRow + 1); i++)
                {
                    for (int j = Math.Max(0, bmuCol - 1); j <= Math.Min(Cols - 1, bmuCol + 1); j++)
                    {
                        neighborhoodFeatures.Add(Map[i][j]);
                    }
                }

                double[] variances = new double[Dimensions];
                for (int k = 0; k < Dimensions; k++)
                {
                    double mean = neighborhoodFeatures.Average(node => node[k]);
                    variances[k] = neighborhoodFeatures.Average(node => Math.Pow(node[k] - mean, 2));
                }

                return (bmuFeatures, variances);
            }
        }
    }
}
