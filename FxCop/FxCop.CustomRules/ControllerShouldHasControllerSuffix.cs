using System.Linq;
using Microsoft.FxCop.Sdk;

namespace FxCop.CustomRules
{
    class ControllerShouldHasControllerSuffix : BaseIntrospectionRule
    {
        TypeNode mvcController = null;

        public ControllerShouldHasControllerSuffix()
            : base("ControllerShouldHasControllerSuffix", "FxCop.CustomRules.RuleMetadata",
                typeof(ControllerSchouldInheritFromBaseController).Assembly)
        {
        }

        public override TargetVisibilities TargetVisibility => TargetVisibilities.All;

        public override void BeforeAnalysis()
        {
            mvcController = FxCopHelper.GetTypeNodes(Identifier.For("System.Web.Mvc"), Identifier.For("Controller")).FirstOrDefault();
        }


        public override ProblemCollection Check(TypeNode type)
        {
            if (mvcController != null && (type.IsAssignableTo(mvcController) && !type.Name.Name.EndsWith("Controller")))
                Problems.Add(new Problem(this.GetResolution(type.FullName)));

            return this.Problems;
        }
    }
}
