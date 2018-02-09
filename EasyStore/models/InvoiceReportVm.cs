using System;

namespace EasyStore.models
{
    public class InvoiceReportVm
    {
        public string InvoiceNumber { get; set; }
        public DateTime Date { get; set; }
        public string Store { get; set; }
        public string City { get; set; }
        public string Tel { get; set; }
        public string NIF { get; set; }
        public string NRC { get; set; }
        public string Product { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmmount { get; set; }
        public decimal PaidAmmount { get; set; }
        public decimal Left { get; set; }
        public int Qnt { get; set; }
        public decimal SubTotal { get; set; }
        public decimal LineTotal { get; set; }
        public string Customer { get; set; }
        public string MeasureUnit { get; set; }
    }
}