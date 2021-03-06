﻿using System;
using System.ComponentModel;
using System.Data;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace ButcherBlock
{
    public partial class PriceForm : Form
    {
        private int clickedProductId;
        private string clickedProductName;
        private double clickedProductPrice;

        List<Product> products = new List<Product>();

        public PriceForm(InvoiceForm p)
        {
            InitializeComponent();
            loadPrices();
        }

        public void loadPrices()
        {
            SQLiteHelper helper = new SQLiteHelper();
            products = helper.getAllProducts();

            displayProducts();
        }

        public void displayProducts()
        {
            myListView.Clear();

            myListView.Columns.Add("Id");
            myListView.Columns.Add("Product");
            myListView.Columns.Add("Price");

            for (int i = 0; i < products.Count; i++)
            {
                ListViewItem aFooItem = new ListViewItem((i+1).ToString());
                ListViewItem.ListViewSubItem aSubFooItem1 = new ListViewItem.ListViewSubItem(aFooItem, ((Product)products[i]).Name);
                ListViewItem.ListViewSubItem aSubFooItem2 = new ListViewItem.ListViewSubItem(aFooItem, ((Product)products[i]).PriceString); 
                aFooItem.SubItems.Add(aSubFooItem1);
                aFooItem.SubItems.Add(aSubFooItem2);
                myListView.Items.Add(aFooItem);
            }

            ResizeListViewColumns(myListView);
        }

        private void ResizeListViewColumns(ListView lv)
        {
            foreach (ColumnHeader column in lv.Columns)
            {
                column.Width = -2;
            }
        }

        private void myListView_Click(object sender, EventArgs e)
        {
            if (myListView.SelectedItems.Count > 0)
            {

                clickedProductId = ((Product)products[myListView.SelectedItems[0].Index]).Id;
                clickedProductName = ((Product)products[myListView.SelectedItems[0].Index]).Name;
                clickedProductPrice = ((Product)products[myListView.SelectedItems[0].Index]).Price;
                new EditForm(this, clickedProductId, clickedProductName, clickedProductPrice).Show();

            }   
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            new AddForm(this).Show();
        }
    }
}
