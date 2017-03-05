using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Emgu.CV;
using Emgu.CV.Structure;
using ZedGraph;

namespace BimSystRating.Classes
{
    class Ratings
    {
        private TrainingSet trainingSet;
        private TestSet testSet;
        private EigenFacesRecognizer recognizer;
        private readonly List<double> FMR;
        private readonly List<double> FNMR;
        private readonly List<double> TPR;
        private readonly List<double> FPR;
        private readonly List<int> thresholds;
        private readonly bool isFisherSelected = true;

        public Ratings(bool isFisherSelected)
        {
            FMR = new List<double>();
            FNMR = new List<double>();
            TPR = new List<double>();
            FPR = new List<double>();
            thresholds = new List<int>();
            this.isFisherSelected = isFisherSelected;
        }

        public void GenerateRatingCurves(int minThreshold, int maxThreshold, int step, int crossCount)
        {
            for (var threshold = minThreshold; threshold <= maxThreshold; threshold += step)
            {
                System.Diagnostics.Debug.WriteLine("Start threshold: {0}...",threshold);
                CrossValidation(crossCount, threshold);
                System.Diagnostics.Debug.WriteLine("End...");
            }
        }

        public void GenerateGraphs()
        {

            System.Diagnostics.Debug.Write("FMR = {");
            for (var i = 0; i < thresholds.Count; i++)
            {
                System.Diagnostics.Debug.Write("{" + thresholds[i] + "," + FMR[i] + "},");
            }
            System.Diagnostics.Debug.Write("};");

            System.Diagnostics.Debug.WriteLine("");

            System.Diagnostics.Debug.Write("FNMR = {");
            for (var i = 0; i < thresholds.Count; i++)
            {
                System.Diagnostics.Debug.Write("{" + thresholds[i] + "," + FNMR[i] + "},");
            }
            System.Diagnostics.Debug.Write("};");

            System.Diagnostics.Debug.WriteLine("");

            System.Diagnostics.Debug.Write("TPR = {");
            for (var i = 0; i < thresholds.Count; i++)
            {
                System.Diagnostics.Debug.Write("{" + thresholds[i] + "," + TPR[i] + "},");
            }
            System.Diagnostics.Debug.Write("};");

            System.Diagnostics.Debug.WriteLine("");

            System.Diagnostics.Debug.Write("FPR = {");
            for (var i = 0; i < thresholds.Count; i++)
            {
                System.Diagnostics.Debug.Write("{" + thresholds[i] + "," + FPR[i] + "},");
            }
            System.Diagnostics.Debug.Write("};");

            System.Diagnostics.Debug.WriteLine("");

            System.Diagnostics.Debug.Write("ROC2 = {");
            for (var i = 0; i < thresholds.Count; i++)
            {
                System.Diagnostics.Debug.Write("{" + FPR[i] + "," + TPR[i] + "},");
            }
            System.Diagnostics.Debug.Write("};");
         }

        public PointPairList GenerateFMRGraph()
        {

            var result = new PointPairList();

            for (var i = 0; i < thresholds.Count; i++)
                result.Add(thresholds[i], FMR[i]);

            return result;
        }

        public PointPairList GenerateFNMRGraph()
        {

            var result = new PointPairList();

            for (var i = 0; i < thresholds.Count; i++)
                result.Add(thresholds[i], FNMR[i]);

            return result;
        }

        /// <summary>
        /// Vygenerovanie ROC krivky - vyber rozdielnych x hodnot, zoradenie vzostupne
        /// odstranenie y_i, kde y_i-1 > y_i
        /// </summary>
        /// <returns></returns>
        public PointPairList GenerateROCGraph()
        {
            var oldX = 0d;
            var oldY = 0d;
            var result = new PointPairList();

            //for (var i = 0; i < thresholds.Count; i++)
            //{
            //    if (i > 0)
            //    {
            //        if (!result.Select(x => x.X).Contains(FPR[i]))                              //  chceme iba unikatne x
            //            if (TPR[i - 1] <= TPR[i] && FPR[i - 1] < FPR[i])                        //  chceme iba rastuce y
            //                result.Add(FPR[i], TPR[i]);
            //    }
            //    else
            //        result.Add(FPR[i], TPR[i]);
            //}

            for (var i = 0; i < thresholds.Count; i++)
            {
                if (i > 0)
                {
                    if (oldX < FPR[i] && oldY <= TPR[i] && TPR[i] >= FPR[i])
                    {
                        result.Add(FPR[i], TPR[i]);
                        oldX = FPR[i];
                        oldY = TPR[i];
                    }
                }
                else
                {
                    result.Add(FPR[i], TPR[i]);
                    oldX = FPR[i];
                    oldY = TPR[i];
                }
            }

            return result;
        }
        /// <summary>
        /// Krosvalidacia
        /// </summary>
        /// <param name="count">pocet krosvalidacii</param>
        /// <param name="treshold">treshold</param>
        private void CrossValidation(int count, int threshold)
        {
            int FP, TN, FN;
            var TP = FP = TN = FN = 0;

            var counter = 0;

            for (var i = 0; i < count; i++)
            {
                CreateDataSets(21);
                recognizer = new EigenFacesRecognizer(trainingSet, isFisherSelected, 10);
                recognizer.Train();

                for (var j = 0; j < testSet.Images.Count; j++)
                {
                    var label = testSet.Labels[j];
                    var result = recognizer.RecognizeImage(testSet.Images[j], threshold);
                    counter++;

                    //  pocet poz. vzoriek, kt. boli klasif. ako pozitivne
                    if ((label == result.Label) && result.IsAccepted)
                        TP++;

                    //  pocet negat. vzoriek, kt. boli klasif. ako pozitivne
                    if ((label != result.Label) && result.IsAccepted)
                        FP++;

                    //  pocet negat. vzoriek, kt. boli klasif. ako negativne
                    if ((label != result.Label) && !result.IsAccepted)
                        TN++;

                    //  pocet poz. vzoriek, kt. boli klasif. ako negativne
                    if ((label == result.Label) && !result.IsAccepted)
                        FN++;
                }
            }

            FMR.Add(FP/(double)counter);
            FNMR.Add(FN / (double)counter);
            TPR.Add(TP / (double) (TP + FN));
            FPR.Add(FP / (double)(FP + TN));
            thresholds.Add(threshold);
        }

        /// <summary>
        /// Vytvori trenovacie a testovacie data - vygeneruje pole indexov obrazkov int[X] (X = pocet obrazkov vo foldri).
        /// Testovacie data potom bude tvorit prvych Y (param. count) obrazkov s indexami zodpovedajucimi prvym Y nahodnym indexom 
        /// v poli indexov pobrazkov
        /// Pr: Ak folder obsahuje 10 obrazkov a count = 7 => testovacie data budu tvorene 7 nahodne vybranymi obrazkami z foldra a 
        /// trenovacie data budu tvorene zvysnymi tromi obrazkami
        /// </summary>
        /// <param name="count">pocet obrazkov z foldra priradenych do testovacej mnoziny</param>
        private void CreateDataSets(int count)
        {
            trainingSet = new TrainingSet();
            testSet = new TestSet();
            var randomIndexes = GenerateRandomIndexes(27);  //TODO: nevolit napevno [27]

            var directory = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\Faces"));
            var subFolders = Directory.GetDirectories(directory);

            foreach (var folder in subFolders)
            {
                var content = Directory.GetFiles(folder);

                var counter = 0;
                foreach (var index in randomIndexes)
                {
                    var label = Regex.Replace(new FileInfo(content[index]).Name.Replace(".png", ""), @"[0-9]", "");
                    Image img = Image.FromFile(content[index]);
                    var image = new Image<Gray, byte>(new Bitmap(img));

                    if(counter < count)
                        trainingSet.AddImage(image, label);
                    else
                        testSet.AddImage(image, label);

                    counter++;
                }
            }
        }

        private int[] GenerateRandomIndexes(int maxIndexCount)
        {
            var result = new List<int>();
            var rand = new Random();

            while (result.Count != maxIndexCount)
            {
                var randIndex = rand.Next(0, maxIndexCount);
                if(!result.Contains(randIndex))
                    result.Add(randIndex);
            }

            return result.ToArray();
        }
    }
}
