using System;
using System.Collections.Generic;
using System.Text;

namespace MiniCompiler
{
    public class UnaryExpressionNode : SyntaxNode
    {
        public SyntaxInfo Op;
        public SyntaxNode Expression;

        public UnaryExpressionNode(SyntaxInfo op,
            SyntaxNode expression)
            : base(expression)
        {
            Op = op;
            Expression = expression;
        }

        public override string GenCode(ref StringBuilder sb)
        {
            var expressionId = Expression.GenCode(ref sb);
            var id = Context.GetNewId();
            Type = Expression.Type;
            switch (Op.Text)
            {
                case "-":
                {
                    switch (Expression.Type)
                    {
                        case "i32":
                        {
                            sb.AppendLine($"%{id} = sub i32 0, %{expressionId}");
                            break;
                        }
                        case "double":
                        {
                            sb.AppendLine($"%{id} = fsub double -0.0, %{expressionId}");
                            break;
                        }
                        default:
                        {
                            Context.AddError(Op.Line, Op.Column,
                                $"Cannot use '{Op.Text}' with '{Expression.Type}' value");
                            break;
                        }
                    }

                    break;
                }
                case "~":
                {
                    switch (Expression.Type)
                    {
                        case "i32":
                        {
                            sb.AppendLine($"%{id} = xor i32 %{expressionId}, -1");
                            break;
                        }
                        default:
                        {
                            Context.AddError(Op.Line, Op.Column,
                                $"Cannot use '{Op.Text}' with '{Expression.Type}' value");
                            break;
                        }
                    }

                    break;
                }
                case "!":
                {
                    switch (Expression.Type)
                    {
                        case "i1":
                        {
                            sb.AppendLine($"%{id} = xor i1 %{expressionId}, 1");
                            break;
                        }
                        default:
                        {
                            Context.AddError(Op.Line, Op.Column,
                                $"Cannot use '{Op.Text}' with '{Expression.Type}' value");
                            break;
                        }
                    }

                    break;
                }
                case "(int)":
                {
                    switch (Expression.Type)
                    {
                        case "i32":
                        {
                            // simply ignore, just pass expressionId on
                            id = expressionId;
                            break;
                        }
                        case "double":
                        {
                            sb.AppendLine($"%{id} = fptosi double %{expressionId} to i32");
                            Type = "i32";
                            break;
                        }
                        case "i1":
                        {
                            sb.AppendLine($"%{id} = zext i1 %{expressionId} to i32");
                            Type = "i32";
                            break;
                        }
                        default:
                        {
                            Context.AddError(Op.Line, Op.Column,
                                $"Cannot use '{Op.Text}' with '{Expression.Type}' value");
                            break;
                        }
                    }

                    break;
                }
                case "(double)":
                {
                    switch (Expression.Type)
                    {
                        case "i32":
                        {
                            sb.AppendLine($"%{id} = sitofp i32 %{expressionId} to double");
                            Type = "double";
                            break;
                        }
                        case "double":
                        {
                            id = expressionId;
                            break;
                        }
                        default:
                        {
                            Context.AddError(Op.Line, Op.Column,
                                $"Cannot use '{Op.Text}' with '{Expression.Type}' value");
                            break;
                        }
                    }

                    break;
                }
                default:
                {
                    throw new ArgumentOutOfRangeException();
                }
            }

            return id;
        }
    }
}