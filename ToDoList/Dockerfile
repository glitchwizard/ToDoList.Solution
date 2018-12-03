#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/aspnetcore:1.1 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:1.1 AS build
WORKDIR /src
COPY ["ToDoList/ToDoList.csproj", "ToDoList/"]
RUN dotnet restore "ToDoList/ToDoList.csproj"
COPY . .
WORKDIR "/src/ToDoList"
RUN dotnet build "ToDoList.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ToDoList.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ToDoList.dll"]