%using MiniCompiler;
%namespace GardensPoint

%YYSTYPE SyntaxInfo


%token PROGRAM IF ELSE WHILE READ WRITE RETURN 
%token INT DOUBLE BOOL HEX
%token ASSIGNMENT OR AND BITOR BITAND EQ NOTEQ GT GTE LT LTE
%token PLUS MINUS MUL DIV NOT BITNOT SROUND EROUND SCURLY ECURLY COMMA SEMICOLON
%token STRING VALINT VALBOOL VALDOUBLE VALHEX ID
%token BREAK CONTINUE SSQUARE ESQUARE


%%

start			: PROGRAM blockInstruction EOF
			{
				program = new ProgramNode(
					$2 as SyntaxNode
				);
			}  
			;
			
blockInstruction	: SCURLY declarations instructions ECURLY
			{
				$$ = new BlockInstruction(
					$2 as SyntaxNode,
					$3 as SyntaxNode
				);
			}
			;
			    
declarations		: /* empty */
			{
				$$ = new EmptyNode(-1, -1, null);
			}
			| declarations declaration
			{
				$$ = new DeclarationsNode(
					$1 as SyntaxNode,
					$2 as SyntaxNode
				);
			}
			| EOF
			{
				Context.AddError($1.Line, "Unexpected end of file");
				Context.PrintErrors();
				YYABORT;
			}
			;
			    
declaration		: type variableNames variableName SEMICOLON
			{
				$$ = new DeclarationNode(
					$1 as SyntaxNode,
					$2 as SyntaxNode,
					$3 as SyntaxNode
				);
			}
			| type variableNames variableName error
			{
				Context.AddError($1.Line, "Missing semicolon");
				$$ = new DeclarationNode(
					$1 as SyntaxNode,
					$2 as SyntaxNode,
					$3 as SyntaxNode
				);
			}
			;
			    
type			: INT
			{
				$$ = new IntTypeNode($1);
			}
			| DOUBLE
			{
				$$ = new DoubleTypeNode($1);
			}
			| BOOL
			{
				$$ = new BoolTypeNode($1);
			}
			;
			    
variableNames		: /* empty */
			{
				$$ = new EmptyNode(-1, -1, null);
			}
			| variableNames variableName COMMA
			{
				$$ = new VariablesNode(
					$1 as SyntaxNode,
					$2 as SyntaxNode
				);
			}
			;
	
variableName		: ID
			{
				$$ = new VariableNode($1);
			}
			| ID SSQUARE constantTabDimensions VALINT ESQUARE
			{
				$$ = new TabVariableNode(
					$1,
					$3 as TabDimensionsNode,
					$4
				);
			}
			;
			
constantTabDimensions	: /* empty */
			| constantTabDimensions VALINT COMMA
			{
				$$ = new TabDimensionsNode(
					$1 as TabDimensionsNode,
					$2
				);
			}
			;
	
instructions		: /* empty */
			{
				$$ = new EmptyNode(-1, -1, null);
			}
			| instructions instruction
			{
				$$ = new InstructionsNode(
					$1 as SyntaxNode,
					$2 as SyntaxNode
				);
			}
			| EOF
			{
				Context.AddError($1.Line, "Unexpected end of file");
				Context.PrintErrors();
				YYABORT;
			}
			;
			    
instruction		: blockInstruction
			| expression SEMICOLON
			{
				$$ = $1;
			}
			| WHILE SROUND expression EROUND instruction
			{
				$$ = new WhileInstruction(
					$3 as SyntaxNode,
					$5 as SyntaxNode
				);
			}
			| RETURN SEMICOLON
			{
				$$ = new ReturnInstruction($1);
			}
			| ifInstruction
			| readInstruction
			| writeInstruction
			| BREAK SEMICOLON
			{
				$$ = new BreakInstruction($1, "1");
			}
			| BREAK VALINT SEMICOLON
			{
				$$ = new BreakInstruction($1, $2.Text);
			}
			| CONTINUE SEMICOLON
			{
				$$ = new ContinueInstruction($1);
			}
			;
			
ifInstruction		: IF SROUND expression EROUND instruction ELSE instruction
			{
				$$ = new IfInstruction(
					$3 as SyntaxNode,
					$5 as SyntaxNode,
					$7 as SyntaxNode
				);
			}
			| IF SROUND expression EROUND instruction
			{
				$$ = new IfInstruction(
					$3 as SyntaxNode,
					$5 as SyntaxNode
				);
			}
			;

			
readInstruction		: READ ID SEMICOLON
			{
				$$ = new ReadNode($2);
			}
			| READ ID COMMA HEX SEMICOLON
			{
				$$ = new ReadHexNode($2);
			}
			;

writeInstruction	: WRITE expression SEMICOLON
			{
				$$ = new WriteNode(
					$2 as SyntaxNode
				);
			}
			| WRITE expression COMMA HEX SEMICOLON
			{
				$$ = new WriteHexNode(
					$2 as SyntaxNode
				);
			}
			| WRITE STRING SEMICOLON
			{
				$$ = new WriteStringNode($2);
			}
			;
		
expression		: logicalExpression
			| variableName ASSIGNMENT expression
			{
				$$ = new AssignmentExpressionNode(
					$1 as SyntaxNode,
					$3 as SyntaxNode
				);
			}
			;
			
			
logicalExpression	: relationExpression
			| logicalExpression logicalOp relationExpression
			{
				$$ = new LogicalExpressionNode(
					$1 as SyntaxNode,
					$2,
					$3 as SyntaxNode
				);
			}
			;
			
logicalOp		: OR
			| AND
			;
			
relationExpression	: additiveExpression
			| relationExpression relationOperation additiveExpression
			{
				$$ = new RelationExpressionNode(
					$1 as SyntaxNode,
					$2,
					$3 as SyntaxNode
				);
			}
			;
			
relationOperation	: EQ
			| NOTEQ
			| GT
			| GTE
			| LT
			| LTE
			;
			
additiveExpression	: multiplicativeExp
			| additiveExpression additiveOp multiplicativeExp
			{
				$$ = new AdditiveExpressionNode(
					$1 as SyntaxNode,
					$2,
					$3 as SyntaxNode
				);
			}
			;
			
additiveOp		: PLUS
			| MINUS
			;
			
multiplicativeExp	: bitwiseExp
			| multiplicativeExp multiplicativeOp bitwiseExp
			{
				$$ = new MultiplicativeExpressionNode(
					$1 as SyntaxNode,
					$2,
					$3 as SyntaxNode
				);
			}
			;
			
multiplicativeOp	: MUL
			| DIV
			;
			
bitwiseExp		: unaryExp
			| bitwiseExp bitwiseOp unaryExp
			{
				$$ = new BitwiseExpressionNode(
					$1 as SyntaxNode,
					$2,
					$3 as SyntaxNode
				);
			}
			;
			
bitwiseOp		: BITOR
			| BITAND
			;
			
unaryExp		: basicExpression
			| unaryOp unaryExp
			{
				$$ = new UnaryExpressionNode(
					$1,
					$2 as SyntaxNode
				);
			}
			;
			
unaryOp			: MINUS
			| BITNOT
			| NOT
			| SROUND INT EROUND
			{
				$$ = new CastToIntNode($1);
			}
			| SROUND DOUBLE EROUND
			{
				$$ = new CastToDoubleNode($1);
			}
			;

basicExpression		: value
			| ID
			{
				$$ = new IdExpressionNode($1);
			}
			| SROUND expression EROUND
			{
				$$ = $2;
			}
			;
		
value			: VALINT
			{
				$$ = new IntValueNode($1);
			}
			| VALDOUBLE
			{
				$$ = new DoubleValueNode($1);
			}
			| VALBOOL
			{
				$$ = new BoolValueNode($1);
			}
			| VALHEX
			{
				$$ = new HexValueNode($1);
			}
			;
			
%%

public ProgramNode program {get;set;}

public Parser(Scanner scanner, ProgramNode program) : base(scanner)
{
	this.program = program; 
}
