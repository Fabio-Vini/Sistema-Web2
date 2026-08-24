using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Quic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TP01_SWEB2.Negócio
{
    public class Book
    {
        
        public string Name { get; set; }
        public Author[] Authors { get; set; }
        public double Price { get; set; }
        public int Qty { get; set; }

        
        public Book(string name, Author[] authors, double price, int qty)
        {
            this.Name = name;
            this.Authors = authors;
            this.Price = price;
            this.Qty = qty;
        }

        
        public Book(string name, Author[] authors, double price)
        {
            this.Name = name;
            this.Authors = authors;
            this.Price = price;
            this.Qty = 0; 
        }

        public string getName(string name)
        {
            return name;
        }

        public Author[] GetAuthors()
        {
            return Authors;
        }

        public  double getPrice()
        {
            return Price;
        }

        public void setPrice(double price)
        {
            this.Price = price;
        }

        public int getQty()
        {
            return this.Qty;
        }

        public void setQty(int qty)
        {
            this.Qty = qty;
        }

        public override string ToString()
        {
            
            string[] authorsFormatted = new string[this.Authors.Length];
            for (int i = 0; i < this.Authors.Length; i++)
            {
                Author a = this.Authors[i];
                authorsFormatted[i] = $"Author[name={a.Name},email={a.Email},gender={a.Gender}]";
            }

            
            string authorsString = string.Join(",", authorsFormatted);

            
            return $"Book[name={this.Name},authors={{{authorsString}}},price={this.Price},qty={this.Qty}]";
        }

        public string getAuthorNames()
        {
            
            string[] names = new string[this.Authors.Length];
            for (int i = 0; i < this.Authors.Length; i++)
            {
                names[i] = this.Authors[i].Name;
            }

           
            return string.Join(",", names);
        }

    }
}
