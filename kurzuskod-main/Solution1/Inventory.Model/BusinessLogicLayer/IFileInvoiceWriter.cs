namespace Inventory.Model
{
    public interface IFileInvoiceWriter
    {
        void WriteInvoiceLine(string fileName, string invoiceLine);
    }
}
