using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Eshava.Storm.MetaData.Constants;

namespace Eshava.Storm.MetaData.Extensions
{
	internal static class ExpressionExtensions
	{
		public static IReadOnlyList<MemberInfo> GetMemberAccessList(this LambdaExpression memberAccessExpression)
		{
			var memberPaths = memberAccessExpression.MatchMemberAccessList((p, e) => e.MatchSimpleMemberAccess<MemberInfo>(p));

			if (memberPaths == null)
			{
				throw new ArgumentException(String.Format(MessageConstants.InvalidMembersExpression, nameof(memberAccessExpression)));
			}

			return memberPaths;
		}

		public static MemberInfo GetMemberAccess(this LambdaExpression memberAccessExpression)
		{
			return GetInternalMemberAccess<MemberInfo>(memberAccessExpression);
		}

		private static IReadOnlyList<TMemberInfo> MatchMemberAccessList<TMemberInfo>(this LambdaExpression lambdaExpression, Func<Expression, Expression, TMemberInfo> memberMatcher) where TMemberInfo : MemberInfo
		{
			var parameterExpression = lambdaExpression.Parameters[0];

			if (RemoveConvert(lambdaExpression.Body) is NewExpression newExpression)
			{
				var memberInfos = newExpression
						.Arguments
						.Select(a => memberMatcher(a, parameterExpression))
						.Where(p => p != null)
						.ToList();

				return memberInfos.Count != newExpression.Arguments.Count ? null : memberInfos;
			}

			var memberPath = memberMatcher(lambdaExpression.Body, parameterExpression);

			return memberPath != null ? new[] { memberPath } : null;
		}

		private static TMemberInfo GetInternalMemberAccess<TMemberInfo>(this LambdaExpression memberAccessExpression) where TMemberInfo : MemberInfo
		{
			var parameterExpression = memberAccessExpression.Parameters[0];
			var memberInfo = parameterExpression.MatchSimpleMemberAccess<TMemberInfo>(memberAccessExpression.Body);

			if (memberInfo == null)
			{
				throw new ArgumentException(String.Format(MessageConstants.InvalidMemberExpression, nameof(memberAccessExpression)));
			}

			var declaringType = memberInfo.DeclaringType;
			var parameterType = parameterExpression.Type;

			if (declaringType != null
				&& declaringType != parameterType
				&& declaringType.IsInterface
				&& declaringType.IsAssignableFrom(parameterType)
				&& memberInfo is PropertyInfo propertyInfo)
			{
				var propertyGetter = propertyInfo.GetMethod;
				var interfaceMapping = parameterType.GetTypeInfo().GetRuntimeInterfaceMap(declaringType);
				var index = Array.FindIndex(interfaceMapping.InterfaceMethods, p => propertyGetter.Equals(p));
				var targetMethod = interfaceMapping.TargetMethods[index];

				foreach (var runtimeProperty in parameterType.GetRuntimeProperties())
				{
					if (targetMethod.Equals(runtimeProperty.GetMethod))
					{
						return runtimeProperty as TMemberInfo;
					}
				}
			}

			return memberInfo;
		}

		private static TMemberInfo MatchSimpleMemberAccess<TMemberInfo>(this Expression parameterExpression, Expression memberAccessExpression) where TMemberInfo : MemberInfo
		{
			var memberInfos = MatchMemberAccess<TMemberInfo>(parameterExpression, memberAccessExpression);

			return memberInfos?.Count == 1 ? memberInfos[0] as TMemberInfo : null;
		}

		private static IReadOnlyList<TMemberInfo> MatchMemberAccess<TMemberInfo>(this Expression parameterExpression, Expression memberAccessExpression) where TMemberInfo : MemberInfo
		{
			var memberInfos = new List<TMemberInfo>();

			MemberExpression memberExpression;

			do
			{
				memberExpression = RemoveTypeAs(RemoveConvert(memberAccessExpression)) as MemberExpression;

				if (!(memberExpression?.Member is TMemberInfo memberInfo))
				{
					return null;
				}

				memberInfos.Insert(0, memberInfo);

				memberAccessExpression = memberExpression.Expression;
			}
			while (RemoveTypeAs(RemoveConvert(memberExpression.Expression)) != parameterExpression);

			return memberInfos;
		}

		private static Expression RemoveTypeAs(this Expression expression)
		{
			while (expression?.NodeType == ExpressionType.TypeAs)
			{
				expression = ((UnaryExpression)RemoveConvert(expression)).Operand;
			}

			return expression;
		}

		private static Expression RemoveConvert(Expression expression)
		{
			if (expression is UnaryExpression unaryExpression
				&& (expression.NodeType == ExpressionType.Convert
					|| expression.NodeType == ExpressionType.ConvertChecked))
			{
				return RemoveConvert(unaryExpression.Operand);
			}

			return expression;
		}
	}
}