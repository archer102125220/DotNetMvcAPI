# DotNetMvcAPI 部署指南

這份指南說明如何將 `DotNetMvcAPI` 專案發布並部署到正式環境（Production）。

## 1. 準備發布檔案 (發行編譯)

在部署應用程式之前，需要先將程式碼編譯並打包為適合正式環境的發布檔案。使用 `dotnet publish` 指令可以完成這項工作。

請在專案根目錄（`DotNetMvcAPI.csproj` 所在目錄）執行以下指令：

```bash
dotnet publish -c Release -o ./publish
```

**參數說明：**
*   `-c Release`: 指定建置組態為 `Release`，這會進行程式碼最佳化並移除除錯資訊，讓程式執行更有效率。
*   `-o ./publish`: 指定輸出的資料夾名稱（這裡是 `./publish`）。執行完畢後，所有部署需要用到的檔案都會放在這個資料夾內。

進入 `./publish` 資料夾後，你會看到 `DotNetMvcAPI.dll`、`DotNetMvcAPI.exe` (若在 Windows 上編譯) 以及其他依賴檔案與設定檔 (`appsettings.json` 等)。

## 2. 部署方式選項

編譯出來的檔案是跨平台的 (Framework-dependent deployment 預設情況下)，只要目標機器上有安裝 .NET Runtime，就可以執行。

以下提供幾種常見的部署方式：

### 選項 A：直接在伺服器上執行 (Linux / Windows / macOS)

**前提條件：** 目標伺服器上必須安裝與專案對應版本的 **.NET Runtime** 或 **.NET ASP.NET Core Runtime** (目前專案通常是 .NET 6 或 .NET 8)。不需要安裝完整的 SDK。

1.  將 `./publish` 資料夾內的所有檔案複製到伺服器上的某個目錄，例如 `/var/www/dotnetmvcapi` (Linux) 或 `C:\inetpub\dotnetmvcapi` (Windows)。
2.  設定環境變數 `ASPNETCORE_ENVIRONMENT` 為 `Production`。
    *   Linux (bash): `export ASPNETCORE_ENVIRONMENT=Production`
    *   Windows (cmd): `set ASPNETCORE_ENVIRONMENT=Production`
    *   Windows (PowerShell): `$env:ASPNETCORE_ENVIRONMENT="Production"`
3.  進入該目錄並執行應用程式：
    ```bash
    cd /var/www/dotnetmvcapi
    dotnet DotNetMvcAPI.dll
    ```
4.  應用程式預設會監聽 `http://localhost:5000` 或 `https://localhost:5001` (除非在 `appsettings.json` 覆寫)。

### 選項 B：使用 Nginx 或 Apache 作為反向代理 (Linux 環境推薦)

在正式環境中，通常不會讓 Kestrel (ASP.NET Core 內建的網頁伺服器) 直接面對外網，而是透過 Nginx 或 Apache 進行反向代理 (Reverse Proxy)。

1.  在 Linux 伺服器上安裝 .NET Runtime。
2.  將應用程式設定為背景服務 (例如使用 `systemd`) 讓其保持執行狀態 (執行 `dotnet DotNetMvcAPI.dll`)。
3.  設定 Nginx (或 Apache) 監聽 80 / 443 Port，並將請求轉發至本機的 5000 Port。

**Nginx 設定範例 (`/etc/nginx/sites-available/default`)：**
```nginx
server {
    listen 80;
    server_name example.com; # 替換成你的網域或 IP

    location / {
        proxy_pass http://localhost:5000;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection keep-alive;
        proxy_set_header Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
    }
}
```

### 選項 C：使用 Docker 容器化部署

如果你偏好容器化部署，可以建立一個 `Dockerfile` 來打包應用程式。這能確保應用程式在任何支援 Docker 的環境中都能一致地執行。

**專案根目錄下建立 `Dockerfile` 範例：**

```dockerfile
# 階段 1：建置環境
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["DotNetMvcAPI.csproj", "./"]
RUN dotnet restore "DotNetMvcAPI.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "DotNetMvcAPI.csproj" -c Release -o /app/build

# 階段 2：發布環境
FROM build AS publish
RUN dotnet publish "DotNetMvcAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 階段 3：執行環境
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotNetMvcAPI.dll"]
```
*(請根據專案實際使用的 .NET 版本修改 `8.0` 的標籤)*

**建置與執行 Docker 映像檔：**
```bash
# 建置 Docker 映像檔
docker build -t dotnetmvcapi-app .

# 執行 Docker 容器 (將主機的 8080 Port 對應到容器的 80 Port)
docker run -d -p 8080:80 --name my-mvc-api dotnetmvcapi-app
```
### 選項 D：部署至 Windows IIS (Windows 環境推薦)

在 Windows 伺服器上，IIS (Internet Information Services) 是最標準且強大的網頁伺服器，負責管理應用程式集區 (App Pools) 與請求轉發。

1. **安裝環境**：
   * 在伺服器上安裝 **.NET Hosting Bundle** (請確保版本與專案一致)。這會同時安裝 .NET Runtime 以及 IIS 整合模組 (ASP.NET Core Module v2)。
   * 安裝完成後，建議重新啟動 IIS (於 CMD 執行 `iisreset`)。
2. **準備發布檔案**：
   * 將 `./publish` 資料夾內的檔案複製到伺服器 (例如 `C:\inetpub\wwwroot\DotNetMvcAPI`)。
3. **設定 IIS**：
   * 開啟 **IIS 管理員**。
   * 新增一個 **應用程式集區 (Application Pool)**，將 **.NET CLR 版本**設定為 **無 Managed 程式碼 (No Managed Code)** (因為 .NET Core/5+ 是獨立運作的，IIS 僅作為反向代理)。
   * **新增網站**或在現有網站下**新增應用程式**，實體路徑指向剛才複製的發布資料夾。
   * 將此網站指派給剛建立的應用程式集區。
4. **權限設定**：確保應用程式集區身分 (例如 `IIS AppPool\YourAppPoolName`) 對該資料夾擁有**讀取**與**執行**權限。若有檔案寫入需求，也需賦予寫入權限。

### 選項 E：部署為 Windows Service (適合背景執行的 API)

如果不需要 IIS 的完整網頁伺服器功能，將應用程式註冊為 Windows 服務也是一種輕量且能隨系統自動啟動的做法。

1. **修改程式碼支援服務 (若專案尚未支援)**：
   * 在專案中安裝 NuGet 套件：`Microsoft.Extensions.Hosting.WindowsServices`
   * 修改 `Program.cs`，在 `builder.Build()` 之前加入：`builder.Host.UseWindowsService();`
   * 重新發布專案以產生新的 `.exe` 檔。
2. **註冊服務**：
   * 開啟系統管理員權限的命令提示字元 (CMD) 或 PowerShell。
   * 執行以下 `sc create` 指令建立服務 (注意：`binPath=` 等號後面**必須**有一個空格)：
     ```cmd
     sc create "DotNetMvcAPI" binPath= "C:\你的部署路徑\DotNetMvcAPI.exe" start= auto
     ```
3. **啟動服務**：
   * 執行 `sc start "DotNetMvcAPI"`，或是開啟 Windows 的「服務 (services.msc)」介面來啟動並檢查狀態。

## 3. 環境設定 (Environment Variables)

在部署到正式環境時，請確保：
1.  **停用 Swagger：** Swagger 通常只在開發環境 (`Development`) 開啟。當 `ASPNETCORE_ENVIRONMENT` 設為 `Production` 時，`Program.cs` 中的設定預設會將其隱藏，請確認這點，以免暴露 API 結構。
2.  **Appsettings：** 若正式環境有不同的資料庫連線字串或 API 金鑰，請在部署的資料夾中建立或修改 `appsettings.Production.json`，系統會自動覆蓋 `appsettings.json` 裡的預設值。也可以透過環境變數覆寫設定 (例如 `ConnectionStrings__DefaultConnection`)。
3.  **HTTPS 憑證：** 建議正式環境的對外接口都應設定 SSL/TLS 憑證 (可由 Nginx/反向代理層處理，或在 Kestrel 中設定憑證)。
