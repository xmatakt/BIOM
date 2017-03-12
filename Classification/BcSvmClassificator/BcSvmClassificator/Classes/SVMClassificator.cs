using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.ML;
using Emgu.CV.ML.MlEnum;
using Emgu.CV.Structure;

namespace BcSvmClassificator.Classes
{
    class SVMClassificator
    {
        private Matrix<float> trainDataMatrix;
        private Matrix<int> trainClasses;
        private SVM model;
        private bool isTrained;
        public int tmpCounter = 0; //  TODO: dat doprec

        public SVMClassificator()
        {
            
        }

        public SVMClassificator(Matrix<float> trainData, Matrix<int> trainClasses )
        {
            this.trainDataMatrix = trainData;
            this.trainClasses = trainClasses;
        }

        public void TrainClassificator(int kFold)
        {
            model = new SVM();
            var trainingData = new TrainData(trainDataMatrix, DataLayoutType.RowSample, trainClasses);
            isTrained = model.TrainAuto(trainingData, kFold);
        }

        /// <summary>
        /// Cross validacia SVM klasifikatora
        /// Prvky trenovacej a testovacej mnoziny su vyberane nahodne
        /// </summary>
        /// <param name="data">Vstupne data</param>
        /// <param name="xValCount">Stupen crossvalidacie (pocet podmnozin vstupnej mnoziny)</param>
        /// <param name="kFold">Stupen crossvalidacie pre natrenovanie EmguCV SVM modelu</param>
        /// <returns>Chyba klasifikatora</returns>  
        public Error ValidateClassificator(DataStorage data, int xValCount, int kFold, double gamma, bool isRbfKernel)
        {
            var result = new Error()
            {
                ClassificationError = 0d,
                TrainDataCount = 0
            };

            for (var x = 0; x < xValCount; x++)
            {
                //  66% povodnych dat bude tvorit trenovacie data, zvysok testovacie
                //var indexes = GenerateRandomIndexes((int)(data.Items.Count * 0.66), data.Items.Count);
                var trainSet = new DataStorage();
                var testSet = new DataStorage();

                foreach (var label in data.Items.Select(l => l.Label).Distinct())
                {
                    var dataWithSameLabel = data.Items.Where(l => l.Label == label).ToList();
                    var indexes = GenerateRandomIndexes((int)(dataWithSameLabel.Count * 0.66), dataWithSameLabel.Count);
                    for (var i = 0; i < dataWithSameLabel.Count; i++)
                    {

                        if (indexes.Contains(i))
                            trainSet.Items.Add(dataWithSameLabel[i]);
                        else
                            testSet.Items.Add(dataWithSameLabel[i]);
                    }
                }

                //  natrenovanie modelu
                model = new SVM();
                trainDataMatrix = trainSet.ToMatrix();
                trainClasses = trainSet.GetLabelsMatrix();
                var trainingData = new TrainData(trainDataMatrix, DataLayoutType.RowSample, trainClasses);

                //  http://www.emgu.com/wiki/files/3.0.0/document/html/87eaf2bf-4bb4-74b6-b750-2eaeeb5f3b6c.htm
                try
                {
                    model.Gamma = gamma;
                    model.SetKernel(isRbfKernel ? SVM.SvmKernelType.Rbf : SVM.SvmKernelType.Chi2);

                    //isTrained = model.TrainAuto(trainingData, kFold);
                    isTrained = model.Train(trainDataMatrix, DataLayoutType.RowSample, trainClasses);

                }
                catch (Exception e)
                {
                    continue;
                }

                //  vypocet chyby SVM klasifikatora
                if (!isTrained) continue;

                var badClassifiedCounter = 0;
                foreach (var testData in testSet.Items)
                {
                    var predictedLabel = Predict(testData);
                    if (predictedLabel == null) continue;
                    if ((int) predictedLabel != testData.Label)
                        badClassifiedCounter++;
                }
                result.TrainDataCount = trainSet.Items.Count;
                result.ClassificationError += badClassifiedCounter / (double)testSet.Items.Count;
            }
            result.ClassificationError /= xValCount;

            return result;
        }

        public float? Predict(DataStorageItem data)
        {
            try
            {
                var matrix = new Matrix<float>(data.Data);
                matrix = matrix.Transpose();
                return model.Predict(matrix);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return null;
            }
        }

        /// <summary>
        /// Vrati pole nahodne vygenerovanych indexov dlzky retArrLength
        /// </summary>
        /// <param name="retArrLength">Dlzka vrateneho pola indexov (nemoze byt vacsie ako maxIndexCount)</param>
        /// <param name="maxIndexCount">Maximalny index</param>
        /// <returns></returns>
        private int[] GenerateRandomIndexes(int retArrLength, int maxIndexCount)
        {
            var result = new List<int>();
            var rand = new Random();

            while (result.Count != retArrLength)
            {
                var randIndex = rand.Next(0, maxIndexCount);
                if (!result.Contains(randIndex))
                    result.Add(randIndex);
            }

            return result.ToArray();
        }
    }
}
