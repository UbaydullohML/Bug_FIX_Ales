System.Drawing.Common is not available in the Telerik NuGet repository. This is because the package is only available on the official NuGet.org repository.

    dotnet add package System.Drawing.Common --source https://api.nuget.org/v3/index.json

This will install the package from the official NuGet.org repository.

Once the package is installed, you will be able to use the System.Drawing.Common namespace in your code.

Note: System.Drawing.Common is only supported on Windows. If you are targeting a non-Windows platform, you will need to use a different library for graphics functionality.



Use the Official NuGet Package Source:

    dotnet add package CsvHelper -s https://api.nuget.org/v3/index.json
The official NuGet package source is the most reliable source for packages. You can specify it explicitly when adding the package. Use the -s option to specify the NuGet.org source:



3. This will install the NETStandard.Library NuGet package to the MAVLink project.

Here is an example of how to install the NETStandard.Library NuGet package to the MAVLink project:


    dotnet add package NETStandard.Library --version 2.0.3 --source https://api.nuget.org/v3/index.json


4. "C:\Users\ubayd\source\repos\argosALES_desktop\Srtm\bin\NoAzure_Debug\Alpinechough.Srtm.dll" could not be resolved because it was built against the ".NETFramework,Version=v4.8" framework. This is a higher version than the currently targeted framework ".NETFramework,Version=v4.7.1".



![image](https://github.com/UbaydullohML/VS-Projects_BugsFix/assets/75980506/fd20f831-7a8d-45a5-9de4-7a8fb7d31d6b)

![image](https://github.com/UbaydullohML/VS-Projects_BugsFix/assets/75980506/3747311b-b584-400a-bed0-c77c55579492)


5. how to add more package sources online, i have telerik nuget, mic vs offline packages, i need online one which id not face to much problems to run my project.

either code 

    dotnet nuget add source nuget.org https://api.nuget.org/v3/index.json

or 
Nuget Package sources:
Name: nuget.org
Source: https://api.nuget.org/v3/index.json


![image](https://github.com/UbaydullohML/VS-Projects_BugsFix/assets/75980506/edc1175b-5364-4162-94ea-d9cb43df88d4)

