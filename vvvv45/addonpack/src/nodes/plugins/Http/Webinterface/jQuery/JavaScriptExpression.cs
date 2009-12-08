using System;
using System.Collections.Generic;
using System.Text;

namespace VVVV.Webinterface.jQuery
{
	public class JavaScriptExpression : Expression
	{
		protected JavaScriptObject FJsObject;
		
		protected JavaScriptExpression()
		{
			FJsObject = null;
		}

		public JavaScriptExpression(JavaScriptObject jsObject)
		{
			FJsObject = jsObject;
		}

		public JavaScriptExpression ApplyMethodCall(string methodName, params object[] arguments)
		{
			JavaScriptExpression expression = WrapObjectAsExpression();
			expression.AddChainedMethodCall(methodName, arguments);
			return expression;
		}

		public JavaScriptExpression Member(string memberName)
		{
			JavaScriptExpression expression = WrapObjectAsExpression();
			expression.FChainedOperations.Add(new DataMemberAccessor(memberName));
			return expression;
		}

		public JavaScriptExpression toString()
		{
			JavaScriptExpression expression = WrapObjectAsExpression();
			expression.FChainedOperations.Add(new MethodCall("toString"));
			return expression;
		}

		protected JavaScriptExpression WrapObjectAsExpression()
		{

			if (this is JavaScriptObject)
			{
				return new JavaScriptExpression((JavaScriptObject)this);
			}
			else
			{
				return this;
			}
		}

		protected override string GenerateObjectScript(int indentSteps, bool breakInternalLines, bool breakAfter)
		{
			return FJsObject.GenerateObjectScript(indentSteps, breakInternalLines, breakAfter);
		}
	}
}