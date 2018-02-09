using System.IO;

namespace EasyStore.Infrastructure
{
    public static class FileManager
    {
        public static void SavePdfFile(MemoryStream stream, string filename)
        {
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;
            File.WriteAllBytes(filename, byteInfo);
        }
    }

}
