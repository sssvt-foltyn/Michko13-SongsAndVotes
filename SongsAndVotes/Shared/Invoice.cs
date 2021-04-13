using System;
using System.Collections.Generic;



namespace SongsAndVotes.Shared
{



    /// <summary>
    /// Represents an invoice.
    /// </summary>
    public class Invoice
    {



        /// <summary>Number of this invoice.</summary>
        public int InvoiceNumber { get; set; }

        /// <summary>Accounting year.</summary>
        public int Year { get; set; }

        /// <summary>Name of the counterpart.</summary>
        public string CompanyName { get; set; }

        /// <summary>Total amount invoiced.</summary>
        public decimal TotalAmount { get; set; }

        /// <summary>Date that this invoice was issued on.</summary>
        public DateTime? DateIssued { get; set; }

        /// <summary>Due date.</summary>
        public DateTime? DateDue { get; set; }



        /// <summary>Items of the invoice.</summary>
        public IList<InvoiceItem> InvoiceItems { get; set; }



    }



}
