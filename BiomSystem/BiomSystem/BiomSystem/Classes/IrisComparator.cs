using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;

namespace BiomSystem.Classes
{
    class IrisComparator
    {
        public static Image<Gray, byte> GetXorImage(Image<Gray, byte> template, Image<Gray, byte> mask)
        {
            var result = new Image<Gray, Byte>(template.Width, template.Height);

            for (var col = 0; col < result.Width; col++)
            {
                for (var row = 0; row < result.Height; row++)
                {
                    if (mask.Data[row, col, 0] == 255)
                        result.Data[row, col, 0] = 255;
                    else
                        result.Data[row, col, 0] = template.Data[row, col, 0];
                }
            }

            return result;
        }

        public static IrisCompareResult GetHammingDistance(Image<Gray, byte> iris1, Image<Gray, byte> iris2)
        {
            if (iris1.Width != iris2.Width && iris1.Height != iris2.Height)
            {
                MessageBox.Show("Iris templates must have same dimensoins!");
                return null;
            }

            var minimalDistance = double.MaxValue;
            var degree = 0.0;
            var width = iris1.Width;
            var degreeOfOneRot = 360 / (double) width;

            //  realne nemusim obrazok rotovat, staci rozumne vymysliet forcykli
            for (var rotCount = 0; rotCount < width; rotCount++)
            {
                var distance = 0;
                var firstRotCol = (width - rotCount) % width;

                for (var col = 0; col < width; col++)
                {
                    var rotCol = (firstRotCol + col) % width;
                    //System.Diagnostics.Debug.WriteLine("{0} -> {1}", col, rotCol);
                    for (var row = 0; row < iris1.Height; row++)
                    {
                        if (iris1.Data[row, col, 0] != iris2.Data[row, rotCol, 0])
                            distance++;
                    }
                }
                //System.Diagnostics.Debug.WriteLine("----------------------");

                if (distance < minimalDistance)
                {
                    minimalDistance = distance;
                    degree = degreeOfOneRot * rotCount;
                }

                //  rovnake duhovky (obrazky), tos nemusim dalej pokracovat v hladani min. rotacie
                if (minimalDistance == 0)
                    break;
            }

            return new IrisCompareResult()
            {
                Angle = degree,
                HammingDistance = minimalDistance / ((double) width * iris1.Height)
            };
        }

        public static Dictionary<Tuple<double, double>, int> CreateHistogram(List<Image<Gray, byte>> irisData,
            int intervalsCount)
        {
            var result = GetDictionary(intervalsCount);

            for (var i = 0; i < irisData.Count; i++)
            {
                for (var j = i + 1; j < irisData.Count; j++)
                {
                    var hammingDistance = GetHammingDistance(irisData[i], irisData[j]).HammingDistance;
                    AddDataToDictionary(result, hammingDistance);
                }

                System.Diagnostics.Debug.WriteLine("{0}/{1}", i + 1, irisData.Count);
            }

            var tmp = result.Values.Sum();
            System.Diagnostics.Debug.WriteLine("2415 =?= {0}", tmp);

            return result;
        }

        private static Dictionary<Tuple<double, double>, int> GetDictionary(int intervalsCount)
        {
            var result = new Dictionary<Tuple<double, double>, int>();

            var stepSize = 1.0d / intervalsCount;
            for (var i = 0d; i < 1; i += stepSize)
            {
                var key = new Tuple<double, double>(i, i + stepSize);
                result.Add(key, 0);
            }

            return result;
        }

        private static void AddDataToDictionary(Dictionary<Tuple<double, double>, int> dict, double hammingDistance)
        {
            var key = dict.Keys.FirstOrDefault(x =>
                x.Item1 <= hammingDistance &&
                x.Item2 > hammingDistance);

            dict[key]++;
        }
    }
}
