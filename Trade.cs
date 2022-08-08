using System;
using System.Collections.Generic;
using System.Text;

namespace CreditSuisse
{
    class Trade : ITrade
    {
        public double Value { get; set; }

        public ClientType ClientType { get; set; }

        public DateTime NextPaymentDate { get; set; }

        public string ClientSector { get; set; }
        public Categories Category { get; set; }
        public int NumLines { get; set; }
        public DateTime ReferenceDate { get; set; }

    }

    internal interface ITrade
    {
        double Value { get; }
        string ClientSector { get; }
        DateTime NextPaymentDate { get; }
    }

    public enum ClientType { PRIVATE, PUBLIC }
    public enum Categories { EXPIRED, HIGHRISK, MEDIUMRISK }
}
