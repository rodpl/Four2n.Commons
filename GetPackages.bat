SET NUGET=%~dp0\tools\nuget\NuGet.exe
%NUGET% i Antlr -v 3.1.1 -o packages
%NUGET% i NHibernate -v 2.1.2.4000 -o packages
%NUGET% i SqlServerCompact -v 4.0.8482.1 -o packages
%NUGET% i nunit -v 2.5.10.11092 -o packages
%NUGET% i Moq -v 4.0.8435.1 -o packages
