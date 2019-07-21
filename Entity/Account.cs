using System;

namespace Entity
{
    public class Account : EntityBase
    {
        public Guid Guid { get; set; }
        public string HolderAccountName { get; set; }
        public string AccountNumber { get; set; }
        public decimal ValueBalance { get; set; }
        public decimal ValueLimit { get; set; }
        public bool Blocked { get; set; }
        public decimal GetTotalAmount()
        {
            return this.ValueBalance + this.ValueLimit;
        }
    }
}
