using System;
using System.Collections.Generic;
using System.Text;

namespace CommonDatabaseActionReusables.AccountManager.Hasher
{
    abstract class AbstractHasher
    {
        abstract public String HashPlainTextWithSalt(String argText);

        abstract public bool VerifyPlainTextToHashedPassword(String plainText, String hashedPassword);



    }
}
