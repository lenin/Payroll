using System;
using System.Data.SqlClient;
using NUnit.Framework;
using Payroll;

namespace PayrollTest
{
    [TestFixture]
    public class PayrollTest
    {
        private static PayrollDatabase database;
        
        [SetUp]
        public void SetUp()
        {
            database = new InMemoryPayrollDatabase();
        }

        [Test]
        public void AddSalariedEmployee()
        {
            int empId = 1;
            AddSalariedEmployee t = new AddSalariedEmployee(empId, "Bob", "Home", 1000.00, database);
            t.Execute();

            Employee e = database.GetEmployee(empId);
            Assert.AreEqual("Bob", e.Name);

            PaymentClassification pc = e.Classification;
            Assert.IsTrue(pc is SalariedClassification);
            SalariedClassification sc = pc as SalariedClassification;
            Assert.AreEqual(1000.00, sc.Salary, .001);
            PaymentSchedule ps = e.Schedule;
            Assert.IsTrue(ps is MonthlySchedule);

            PaymentMethod pm = e.Method;
            Assert.IsTrue(pm is HoldMethod);
        }

        [Test]
        public void DeleteEmployee()
        {
            int empId = 4;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empId, "Bob", "Home", 2500, 3.2, database);
            t.Execute();

            Employee e = database.GetEmployee(empId);
            Assert.IsNotNull(e);
            DeleteEmployeeTransaction dt = new DeleteEmployeeTransaction(empId, database);
            dt.Execute();

            e = database.GetEmployee(empId);
            Assert.IsNull(e);
        }

        [Test]
        public void AddTimeCardTransaction()
        {
            int empId = 5;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25, database);
            t.Execute();

            TimeCardTransaction tct = new TimeCardTransaction(new DateTime(2005, 7, 31), 8.0, empId, database);
            tct.Execute();

            Employee e = database.GetEmployee(empId);
            Assert.IsNotNull(e);

            PaymentClassification pc = e.Classification;
            Assert.IsTrue(pc is HourlyClassification);
            HourlyClassification hc = pc as HourlyClassification;

            TimeCard tc = hc.GetTimeCard(new DateTime(2005, 7, 31));
            Assert.IsNotNull(tc);
            Assert.AreEqual(8.0, tc.Hours);
        }

        [Test]
        public void AddServiceCharge()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25, database);
            t.Execute();

            Employee e = database.GetEmployee(empId);
            Assert.IsNotNull(e);
            UnionAffiliation af = new UnionAffiliation();
            e.Affiliation = af;

            int memberId = 86; // Maxwell Smart
            database.AddUnionMember(memberId, e);
            ServiceChargeTransaction sct = new ServiceChargeTransaction(
                memberId, new DateTime(2005, 8, 8), 12.95, database);
            sct.Execute();
            ServiceCharge sc = af.GetServiceCharge(new DateTime(2005, 8, 8));
            Assert.IsNotNull(sc);
            Assert.AreEqual(12.95, sc.Amount, .001);
        }

        [Test]
        public void TestChangeNameTransaction()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25, database);
            t.Execute();

            ChangeNameTransaction cnt = new ChangeNameTransaction(empId, "Bob", database);
            cnt.Execute();

            Employee e = database.GetEmployee(empId);
            Assert.IsNotNull(e);
            Assert.AreEqual("Bob", e.Name);
        }

        [Test]
        public void TestChangeHourlyTransaction()
        {
            int empId = 3;
            AddCommissionedEmployee t = new AddCommissionedEmployee(
                empId, "Lance", "Home", 2500, 3.2, database);
            t.Execute();

            ChangeHourlyTransaction cht = new ChangeHourlyTransaction(empId, 27.52, database);
            cht.Execute();

            Employee e = database.GetEmployee(empId);
            Assert.IsNotNull(e);
            PaymentClassification pc = e.Classification;
            Assert.IsNotNull(pc);
            Assert.IsTrue(pc is HourlyClassification);
            HourlyClassification hc = pc as HourlyClassification;
            Assert.AreEqual(27.52, hc.HourlyRate, .001);
            PaymentSchedule ps = e.Schedule;
            Assert.IsTrue(ps is WeeklySchedule);
        }

        [Test]
        public void ChangeUnionMember()
        {
            int empId = 8;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25, database);
            t.Execute();

            int memberId = 7743;
            ChangeMemberTransaction cmt = new ChangeMemberTransaction(empId, memberId, 99.42, database);
            cmt.Execute();

            Employee e = database.GetEmployee(empId);
            Assert.IsNotNull(e);
            Affiliation affiliation = e.Affiliation;
            Assert.IsNotNull(affiliation);
            Assert.IsTrue(affiliation is UnionAffiliation);
            UnionAffiliation uf = affiliation as UnionAffiliation;
            Assert.AreEqual(99.42, uf.Dues, .001);
            Employee member = database.GetUnionMember(memberId);
            Assert.IsNotNull(member);
            Assert.AreEqual(e, member);
        }

        [Test]
        public void PaySingleSalariedEmployee()
        {
            int empId = 1;
            AddSalariedEmployee t = new AddSalariedEmployee(
                empId, "Bob", "Home", 1000.00, database);
            t.Execute();

            DateTime payDate = new DateTime(2001, 11, 30);
            PaydayTransaction pt = new PaydayTransaction(payDate, database);
            pt.Execute();

            Paycheck pc = pt.GetPaycheck(empId);
            Assert.IsNotNull(pc);
            Assert.AreEqual(payDate, pc.PayPeriodEndDate);
            Assert.AreEqual(1000.00, pc.GrossPay, .001);
            Assert.AreEqual("Hold", pc.GetField("Disposition"));
            Assert.AreEqual(0.0, pc.Deductions, .001);
            Assert.AreEqual(1000.00, pc.NetPay, .001);
        }

        [Test]
        public void PaySingleSalariedEmployeeOnWrongDate()
        {
            int empId = 1;
            AddSalariedEmployee t = new AddSalariedEmployee(
                empId, "Bob", "Home", 1000.00, database);
            t.Execute();

            DateTime payDate = new DateTime(2001, 11, 29);
            PaydayTransaction pt = new PaydayTransaction(payDate, database);
            pt.Execute();

            Paycheck pc = pt.GetPaycheck(empId);
            Assert.IsNull(pc);
        }

        private void ValidatePaycheck(PaydayTransaction pt, int empid, DateTime payDate, double pay)
        {
            Paycheck pc = pt.GetPaycheck(empid);
            Assert.IsNotNull(pc);

            Assert.AreEqual(payDate, pc.PayPeriodEndDate);
            Assert.AreEqual(pay, pc.GrossPay, .001);
            Assert.AreEqual("Hold", pc.GetField("Disposition"));
            Assert.AreEqual(0.0, pc.Deductions, .001);
            Assert.AreEqual(pay, pc.NetPay, .001);
        }

        [Test]
        public void PaySingleHourlyEmployeeNoTimeCards()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25, database);
            t.Execute();

            DateTime payDate = new DateTime(2001, 11, 9);
            PaydayTransaction pt = new PaydayTransaction(payDate, database);
            pt.Execute();

            ValidatePaycheck(pt, empId, payDate, 0.0);
        }

        [Test]
        public void PaySingleHourlyEmployeeOneTimeCard()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25, database);
            t.Execute();

            DateTime payDate = new DateTime(2001, 11, 9); // Friday

            TimeCardTransaction tc = new TimeCardTransaction(payDate, 2.0, empId, database);
            tc.Execute();
            PaydayTransaction pt = new PaydayTransaction(payDate, database);
            pt.Execute();

            ValidatePaycheck(pt, empId, payDate, 30.5);
        }

        [Test]
        public void PaySingleHourlyEmployeeOvertimeOneTimeCard()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25, database);
            t.Execute();

            DateTime payDate = new DateTime(2001, 11, 9); // Friday

            TimeCardTransaction tc = new TimeCardTransaction(payDate, 9.0, empId, database);
            tc.Execute();
            PaydayTransaction pt = new PaydayTransaction(payDate, database);
            pt.Execute();

            ValidatePaycheck(pt, empId, payDate, (8 + 1.5) * 15.25);
        }

        [Test]
        public void PaySingleHourlyEmployeeOnWrongDate()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25, database);
            t.Execute();
            
            DateTime payDate = new DateTime(2001, 11, 8); // Thursday

            TimeCardTransaction tc = new TimeCardTransaction(payDate, 9.0, empId, database);
            tc.Execute();
            PaydayTransaction pt = new PaydayTransaction(payDate, database);
            pt.Execute();

            Paycheck pc = pt.GetPaycheck(empId);
            Assert.IsNull(pc);
        }

        [Test]
        public void PaySingleHourlyEmployeeTwoTimeCards()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25, database);
            t.Execute();

            DateTime payDate = new DateTime(2001, 11, 9); // Friday

            TimeCardTransaction tc = new TimeCardTransaction(payDate, 2.0, empId, database);
            tc.Execute();
            TimeCardTransaction tc2 = new TimeCardTransaction(payDate.AddDays(-1), 5.0, empId, database);
            tc2.Execute();
            PaydayTransaction pt = new PaydayTransaction(payDate, database);
            pt.Execute();

            ValidatePaycheck(pt, empId, payDate, 7 * 15.25);
        }

        [Test]
        public void PaySingleHourlyEmployeeWithTimeCardsSpanningTwoPayPeriods()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25, database);
            t.Execute();
            
            DateTime payDate = new DateTime(2001, 11, 9); // Friday
            DateTime dateInPreviousPayPeriod = new DateTime(2001, 11, 2);

            TimeCardTransaction tc = new TimeCardTransaction(payDate, 2.0, empId, database);
            tc.Execute();
            TimeCardTransaction tc2 = new TimeCardTransaction(dateInPreviousPayPeriod, 5.0, empId, database);
            tc2.Execute();
            PaydayTransaction pt = new PaydayTransaction(payDate, database);
            pt.Execute();

            ValidatePaycheck(pt, empId, payDate, 2 * 15.25);
        }

        [Test]
        public void SalariedUnionMemberDues()
        {
            int empId = 1;
            AddSalariedEmployee t = new AddSalariedEmployee(empId, "Bob", "Home", 1000.00, database);
            t.Execute();

            int memberId = 7734;
            ChangeMemberTransaction cmt = new ChangeMemberTransaction(empId, memberId, 9.42, database);
            cmt.Execute();
            
            DateTime payDate = new DateTime(2001, 11, 30);
            PaydayTransaction pt = new PaydayTransaction(payDate, database);
            pt.Execute();
            
            Paycheck pc = pt.GetPaycheck(empId);
            Assert.IsNotNull(pc);
            Assert.AreEqual(payDate, pc.PayPeriodEndDate);
            Assert.AreEqual(1000.0, pc.GrossPay, .001);
            Assert.AreEqual("Hold", pc.GetField("Disposition"));
            Assert.AreEqual(5 * 9.42, pc.Deductions, .001);
            Assert.AreEqual(1000.0 - 5 * 9.42, pc.NetPay, .001);
        }

        [Test]
        public void HourlyUnionMemberServiceCharge()
        {
            int empId = 1;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.24, database);
            t.Execute();

            int memberId = 7734;
            ChangeMemberTransaction cmt = new ChangeMemberTransaction(empId, memberId, 9.42, database);
            cmt.Execute();

            DateTime payDate = new DateTime(2001, 11, 9);
            ServiceChargeTransaction sct = new ServiceChargeTransaction(memberId, payDate, 19.42, database);
            sct.Execute();

            TimeCardTransaction tct = new TimeCardTransaction(payDate, 8.0, empId, database);
            tct.Execute();

            PaydayTransaction pt = new PaydayTransaction(payDate, database);
            pt.Execute();

            Paycheck pc = pt.GetPaycheck(empId);
            Assert.IsNotNull(pc);
            Assert.AreEqual(payDate, pc.PayPeriodEndDate);
            Assert.AreEqual(8 * 15.24, pc.GrossPay, .001);
            Assert.AreEqual("Hold", pc.GetField("Disposition"));
            Assert.AreEqual(9.42 + 19.42, pc.Deductions, .001);
            Assert.AreEqual((8 * 15.24) - (9.42 + 19.42), pc.NetPay, .001);
        }

        [Test]
        public void ServiceChargesSpanningMultiplePayPeriods()
        {
            int empId = 1;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.24, database);
            t.Execute();

            int memberId = 7734;
            ChangeMemberTransaction cmt = new ChangeMemberTransaction(empId, memberId, 9.42, database);
            cmt.Execute();

            DateTime payDate = new DateTime(2001, 11, 9);
            DateTime earlyDate = new DateTime(2001, 11, 2); // previous Friday
            DateTime lateDate = new DateTime(2001, 11, 16); // next Friday
            ServiceChargeTransaction sct = new ServiceChargeTransaction(memberId, payDate, 19.42, database);
            sct.Execute();

            ServiceChargeTransaction sctEarly = new ServiceChargeTransaction(memberId, earlyDate, 100.00, database);
            sctEarly.Execute();

            ServiceChargeTransaction sctLate = new ServiceChargeTransaction(memberId, lateDate, 200.00, database);
            sctLate.Execute();

            TimeCardTransaction tct = new TimeCardTransaction(payDate, 8.0, empId, database);
            tct.Execute();

            PaydayTransaction pt = new PaydayTransaction(payDate, database);
            pt.Execute();

            Paycheck pc = pt.GetPaycheck(empId);
            Assert.IsNotNull(pc);
            Assert.AreEqual(payDate, pc.PayPeriodEndDate);
            Assert.AreEqual(8 * 15.24, pc.GrossPay, .001);
            Assert.AreEqual("Hold", pc.GetField("Disposition"));
            Assert.AreEqual(9.42 + 19.42, pc.Deductions, .001);
            Assert.AreEqual((8 * 15.24) - (9.42 + 19.42), pc.NetPay, .001);
        }

        [Test]
        public void PaySingleCommissionedEmployeeNoSalesReceipts()
        {
            int empId = 1;
            AddCommissionedEmployee t = new AddCommissionedEmployee(
                empId, "Bob", "Home", 1000.00, 10, database);
            t.Execute();

            DateTime payDate = new DateTime(2001, 11, 16); // Friday
            PaydayTransaction pt = new PaydayTransaction(payDate, database);
            pt.Execute();

            Paycheck pc = pt.GetPaycheck(empId);
            Assert.IsNotNull(pc);
            Assert.AreEqual(payDate, pc.PayPeriodEndDate);
            Assert.AreEqual(1000.00, pc.GrossPay, .001);
            Assert.AreEqual("Hold", pc.GetField("Disposition"));
            Assert.AreEqual(0.0, pc.Deductions, .001);
            Assert.AreEqual(1000.00, pc.NetPay, .001);
        }

        [Test]
        public void SalesReceiptTransaction()
        {
            int empId = 5;
            AddCommissionedEmployee t = new AddCommissionedEmployee(
                empId, "Bob", "Home", 1000.00, 10, database);
            t.Execute();

            SalesReceiptTransaction srt = new SalesReceiptTransaction(
                new DateTime(2005, 7, 31), 100.0, empId, database);
            srt.Execute();

            Employee e = database.GetEmployee(empId);
            Assert.IsNotNull(e);

            PaymentClassification pc = e.Classification;
            Assert.IsTrue(pc is CommissionedClassification);
            CommissionedClassification cc = pc as CommissionedClassification;

            SalesReceipt sr = cc.GetSalesReceipt(new DateTime(2005, 7, 31));
            Assert.IsNotNull(sr);
            Assert.AreEqual(100, sr.Amount);
        }
        
        [Test]
        public void PaySingleCommissionedEmployeeOneSalesReceipt()
        {
            int empId = 5;
            AddCommissionedEmployee t = new AddCommissionedEmployee(
                empId, "Bob", "Home", 1000.00, 10.00, database);
            t.Execute();

            DateTime payDate = new DateTime(2001, 11, 9); // Friday

            SalesReceiptTransaction srt = new SalesReceiptTransaction(
                payDate, 100.00, empId, database);
            srt.Execute();
            PaydayTransaction pt = new PaydayTransaction(payDate, database);
            pt.Execute();

            ValidatePaycheck(pt, empId, payDate, 1010);
        }

        [Test]
        public void PaySingleCommissionedEmployeeOnWrongDate()
        {
            int empId = 2;
            AddCommissionedEmployee t = new AddCommissionedEmployee(
                empId, "Bob", "Home", 1000.00, 10.00, database);
            t.Execute();

            DateTime payDate = new DateTime(2001, 11, 8); // Thursday

            SalesReceiptTransaction srt = new SalesReceiptTransaction(
                payDate, 100.00, empId, database);
            srt.Execute();
            PaydayTransaction pt = new PaydayTransaction(payDate, database);
            pt.Execute();

            Paycheck pc = pt.GetPaycheck(empId);
            Assert.IsNull(pc);
        }

        [Test]
        public void PaySingleCommissionedEmployeeTwoSalesReceipts()
        {
            int empId = 2;
            AddCommissionedEmployee t = new AddCommissionedEmployee(
                empId, "Bob", "Home", 1000.00, 10.00, database);
            t.Execute();

            DateTime payDate = new DateTime(2001, 11, 9); // Friday

            SalesReceiptTransaction srt = new SalesReceiptTransaction(
                payDate, 100.00, empId, database);
            srt.Execute();
            SalesReceiptTransaction srt2 = new SalesReceiptTransaction(
                payDate.AddDays(-1), 200.00, empId, database);
            srt2.Execute();
            PaydayTransaction pt = new PaydayTransaction(payDate, database);
            pt.Execute();

            ValidatePaycheck(pt, empId, payDate, 1030.00);
        }

        [Test]
        public void PaySingleCommissionedEmployeeWithSalesReceiptsSpanningTwoPayPeriods()
        {
            int empId = 2;
            AddCommissionedEmployee t = new AddCommissionedEmployee(
                empId, "Bob", "Home", 1000.00, 10.00, database);
            t.Execute();

            DateTime payDate = new DateTime(2001, 11, 16); // Friday
            DateTime dateInPreviousPayPeriod = new DateTime(2001, 11, 2);

            SalesReceiptTransaction srt = new SalesReceiptTransaction(
                payDate, 100.00, empId, database);
            srt.Execute();
            SalesReceiptTransaction srt2 = new SalesReceiptTransaction(
                dateInPreviousPayPeriod, 200.00, empId, database);
            srt2.Execute();
            PaydayTransaction pt = new PaydayTransaction(payDate, database);
            pt.Execute();

            ValidatePaycheck(pt, empId, payDate, 1010.00);
        }

        [Test]
        public void PaySingleCommissionedEmployeeBiWeekly()
        {
            int empId = 2;
            AddCommissionedEmployee t = new AddCommissionedEmployee(
                empId, "Bob", "Home", 1000.00, 10.00, database);
            t.Execute();

            DateTime payDate = new DateTime(2001, 11, 9); // Friday
            DateTime payDate2 = new DateTime(2001, 11, 16); // Next friday

            PaydayTransaction pt = new PaydayTransaction(payDate, database);
            pt.Execute();
            ValidatePaycheck(pt, empId, payDate, 1000.00);

            PaydayTransaction pt2 = new PaydayTransaction(payDate2, database);
            pt2.Execute();
            Paycheck pc = pt2.GetPaycheck(empId);
            Assert.IsNull(pc);
        }
    }
}
