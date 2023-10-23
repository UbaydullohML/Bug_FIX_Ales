System.Drawing.Common is not available in the Telerik NuGet repository. This is because the package is only available on the official NuGet.org repository.

    dotnet add package System.Drawing.Common --source https://api.nuget.org/v3/index.json

This will install the package from the official NuGet.org repository.

Once the package is installed, you will be able to use the System.Drawing.Common namespace in your code.

Note: System.Drawing.Common is only supported on Windows. If you are targeting a non-Windows platform, you will need to use a different library for graphics functionality.



Use the Official NuGet Package Source:

    dotnet add package CsvHelper -s https://api.nuget.org/v3/index.json
The official NuGet package source is the most reliable source for packages. You can specify it explicitly when adding the package. Use the -s option to specify the NuGet.org source:
