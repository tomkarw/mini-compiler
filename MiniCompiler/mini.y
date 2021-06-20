%using MiniCompiler;
%namespace GardensPoint

%YYSTYPE SyntaxInfo


%token PROGRAM IF ELSE WHILE READ WRITE RETURN 
%token INT DOUBLE BOOL HEX
%token ASSIGNMENT OR AND BITOR BITAND EQ NOTEQ GT GTE LT LTE
%token PLUS MINUS MUL DIV NOT BITNOT SROUND EROUND SCURLY ECURLY COMMA SEMICOLON
%token STRING INTVAL BOOLVAL DOUBLEVAL HEXVAL ID
%token BREAK CONTINUE SSQUARE ESQUARE


%%
	
start				: PROGRAM block_instruction EOF
				{
					program = new ProgramNode(
						$2 as SyntaxNode
					);
				}  
				;
				
block_instruction		: SCURLY declarations instructions ECURLY
				{
					$$ = new BlockInstruction(
						$2 as SyntaxNode,
						$3 as SyntaxNode
					);
				}
				;
				    
declarations			: /* empty */
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
				    
declaration			: type variable_names variable_name SEMICOLON
				{
					$$ = new DeclarationNode(
						$1 as SyntaxNode,
						$2 as SyntaxNode,
						$3 as SyntaxNode
					);
				}
				| type variable_names variable_name error
				{
					Context.AddError($1.Line, "Missing semicolon");
					$$ = new DeclarationNode(
						$1 as SyntaxNode,
						$2 as SyntaxNode,
						$3 as SyntaxNode
					);
				}
				;
				    
type				: INT
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
				    
variable_names			: /* empty */
				{
					$$ = new EmptyNode(-1, -1, null);
				}
				| variable_names variable_name COMMA
				{
					$$ = new VariablesNode(
						$1 as SyntaxNode,
						$2 as SyntaxNode
					);
				}
				;
		
variable_name			: ID
				{
					$$ = new VariableNode($1);
				}
				| ID SSQUARE constant_tab_dimensions INTVAL ESQUARE
				{
					$$ = new TabVariableNode(
						$1,
						$3 as TabDimensionsNode,
						$4
					);
				}
				;
				
constant_tab_dimensions		: /* empty */
				| constant_tab_dimensions INTVAL COMMA
				{
					$$ = new TabDimensionsNode(
						$1 as TabDimensionsNode,
						$2
					);
				}
				;
		
instructions			: /* empty */
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
				    
instruction			: block_instruction
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
				| if_instruction
				| read_instruction
				| write_instruction
				| BREAK SEMICOLON
				{
					$$ = new BreakInstruction($1, "1");
				}
				| BREAK INTVAL SEMICOLON
				{
					$$ = new BreakInstruction($1, $2.Text);
				}
				| CONTINUE SEMICOLON
				{
					$$ = new ContinueInstruction($1);
				}
				;
				
if_instruction			: IF SROUND expression EROUND instruction ELSE instruction
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
	
				
read_instruction		: READ ID SEMICOLON
				{
					$$ = new ReadNode($2);
				}
				| READ ID COMMA HEX SEMICOLON
				{
					$$ = new ReadHexNode($2);
				}
				;
	
write_instruction		: WRITE expression SEMICOLON
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
			
expression			: logical_expression
				| variable_name ASSIGNMENT expression
				{
					$$ = new AssignmentExpressionNode(
						$1 as SyntaxNode,
						$3 as SyntaxNode
					);
				}
				;
				
				
logical_expression		: relation_expression
				| logical_expression logical_operator relation_expression
				{
					$$ = new LogicalExpressionNode(
						$1 as SyntaxNode,
						$2,
						$3 as SyntaxNode
					);
				}
				;
				
logical_operator		: OR
				| AND
				;
				
relation_expression		: additive_expression
				| relation_expression relation_operation additive_expression
				{
					$$ = new RelationExpressionNode(
						$1 as SyntaxNode,
						$2,
						$3 as SyntaxNode
					);
				}
				;
				
relation_operation		: EQ
				| NOTEQ
				| GT
				| GTE
				| LT
				| LTE
				;
				
additive_expression		: multiplicative_expression
				| additive_expression additive_operation multiplicative_expression
				{
					$$ = new AdditiveExpressionNode(
						$1 as SyntaxNode,
						$2,
						$3 as SyntaxNode
					);
				}
				;
				
additive_operation		: PLUS
				| MINUS
				;
				
multiplicative_expression	: bitwise_expression
				| multiplicative_expression multiplicative_operation bitwise_expression
				{
					$$ = new MultiplicativeExpressionNode(
						$1 as SyntaxNode,
						$2,
						$3 as SyntaxNode
					);
				}
				;
				
multiplicative_operation	: MUL
				| DIV
				;
				
bitwise_expression		: unary_expression
				| bitwise_expression bitwise_operation unary_expression
				{
					$$ = new BitwiseExpressionNode(
						$1 as SyntaxNode,
						$2,
						$3 as SyntaxNode
					);
				}
				;
				
bitwise_operation		: BITOR
				| BITAND
				;
				
unary_expression		: basic_expression
				| unary_operation unary_expression
				{
					$$ = new UnaryExpressionNode(
						$1,
						$2 as SyntaxNode
					);
				}
				;
				
unary_operation			: MINUS
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
	
basic_expression		: value
				| ID
				{
					$$ = new IdExpressionNode($1);
				}
				| SROUND expression EROUND
				{
					$$ = $2;
				}
				;
			
value				: INTVAL
				{
					$$ = new IntValueNode($1);
				}
				| DOUBLEVAL
				{
					$$ = new DoubleValueNode($1);
				}
				| BOOLVAL
				{
					$$ = new BoolValueNode($1);
				}
				| HEXVAL
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
