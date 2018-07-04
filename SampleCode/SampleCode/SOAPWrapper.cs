using System;
using System.Collections.Generic;
using System.Linq;
using LumenWorks.Framework.IO.Csv;
using System.IO;
using System.Text;
using WS.Utilities.Csv;
using System.Threading;
//using CsvHelper;



//namespace AuthorizeNetRest.QA
namespace net.authorize.sample
{
    public class SOAPWrapper
    {
        static void Main(string[] args)
        {

            Apicollection();
        }

        public static void Apicollection()
        {
            

            //read the data from csv file for condition flag = 1
            try
                
            {
                CsvReader csv = null;
                //Console.WriteLine("hello");
                //using (csv = new CsvReader(new StreamReader(@"../../../CSV_DATA/driver.csv"), true))
                //{
                using (csv = new CsvReader(new StreamReader(new FileStream(@"../../../CSV_DATA/SOAPDriver.csv", FileMode.Open)), true))
                   
                {
                    string apiName = null;
                    int fieldCount = csv.FieldCount;
                    string[] headers = csv.GetFieldHeaders();
                    while (csv.ReadNextRecord())
                    {
                        if (csv["Flag"] == "1")
                        //if (csv["RunApi"] == "1")
                        {
                            apiName = csv["fileName"];
                            SampleCode.RunMethod(apiName);
                        }
                    }

                }
                //  Console.ReadLine();
                //  appli
            }

            catch (Exception e)
            {
                Console.WriteLine("Error Message " + e.Message);
                Console.WriteLine("Inside erro");
                Console.ReadLine();
            }
        }

    }

    //public class DataAppend
    //{
    //    public IEnumerable<CSVLoadData> ReadPrevData()
    //    {
    //        //TextReader txtReader1 = new StreamReader(@"../../CSV_DATA/OutputCustomers.csv");
    //        TextReader txtReader1 = new StreamReader(new FileStream(@"../../../CSV_DATA/outputfile.csv", FileMode.Open), true);
    //        var csv1 = new CsvHelper.CsvReader(txtReader1);
    //        IEnumerable<CSVLoadData> records = csv1. GetRecords<CSVLoadData>().ToList(); ;
    //        csv1.Dispose();
    //        return records;

    //    }
    //}
    public class DataAppend
    {
        //public IEnumerable<CSVLoadData> ReadPrevData()
        public static List<CsvRow> ReadPrevData()
        {
            // TextReader txtReader1 = new StreamReader(new FileStream(@"../../../CSV_DATA/outputfile.csv", FileMode.Open),true);
            // var csv1 = new CsvReader(txtReader1);
            // //IEnumerable<CSVLoadData> records = csv1//Select<[],CSVLoadData>();
            // //Thread.Sleep(5000);
            // bool records2=csv1.ReadNextRecord();
            // var records = csv1.GetCurrentRawData();
            // //IEnumerable<CSVLoadData> records1 = records.ToList<CSVLoadData>();
            // csv1.Dispose();
            //// return records;

            List<CsvRow> lstCsv = new List<CsvRow>();
            using (CsvReader reader = new CsvReader(new StreamReader(new FileStream(@"../../../CSV_DATA/Outputfile.csv",FileMode.Open)), true))
            {
                int fieldCount1 = reader.FieldCount;

                string[] headers1 = reader.GetFieldHeaders();
                while (reader.ReadNextRecord())
                {
                    CsvRow rowcsv = new CsvRow();
                    for (int i = 0; i < fieldCount1; i++)
                    {
                        try { rowcsv.Add(reader[i]); }
                        catch (Exception e)
                        { }
                    }
                    lstCsv.Add(rowcsv);
                }
                return lstCsv;
            }

            //sharath

        }
        //public IEnumerable<CSVLoadData>
    }



    public class CSVLoadData
    {
        public string TestCaseId { get; set; }
        public string APIName { get; set; }
        public string Status { get; set; }
        public string TimeStamp { get; set; }
    }

    /// <summary>
    /// Class to write data to a CSV file
    /// </summary>
    public class CsvFileWriter : StreamWriter
    {
        public CsvFileWriter(Stream stream)
            : base(stream)
        {
        }

        //public CsvFileWriter(string filename)
        //    : base(filename)
        //{
        //}

        /// <summary>
        /// Writes a single row to a CSV file.
        /// </summary>
        /// <param name="row">The row to be written</param>
        public void WriteRow(CsvRow row)
        {
            StringBuilder builder = new StringBuilder();
            bool firstColumn = true;
            foreach (string value in row)
            {
                // Add separator if this isn't the first value
                if (!firstColumn)
                    builder.Append(',');
                // Implement special handling for values that contain comma or quote
                // Enclose in quotes and double up any double quotes
                if (value.IndexOfAny(new char[] { '"', ',' }) != -1)
                    builder.AppendFormat("\"{0}\"", value.Replace("\"", "\"\""));
                else
                    builder.Append(value);
                firstColumn = false;
            }
            row.LineText = builder.ToString();
            WriteLine(row.LineText);
        }
    }

    public class CsvRow : List<string>
    {
        public string LineText { get; set; }
    }
    /// <summary>
    /// Class to read data from a CSV file
    /// </summary>
    public class CsvFileReader : StreamReader
    {
        public CsvFileReader(Stream stream)
            : base(stream)
        {
        }

        //public CsvFileReader(string filename)
        //    : base(filename)
        //{
        //}

        /// <summary>
        /// Reads a row of data from a CSV file
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public bool ReadRow(CsvRow row)
        {
            row.LineText = ReadLine();
            if (String.IsNullOrEmpty(row.LineText))
                return false;

            int pos = 0;
            int rows = 0;

            while (pos < row.LineText.Length)
            {
                string value;

                // Special handling for quoted field
                if (row.LineText[pos] == '"')
                {
                    // Skip initial quote
                    pos++;

                    // Parse quoted value
                    int start = pos;
                    while (pos < row.LineText.Length)
                    {
                        // Test for quote character
                        if (row.LineText[pos] == '"')
                        {
                            // Found one
                            pos++;

                            // If two quotes together, keep one
                            // Otherwise, indicates end of value
                            if (pos >= row.LineText.Length || row.LineText[pos] != '"')
                            {
                                pos--;
                                break;
                            }
                        }
                        pos++;
                    }
                    value = row.LineText.Substring(start, pos - start);
                    value = value.Replace("\"\"", "\"");
                }
                else
                {
                    // Parse unquoted value
                    int start = pos;
                    while (pos < row.LineText.Length && row.LineText[pos] != ',')
                        pos++;
                    value = row.LineText.Substring(start, pos - start);
                }

                // Add field to list
                if (rows < row.Count)
                    row[rows] = value;
                else
                    row.Add(value);
                rows++;

                // Eat up to and including next comma
                while (pos < row.LineText.Length && row.LineText[pos] != ',')
                    pos++;
                if (pos < row.LineText.Length)
                    pos++;
            }
            // Delete any unused items
            while (row.Count > rows)
                row.RemoveAt(rows);

            // Return true if any columns read
            return (row.Count > 0);
        }
    }

}





