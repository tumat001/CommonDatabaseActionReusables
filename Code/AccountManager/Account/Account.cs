using System;
using System.Collections.Generic;
using System.Text;
using CommonDatabaseActionReusables.AccountManager.Actions.Configs;

namespace CommonDatabaseActionReusables.AccountManager
{
    public class Account
    {

        private Account(int id, string username, bool disabledFromLogIn, string email, string accountType)
        {
            Id = id;
            Username = username;
            DisabledFromLogIn = disabledFromLogIn;
            Email = email;
            AccountType = accountType;
        }

        public int Id { get; }

        public string Username { get; }

        public bool DisabledFromLogIn { get; }

        public string Email { get; }

        public string AccountType { get; }

        public Builder ConstructBuilderUsingSelf()
        {
            var builder = new Builder();
            builder.Username = Username;
            builder.DisabledFromLogIn = DisabledFromLogIn;

            return builder;
        }


        public override bool Equals(object obj)
        {
            if (obj.GetType() == this.GetType())
            {
                return ((Account)(obj)).Id == Id;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }


        /// <summary>
        /// Used by Actions to build an account based on data found here. Does not contain the build method.
        /// </summary>
        public class Builder
        {
           
            public string Username { set;  get; }

            public bool DisabledFromLogIn { set; get; }

            public string Email { set; get; }

            public string AccountType { set; get; }

            internal Account Build(int id)
            {
                return new Account(id, Username, DisabledFromLogIn, Email, AccountType);
            }

        }

    }





}
