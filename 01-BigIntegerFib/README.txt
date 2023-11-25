From this directory:

--- Build (Debug) ---
dotnet build

--- Build, if needed, and run (Debug) ---
dotnet run

--- Build, if needed, and run (Release) ---
dotnet run -c Release

-- The output (IL) is here ---
explorer ..\artifacts\bin\BigIntegerFib\release\


-------------------
---- Native AOT ---
dotnet publish -c Release

-- The output is here ---
explorer ..\artifacts\publish\BigIntegerFib\release\

--- Run the AOT binary ---
..\artifacts\publish\BigIntegerFib\release\BigIntegerFib.exe

--- Run the AOT binary ---
..\artifacts\publish\BigIntegerFib\release\BigIntegerFib.exe -wait
