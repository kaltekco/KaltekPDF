using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Kaltek.PDF
{
    public class Image : IPageElement
    {
        private readonly string _path;
        private byte[] _data;

        public Image(string path)
        {
            _path = path;

            if (File.Exists(_path))
            {
                _data = File.ReadAllBytes(_path);
            }
        }

        public Image(byte[] data)
        {
            _data = data;
        }

        public Image (Stream stream)
        {
            _data = new byte[stream.Length];
            stream.Read(_data, 0, _data.Length);
        }

        public byte[] Data => _data;

        public int Left { get; set; }

        public int Top { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }
}
