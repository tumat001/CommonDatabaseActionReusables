using System;
using System.Collections.Generic;
using System.Text;
using BCrypt.Net;

namespace CommonDatabaseActionReusables.AccountManager.Hasher
{

    class BCryptHasher : AbstractHasher
    {
        const int WORKFACTOR = 7;

        public override String HashPlainTextWithSalt(String argText)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(argText, WORKFACTOR);
        }

        public override bool VerifyPlainTextToHashedPassword(String plainText, String hashedPassword)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(plainText, hashedPassword);
        }
    }
}
