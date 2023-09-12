using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Gtr.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string UnitName { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ParentCode { get; set; }
        public string ProductBarcode { get; set; }
        public string Description { get; set; }
        public string BrandName { get; set; }
        public string SizeName { get; set; }
        public string ColorName { get; set; }
        public string ModelName { get; set; }
        public string VariantName { get; set; }
        public double OldPrice { get; set; }
        public double Price { get; set; }
        public double CostPrice { get; set; }
        public double Stock { get; set; }
        public double TotalPurchase { get; set; }
        public string LastPurchaseDate { get; set; }
        public string LastPurchaseSupplier { get; set; }
        public double TotalSales { get; set; }
        public string LastSalesDate { get; set; }
        public string LastSalesCustomer { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public double CommissionAmount { get; set; }
        public double CommissionPer { get; set; }
        public double PCTN { get; set; }
    }
}
