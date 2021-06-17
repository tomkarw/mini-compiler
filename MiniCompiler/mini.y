%using MiniCompiler;
%namespace GardensPoint

%YYSTYPE SyntaxInfo


%token PROGRAM IF ELSE WHILE READ WRITE RETURN INT DOUBLE BOOL HEX
%token ASSIGNMENT OR AND BITOR BITAND EQ NOTEQ GT GTE LT LTE
%token PLUS MINUS MUL DIV NOT BITNOT BOPEN BCLOSE POPEN PCLOSE COMMA SEMICOLON
%token STRING VALINT VALBOOL VALDOUBLE VALHEX ID
%token EOF

%%

start				: EOF 
                    ;

%%

public Parser(Scanner scanner) : base(scanner) { }
