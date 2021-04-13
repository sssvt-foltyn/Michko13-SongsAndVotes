﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SongsAndVotes.Shared;



namespace SongsAndVotes.Server.Controllers
{



    [ApiController]
    [Route("/invoice")]
    public class RoutingTestController : ControllerBase
    {



        private readonly ILogger<RoutingTestController> _logger;



        public RoutingTestController(ILogger<RoutingTestController> logger)
        {
            _logger = logger;
        }



        [HttpGet]
        [Route("list")]
        public IEnumerable<Invoice> GetList()
        {
            _logger.LogInformation(this.ControllerContext.ToCtxString());

            return MockInvoices();
        }



        [HttpGet]
        [Route("{id}")]
        public Invoice Load(int id)
        {
            _logger.LogInformation(this.ControllerContext.ToCtxString());

            return MockInvoices().First();
        }



        private IList<Invoice> MockInvoices()
        {
            IList<Invoice> invoices = new List<Invoice>();

            Invoice invoice1 = new Invoice { InvoiceNumber = 1, Year = 2021, CompanyName = "IBM", TotalAmount = 520.40m, DateIssued = new DateTime(2021, 01, 03), DateDue = new DateTime(2021, 01, 24) };
            Invoice invoice2 = new Invoice { InvoiceNumber = 2, Year = 2021, CompanyName = "Microsoft", TotalAmount = 1011.20m, DateIssued = new DateTime(2021, 01, 17), DateDue = new DateTime(2021, 02, 7) };
            Invoice invoice3 = new Invoice { InvoiceNumber = 3, Year = 2021, CompanyName = "Alza", TotalAmount = 490.00m, DateIssued = new DateTime(2021, 01, 29), DateDue = new DateTime(2021, 02, 19) };
            Invoice invoice4 = new Invoice { InvoiceNumber = 4, Year = 2021, CompanyName = "IBM", TotalAmount = 110.30m, DateIssued = new DateTime(2021, 02, 16), DateDue = new DateTime(2021, 03, 9) };
            Invoice invoice5 = new Invoice { InvoiceNumber = 5, Year = 2021, CompanyName = "Albert", TotalAmount = 343.20m, DateIssued = new DateTime(2021, 02, 21), DateDue = new DateTime(2021, 03, 21) };
            Invoice invoice6 = new Invoice { InvoiceNumber = 6, Year = 2021, CompanyName = "Rohlík.cz", TotalAmount = 1410.65m, DateIssued = new DateTime(2021, 03, 15), DateDue = new DateTime(2021, 04, 5) };
            Invoice invoice7 = new Invoice { InvoiceNumber = 7, Year = 2021, CompanyName = "Rohlík.cz", TotalAmount = 2900.48m, DateIssued = new DateTime(2021, 03, 19), DateDue = new DateTime(2021, 04, 19) };

            invoice1.InvoiceItems = MockInvoiceItems1(invoice1);
            invoice3.InvoiceItems = MockInvoiceItems3(invoice3);

            invoices.Add(invoice1);
            invoices.Add(invoice2);
            invoices.Add(invoice3);
            invoices.Add(invoice4);
            invoices.Add(invoice5);
            invoices.Add(invoice6);
            invoices.Add(invoice7);

            return invoices;
        }



        // Caution: This method adjusts the value of invoice.TotalAmount.
        private IList<InvoiceItem> MockInvoiceItems1(Invoice invoice)
        {
            IList<InvoiceItem> invoiceItems = new List<InvoiceItem>();

            InvoiceItem invoiceItem1 = new InvoiceItem { Ordinal = 1, Description = "New laptop", Quantity = 2, AmountPerUnit = 199.00m, Amount = 398.00m, Invoice = invoice };
            InvoiceItem invoiceItem2 = new InvoiceItem { Ordinal = 2, Description = "Accessories", Quantity = 1, AmountPerUnit = 3.99m, Amount = 3.99m, Invoice = invoice };
            InvoiceItem invoiceItem3 = new InvoiceItem { Ordinal = 3, Description = "Ethernet cables", Quantity = 4, AmountPerUnit = 29.50m, Amount = 118.00m, Invoice = invoice };
            InvoiceItem invoiceItem4 = new InvoiceItem { Ordinal = 4, Description = "Adjustment", Quantity = 1, AmountPerUnit = 0.41m, Amount = 0.41m, Invoice = invoice };

            invoiceItems.Add(invoiceItem1);
            invoiceItems.Add(invoiceItem2);
            invoiceItems.Add(invoiceItem3);
            invoiceItems.Add(invoiceItem4);

            //IQueryable<InvoiceItem> query = from ii in ((List<InvoiceItem>) invoiceItems)
            //                                select ii;
            //IQueryable<InvoiceItem> query = ((List<InvoiceItem>) invoiceItems);
            //IQueryable<InvoiceItem> query = from ii in invoiceItems.AsQueryable<InvoiceItem>()
            //                                select ii;

            //IEnumerable<InvoiceItem> selectedInvoiceItems = from ii in invoiceItems
            //                                                where ii.Amount >= 100.00m
            //                                                select ii;
            //return selectedInvoiceItems.ToList<InvoiceItem>();
            //decimal totalAmount = from ii in invoiceItems
            //                      where ii.Amount >= 100.00m
            //                      select ii;

            //IQueryable<decimal> query = from ii in invoiceItems.AsQueryable<InvoiceItem>()
            //                            select ii.Amount;
            //decimal totalAmount = query.Sum();
            //decimal totalAmount = invoiceItems.Select(ii => ii.Amount).Sum();
            //decimal totalAmount = invoiceItems.Map(ii => ii.Amount).Sum();
            decimal totalAmount = invoiceItems.Sum(ii => ii.Amount);
            invoice.TotalAmount = totalAmount;

            return invoiceItems;
        }



        // Caution: This method adjusts the value of invoice.TotalAmount.
        private IList<InvoiceItem> MockInvoiceItems3(Invoice invoice)
        {
            IList<InvoiceItem> invoiceItems = new List<InvoiceItem>();

            InvoiceItem invoiceItem1 = new InvoiceItem { Ordinal = 1, Description = "Mop", Quantity = 1, AmountPerUnit = 120.00m, Amount = 120.00m, Invoice = invoice };
            InvoiceItem invoiceItem2 = new InvoiceItem { Ordinal = 2, Description = "Bucket", Quantity = 2, AmountPerUnit = 70.00m, Amount = 140.00m, Invoice = invoice };
            InvoiceItem invoiceItem3 = new InvoiceItem { Ordinal = 3, Description = "Soap", Quantity = 10, AmountPerUnit = 19.00m, Amount = 190.00m, Invoice = invoice };
            InvoiceItem invoiceItem4 = new InvoiceItem { Ordinal = 4, Description = "Water in PET", Quantity = 3, AmountPerUnit = 7.00m, Amount = 21.00m, Invoice = invoice };
            InvoiceItem invoiceItem5 = new InvoiceItem { Ordinal = 5, Description = "SAVO", Quantity = 1, AmountPerUnit = 19.00m, Amount = 19.00m, Invoice = invoice };

            invoiceItems.Add(invoiceItem1);
            invoiceItems.Add(invoiceItem2);
            invoiceItems.Add(invoiceItem3);
            invoiceItems.Add(invoiceItem4);
            invoiceItems.Add(invoiceItem5);

            decimal totalAmount = invoiceItems.Sum(ii => ii.Amount);
            invoice.TotalAmount = totalAmount;

            return invoiceItems;
        }



    }



}
