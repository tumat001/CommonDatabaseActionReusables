using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonDatabaseActionReusables.GeneralUtilities.InputConstraints
{
    abstract public class AbstractStringInputConstraint
    {

        abstract public bool SatisfiesConstraint(string toCheck);

        abstract public bool TrySatisfiesContraint(string toCheck);


        abstract public IReadOnlyList<string> GetConstraintList();

    }
}