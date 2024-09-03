# Используем официальный образ .NET SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Устанавливаем рабочую директорию внутри контейнера
WORKDIR /app

# Копируем файл проекта и восстанавливаем зависимости
COPY *.csproj ./
RUN dotnet restore

# Копируем все остальные файлы проекта в контейнер
COPY . ./

# Публикуем приложение в папку "out"
RUN dotnet publish -c Release -o out

# Используем официальный образ .NET Runtime для запуска
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Устанавливаем рабочую директорию внутри контейнера
WORKDIR /app

# Копируем собранное приложение из предыдущего этапа
COPY --from=build /app/out .

# Открываем порт, на котором будет работать приложение
EXPOSE 8080

# Команда для запуска приложения
ENTRYPOINT ["dotnet", "PasswordManager.dll"]

# Установка и настройка Redis
RUN apt-get update && apt-get install -y redis-server
EXPOSE 6379
CMD ["redis-server"]
