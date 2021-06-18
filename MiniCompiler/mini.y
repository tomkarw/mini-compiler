%using MiniCompiler;
%namespace GardensPoint

%YYSTYPE SyntaxInfo


%token PROGRAM IF ELSE WHILE READ WRITE RETURN 
%token INT DOUBLE BOOL HEX
%token ASSIGNMENT OR AND BITOR BITAND EQ NOTEQ GT GTE LT LTE
%token PLUS MINUS MUL DIV NOT BITNOT SROUND EROUND SCURLY ECURLY COMMA SEMICOLON
%token STRING VALINT VALBOOL VALDOUBLE VALHEX ID


%%

start		: PROGRAM SCURLY declarations instructions ECURLY EOF
		{
			program = new ProgramNode(
				$3 as DeclarationsNode,
				$4 as InstructionsNode
			);
		}  
		;
                    
declarations	:
		{
			$$ = new EmptyDeclarationsNode(-1, -1, null);
		}
		| declarations declaration
		{
			$$ = new DeclarationsNode(
				$1 as DeclarationsOrEmptyNode,
				$2 as DeclarationNode
			);
		}
		;
                    
declaration	: type ids id SEMICOLON
		{
			$$ = new DeclarationNode(
				$1 as BaseTypeNode,
				$2 as IdsOrEmptyNode,
				$3 as IdNode
			);
		}
		;
                    
type		: INT
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
		| HEX
		{
			/* TODO: good choice? */
			$$ = new IntTypeNode($1);
		}
		;
                    
ids		: 		 
		{
			$$ = new EmptyIdsNode(-1, -1, null);
		}
                | ids id COMMA
		{
			$$ = new IdsNode(
				$1 as IdsOrEmptyNode,
				$2 as IdNode
		    	);
		}
		;

id		: ID
		{
			$$ = new IdNode($1);
		}
		;

instructions	:
		{
			$$ = new EmptyInstructionsNode(-1, -1, null);
		}
		| instructions instruction
		{
			$$ = new InstructionsNode(
				$1 as InstructionsOrEmptyNode,
				$2 as InstructionNode
			);
		}
		;
                    
instruction	: ASSIGNMENT
		{
			$$ = new InstructionNode(
			);
		}
		;

%%

public ProgramNode program {get;set;}

public Parser(Scanner scanner, ProgramNode program) : base(scanner)
{
	this.program = program; 
}
