From this directory:

--- Build (Debug) ---
dotnet build

--- Build, if needed, and run (Debug) ---
dotnet run

--- Build, if needed, and run (Release) ---
dotnet run -c Release

-- The output (IL) is here ---
explorer .\bin\Release\net8.0\


-------------------
---- Native AOT ---
dotnet publish -c Release

-- The output is here ---
explorer .\bin\Release\net8.0\win-x64\publish\

--- Run the AOT binary ---
.\bin\Release\net8.0\win-x64\publish\BigIntegerFib.exe

--- Run the AOT binary ---
.\bin\Release\net8.0\win-x64\publish\BigIntegerFib.exe -wait
