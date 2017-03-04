using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Emgu.CV;
using Emgu.CV.Structure;

namespace BimSystRating.Classes
{
    class TrainingSet
    {
        private readonly List<Image<Gray, byte>> images;
        private List<string> labels;
        private List<int> ids;

        public TrainingSet()
        {
            images = new List<Image<Gray, byte>>();
            labels = new List<string>();
            ids = new List<int>();
        }

        /// <summary>
        /// Popis obrazka sa ziska ako labels[id] (id by mal vratit EigenFaceRecognizer)
        /// </summary>
        /// <param name="image">Novy trenovaci obrazok</param>
        /// <param name="label">Popis trenovacieho obrazka</param>
        public void AddImage(Image<Gray, byte> image, string label)
        {
            ids.Add(images.Count);
            labels.Add(label);
            images.Add(image);
        }

        public List<Image<Gray, byte>> GetImages()
        {
            return images;
        }

        public List<int> GetIDs()
        {
            return ids;
        }

        public List<string> GetLabels()
        {
            return labels;
        }
    }
}
