using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonDatabaseActionReusables.GeneralUtilities.InputConstraints
{
    public class UsernameInputConstraint : AbstractStringInputConstraint
    {

        public static IReadOnlyList<string> UnallowedStrings = new List<string> { ":", "/", ",", "|", "\\", ";", "=" };



        public override bool SatisfiesConstraint(string toCheck)
        {
            bool isSatisfied = true;

            foreach (string unallowed in UnallowedStrings)
            {
                if (toCheck.Contains(unallowed))
                {
                    throw new InputStringConstraintsViolatedException(this, unallowed);
                }
            }

            return isSatisfied;
        }

        public override bool TrySatisfiesContraint(string toCheck)
        {
            try
            {
                return SatisfiesConstraint(toCheck);
            }
            catch (InputStringConstraintsViolatedException)
            {
                return false;
            }
        }



        public override IReadOnlyList<string> GetConstraintList()
        {
            return UnallowedStrings;
        }

    }
}