using System.Linq.Expressions;

namespace CleanArchitecture.Advanced.Common.Extensions
{
    public static class ExpressionExtensions
    {
        public static string ToCachableString(this Expression expression) => expression.Simplify().ToString();

        public static Expression Simplify(this Expression expression)
        {
            var searcher = new ParameterlessExpressionSearcher();
            searcher.Visit(expression);
            return new ParameterlessExpressionEvaluator(searcher.ParameterlessExpressions).Visit(expression);
        }

        public static Expression<T> Simplify<T>(this Expression<T> expression) => (Expression<T>)((Expression)expression).Simplify();

        private sealed class ParameterlessExpressionSearcher : ExpressionVisitor
        {
            public HashSet<Expression> ParameterlessExpressions { get; } = new HashSet<Expression>();

            private bool containsParameter;

            public override Expression Visit(Expression node)
            {
                bool originalContainsParameter = containsParameter;
                containsParameter = false;
                base.Visit(node);

                if (!containsParameter)
                {
                    if (node?.NodeType == ExpressionType.Parameter)
                    {
                        containsParameter = true;
                    }
                    else
                    {
                        ParameterlessExpressions.Add(node);
                    }
                }

                containsParameter |= originalContainsParameter;

                return node;
            }
        }

        private sealed class ParameterlessExpressionEvaluator : ExpressionVisitor
        {
            private readonly HashSet<Expression> _parameterlessExpressions;

            public ParameterlessExpressionEvaluator(HashSet<Expression> parameterlessExpressions)
            {
                _parameterlessExpressions = parameterlessExpressions;
            }

            public override Expression Visit(Expression node)
            {
                return
                    _parameterlessExpressions.Contains(node) ?
                    Evaluate(node) :
                    base.Visit(node);
            }

            private static Expression Evaluate(Expression node)
            {
                if (node.NodeType == ExpressionType.Constant)
                {
                    return node;
                }

                object value = Expression.Lambda(node).Compile().DynamicInvoke();
                return Expression.Constant(value, node.Type);
            }
        }
    }
}
