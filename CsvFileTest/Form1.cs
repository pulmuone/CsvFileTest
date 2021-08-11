using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsvFileTest
{
    //https://joshclose.github.io/CsvHelper/examples/csvdatareader/
    //https://github.com/JoshClose/CsvHelper/issues/1726
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false,
            Delimiter = ";",
            ShouldQuote = args => true
        };

        public class Foo
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            using (var reader = new StreamReader("file.csv"))
            using (var csv = new CsvReader(reader, config))
            {
                var record = new Foo();
                var records = csv.EnumerateRecords(record);
                foreach (var r in records)
                {
                    Console.WriteLine(string.Format("{0} {1}", r.Id, r.Name));
                }
            }
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            var records = new List<Foo>
            {
                new Foo { Id = 1, Name = "one" },
                new Foo { Id = 2, Name = "two" },
            };

            using (var writer = new StreamWriter("file.csv"))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteRecords(records);
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
