namespace Synthesis
{
    using System;
    using System.Linq;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal sealed class Rewriter : CSharpSyntaxRewriter
    {
        public override SyntaxNode VisitExpressionStatement(ExpressionStatementSyntax node)
        {
            
            var leadingTrivia = node.GetLeadingTrivia();
            var space = new SyntaxTriviaList().Add(SyntaxFactory.Whitespace(" "));
            var leadingWithNewLine = new SyntaxTriviaList().Add(SyntaxFactory.EndOfLine(Environment.NewLine)).Add(leadingTrivia.Last());

            var expression = node.ChildNodes().OfType<InvocationExpressionSyntax>().FirstOrDefault()?.ChildNodes().FirstOrDefault();
            if (expression != null)
            {
                if (expression.IsKind(SyntaxKind.IdentifierName))
                {
                    var expressionName = (IdentifierNameSyntax)expression;

                    if (@"YIELD".Equals(expressionName.Identifier.ValueText, StringComparison.Ordinal))
                    {
                        var argumentName = (ArgumentSyntax)expression.Parent.ChildNodes().OfType<ArgumentListSyntax>().FirstOrDefault()?.ChildNodes().FirstOrDefault();

                        var myExpression = SyntaxFactory.ForEachStatement(
                            SyntaxFactory.Token(SyntaxKind.ForEachKeyword).WithLeadingTrivia(leadingTrivia).WithTrailingTrivia(space),
                            SyntaxFactory.Token(SyntaxKind.OpenParenToken),
                            SyntaxFactory.IdentifierName("var").WithTrailingTrivia(space),
                            SyntaxFactory.Identifier(new SyntaxTriviaList().Add(SyntaxFactory.Whitespace("")), SyntaxKind.IdentifierToken, "item", "item", space),
                            SyntaxFactory.Token(SyntaxKind.InKeyword).WithTrailingTrivia(space),
                            argumentName.Expression,
                            SyntaxFactory.Token(SyntaxKind.CloseParenToken),
                            SyntaxFactory.Block(
                                    SyntaxFactory.YieldStatement(
                                        SyntaxKind.YieldReturnStatement,
                                        SyntaxFactory.IdentifierName("item")
                                        ).NormalizeWhitespace()
                                        .WithLeadingTrivia(leadingWithNewLine.Add(SyntaxFactory.Whitespace("    ")))
                                        .WithTrailingTrivia(SyntaxFactory.EndOfLine(Environment.NewLine))
                                    ).WithOpenBraceToken(SyntaxFactory.Token(SyntaxKind.OpenBraceToken).WithLeadingTrivia(leadingWithNewLine))
                                    .WithCloseBraceToken(SyntaxFactory.Token(SyntaxKind.CloseBraceToken).WithLeadingTrivia(leadingTrivia.Last()))
                            ).WithTrailingTrivia(SyntaxFactory.EndOfLine(Environment.NewLine));

                        return myExpression;
                    }
                }
                else if (expression.IsKind(SyntaxKind.GenericName))
                {
                    var espressionName = (GenericNameSyntax)expression;

                    if (@"YIELD".Equals(espressionName.Identifier.ValueText, StringComparison.Ordinal))
                    {
                        var argumentName = (ArgumentSyntax)expression.Parent.ChildNodes().OfType<ArgumentListSyntax>().FirstOrDefault()?.ChildNodes().FirstOrDefault();
                        var predefinedTypeName = (PredefinedTypeSyntax)expression.ChildNodes().OfType<TypeArgumentListSyntax>().FirstOrDefault()?.ChildNodes().FirstOrDefault(); ;

                        var myExpression = SyntaxFactory.ForEachStatement(
                            SyntaxFactory.Token(SyntaxKind.ForEachKeyword).WithLeadingTrivia(leadingTrivia).WithTrailingTrivia(space),
                            SyntaxFactory.Token(SyntaxKind.OpenParenToken),
                            predefinedTypeName.WithTrailingTrivia(space),
                            SyntaxFactory.Identifier(new SyntaxTriviaList().Add(SyntaxFactory.Whitespace("")), SyntaxKind.IdentifierToken, "item", "item", space),
                            SyntaxFactory.Token(SyntaxKind.InKeyword).WithTrailingTrivia(space),
                            argumentName.Expression,
                            SyntaxFactory.Token(SyntaxKind.CloseParenToken),
                            SyntaxFactory.Block(
                                    SyntaxFactory.YieldStatement(
                                        SyntaxKind.YieldReturnStatement,
                                        SyntaxFactory.IdentifierName("item")
                                        ).NormalizeWhitespace()
                                        .WithLeadingTrivia(leadingWithNewLine.Add(SyntaxFactory.Whitespace("    ")))
                                        .WithTrailingTrivia(SyntaxFactory.EndOfLine(Environment.NewLine))
                                    ).WithOpenBraceToken(SyntaxFactory.Token(SyntaxKind.OpenBraceToken).WithLeadingTrivia(leadingWithNewLine))
                                    .WithCloseBraceToken(SyntaxFactory.Token(SyntaxKind.CloseBraceToken).WithLeadingTrivia(leadingTrivia.Last()))
                            ).WithTrailingTrivia(SyntaxFactory.EndOfLine(Environment.NewLine));

                        return myExpression;
                    }
                }
            }
            
            return base.VisitExpressionStatement(node);
        }
        
    }
}
