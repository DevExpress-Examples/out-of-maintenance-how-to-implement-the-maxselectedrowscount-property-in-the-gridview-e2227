using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Q257245
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var list = new BindingList<Item>();
            for (int i = 0; i < 10; i++)
                list.Add(new Item() { ID = i, Name = "Name" + i });
            myGridControl1.DataSource = list;
        }
    }
    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}