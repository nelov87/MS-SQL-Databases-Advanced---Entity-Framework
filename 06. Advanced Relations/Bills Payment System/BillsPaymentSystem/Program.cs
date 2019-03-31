using BillsPaymentSystem.Data;
using System;


namespace BillsPaymentSystem.App
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (BillsPaymentSystemContext context = new BillsPaymentSystemContext())
            {
                DbInitializer.Seed(context);
            }  
        }
    }
}
