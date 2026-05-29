# DotNetMvcAPI 專案結構與設定檔說明

## 專案結構概覽 (Project Structure Overview)

本專案是一個基於 .NET 10 的 ASP.NET Core Web API 專案，主要目錄結構如下：

- **`Controllers/`**：
  存放 MVC 架構中的控制器 (Controller)。負責接收與處理來自客戶端 (Client) 的 HTTP 請求 (GET, POST, PUT, DELETE 等)，並回傳相對應的資料 (通常是 JSON 格式)。
- **`Models/`**：
  存放資料模型 (Data Models) 或資料傳輸物件 (DTOs)。用於定義應用程式中的資料結構、與資料庫對應的實體 (Entities)，以及商業邏輯。
- **`Properties/`**：
  包含專案層級的設定檔案，最主要的是 `launchSettings.json`，負責控制本機端的啟動行為。
- **`note/`**：
  放置與專案相關的筆記、參考文件與說明檔案（例如本篇結構說明與安裝指南等）。
- **`bin/` & `obj/`**：
  編譯與建置 (Build) 過程中所產生的中繼檔案及最終編譯出的執行檔 (DLL 等)。這些資料夾通常會被 `.gitignore` 忽略，不加入版本控制。

---

## 重要設定檔說明 (Configuration Files)

### 1. `Program.cs`
這是 .NET 6 (及後續版本) 專案的**進入點 (Entry Point)**，採用了 Minimal Hosting Model，將過去的 `Program.cs` 與 `Startup.cs` 合併在一起。
主要負責兩大任務：
- **服務註冊與依賴注入 (Dependency Injection)**：透過 `builder.Services` 註冊應用程式所需的服務。例如 `AddControllers()` 啟用 Controller 支援，`AddOpenApi()` 啟用 OpenAPI 支援。
- **請求管線 (HTTP Request Pipeline)**：透過 `app` 物件設定中介軟體 (Middleware)，決定如何處理每一個 HTTP 請求。專案中設定了開發環境下啟用 OpenAPI 與 Scalar API 文件介面，以及設定 HTTPS 重新導向 (`UseHttpsRedirection`)、授權 (`UseAuthorization`) 和控制器路由 (`MapControllers`)。

### 2. `DotNetMvcAPI.csproj`
專案檔 (C# Project File)，採用 XML 格式，定義了專案的建置方式、目標框架與依賴的第三方套件。
- `<TargetFramework>`：指定專案使用的 .NET 版本 (此專案為 `net10.0`)。
- `<ImplicitUsings>`：啟用隱式 Using (自動幫你引入常用的命名空間，如 `System` 等，減少程式碼冗餘)。
- `<PackageReference>`：列出透過 NuGet 安裝的外部套件與版本。例如 `Microsoft.AspNetCore.OpenApi` 與 `Scalar.AspNetCore` 用於產生與呈現 API 文件。

### 3. `appsettings.json` 與 `appsettings.Development.json`
應用程式的組態設定檔，用來存放應用程式執行時需要的參數 (如 Log 層級、資料庫連線字串、外部 API 金鑰等)。
- **`appsettings.json`**：預設的全域設定檔，所有環境都會讀取。
- **`appsettings.Development.json`**：開發環境專用的設定檔。當應用程式執行在開發環境 (Development) 時，系統會自動載入此檔案，並且其設定值會覆寫 `appsettings.json` 的同名設定，方便在本地端使用不同的設定 (例如本地資料庫連線)。

### 4. `Properties/launchSettings.json`
**本機開發專用**的啟動設定檔，不會部署到正式環境 (Production)。
- 定義了不同的啟動設定檔 (Profiles)，如 `http` 和 `https`。
- 設定了本地測試時應用程式監聽的 URL 與 Port，例如 `http://localhost:5255` 和 `https://localhost:7023`。
- 設定了環境變數 `"ASPNETCORE_ENVIRONMENT": "Development"`，讓 `Program.cs` 知道目前處於開發環境，進而載入對應的 `appsettings` 或啟用開發者專用的中介軟體 (如 Swagger/Scalar 文件)。

### 5. `DotNetMvcAPI.http`
這是一個用於測試 API 端點的 HTTP 請求腳本檔。
- 在 Visual Studio 或 VS Code 等現代 IDE 中，你可以直接開啟此檔案，點擊按鈕來發送 HTTP 請求 (類似 Postman 的功能)，能幫助開發者快速測試 API 是否能正確回傳資料。

### 6. `WeatherForecast.cs`
這是 .NET Web API 專案範本預設提供的一個簡單 Model 類別，通常作為範例程式碼，用來展示資料結構如何與 API 搭配運作。

---
這份文件能幫助您與團隊快速理解專案的基礎架構與各個設定檔的用途。
