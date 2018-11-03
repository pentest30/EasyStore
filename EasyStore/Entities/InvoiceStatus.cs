using System.ComponentModel;

namespace EasyStore.Entities
{
    public enum InvoiceStatus
    {
        [Description("Facture réglée")]
        Reglée,
        [Description("Facture non réglée")]
        Non_Reglée,
        Canceled
    }

}