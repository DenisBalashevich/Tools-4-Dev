using Microsoft.FxCop.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FxCop.CustomRules
{
	class NoDirectCallLogManager : BaseIntrospectionRule
	{
		TypeNode logManager = null;
		Method currentMethod = null;

		public NoDirectCallLogManager()
			: base("NoDirectCallLogManager", "FxCop.CustomRules.RuleMetadata",	typeof(NoDirectCallLogManager).Assembly)
		{ }

		public override TargetVisibilities TargetVisibility
		{
			get
			{
				return TargetVisibilities.All;
			}
		}

		public override void BeforeAnalysis()
		{
			logManager = FxCopHelper.GetTypeNodes(Identifier.For("log4net"), Identifier.For("LogManager")).FirstOrDefault();
		}


		public override ProblemCollection Check(Member member)
		{
			if (member.NodeType == NodeType.Method && logManager != null)
			{
				currentMethod = member as Method;
				VisitMethod(currentMethod);
			}

			return this.Problems;
		}

		public override void VisitMethodCall(MethodCall call)
		{
			var type = (call.Callee as MemberBinding).BoundMember.DeclaringType;
			if (type == logManager)
				this.Problems.Add(new Problem(this.GetResolution(currentMethod), call));
		}
	}
}
