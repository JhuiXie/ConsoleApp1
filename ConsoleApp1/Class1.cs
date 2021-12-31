using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    public class InvoiceRepository
    {
        //private IQueryable<Invoice> currentInvoices;
        private List<Invoice> currentInvoices;

        //todo:
        //public InvoiceRepository(IQueryable<Invoice> invoices)
        public InvoiceRepository(List<Invoice> invoices)
        {
            //check if invoices is null 
            if (invoices == null || invoices?.Count() == 0)
            {
                throw new ArgumentNullException();
            }

            currentInvoices = invoices;
        }

        /// <summary>
        /// Should return a total value of an invoice with a given id. If an invoice does not exist null should be returned.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        public decimal? GetTotal(int invoiceId)
        {
            if (currentInvoices.Any(o => o.Id == invoiceId))
            {
                decimal total = 0;
                //incase more than 1 invoices has the same id
                var invoiceItems = currentInvoices.Where(o => o.Id == invoiceId && o.InvoiceItems != null && o.InvoiceItems.Count > 0).ToList();

                if (invoiceItems?.Count > 0)
                {
                    foreach (var item in invoiceItems)
                    {
                        total += item.InvoiceItems.Sum(o => o.Price * o.Count);
                    }

                    return total;
                }
            }
            return null;
        }

        /// <summary>
        /// Should return a total value of all unpaid invoices.
        /// </summary>
        /// <returns></returns>
        public decimal GetTotalOfUnpaid()
        {
            decimal unpaid = 0;

            var unpaidInvoices = currentInvoices.Where(o => o.AcceptanceDate == null).ToList();

            if (unpaidInvoices?.Count > 0 && unpaidInvoices.Any(o => o?.InvoiceItems?.Count > 0))
            {
                foreach (var item in unpaidInvoices.Where(o=>o?.InvoiceItems?.Count > 0))
                {
                    unpaid += item.InvoiceItems.Sum(o => o.Price * o.Count);
                }
            }

            return unpaid;
        }

        /// <summary>
        /// Should return a dictionary where the name of an invoice item is a key and the number of bought items is a value.
        /// The number of bought items should be summed within a given period of time (from, to). Both the from date and the end date can be null.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public IReadOnlyDictionary<string, long> GetItemsReport(DateTime? from, DateTime? to)
        {
            Dictionary<string, long> result = new Dictionary<string, long>();

            var filteredInvoices = currentInvoices.Where(o => (from == null || o.CreationDate >= from) && (to == null || o.CreationDate <= to)).ToList();

            if (filteredInvoices?.Count > 0 && filteredInvoices.Any(o => o?.InvoiceItems?.Count > 0))
            {
                foreach (var invoice in filteredInvoices.Where(o=>o?.InvoiceItems?.Count > 0))
                {
                    foreach (var item in invoice.InvoiceItems)
                    {
                        if (result.ContainsKey(item.Name))
                        {
                            result[item.Name] += item.Count;
                        }
                        else
                        {
                            result.Add(item.Name, item.Count);
                        }
                    }
                }
            }

            return result;
        }
    }


    public class Invoice
    {
        // A unique numerical identifier of an invoice (mandatory)
        public int Id { get; set; }
        // A short description of an invoice (optional).
        public string Description { get; set; }
        // A number of an invoice e.g. 134/10/2018 (mandatory).
        public string Number { get; set; }
        // An issuer of an invoice e.g. Metz-Anderson, 600  Hickman Street,Illinois (mandatory).
        public string Seller { get; set; }
        // A buyer of a service or a product e.g. John Smith, 4285  Deercove Drive, Dallas (mandatory).
        public string Buyer { get; set; }
        // A date when an invoice was issued (mandatory).
        public DateTime CreationDate { get; set; }
        // A date when an invoice was paid (optional).
        public DateTime? AcceptanceDate { get; set; }
        // A collection of invoice items for a given invoice (can be empty but is never null).
        public IList<InvoiceItem> InvoiceItems { get; }

        public Invoice()
        {
            InvoiceItems = new List<InvoiceItem>();
        }
    }



    public class InvoiceItem
    {
        // A name of an item e.g. eggs.
        public string Name { get; set; }
        // A number of bought items e.g. 10.
        public uint Count { get; set; }
        // A price of an item e.g. 20.5.
        public decimal Price { get; set; }
    }

}
