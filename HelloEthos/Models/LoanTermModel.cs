using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HelloEthos.Models
{
    public class LoanTermModel
    {
        public static int MONTHS_IN_YEAR = 12;

        [DisplayName("Loan Amount")]
        [Required(ErrorMessage = "Loan amount is required")]
        [Range(0, 9999999999.99, ErrorMessage = "Please enter a valid decimal Number with 2 decimal places")]
        public double amount { get; set; }

        [DisplayName("Annual Interest Rate")]
        public double annualInterestRate { get; set; }

        [DisplayName("Number of Months")]
        [Required(ErrorMessage = "Number of months is required")]
        public int numOfMonths { get; set; }

        public LoanTermModel(double amt, double intRate, int months)
        {
            this.amount = amt;
            this.annualInterestRate = intRate;
            this.numOfMonths = months;
        }

        public LoanTermModel() {}

        public string toString() {
            return "Given loan terms : $"+this.amount+" at an annual rate of "+this.annualInterestRate+"% for "+this.numOfMonths+" months.";
        }

        //checked numbers against - http://www.calculator.net/amortization-calculator.html
        public List<LoanSchedModel> getLoanScheduleForInputs() {
            var sched = new List<LoanSchedModel>();

            if (this.amount <= 0 || this.numOfMonths <= 0)
            {
              return sched;
            }

            double loanAmount = this.amount;
            //for number% into decimal
            double annualInterestRate = this.annualInterestRate / 100;
            int totalMonths = this.numOfMonths;

            double monthlyInterestRate = annualInterestRate / MONTHS_IN_YEAR;
            double monthlyPayment = Math.Round(computeMonthlyPayment(loanAmount, monthlyInterestRate, totalMonths), 2);
            double totalPayment = monthlyPayment * totalMonths;
            double newBalance = loanAmount;
            double oldBalance = loanAmount;
            double interest;
            double principal;

            for (int month = 1; month <= totalMonths; month++) {
                interest = Math.Round(monthlyInterestRate * newBalance, 2);
                principal = Math.Round(monthlyPayment - interest, 2);
                newBalance = Math.Round(newBalance - principal, 2);

                if(month == totalMonths) { //some rounding issues due to use of double as opposed to decimal
                    newBalance = 0.0;
                }

                sched.Add(new LoanSchedModel(month, interest, principal, newBalance, oldBalance, monthlyPayment));

                oldBalance = (int) (oldBalance - principal);
                //oldBalance -= principal;
            }

            return sched;
        }

        public double computeMonthlyPayment(double loanAmt, double monthlyIntRate, int numOfMonths)
        {
            return loanAmt * (monthlyIntRate / (1 - Math.Pow(1 + monthlyIntRate, -numOfMonths)));
        }
    }
}