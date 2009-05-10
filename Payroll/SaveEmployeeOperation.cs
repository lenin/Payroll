using System;
using System.Data.SqlClient;

namespace Payroll
{
    public class SaveEmployeeOperation
    {
        private readonly Employee employee;
        private readonly SqlConnection connection;

        private string methodCode;
        private string classificationCode;
        private SqlCommand insertPaymentMethodCommand;
        private SqlCommand insertEmployeeCommand;
        private SqlCommand insertClassificationCommand;

        public SaveEmployeeOperation(Employee employee, SqlConnection connection)
        {
            this.employee = employee;
            this.connection = connection;
        }

        public void Execute()
        {
            PrepareToSavePaymentClassification();
            PrepareToSavePaymentMethod();
            PrepareToSaveEmployee();

            SqlTransaction transaction = connection.BeginTransaction("Save Employee");
            try
            {
                ExecuteCommand(insertEmployeeCommand, transaction);
                ExecuteCommand(insertPaymentMethodCommand, transaction);
                ExecuteCommand(insertClassificationCommand, transaction);
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
        }

        private void ExecuteCommand(SqlCommand command, SqlTransaction transaction)
        {
            if (command != null)
            {
                command.Connection = connection;
                command.Transaction = transaction;
                command.ExecuteNonQuery();
            }
        }

        private void PrepareToSaveEmployee()
        {
            string SQL = "INSERT INTO Employee VALUES (" +
                "@EmpId, @Name, @Address, @ScheduleType, " +
                "@PaymentMethodType, @PaymentClassificationType)";
            insertEmployeeCommand = new SqlCommand(SQL);

            insertEmployeeCommand.Parameters.AddWithValue(
                "@EmpId", employee.Id);
            insertEmployeeCommand.Parameters.AddWithValue(
                "@Name", employee.Name);
            insertEmployeeCommand.Parameters.AddWithValue(
                "@Address", employee.Address);
            insertEmployeeCommand.Parameters.AddWithValue(
                "@ScheduleType", ScheduleCode(employee.Schedule));
            insertEmployeeCommand.Parameters.AddWithValue(
                "@PaymentMethodType", methodCode);
            insertEmployeeCommand.Parameters.AddWithValue(
                "@PaymentClassificationType", classificationCode);
        }

        private void PrepareToSavePaymentMethod()
        {
            PaymentMethod method = employee.Method;
            if (method is HoldMethod)
                methodCode = "hold";
            else if (method is DirectDepositMethod)
            {
                methodCode = "directdeposit";
                DirectDepositMethod ddMethod = method as DirectDepositMethod;
                insertPaymentMethodCommand =
                    CreateInsertDirectDepositCommand(ddMethod);
            }
            else if (method is MailMethod)
            {
                methodCode = "mail";
                MailMethod mailMethod = method as MailMethod;
                insertPaymentMethodCommand =
                    CreateInsertMailMethodCommand(mailMethod);
            }
            else
                methodCode = "unknown";
        }

        private SqlCommand CreateInsertDirectDepositCommand(
            DirectDepositMethod ddMethod)
        {
            string SQL = "INSERT INTO DirectDepositAccount " +
                "VALUES (@Bank, @Account, @EmpId)";
            SqlCommand command = new SqlCommand(SQL);
            command.Parameters.AddWithValue("@Bank", ddMethod.Bank);
            command.Parameters.AddWithValue("@Account", ddMethod.AccountNumber);
            command.Parameters.AddWithValue("@EmpId", employee.Id);

            return command;
        }

        private SqlCommand CreateInsertMailMethodCommand(
            MailMethod mailMethod)
        {
            string SQL = "INSERT INTO PaycheckAddress " +
                "VALUES (@Address, @EmpId)";
            SqlCommand command = new SqlCommand(SQL);
            command.Parameters.AddWithValue("@Address", mailMethod.Address);
            command.Parameters.AddWithValue("@EmpId", employee.Id);

            return command;
        }

        private static string ScheduleCode(PaymentSchedule schedule)
        {
            if (schedule is MonthlySchedule)
                return "monthly";
            if (schedule is WeeklySchedule)
                return "weekly";
            if (schedule is BiWeeklySchedule)
                return "biweekly";
            else
                return "unknown";
        }

        private void PrepareToSavePaymentClassification()
        {
            PaymentClassification classification = employee.Classification;
            if (classification is SalariedClassification)
            {
                classificationCode = "salaried";
                SalariedClassification salariedClassification =
                    classification as SalariedClassification;
                insertClassificationCommand =
                    CreateInsertSalariedClassificationCommand(
                    salariedClassification);
            }
            else if (classification is HourlyClassification)
                classificationCode = "hourly";
            else if (classification is CommissionedClassification)
                classificationCode = "commissioned";
            else
                classificationCode = "unknown";
        }

        private SqlCommand CreateInsertSalariedClassificationCommand(
            SalariedClassification salariedClassification)
        {
            string SQL = "INSERT INTO SalariedClassification " +
                "VALUES (@Salary, @EmpId)";
            SqlCommand command = new SqlCommand(SQL);
            command.Parameters.AddWithValue("@Salary", salariedClassification.Salary);
            command.Parameters.AddWithValue("@EmpId", employee.Id);

            return command;
        }
    }
}
