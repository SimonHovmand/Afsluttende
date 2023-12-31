﻿namespace API
{
    public class Item //Public class Item
    {
        public Item(string name, int price, int amount, string image1, string image2, string image3, int size, int color, int iD) //Constructor
        {
            Name = name;
            Price = price;
            Amount = amount;
            Image1 = image1;
            Image2 = image2;
            Image3 = image3;
            Size = size;
            Color = color;
            ID = iD;
        }

        public string Name { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public int Size { get; set; }
        public int Color { get; set; }
        public int ID { get; set; }

    }

    public class BItem //Public class BItem
    {
        public BItem(string name, int price, int itemamount, string image1, string image2, string image3, string color, string size, int amount, int iD) //Constructor
        {
            Name = name;
            Price = price;
            ItemAmount = itemamount;
            Image1 = image1;
            Image2 = image2;
            Image3 = image3;
            Color = color;
            Size = size;
            Amount = amount;
            ID = iD;
        }

        public string Name { get; set; }
        public int Price { get; set; }
        public int ItemAmount { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public int Amount { get; set; }
        public int ID { get; set; }
    }
}
