using System;
using System.IO;

namespace Inventory.Model
{
    public class FileInvoiceWriter : IFileInvoiceWriter
    {
        private readonly IApplicationServices applicationServices;

        public FileInvoiceWriter(IApplicationServices applicationServices)
        {
            this.applicationServices = applicationServices ?? throw new ArgumentNullException(nameof(applicationServices));
        }

        public void WriteInvoiceLine(string fileName, string invoiceLine)
        {
            File.WriteAllText(fileName, invoiceLine);
        }
    }
}
