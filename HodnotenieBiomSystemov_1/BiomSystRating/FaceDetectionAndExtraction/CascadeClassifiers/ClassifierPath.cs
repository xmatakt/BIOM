using System.IO;

namespace FaceDetectionAndExtraction.CascadeClassifiers
{
    public static class ClassifierPath
    {
        public static string FrontalFaceAltTree()
        {
            return Path.GetFullPath(Path.Combine(
                Directory.GetCurrentDirectory(),
                @"..\..\..\FaceDetectionAndExtraction\CascadeClassifiers\haarcascade_frontalface_alt_tree.xml"));
        }

        public static string FrontalFaceAltDefault()
        {
            return Path.GetFullPath(Path.Combine(
                Directory.GetCurrentDirectory(),
                @"..\..\..\FaceDetectionAndExtraction\CascadeClassifiers\haarcascade_frontalface_default.xml"));
        }

        public static string ProfileFace()
        {
            return Path.GetFullPath(Path.Combine(
                Directory.GetCurrentDirectory(),
                @"..\..\..\FaceDetectionAndExtraction\CascadeClassifiers\haarcascade_profileface.xml"));
        }
    }
}
