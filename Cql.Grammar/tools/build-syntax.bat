@echo off
setlocal enabledelayedexpansion

color f9

echo Removing old files...
del *.class
del *.java
del *.tokens

echo Generating lexer plus parser...
java -cp .\antlr-4.7-complete.jar -jar .\antlr-4.7-complete.jar -o .\tools ..\Cql.g4

echo Compiling lexer plus parser...
javac -cp .\antlr-4.7-complete.jar Cql*.java

:test-syntax
echo Starting test-rig...
java -cp .\antlr-4.7-complete.jar;. org.antlr.v4.gui.TestRig Cql queries -gui -tree -tokens
set /p continue= Would you like to test another statement? [Y/N]:
if %continue% equ y (
	goto :test-syntax
)
