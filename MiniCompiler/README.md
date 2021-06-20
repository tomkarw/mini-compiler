Provided you added `gplex.exe` and `gppp.exe`
to project folder run these commands.
```
.\gplex.exe -out:Scanner.cs mini.lex
.\gppg.exe -gplex mini.y > Parser.cs
```
Then build the project normally.
