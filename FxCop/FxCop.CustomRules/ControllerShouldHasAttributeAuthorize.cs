using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.FxCop.Sdk;

namespace FxCop.CustomRules
{
    class ControllerShouldHasAttributeAuthorize : BaseIntrospectionRule
    {
        TypeNode mvcController = null;
        TypeNode authorizeAttribute = null;

        public ControllerShouldHasAttributeAuthorize()
            : base("ControllerShouldHasAttributeAuthorize2", "FxCop.CustomRules.RuleMetadata",
                typeof(ControllerSchouldInheritFromBaseController).Assembly)
        {
        }

        public override TargetVisibilities TargetVisibility
        {
            get
            {
                return TargetVisibilities.All;
            }
        }

        public override void BeforeAnalysis()
        {
            mvcController = FxCopHelper.GetTypeNodes(Identifier.For("System.Web.Mvc"), Identifier.For("Controller")).FirstOrDefault();
            authorizeAttribute = FxCopHelper.GetTypeNodes(Identifier.For("System.Web.Mvc"), Identifier.For("AuthorizeAttribute"))
                .FirstOrDefault();
        }


        public override ProblemCollection Check(TypeNode type)
        {
            if (mvcController != null
                && type.IsAssignableTo(mvcController)
                && type.Attributes?.FirstOrDefault(a => a.Type == authorizeAttribute) == null)
            {
                Problems.Add(new Problem(this.GetResolution(type.FullName)));
            }

            return this.Problems;
        }
    }
}
