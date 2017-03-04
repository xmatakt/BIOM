﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;

namespace BiomSystRating.Classes
{
    public static class VectorManilulation
    {
        /// <summary>
        /// Metoda vrati stlpcovy (W * H x 1) vektor reprezentujuci vstupny obraz velkosti W x H
        /// </summary>
        /// <param name="image">Vstupny obraz v sedotone</param>
        /// <param name="normalize">Volba normalizovania vstupneho vektora, predvolene false</param>
        /// <returns>Stlpcovy vektor reprezentujuci 2D obraz pre potreby (aj) PCA</returns>
        public static Matrix<double> GetImageVector(Image<Gray, byte> image, bool normalize = false)
        {
            //NormalizeImage(image);
            var w = image.Width;
            var h = image.Height;
            var imageVector = new Matrix<double>(w * h, 1);

            for (var row = 0; row < h; row++)
            {
                for (var col = 0; col < w; col++)
                {
                    var index = w * row + col;
                    imageVector[index, 0] = image.Data[row, col, 0] / 255d;
                }
            }

            if (normalize)
                NormalizeVector(imageVector);
            
            return imageVector;
        }

        /// <summary>
        /// Metoda prevedie vektor typu M x 1 na obrazok s rozmermi vstupneho obrazku
        /// </summary>
        /// <param name="imageVector"></param>
        /// <param name="originalImageWidth"></param>
        /// <param name="originalImageHeight"></param>
        /// <returns></returns>
        public static Image<Gray, byte> GetImageFromVector(Matrix<double> imageVector, int originalImageWidth, int originalImageHeight)
        {
            var imgVect = RescaleVector(imageVector);
            var image = new Image<Gray, byte>(originalImageWidth, originalImageHeight);
            for (var row = 0; row < originalImageHeight; row++)
            {
                for (var col = 0; col < originalImageWidth; col++)
                {
                    var index = originalImageWidth * row + col;
                    image.Data[row, col, 0] = (byte)(imgVect[index, 0]);
                    //image.Data[row, col, 0] = (byte)(imgVect[0, index]);
                }
            }

            return image;
        }

        public static Image<Gray, byte> GetImageFromVector2(Matrix<double> imageVector, int originalImageWidth, int originalImageHeight)
        {
            var imgVect = RescaleVector(imageVector);
            var image = new Image<Gray, byte>(originalImageWidth, originalImageHeight);
            for (var row = 0; row < originalImageHeight; row++)
            {
                for (var col = 0; col < originalImageWidth; col++)
                {
                    var index = originalImageWidth * row + col;
                    //image.Data[row, col, 0] = (byte)(imgVect[index, 0]);
                    image.Data[row, col, 0] = (byte)(imgVect[0, index]);
                }
            }

            return image;
        }

        public static Image<Gray, byte> GetInvertedImage(Matrix<double> imageVector, int originalImageWidth, int originalImageHeight)
        {
            //RescaleVector(imageVector);
            var image = new Image<Gray, byte>(originalImageWidth, originalImageHeight);
            for (var row = 0; row < originalImageHeight; row++)
            {
                for (var col = 0; col < originalImageWidth; col++)
                {
                    var index = originalImageWidth * row + col;
                    image.Data[row, col, 0] = (byte)(255 - imageVector[index, 0]);
                }
            }

            return image;
        }

        public static void NormalizeImage(Image<Gray, byte> image)
        {
            var minI = FindMinimumIntensity(image);
            var maxI = FindMaximumIntensity(image);
            //var normalizedImage = new Image<Gray, byte>(image.Width, image.Height);
            for (var row = 0; row < image.Height; row++)
            {
                for (var col = 0; col < image.Width; col++)
                {
                    //image.Data[row, col, 0] =
                    //    (byte)(0 + (image.Data[row, col, 0] - minI) * ((255 - 0) / (maxI - minI)));
                    image.Data[row, col, 0] =
                        (byte)(255 * (image.Data[row, col, 0] - minI) / (maxI - minI));
                }
            }
        }

        /// <summary>
        /// Metoda preberie vektor typu M x 1 [nemusi byt] a preskaluje jeho hodnoty na interval [0, 255]
        /// </summary>
        /// <param name="vector">Vektor na preskalovanie</param>
        private static Matrix<double> RescaleVector(Matrix<double> vector)
        {
            var result = new Matrix<double>(vector.Height, vector.Width);
            var oldMin = FindMinimumIntensity(vector);
            var oldMax = FindMaximumIntensity(vector);
            var len = Math.Abs(oldMax - oldMin);

            for (var i = 0; i < vector.Height; i++)
                for (var j = 0; j < vector.Width; j++)
                    result[i, j] = (255 * (vector[i, j] - oldMin)) / len;

            //oldMin = FindMinimumIntensity(vector);
            //oldMax = FindMaximumIntensity(vector);
            return result;
        }

        private static byte FindMinimumIntensity(Image<Gray, byte> image)
        {
            var minimum = 256;
            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    var intensity = image.Data[i, j, 0];
                    if (intensity <= minimum)
                        minimum = intensity;
                }
            }

            return (byte)minimum;
        }

        private static byte FindMaximumIntensity(Image<Gray, byte> image)
        {
            var maximum = 0;
            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    var intensity = image.Data[i, j, 0];
                    if (intensity >= maximum)
                        maximum = intensity;
                }
            }

            return (byte)maximum;
        }

        /// <summary>
        /// Ocakava vektor typu M x 1
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        private static double FindMinimumIntensity(Matrix<double> vector)
        {
            var minimum = double.MaxValue;
            for (int i = 0; i < vector.Height; i++)
            {
                for (int j = 0; j < vector.Width; j++)
                {
                    var intensity = vector[i, j];
                    if (intensity <= minimum)
                        minimum = intensity;
                }
            }

            return minimum;
        }

        /// <summary>
        /// Ocakava vektor typu M x 1
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        private static double FindMaximumIntensity(Matrix<double> vector)
        {
            var maximum = double.MinValue;
            for (int i = 0; i < vector.Height; i++)
            {
                for (int j = 0; j < vector.Width; j++)
                {
                    var intensity = vector[i, j];
                    if (intensity >= maximum)
                        maximum = intensity;
                }
            }

            return maximum;
        }

        /// <summary>
        /// Metoda predpoklada vstupny vektor typu N x 1
        /// </summary>
        /// <param name="vector">Vektor, ktory ma byt normalizovany</param>
        /// <returns></returns>
        private static void NormalizeVector(Matrix<double> vector)
        {
            var vectorLength = GetVectorLength(vector);
            
            for (var row = 0; row < vector.Height; row++)
                vector[row, 0] /= vectorLength;
        }

        /// <summary>
        /// Metoda predpoklada vstupny vektor typu N x 1
        /// </summary>
        /// <param name="vector">Vektor, ktoreho dlzka ma byt vypocitana</param>
        /// <returns>Dlazka vstupneho vektora</returns>
        private static double GetVectorLength(Matrix<double> vector)
        {
            var sum = 0.0d;
            for (var row = 0; row < vector.Height; row++)
                sum += vector[row, 0] * vector[row, 0];

            return Math.Sqrt(sum);
        }
    }
}
