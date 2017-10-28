using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Kaltek.PDF
{
    public interface IPdfDocument
    {
        List<Page> Pages { get; set; }

        string Author { get; set; }

        string Creator { get; set; }

        string Keywords { get; set; }

        string Producer { get; set; }

        string Subject { get; set; }

        string Title { get; set; }

        void Save(string path);

        byte[] Save();
    }
}
