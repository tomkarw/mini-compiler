%using MiniCompiler;
%namespace GardensPoint

Id			    [a-zA-Z][a-zA-Z0-9]*

DoubleVal   	(0|(([1-9][0-9]*))+)\.([0-9])+
IntVal		    0|[1-9]([0-9])*
HexVal          0[xX][0-9a-fA-F]+
BoolVal		    true|false

Comment		    \/\/([^\n]|\\.)*
String		    \"([^\\\"\n]|\\.)*\"

%%
"program"		{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.PROGRAM;			}
"if"			{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.IF;				}
"else"			{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.ELSE;				}
"while"			{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.WHILE;		    	}
"read"			{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.READ;				}
"write"			{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.WRITE;		    	}
"return"		{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.RETURN;			}
"int"			{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.INT;				}
"double"		{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.DOUBLE;			}
"bool"			{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.BOOL;				}
"hex"			{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.HEX;				}
"break"         { yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.BREAK;				}
"continue"      { yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.CONTINUE;			}
"="				{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.ASSIGNMENT;		}
"||"			{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.OR;		    	}
"&&"			{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.AND;		    	}
"|"				{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.BITOR;		    	}
"&"				{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.BITAND;			}
"=="			{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.EQ;		   		}
"!="			{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.NOTEQ;		    	}
">"				{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.GT;				}
">="			{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.GTE;				}			
"<"				{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.LT;				}
"<="			{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.LTE;				}
"+"				{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.PLUS;				}
"-"				{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.MINUS;		    	}
"*"				{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.MUL;				}
"/"				{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.DIV;				}
"!"				{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.NOT;      	    	}
"~"				{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.BITNOT;			}
"("				{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.SROUND;	    	}
")"				{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.EROUND;			}
"{"				{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.SCURLY;	    	}
"}"				{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.ECURLY;			}
"["				{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.SSQUARE;	    	}
"]"				{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.ESQUARE;			}
","				{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.COMMA;    	    	}
";"				{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.SEMICOLON;	    	}
" "				{																						}
"\t"			{																						}
"\n"			{																						}
"\r"			{																						}
{Comment}		{																						}
{DoubleVal}		{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.DOUBLEVAL;		    }
{IntVal}		{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.INTVAL;			}
{HexVal}		{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.HEXVAL;			}
{BoolVal}		{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.BOOLVAL;			}
{String}		{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.STRING;			}
{Id}			{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.ID;				}
<<EOF>>			{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.EOF;				}
.				{ yylval = new SyntaxInfo(yyline, yycol, yytext); return (int)Tokens.error;		    	}
%%


public override void yyerror(string msg, params object[] args)
{
	Console.WriteLine("[" + yyline + "] ERROR: " + msg + ".");
}
