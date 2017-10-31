using System;
using System.Collections.Generic;
using System.Text;

namespace Kaltek.Chart
{
    public interface IChart : IDisposable
    {
        void Save(string path);

        byte[] Save();
    }
}
