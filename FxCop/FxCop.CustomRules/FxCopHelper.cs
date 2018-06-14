using Microsoft.FxCop.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FxCop.CustomRules
{
    static class FxCopHelper
    {
        public static IEnumerable<TypeNode> GetTypeNodes(Identifier @namespace, Identifier name)
        {
            return RuleUtilities.AnalysisAssemblies.Select(a => a.GetType(@namespace, name, true));
        }

        public static IEnumerable<AttributeNode> GetAttributes(TypeNode attributeName)
        {
            return RuleUtilities.AnalysisAssemblies.Select(a => a.GetAttribute(attributeName));
        }

    }
}
