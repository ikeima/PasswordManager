# ���������� ����������� ����� .NET SDK ��� ������
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# ������������� ������� ���������� ������ ����������
WORKDIR /app

# �������� ���� ������� � ��������������� �����������
COPY *.csproj ./
RUN dotnet restore

# �������� ��� ��������� ����� ������� � ���������
COPY . ./

# ��������� ���������� � ����� "out"
RUN dotnet publish -c Release -o out

# ���������� ����������� ����� .NET Runtime ��� �������
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# ������������� ������� ���������� ������ ����������
WORKDIR /app

# �������� ��������� ���������� �� ����������� �����
COPY --from=build /app/out .

# ��������� ����, �� ������� ����� �������� ����������
EXPOSE 8080

# ������� ��� ������� ����������
ENTRYPOINT ["dotnet", "PasswordManager.dll"]

# ��������� � ��������� Redis
RUN apt-get update && apt-get install -y redis-server
EXPOSE 6379
CMD ["redis-server"]
