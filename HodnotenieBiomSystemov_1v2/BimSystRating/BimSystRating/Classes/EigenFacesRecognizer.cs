using System;
using Emgu.CV;
using Emgu.CV.Face;
using Emgu.CV.Structure;

//https://www.codeproject.com/articles/261550/emgu-multiple-face-recognition-using-pca-and-paral

namespace BimSystRating.Classes
{
    class EigenFacesRecognizer
    {
        FaceRecognizer recognizer;
        private TrainingSet trainingSet;
        private bool isTrained = false;

        public double min = double.MaxValue;
        public double max = double.MinValue;

        public EigenFacesRecognizer()
        {
            trainingSet = new TrainingSet();
        }

        /// <summary>
        /// Vytvori innstanciu EigenFaceRecognizera a priradi trenovacie vzorky
        /// </summary>
        /// <param name="trainingSet">trenovacei data</param>
        /// <param name="numComponents">
        /// The number of components (read: Eigenfaces) kept for this Prinicpal 
        /// Component Analysis. As a hint: There’s no rule how many components (read: Eigenfaces) 
        /// should be kept for good reconstruction capabilities. It is based on your input data, 
        /// so experiment with the number. Keeping 80 components should almost always be sufficient.</param>
        /// <param name="treshold">
        /// The threshold applied in the prediciton. This still has issues as it work inversly to LBH and Fisher Methods.
        /// if you use 0.0 recognizer.Predict will always return -1 or unknown if you use 5000 for example unknow won't be reconised.
        /// As in previous versions I ignore the built in threhold methods and allow a match to be found i.e. double.PositiveInfinity
        /// and then use the eigen distance threshold that is return to elliminate unknowns. 
        /// TO ALLOW US TO SET A THRESHOLD RULE LATER WE SET THE THRESHOLD VALUE TO POSITIVE INFINITY, ALLOWING ALL FACES TO BE RECOGNIZED</param>
        public EigenFacesRecognizer(TrainingSet trainingSet, bool isFiherRecognizer, int numComponents = 80, double threshold = double.PositiveInfinity)
        {
            //radius – The radius used for building the Circular Local Binary Pattern.
            //neighbors – The number of sample points to build a Circular Local Binary Pattern from. An value suggested by OpenCV Documentations is ‘8’ sample points. Keep in mind: the more sample points you include, the higher the computational cost.
            //grid_x – The number of cells in the horizontal direction, 8 is a common value used in publications. The more cells, the finer the grid, the higher the dimensionality of the resulting feature vector.
            //grid_y – The number of cells in the vertical direction, 8 is a common value used in publications. The more cells, the finer the grid, the higher the dimensionality of the resulting feature vector.
            //threshold – The threshold applied in the prediction. If the distance to the nearest neighbour is larger than the threshold, this method returns -1.
            if(isFiherRecognizer)
                recognizer = new LBPHFaceRecognizer(1, 8, 8, 8, threshold);
            else
                recognizer = new EigenFaceRecognizer(numComponents, threshold);

            this.trainingSet = trainingSet;
        }

        public void Train()
        {
            try
            {
                recognizer.Train(trainingSet.GetImages().ToArray(), trainingSet.GetIDs().ToArray());
                isTrained = true;
            }
            catch (Exception e)
            {
                // ignored
            }
        }

        /// <summary>
        /// With the Eigen recogniser we must use the return distance to provide our own test for unknowns.
        /// In the Eigen recogniser the larger the value returned is, the closer to a match we have.
        /// </summary>
        /// <param name="image">Vsutpny obrazok</param>
        /// <param name="treshold">hranica akceptovania obrazku</param>
        /// <returns></returns>
        public RecognizationResult RecognizeImage(Image<Gray, byte> image, int threshold)
        {
            if (isTrained)
            {
                var predictionResult = recognizer.Predict(image);

                if (predictionResult.Label == -1)
                    return new RecognizationResult() {Label = "UnknownPreson", IsAccepted = false};

                var eigenLabel = trainingSet.GetLabels()[predictionResult.Label];
                var eigenDistance = predictionResult.Distance;

                //TODO: dat prec
                if (min > eigenDistance)
                    min = eigenDistance;
                if (max < eigenDistance)
                    max = eigenDistance;
                //

                //return eigenDistance > threshold
                //    ? new RecognizationResult() {Label = eigenLabel, IsAccepted = true}
                //    : new RecognizationResult() {Label = eigenLabel, IsAccepted = false};

                //Note how the Eigen Distance must be below the threshold unlike as above
                return eigenDistance < threshold
                    ? new RecognizationResult() { Label = eigenLabel, IsAccepted = true }
                    : new RecognizationResult() { Label = eigenLabel, IsAccepted = false };
            }
            else
                return new RecognizationResult() {Label = "Recognizer was not trained!", IsAccepted = false};
        }
    }
}
