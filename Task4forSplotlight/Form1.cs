using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task4forSplotlight
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            createtable();
            SplitColumns();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public void createtable()
        {

            string csv = System.IO.File.ReadAllText("items.bat");


            GridView1.DataSource = SplitColumns();

            Console.WriteLine(" Retrieving all the products that are not 'Heineken'\n");


            DataTable dr = SplitColumns();


            DataRow[] foundRows;

            //foundRows = dr.Select(expression, sortOrder);

            DataSet ds = new DataSet();
            ds.Tables.Add(dr);

            //foundRows = dr.Select("product_name <> 'Heineken'");
            DataView dv = new DataView(dr);

            // Row filter was better than linq <> means not 

            dv.RowFilter = "product_name <> 'Heineken'";

            PrintDataView(dv); // uses the print method as seen below

        }
        private static void PrintDataView(DataView dv)
        {
            // Printing first DataRowView to demo that the row in the first index of the DataView changes depending on sort and filters
            Console.WriteLine("First DataRowView value is '{0}'", dv[0]["site_id"]);

            // Printing all DataRowViews
            foreach (DataRowView drv in dv)
            {
                
                Console.WriteLine("\t" + drv["site_id"] + "\t" + drv["post_code_char"] + "\t" + drv["region"] + "\t" + drv["transaction_id"] + "\t" + drv["date_of_day"] + "\t" + drv["barcode"] + "\t" + drv["product_name"] + "\t" + drv["category"] + "\t" + drv["units"] + "\t" + drv["gross_value"]);
            }
        }




            private static System.Data.DataTable SplitColumns()
        {
            System.Data.DataTable table = new System.Data.DataTable("dataFromFile");




            string file = "items.bat";


            using (StreamReader sr = new StreamReader(file))
            {
                string line;
                int rowsCount = 0;
                int getridofffristrow = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    /// If there are any blanks then the program will fill the empty slots with a " " space.
                    /// // This would work for any other empty slots.

                    if(line.Contains("\t\t"))
                    {
                        int index = line.IndexOf("\t\t");

                        string value = " ";

                        int newindexforinser = index + 1;

                        string newline = line.Insert(newindexforinser, value);

                        line = newline;
                    }

                    string[] data = line.Split(new string[] { "\t"}, StringSplitOptions.RemoveEmptyEntries);


                    if (table.Columns.Count == 0)
                    {

                        foreach (string headerWord in data)
                        {                        
                            table.Columns.Add(new DataColumn(headerWord));
                        }
                    }
                    table.Rows.Add();

                    
                        for (int i = 0; i < data.Length; i++) //this was 0 
                        {                     
                            table.Rows[rowsCount][i] = data[i];
                        }
                      
                   rowsCount++;
                }
            }
            table.Rows.RemoveAt(0); // just removes collum names being the first row
            return table;
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
