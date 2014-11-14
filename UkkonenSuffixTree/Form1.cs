using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UkkonenSuffixTree
{
    public partial class Form1 : Form
    {
        private SuffixTree suffixTree;
        public Form1()
        {
            InitializeComponent();
        }


        private void IspisiStablo()
        {
            suffixTree.DrawTree();
        }

   
        private void button1_Click(object sender, EventArgs e)
        {
            suffixTree = new SuffixTree("abcabx");
            suffixTree.CreateSuffixTree();
            IspisiStablo();
            MessageBox.Show("Stablo stvoreno");
        }
        
    }
}
