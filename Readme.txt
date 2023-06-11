JerrysQuickMart
***************

This project contains the solution for:
1. Select the type of customer between Reward Member or Regular Customer
2. Add Items to cart
3. View cart (including totals)
4. Checkout and Print receipt
5. Cancel Transaction

Additional information:
1.  Inventory is passed into the application in a .txt file with item information on each line.
2.  The receipt is printed as .txt file with the transaction number and date included in the file name.
3.  Inventory is updated after checkout to avoid customers buying items that are out of stock.

Technical information:
1.  The project was implemented using Visual Studio Code and .Net Core.
2.  The solution consists on two projects: JerrysConsole.csproj and JerrysTests.csproj
The following lines where executed in order to create the structure:
# dotnet new sun
# dotnet new console -o JerrysConsole 
# dotnet sln add 
# dotnet sln add JerrysConsole/JerrysConsole.csproj
# dotnet new mstest -o JerrysTest
# dotnet sln add JerrysTest/JerrysTest.csproj
# dotnet add JerrysTest/JerrysTest.csproj reference JerrysConsole/JerrysConsole.csproj
3.  JerrysConsole project, has been divided into the following layers: 
3.1. domain:  it contains all the business logic for the project
3.2. entities:  it contains the plain objects
3.3  infraestructure:  it contains the data access logic
4.  The files:  inventory.txt and trasnactio_xx.txt will be under the root directory of the project
5.  The solution was build using TDD.

To execute the project, type the following commands on a terminal section:
JerryConsole# dotnet clean
JerryConsole# dotnet build
JerryConsole# dotnet run
You will see a menu in the terminal, displaying the options available for the user.

To run the tests, you must be in the path JerryQuickMart
JerryQuickMart# dotnet build
JerryQuickMart# dotnet test JerrysTest/JerrysTest.csproj
Or
Use the menú option "Test" of Visual Studio Code and press the button to Run All Test



