using Microsoft.FxCop.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FxCop.CustomRules
{
	class ControllerSchouldInheritFromBaseController : BaseIntrospectionRule
	{
		TypeNode mvcController = null;
		TypeNode baseController = null;

		public ControllerSchouldInheritFromBaseController()
			: base("ControllerSchouldInheritFromBaseController", "FxCop.CustomRules.RuleMetadata", 
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
			baseController = FxCopHelper.GetTypeNodes(Identifier.For("WebApplication.Controllers"), Identifier.For("BaseController")).FirstOrDefault();
		}


		public override ProblemCollection Check(TypeNode type)
		{
			if (mvcController != null && baseController != null)
			{
				if (type.IsAssignableTo(mvcController) && !type.IsAssignableTo(baseController))
					Problems.Add(new Problem(this.GetResolution(type.FullName)));
			}

			return this.Problems;
		}
	}
}
