using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<Category> lst = new List<Category>();

        private void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["northwind"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT CategoryID,CategoryName,Description FROM [Categories]";
                command.Connection = connection;
                connection.Open();

                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        int categoryId = (int)dataReader["CategoryID"];
                        string categoryName = (string)dataReader["CategoryName"];
                        string categoryDesc = (string)dataReader["Description"];

                        Category cat = new Category();
                        cat.CategoryId = categoryId;
                        cat.CategoryName = categoryName;
                        cat.Description = categoryDesc;


                        comboBox1.DataSource = lst;
                        comboBox1.DisplayMember = "CategoryName";
                        comboBox1.ValueMember = "CategoryId";

                        comboBox1.Items.Add(dataReader["CategoryName"]);
                    }
                        command.Close();
                }
            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Text = comboBox1.SelectedValue.ToString();
        }
    }

    class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{CategoryName} {Description}";
        }
    }
}
