%using MiniCompiler;
%namespace GardensPoint

%YYSTYPE SyntaxInfo


%token PROGRAM IF ELSE WHILE READ WRITE RETURN 
%token INT DOUBLE BOOL HEX
%token ASSIGNMENT OR AND BITOR BITAND EQ NOTEQ GT GTE LT LTE
%token PLUS MINUS MUL DIV NOT BITNOT SROUND EROUND SCURLY ECURLY COMMA SEMICOLON
%token STRING VALINT VALBOOL VALDOUBLE VALHEX ID


%%

start		    : PROGRAM SCURLY declarations ECURLY EOF
		    {
			program = new ProgramNode(
			    $3 as DeclarationsNode
			);
		    }  
		    ;
                    
declarations        :
                    {
                        $$ = new EmptyDeclarationsNode(-1, -1, "");
                    }
                    | declarations declaration
                    {
                        $$ = new DeclarationsNode(
                            $1 as BaseDeclarationsNode,
                            $2 as DeclarationNode
                        );
                    }
                    ;
                    
declaration         : type ids id SEMICOLON
                    {
                        $$ = new DeclarationNode(
                            $1 as BaseTypeNode,
                            $2 as BaseIdsNode,
                            $3 as IdNode
                        );
                    }
                    ;
                    
type                : INT
                    {
                        $$ = new IntTypeNode($1);
                    }
                    ;
                    
ids		    :
		    {
			$$ = new EmptyIdsNode(-1, -1, "");
		    }
		    | ids id COMMA
		    {
		    	$$ = new IdsNode(
		    		$1 as BaseIdsNode,
		    		$2 as IdNode
		    	);
		    }
		    ;

id		    : ID
		    {
			$$ = new IdNode($1);
		    }
		    ;

%%

public ProgramNode program {get;set;}

public Parser(Scanner scanner, ProgramNode program) : base(scanner)
{
	this.program = program; 
}
