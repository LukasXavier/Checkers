# Checkers

This program was made to demonstrate a rudimentary knowledge of C# as a 4-week project to learn a new language.

### Disclaimer: C# code and it's complied binary will only run on Windows machines*


There is a release tab on the right of github where you can download an already compiled version with all the necessary
files to run Checkers, if you would like to run it in the IDE, we recommend using Microsoft's Visual Studio.
https://visualstudio.microsoft.com/thank-you-downloading-visual-studio/?sku=Community&rel=16

You might need to also download some frameworks, we used the .Net 5.0 framework for this project
https://dotnet.microsoft.com/download/dotnet

Once you have VS open on the right there is a button that says "Clone a repository" From there you can paste
https://github.com/LukasXavier/Checkers.git into the repo location and click clone. Once everything loads
find the 'Solution Explorer' double click on the Checkers.sln, now you're inside the project at the top
there is a green play button labeled 'Checkers' if you click it, the game will start. 

The game is very simple, click on the piece you would like to move, then click where you would like to move it,
we did not implement a turn system so turns are played on the honor system where you make your turn then let
the other player go. Otherwise its the same rules as checkers.

If you run the code yourself through VS you might get an error saying board.png is not found, simply move the board.png
file from the root of the repository to bin/Debug/net5.0-windows/ or bin/Release/net5.0-windows/ if the first location
did not solve your problem



*if you use some sort of emulation layer it might work on non-windows machines
