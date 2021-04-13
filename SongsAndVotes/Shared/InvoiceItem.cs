using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;



namespace SongsAndVotes.Shared
{



    /// <summary>
    /// Represents an item on an invoice.
    /// </summary>
    public class InvoiceItem
    {



        /// <summary>Primary key.</summary>
        public int ID { get; set; }



        /// <summary>Ordinal number of this item on the invoice.</summary>
        public int Ordinal { get; set; }

        /// <summary>Description of the item.</summary>
        public string Description { get; set; }

        /// <summary>Quantity of the goods.</summary>
        public double Quantity { get; set; }

        /// <summary>Price of the goods per unit.</summary>
        public decimal AmountPerUnit { get; set; }

        /// <summary>Total price of the goods related to this invoice item.</summary>
        public decimal Amount { get; set; }



        /// <summary>Invoice that this invoice item belongs to.</summary>
        [JsonIgnore]
        public Invoice Invoice { get; set; }



    }



}
