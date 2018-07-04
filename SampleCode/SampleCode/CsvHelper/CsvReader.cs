using System.IO;

namespace CsvHelper
{
    internal class CsvReader
    {
        private TextReader txtReader1;

        public CsvReader(TextReader txtReader1)
        {
            this.txtReader1 = txtReader1;
        }
    }
}