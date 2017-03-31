using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using OpenTK.Graphics.OpenGL;
using System.Threading;

namespace BcSvmClassificator.Classes
{
    internal class DataStorage
    {
        public List<DataStorageItem> Items { get; private set; }
        private Matrix<int> labels;

        public DataStorage()
        {
            Items = new List<DataStorageItem>();
        }

        public DataStorage(string path)
        {
            if (!File.Exists(path))
            {
                MessageBox.Show("File " + path + "does not exist!", "Vnimanie!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            Items = new List<DataStorageItem>();
            LoadData(path);
        }

        public Matrix<float> GetTrainigDatamatrix(Dictionary<int, int> counts)
        {
            var rowsCount = counts.Values.Sum();
            var result =  new Matrix<float>(rowsCount, Items[0].Data.Length);
            labels = new Matrix<int>(rowsCount, 1);

            var row = 0;
            foreach (var pair in counts)
            {
                var counter = 1;
                foreach (var dataRow in Items.Where(x=> x.Label == pair.Key))
                {
                    if (counter > pair.Value)
                        break;

                    for (var col = 0; col < dataRow.Data.Length; col++)
                        result[row, col] = dataRow.Data[col];

                    labels[row, 0] = dataRow.Label;
                    
                    counter++;
                    row++;
                }
            }

            return result;
        }

        public Matrix<float> ToMatrix()
        {
            var result = new Matrix<float>(Items.Count, Items[0].Data.Length);
            labels = new Matrix<int>(Items.Count, 1);

            for (var row = 0; row < Items.Count; row++)
            {
                for (var col = 0; col < Items[row].Data.Length; col++)
                    result[row, col] = Items[row].Data[col];

                labels[row, 0] = Items[row].Label;
            }

            return result;
        }

        public Matrix<int> GetLabelsMatrix()
        {
            return labels;
        }

        private void LoadData(string path)
        {
            var lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
                Thread.CurrentThread.CurrentCulture  = System.Globalization.CultureInfo.InvariantCulture;
                var lineData = line.Split(' ');
                var dataVector = Array.ConvertAll(SubArray(lineData, 0, lineData.Length - 1), float.Parse);
                var dataLabel = int.Parse(lineData[lineData.Length - 1]);

                var dataStorageItem = new DataStorageItem()
                {
                    Data = dataVector,
                    Label = dataLabel
                };

                Items.Add(dataStorageItem);
            }

            //var tmp = data.Select(x => x.Label).Distinct().ToArray();
        }

        private static string[] SubArray(string[] data, int index, int length)
        {
            var result = new string[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
    }
}
