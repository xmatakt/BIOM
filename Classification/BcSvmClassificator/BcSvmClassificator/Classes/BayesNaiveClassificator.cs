using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BcSvmClassificator.Classes
{
    class BayesNaiveClassificator
    {
        private DataStorage trainSet;
        private DataStorage testSet;
        private Dictionary<int, double> aPrioriProbabilities;
        private Dictionary<int, double> aPosterioriProbabilities;
        private Dictionary<int,Dictionary<float, int>> labeledCounts;

        public void ValidateClassificator(DataStorage trainSet, DataStorage testSet, Error error)
        {
            this.trainSet = trainSet;
            this.testSet = testSet;
            var badClassifiedCounter = 0;
            error.TrainDataCount = trainSet.Items.Count;

            CalculateAPrioriProbabilities(trainSet);
            SetCountsDicitonary();

            //  tento pocet musi sediet s poctom labeledCounts
            //var tmp = trainSet.Items.SelectMany(x => x.Data).Distinct().ToArray().Length;

            foreach (var testData in testSet.Items)
            {
                var predictedLabel = Predict(testData);

                if (predictedLabel != testData.Label)
                    badClassifiedCounter++;
            }
            error.ClassificationError += badClassifiedCounter / (double)testSet.Items.Count;
        }

        private void SetCountsDicitonary()
        {
            labeledCounts = new Dictionary<int, Dictionary<float, int>>();

            foreach (var label in trainSet.Items.Select(x => x.Label).Distinct())
            {
                //  vyberiem vsetky polia prisluchajuce k Label == label a spravim z nich 1D pole
                var valuesInSameClass = trainSet.Items.Where(x => x.Label == label).SelectMany(x => x.Data).ToArray();
                //var tmp = valuesInSameClass.Distinct().ToArray().Length;
                
                foreach (var item in trainSet.Items)
                {
                    foreach (var value in item.Data)
                    {
                        if (labeledCounts.ContainsKey(label))
                        {
                            if (labeledCounts[label].ContainsKey(value))
                                continue;

                            var countOfValueInClass = valuesInSameClass.Count(x => x == value);
                            labeledCounts[label].Add(value, countOfValueInClass);
                        }
                        else
                        {
                            labeledCounts.Add(label, new Dictionary<float, int>());

                            var countOfValueInClass = valuesInSameClass.Count(x => x == value);
                            labeledCounts[label].Add(value, countOfValueInClass);
                        }
                    }
                }
            }
        }

        private int Predict(DataStorageItem testData)
        {
            aPosterioriProbabilities = CalculateAPosterioriProbabilities(testData);
            var predictedLabel = -1;
            var maximum = double.MinValue;

            foreach (var item in aPrioriProbabilities)
            {
                var probability = aPrioriProbabilities[item.Key] * aPosterioriProbabilities[item.Key];

                if (probability > maximum)
                {
                    maximum = probability;
                    predictedLabel = item.Key;
                }
            }

            return predictedLabel;
        }

        private void CalculateAPrioriProbabilities(DataStorage trainSet)
        {
            aPrioriProbabilities =  new Dictionary<int, double>();

            var totalCount = trainSet.Items.Count;

            foreach (var label in trainSet.Items.Select(x => x.Label).Distinct())
            {
                var countInClass = trainSet.Items.Count(x => x.Label == label);
                var probability = countInClass / (double) totalCount;
                aPrioriProbabilities.Add(label, probability);
            }
        }

        private Dictionary<int, double> CalculateAPosterioriProbabilities(DataStorageItem storageItem)
        {
            var result =  new Dictionary<int, double>();

            foreach (var label in trainSet.Items.Select(x => x.Label).Distinct())
            {
                var countInClass = trainSet.Items.Count(x => x.Label == label);
                var product = 1d;

                foreach (var attribute in storageItem.Data)
                {
                    var sameAsAttributeCount = 0;

                    if (labeledCounts.ContainsKey(label))
                    {
                        if (labeledCounts[label].ContainsKey(attribute))
                            sameAsAttributeCount = labeledCounts[label][attribute];
                    }

                    product *= sameAsAttributeCount / (double) countInClass;
                }

                result.Add(label, product);
            }

            return result;
        }
    }
}
