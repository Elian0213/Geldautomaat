using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models
{
    public class UserModel
    {
        public uint Id { get; set; }
        public string Email { get; set; }
        public string Sex { get; set; }
        public DateTime Birthday { get; set; }
        public int Bsn_number { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Address { get; set; }
        public string Zipcode { get; set; }
        public string Town { get; set; }
        public string Pincode { get; set; }
        public int Account_number { get; set; }
        public decimal Balance { get; set; }
        public int Blocked { get; set; }
    }
}
