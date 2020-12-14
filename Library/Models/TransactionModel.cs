using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models
{
    public class TransactionModel
    {
        public uint Id { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public DateTime Created_at { get; set; }
        public uint Users_id { get; set; }
    }
}
