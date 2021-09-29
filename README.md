# Checkers

This program was made to demonstrate a rudimentary knowledge of C# as a 4-week project to learn a new language.

### Disclaimer: C# code and it's complied binary will only run on Windows machines*


There is a release tab on the right of github where you can download an already compiled version with all the necessary
files to run Checkers, simply run the Checkers.exe file, if you would like to run it in the IDE, we recommend using
Microsoft's Visual Studio.
https://visualstudio.microsoft.com/thank-you-downloading-visual-studio/?sku=Community&rel=16

You might need to also download some frameworks, we used the .Net 5.0 framework for this project
https://dotnet.microsoft.com/download/dotnet

Once you have VS open on the right there is a button that says "Clone a repository" From there you can paste
https://github.com/LukasXavier/Checkers.git into the repo location and click clone. Once everything loads
find the 'Solution Explorer' double click on the Checkers.sln, now you're inside the project at the top
there is a green play button labeled 'Checkers' if you click it, the game will start. 

The game is very simple, you play as the red pieces, you click on the piece you would like to move, then click where
you would like to move it, we implement an AI that can only be described as "aggressively stupid", in that it will
pick a random move to make unless there is a capture move, it will also pick the capture move that will result
in the most captures. There is a turn based check that does not allow you to make multiple moves in a row, unless it is
for a multi-capture. There is checks for draws as well as when either play has no further moves to make but still has pieces.

If you run the code yourself through VS you might get an error saying board.png is not found, simply move the board.png
file from the root of the repository to bin/Debug/net5.0-windows/ or bin/Release/net5.0-windows/ if the first location
did not solve your problem



*if you use some sort of emulation layer it might work on non-windows machines
