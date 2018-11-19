using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using refactor_this.Business.Dtos;
using refactor_this.Data.Service;

namespace refactor_this.Business.Service.Transactions
{
   public class TransactionBusiness
    {

        public List<Transaction> GetTransactions(Guid id)
        {
            var transactions = new List<Transaction>();

            using (var connection = ConnectionHelper.NewConnection())
            {
            SqlCommand command = new SqlCommand($"select Amount, Date from Transactions where AccountId = '{id}'", connection);
            connection.Open();
            var reader = command.ExecuteReader();
           

            while (reader.Read())
            {
            var amount = (float)reader.GetDouble(0);
            var date = reader.GetDateTime(1);
            transactions.Add(new Transaction(amount, date));
            }
                
            }

            return transactions;

        }


        public bool UpdateTransaction(Guid id, Transaction transaction)
        {

            using (var connection = ConnectionHelper.NewConnection())
            {

                SqlCommand command = new SqlCommand($"update Accounts set Amount = Amount + {transaction.Amount} where Id = '{id}'", connection);
                connection.Open();
                if (command.ExecuteNonQuery() != 1)
                    return true;
                else
                    return false;


            } 

        }

        public bool CreateTransaction(Guid id, Transaction transaction)
        { 

            using (var connection = ConnectionHelper.NewConnection())
            {

            SqlCommand command = new SqlCommand($"INSERT INTO Transactions (Id, Amount, Date, AccountId) VALUES ('{Guid.NewGuid()}', {transaction.Amount}, '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', '{transaction}')", connection);
                connection.Open();
            if (command.ExecuteNonQuery() != 1)
                        return true;
                    else
                        return false;
            }

        }

    }
}
