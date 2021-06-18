%using MiniCompiler;
%namespace GardensPoint

%YYSTYPE SyntaxInfo


%token PROGRAM IF ELSE WHILE READ WRITE RETURN 
%token INT DOUBLE BOOL HEX
%token ASSIGNMENT OR AND BITOR BITAND EQ NOTEQ GT GTE LT LTE
%token PLUS MINUS MUL DIV NOT BITNOT SROUND EROUND SCURLY ECURLY COMMA SEMICOLON
%token STRING VALINT VALBOOL VALDOUBLE VALHEX ID


%%

start			: PROGRAM SCURLY declarations instructions ECURLY EOF
			{
				program = new ProgramNode(
					$3 as SyntaxNode,
					$4 as SyntaxNode
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
			;
			    
declaration		: type variableNames variableName SEMICOLON
			{
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
			;
			    
instruction		: SCURLY instructions ECURLY
			{
				$$ = $2;
			}
			| assignmentExpression SEMICOLON
			{
				
			}
			| writeInstruction
			;
		
assignmentExpression	: endExp /* TODO: change this to logicalExpression */
			{
			
			}
			| variableName ASSIGNMENT assignmentExpression
			{
				$$ = new AssignmentExpressionNode(
					$1 as SyntaxNode,
					$3 as SyntaxNode
				);
			}
			;
			
/* minimalExpression */
endExp			: value
			{
				$$ = $1;
			}
			| ID
			{
				$$ = new IdExpressionNode($1);
			}
			| SROUND assignmentExpression EROUND
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

writeInstruction	: WRITE assignmentExpression SEMICOLON
			{
				$$ = new WriteNode(
					$2 as SyntaxNode
				);
			}
			| WRITE assignmentExpression COMMA HEX SEMICOLON
			{
				$$ = new WriteHexNode(
					$2 as SyntaxNode
				);
			}
			;

%%

public ProgramNode program {get;set;}

public Parser(Scanner scanner, ProgramNode program) : base(scanner)
{
	this.program = program; 
}
