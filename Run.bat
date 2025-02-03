dotnet clean
dotnet build .\TrippleZero.sln
dotnet tool install --global SpecFlow.Plus.LivingDoc.CLI
cd .\TrippleZero.Online\
dotnet test --filter Category=Online --logger:trx
livingdoc feature-folder features --output TestResult/TestReport.html
echo "Test report generated at TestResult/TestReport.html"
start TestResult\TestReport.html