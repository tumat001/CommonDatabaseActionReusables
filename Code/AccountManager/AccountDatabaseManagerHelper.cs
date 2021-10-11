using System;
using System.Collections.Generic;
using CommonDatabaseActionReusables.AccountManager.Actions.Configs;
using CommonDatabaseActionReusables.AccountManager.Actions;
using CommonDatabaseActionReusables.GeneralUtilities.PathConfig;
using CommonDatabaseActionReusables.GeneralUtilities.DatabaseActions;

namespace CommonDatabaseActionReusables.AccountManager
{
    public class AccountDatabaseManagerHelper
    {

        public AccountRelatedDatabasePathConfig PathConfig { set; get; }


        public AccountDatabaseManagerHelper(AccountRelatedDatabasePathConfig argPathConfig)
        {
            PathConfig = argPathConfig;
        }


        #region IfAccountExists

        /// <summary>
        /// 
        /// </summary>
        /// <param name="argUsername"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InputStringConstraintsViolatedException"></exception>
        /// <returns>True if the username exists in the given parameters of <see cref="DatabasePathConfig"/></returns>
        public bool IfAccountUsernameExists(string username)
        {
            var action = new AccountExistsAction(PathConfig);
            return action.IfAccountUsernameExsists(username);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="argUsername"></param>
        /// <returns>True if the username exists in the given parameters of <see cref="DatabasePathConfig"/>. False otherwise, or if an exception was thrown.</returns>
        public bool TryIfAccountUsernameExists(string username)
        {
            return new AccountExistsAction(PathConfig).TryIfAccountUsernameExsists(username);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <returns>True if the account id exists in the given parameters of <see cref="DatabasePathConfig"/></returns>
        public bool IfAccountIdExists(int accountId)
        {
            return new AccountExistsAction(PathConfig).IfAccountIdExsists(accountId);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns>True if the account id exists in the given parameters of <see cref="DatabasePathConfig"/>. False otherwise</returns>
        public bool TryIfAccountIdExists(int accountId)
        {
            return new AccountExistsAction(PathConfig).TryIfAccountIdExists(accountId);
        }


        #endregion

        #region CreateAccount

        /// <summary>
        /// Creates an account with the given parameters in the given <paramref name="builder"/> and the password.<br></br>
        /// The account is created in the database and table specified in this object's <see cref="DatabasePathConfig"/>
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="password"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InputStringConstraintsViolatedException"></exception>
        /// <exception cref="AccountAlreadyExistsException"></exception>
        /// <returns>The Id corresponding to the created account.</returns>
        public int CreateAccount(Account.Builder builder, string password)
        {
            return new CreateAccountAction(PathConfig).CreateAccount(builder, password);
        }

        /// <summary>
        /// Creates an account with the given parameters in the given <paramref name="builder"/> and the password.<br></br>
        /// The account is created in the database and table specified in this object's <see cref="DatabasePathConfig"/>
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="password"></param>
        /// <returns>The Id corresponding to the created account, or -1 if the creation has failed.</returns>
        public int TryCreateAccount(Account.Builder builder, string password)
        {
            return new CreateAccountAction(PathConfig).TryCreateAccount(builder, password);
        }

        #endregion

        #region DeleteSingleAccount


        /// <summary>
        /// Deletes the account with the given <paramref name="username"/>.<br/>
        /// This object's <see cref="DatabasePathConfig"/> determines which database and table is affected.
        /// </summary>
        /// <param name="username"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InputStringConstraintsViolatedException"></exception>
        /// <exception cref="AccountDoesNotExistException"></exception>
        /// <returns>True if the delete operation was successful. False otherwise.</returns>
        public bool DeleteAccountWithUsername(string username)
        {
            return new DeleteAccountAction(PathConfig).DeleteAccountWithUsername(username);
        }


        /// <summary>
        /// Deletes the account with the given <paramref name="username"/>.<br/>
        /// This object's <see cref="DatabasePathConfig"/> determines which database and table is affected.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>True if the delete operation was successful. False otherwise.</returns>
        public bool TryDeleteAccountWithUsername(string username)
        {
            return new DeleteAccountAction(PathConfig).TryDeleteAccountWithUsername(username);
        }


        /// <summary>
        /// Deletes the account with the given <paramref name="accountId"/>.<br/>
        /// This object's <see cref="DatabasePathConfig"/> determines which database and table is affected.
        /// </summary>
        /// <param name="username"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="AccountDoesNotExistException"></exception>
        /// <returns>True if the delete operation was successful. False otherwise.</returns>
        public bool DeleteAccountWithId(int accountId)
        {
            return new DeleteAccountAction(PathConfig).DeleteAccountWithId(accountId);
        }


        /// <summary>
        /// Deletes the account with the given <paramref name="accountId"/>.<br/>
        /// This object's <see cref="DatabasePathConfig"/> determines which database and table is affected.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>True if the delete operation was successful. False otherwise.</returns>
        public bool TryDeleteAccountWithId(int accountId)
        {
            return new DeleteAccountAction(PathConfig).TryDeleteAccountWithId(accountId);
        }


        #endregion

        #region DeleteAllAccount

        /// <summary>
        /// Deletes all accounts.<br/>
        /// This object's <see cref="DatabasePathConfig"/> determines which database and table is affected.
        /// </summary>
        /// <param name="username"></param>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <returns>True if the delete operation was successful, even if no account was deleted.</returns>
        public bool DeleteAllAccounts()
        {
            return new DeleteAllAccountAction(PathConfig).DeleteAllAccounts();
        }

        #endregion

        #region GetAccount

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InputStringConstraintsViolatedException"></exception>
        /// <exception cref="AccountDoesNotExistException"></exception>
        /// <returns>An <see cref="Account"/> object containing information about the account with the provided <paramref name="username"/>.</returns>
        public Account GetAccountInfoFromUsername(string username)
        {
            return new GetAccountAction(PathConfig).GetAccountInfoFromUsername(username);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns>An <see cref="Account"/> object containing information about the account with the provided <paramref name="username"/>, or null if not found..</returns>
        public Account TryGetAccountInfoFromUsername(string username)
        {
            return new GetAccountAction(PathConfig).TryGetAccountInfoFromUsername(username);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="AccountDoesNotExistException"></exception>
        /// <returns>An <see cref="Account"/> object containing information about the account with the provided <paramref name="id"/>.</returns>
        public Account GetAccountInfoFromId(int id)
        {
            return new GetAccountAction(PathConfig).GetAccountInfoFromId(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An <see cref="Account"/> object containing information about the account with the provided <paramref name="id"/>, or null if not found.</returns>
        public Account TryGetAccountInfoFromId(int id)
        {
            return new GetAccountAction(PathConfig).TryGetAccountInfoFromId(id);
        }


        #endregion

        #region "MatchAccountWithPassword"

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accUsername"></param>
        /// <param name="password"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InputStringConstraintsViolatedException"></exception>
        /// <exception cref="AccountDoesNotExistException"></exception>
        /// <returns>True if the given <paramref name="password"/> matches with the password of the account with the provided <paramref name="accUsername"/>.</returns>
        public bool AccountPasswordMatchesWithGiven(string accUsername, string password)
        {
            return new MatchAccountWithPasswordAction(PathConfig).AccountPasswordMatchesWithGiven(accUsername, password);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accUsername"></param>
        /// <param name="password"></param>
        /// <returns>True if the given <paramref name="password"/> matches with the password of the account with the provided <paramref name="accUsername"/>.</returns>
        public bool TryAccountPasswordMatchesWithGiven(string accUsername, string password)
        {
            return new MatchAccountWithPasswordAction(PathConfig).TryAccountPasswordMatchesWithGiven(accUsername, password);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="AccountDoesNotExistException"></exception>
        /// <returns>True if the given <paramref name="password"/> matches with the password of the account with the provided <paramref name="id"/>.</returns>
        public bool AccountPasswordMatchesWithGiven(int id, string password)
        {
            return new MatchAccountWithPasswordAction(PathConfig).AccountPasswordMatchesWithGiven(id, password);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns>True if the given <paramref name="password"/> matches with the password of the account with the provided <paramref name="id"/>. False otherwise.< /returns>
        public bool TryAccountPasswordMatchesWithGiven(int id, string password)
        {
            return new MatchAccountWithPasswordAction(PathConfig).TryAccountPasswordMatchesWithGiven(id, password);
        }

        #endregion

        #region "EditAccount"

        /// <summary>
        /// Edits the account with the provided <paramref name="username"/> using the properties found in <paramref name="accBuilder"/> and using the provided <paramref name="password"/>.<br/>
        /// <br/>
        /// Set <paramref name="accBuilder"/> or <paramref name="password"/> to null to indicate that no change is desired. 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="accBuilder"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InputStringConstraintsViolatedException"></exception>
        /// <exception cref="AccountDoesNotExistException"></exception>
        /// <exception cref="AccountAlreadyExistsException"></exception>
        /// <returns>True if the account was edited successfully. Setting both <paramref name="accBuilder"/> and <paramref name="password"/> to null also results in true being returned.</returns>
        public bool EditAccountWithUsername(string username, Account.Builder accBuilder, string password)
        {
            return new EditAccountAction(PathConfig).EditAccountWithUsername(username, accBuilder, password);
        }


        /// <summary>
        /// Edits the account with the provided <paramref name="username"/> using the properties found in <paramref name="accBuilder"/> and using the provided <paramref name="password"/>.<br/>
        /// <br/>
        /// Set <paramref name="accBuilder"/> or <paramref name="password"/> to null to indicate that no change is desired. 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="accBuilder"></param>
        /// <returns>True if the account was edited successfully, false otherwise. Setting both <paramref name="accBuilder"/> and <paramref name="password"/> to null also results in true being returned.</returns>
        public bool TryEditAccountWithUsername(string username, Account.Builder accBuilder, string password)
        {
            return new EditAccountAction(PathConfig).TryEditAccountWithUsername(username, accBuilder, password);
        }


        /// <summary>
        /// Edits the account with the provided <paramref name="id"/> using the properties found in <paramref name="accBuilder"/> and using the provided <paramref name="password"/>.<br/>
        /// <br/>
        /// Set <paramref name="accBuilder"/> or <paramref name="password"/> to null to indicate that no change is desired. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accBuilder"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InputStringConstraintsViolatedException"></exception>
        /// <exception cref="AccountDoesNotExistException"></exception>
        /// <exception cref="AccountAlreadyExistsException"></exception>
        /// <returns>True if the account was edited successfully. Setting both <paramref name="accBuilder"/> and <paramref name="password"/> to null also results in true being returned.</returns>
        public bool EditAccountWithId(int id, Account.Builder accBuilder, string password)
        {
            return new EditAccountAction(PathConfig).EditAccountWithId(id, accBuilder, password);
        }


        /// <summary>
        /// Edits the account with the provided <paramref name="id"/> using the properties found in <paramref name="accBuilder"/> and using the provided <paramref name="password"/>.<br/>
        /// <br/>
        /// Set <paramref name="accBuilder"/> or <paramref name="password"/> to null to indicate that no change is desired. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accBuilder"></param>
        /// <returns>True if the account was edited successfully, false otherwise. Setting both <paramref name="accBuilder"/> and <paramref name="password"/> to null also results in true being returned.</returns>
        public bool TryEditAccountWithId(int id, Account.Builder accBuilder, string password)
        {
            return new EditAccountAction(PathConfig).TryEditAccountWithId(id, accBuilder, password);
        }


        #endregion

        #region "GetAllAccount"

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <returns>A list of accounts found in the database given in this object's <see cref="DatabasePathConfig"/></returns>
        public IReadOnlyList<Account> GetAllAccountsAsList()
        {
            return new GetAllAccountAction(PathConfig).GetAllAccountsAsList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>A list of accounts found in the database given in this object's <see cref="DatabasePathConfig"/>If an exception occurs, an empty list is returned.</returns>
        public IReadOnlyList<Account> TryGetAllAccountsAsList()
        {
            return new GetAllAccountAction(PathConfig).TryGetAllAccountsAsList();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <returns>A list of account ids found in the database given in this object's <see cref="DatabasePathConfig"/></returns>
        public IReadOnlyList<int> GetAllAccountIdsAsList()
        {
            return new GetAllAccountAction(PathConfig).GetAllAccountIdsAsList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>A list of account ids found in the database given in this object's <see cref="DatabasePathConfig"/>If an exception occurs, an empty list is returned.</returns>
        public IReadOnlyList<int> TryGetAllAccountIdsAsList()
        {
            return new GetAllAccountAction(PathConfig).TryGetAllAccountIdsAsList();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <returns>A dictionary of account ids to accounts found in the database given in this object's <see cref="DatabasePathConfig"/></returns>
        public IReadOnlyDictionary<int, Account> GetAllAccountsAsDictionary()
        {
            return new GetAllAccountAction(PathConfig).GetAllAccountsAsDictionary();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>A dictionary of account ids to accounts found in the database given in this object's <see cref="DatabasePathConfig"/>. If an exception occurs, an empty dictionary is returned.</returns>
        public IReadOnlyDictionary<int, Account> TryGetAllAccountsAsDictionary()
        {
            return new GetAllAccountAction(PathConfig).TryGetAllAccountsAsDictionary();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <returns>The number of accounts found in the database given in this object's <see cref="DatabasePathConfig"/> </returns>
        public int GetTotalNumberOfAccounts()
        {
            return new GetAllAccountAction(PathConfig).GetTotalNumberOfAccounts();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>The number of accounts found in the database given in this object's <see cref="DatabasePathConfig"/>. If an exception occurs, -1 is returned. </returns>
        public int TryGetTotalNumberOfAccounts()
        {
            return new GetAllAccountAction(PathConfig).TryGetTotalNumberOfAccounts();
        }


        #endregion

        #region "FilteredGetAccount"

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="AntiSQLInjectionInputConstraint"></exception>
        /// <returns>A list of accounts found in the database given in this object's <see cref="DatabasePathConfig"/> that contains the parameter <paramref name="text"/>.</returns>
        public IReadOnlyList<Account> GetFilteredAccountsWithUsernamesContainingText(string text)
        {
            return new FilteredGetAccountAction(PathConfig).GetFilteredAccountsWithUsernamesContainingText(text);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns>A list of accounts found in the database given in this object's <see cref="DatabasePathConfig"/> that contains the parameter <paramref name="text"/>. Returns an empty list when an exception occurs.</returns>
        public IReadOnlyList<Account> TryGetFilteredAccountsWithUsernamesContainingText(string text)
        {
            return new FilteredGetAccountAction(PathConfig).TryGetFilteredAccountsWithUsernamesContainingText(text);
        }


        #endregion

        #region "LogInAccount (PasswordMatching + DisabledFromLogIn Field Check)"

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accUsername"></param>
        /// <param name="password"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InputStringConstraintsViolatedException"></exception>
        /// <exception cref="AccountDoesNotExistException"></exception>
        /// <exception cref="AccountDisabledFromLoggingInException"></exception>
        /// <returns>True if the given <paramref name="password"/> matches with the password of the account with the provided <paramref name="accUsername"/>.<br /><br /> 
        /// <see cref="AccountDisabledFromLoggingInException"/> is thrown when the account is disabled from logging in. </returns>
        public bool IfAccountCanLogInWithGivenPassword(string accUsername, string password)
        {
            return new LogInAccountAction(PathConfig).IfAccountCanLogInWithGivenPassword(accUsername, password);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accUsername"></param>
        /// <param name="password"></param>
        /// <returns>True if the given <paramref name="password"/> matches with the password of the account with the provided <paramref name="accUsername"/>, false otherwise.
        /// </returns>
        public bool TryIfAccountCanLogInWithGivenPassword(string accUsername, string password)
        {
            return new LogInAccountAction(PathConfig).TryIfAccountCanLogInWithGivenPassword(accUsername, password);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="AccountDoesNotExistException"></exception>
        /// <exception cref="AccountDisabledFromLoggingInException"></exception>
        /// <returns>True if the given <paramref name="password"/> matches with the password of the account with the provided <paramref name="id"/>.<br/><br/>
        /// <see cref="AccountDisabledFromLoggingInException"/> is thrown when the account is disabled from logging in. </returns>
        public bool IfAccountCanLogInWithGivenPassword(int id, string password)
        {
            return new LogInAccountAction(PathConfig).IfAccountCanLogInWithGivenPassword(id, password);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns>True if the given <paramref name="password"/> matches with the password of the account with the provided <paramref name="id"/>, false otherwise.<br/><br/>
        /// </returns>
        public bool TryIfAccountCanLogInWithGivenPassword(int id, string password)
        {
            return new LogInAccountAction(PathConfig).TryIfAccountCanLogInWithGivenPassword(id, password);
        }


        #endregion

        #region "AdvancedGet"

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adGetParameter"></param>
        /// <param name="accountTypeFilter"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <returns>A list of accounts found in the database given in this object's <see cref="DatabasePathConfig"/>, taking into
        /// account the given <paramref name="adGetParameter"/> and <paramref name="accountTypeFilter"/>.</returns>
        public IReadOnlyList<Account> AdvancedGetAccountsAsList(AdvancedGetParameters adGetParameter, string accTypeFilter = null)
        {
            return new AdvancedGetAccountsAction(PathConfig).AdvancedGetAccountsAsList(adGetParameter, accTypeFilter);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="adGetParameter"></param>
        /// <param name="accountTypeFilter"></param>
        /// <returns>A list of accounts found in the database given in this object's <see cref="DatabasePathConfig"/>, taking into
        /// account the given <paramref name="adGetParameter"/> and <paramref name="accountTypeFilter"/>.</returns>
        public IReadOnlyList<Account> TryAdvancedGetAccountsAsList(AdvancedGetParameters adGetParameter, string accTypeFilter = null)
        {
            return new AdvancedGetAccountsAction(PathConfig).TryAdvancedGetAccountsAsList(adGetParameter, accTypeFilter);
        }

        #endregion

    }
}
