using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using refactor_this.Data.Service;
using refactor_this.Business.Dtos;

namespace refactor_this.Business.Service.Accounts
{
   public class AccountBusiness
    {

        public List<Account> GetAllAccounts()
        {
            var accounts = new List<Account>();

            using (var connection = ConnectionHelper.NewConnection())
            {
                SqlCommand command = new SqlCommand($"select Id from Accounts", connection);
                connection.Open();
                var reader = command.ExecuteReader();
               
                while (reader.Read())
                {
                    var id = Guid.Parse(reader["Id"].ToString());
                    var account = Get(id);
                    accounts.Add(account);
                }


            }


            return accounts;
        }


        public void Save(Account account)
        {
            using (var connection = ConnectionHelper.NewConnection())
            {
                SqlCommand command;
                if (account.isNew)
                    command = new SqlCommand($"insert into Accounts (Id, Name, Number, Amount) values ('{Guid.NewGuid()}', '{account.Name}', {account.Number}, 0)", connection);
                else
                    command = new SqlCommand($"update Accounts set Name = '{account.Name}' where Id = '{account.Id}'", connection);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(Account account)
        {
            using (var connection = ConnectionHelper.NewConnection())
            {
                SqlCommand command = new SqlCommand($"delete from Accounts where Id = '{account.Id}'", connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public Account Get(Guid id)
        {


            using (var connection = ConnectionHelper.NewConnection())
            {
                SqlCommand command = new SqlCommand($"select * from Accounts where Id = '{id}'", connection);
                connection.Open();
                var reader = command.ExecuteReader();
                if (!reader.Read())
                    throw new ArgumentException();

                var account = new Account(id);
                account.Name = reader["Name"].ToString();
                account.Number = reader["Number"].ToString();
                account.Amount = float.Parse(reader["Amount"].ToString());
                return account;
            }
        }

    }
}
