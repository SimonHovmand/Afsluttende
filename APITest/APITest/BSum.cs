﻿namespace API
{
    public class BSum //Public class BSum
    {
        public BSum(int subtotal, double moms, int price, int totalamount) //Constructor
        {
            SubTotal = subtotal;
            Moms = moms;
            Price = price;
            TotalAmount = totalamount;
        }

        public int SubTotal { get; set; }
        public double Moms { get; set; }
        public int Price { get; set; }
        public int TotalAmount { get; set; }

    }
}
