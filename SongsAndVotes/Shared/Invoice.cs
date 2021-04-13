using System;
using System.Collections.Generic;



namespace SongsAndVotes.Shared
{



    /// <summary>
    /// Represents an invoice.
    /// </summary>
    public class Invoice
    {



        /// <summary>Primary key.</summary>
        public int ID { get; set; }



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



        public override string ToString()
        {
            return $"ID: {ID}    Invoice number: {InvoiceNumber}    Year: {Year}    Company name: {CompanyName}    Total amount: {TotalAmount}    Date issued: {DateIssued?.ToShortDateString()}    Date due: {DateDue?.ToShortDateString()}";
        }



    }



}
