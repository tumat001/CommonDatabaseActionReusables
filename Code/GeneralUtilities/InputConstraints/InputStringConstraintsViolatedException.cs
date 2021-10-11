using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonDatabaseActionReusables.GeneralUtilities.InputConstraints
{
    public class InputStringConstraintsViolatedException : ApplicationException
    {

        public AbstractStringInputConstraint ViolatedConstraint { get; }
        public string ViolatingString { get; }


        internal InputStringConstraintsViolatedException(AbstractStringInputConstraint violatedConstraint, string violatingString)
        {
            ViolatedConstraint = violatedConstraint;
            ViolatingString = violatingString;
        }

    }
}