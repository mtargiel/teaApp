using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corealate
{
    static class DataContext
    {
        private static readonly string _fileName = "tea-data.txt";
        private static string[] _lines;
        static DataContext()
        {
           _lines = File.ReadAllLines(_fileName);
        }
        public static DataTable GetDataTableFromTextFile()
        {

            var cols = _lines[0].Split(new string[] { ", " }, StringSplitOptions.None);
            DataTable dt = new DataTable();
            foreach (var column in cols)
            {
                dt.Columns.Add(new DataColumn(column));
            }

            for (int i = 2; i < _lines.Length - 1; i++)
            {
                string[] row = _lines[i].Split(new string[] { ", " }, StringSplitOptions.None);
                DataRow dr = dt.NewRow();

                for (int j = 0; j < row.Length; j++)
                {
                    dr[j] = row[j];
                }

                dt.Rows.Add(dr);
            }

            return dt;

        }
        public static List<String> CreateNewSortedFileByColumn(string orderByColumnName)
        {
            DataTable dt = GetDataTableFromTextFile();

            DataView dv = dt.DefaultView;
            dv.Sort = orderByColumnName;
            dt = dv.ToTable();
            List<string> newLines = new List<string>();
            foreach (DataRow dataRow in dt.Rows)
            {
                string newLine = "";
                foreach (var item in dataRow.ItemArray)
                {
                    if (item.ToString() != "")
                        newLine += item + ", ";
                }
                newLines.Add(newLine);
            }

            newLines.Insert(0, _lines[0]);
            newLines.Insert(1, _lines[1]);

            return newLines;
        }

        public static IEnumerable<String> ReverseLines()
        {
            return _lines.Reverse();
        }

    }

}
