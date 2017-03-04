using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;

namespace BimSystRating.Classes
{
    class TestSet
    {
        public List<Image<Gray, byte>> Images;
        public List<string> Labels;

        public TestSet()
        {
            Images = new List<Image<Gray, byte>>();
            Labels = new List<string>();
        }

        public void AddImage(Image<Gray, byte> image, string label)
        {
            Labels.Add(label);
            Images.Add(image);
        }
    }
}
