%USERPROFILE%\.nuget\packages\OpenCover\4.6.519\tools\OpenCover.Console.exe -register:user -target:"%PROGRAMFILES%\dotnet\dotnet.exe" -targetargs:"test test\FirearmsApi.Tests" -filter:"+[*]* -[xunit*]*" -output:%USERPROFILE%\coverage\coverage.xml -oldStyle
%USERPROFILE%\.nuget\packages\ReportGenerator\2.4.6-beta2\tools\ReportGenerator.exe "-reports:%USERPROFILE%\coverage\coverage.xml" "-targetdir:%USERPROFILE%\coverage\report" -reporttypes:Badges
xcopy %USERPROFILE%\coverage\report\badge_combined.svg %USERPROFILE%\Source\fadb\ /Y