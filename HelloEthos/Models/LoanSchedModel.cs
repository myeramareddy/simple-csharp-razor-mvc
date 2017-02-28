using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace HelloEthos.Models
{
    //double should be decimals in real prod finance systems.
    public class LoanSchedModel
    {
        [DisplayName("Month")]
        public int month { get; set; }

        [DisplayName("Monthly Payment")]
        public double monthlyPayment { get; set; }

        [DisplayName("Old Balance")]
        public double oldBalance { get; set; }

        [DisplayName("Interest Paid")]
        public double interestPaid { get; set; }

        [DisplayName("Principal Paid")]
        public double principalpaid { get; set; }

        [DisplayName("New Balance")]
        public double newBalance { get; set; }

        public LoanSchedModel(int mon, double intPaid, double prinPaid, double newBal, double oldBal, double monPayment)
        {
            this.month = mon;
            this.monthlyPayment = monPayment;
            this.interestPaid = intPaid;
            this.principalpaid = prinPaid;
            this.newBalance = newBal;
            this.oldBalance = oldBal;
        }

        public LoanSchedModel() { }
    }
}