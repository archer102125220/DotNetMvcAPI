# DotNetMvcAPI

這是一個用於學習和練習的 .NET (ASP.NET Core Web API) 專案。

## 專案簡介
本專案的主要目的是作為學習 .NET API 開發的練習場。在這裡可以學習和測試如何建立 RESTful API、設計 Controller、處理 Model 等後端開發技巧。

## MVC API vs Minimal API

在 .NET 建立 Web API 專案時，主要有兩種架構可以選擇：**MVC API (Controllers)** 與 **Minimal API**。

- **MVC API (基於 Controller)**：
  這是 .NET 傳統且經典的 API 開發模式。它採用 Model-View-Controller 架構理念（雖然在 API 中通常沒有 View）。您會建立繼承自 `ControllerBase` 的類別，透過路由屬性 (Routing Attributes) 定義端點。這種模式提供了完整的生命週期、過濾器 (Filters)、模型綁定 (Model Binding) 以及依賴注入。
- **Minimal API**：
  這是較新的輕量級開發模式，旨在以最少的程式碼與樣板檔案建立 HTTP API。您可以在 `Program.cs` 檔案中直接使用 `app.MapGet()`, `app.MapPost()` 等方法定義路由與處理邏輯。它效能極佳，且適合微服務或簡單的小型 API。

### 什麼時候適用 MVC API？
- **大型專案與企業級應用**：當專案規模龐大，API 數量眾多時，基於 Controller 的結構能讓程式碼更容易被組織、分類和維護。
- **需要複雜的過濾器 (Filters) 與中介軟體 (Middleware)**：如果您的端點需要共用複雜的授權、驗證或例外處理邏輯。
- **既有團隊的習慣**：如果開發團隊已經非常熟悉傳統的 ASP.NET Core MVC 架構，使用 Controller 可以無縫銜接。
- **需要完整的版本控制 (Versioning) 與 API 文件生成 (Swagger) 高階整合**：MVC 在這些進階功能的支援上通常更為成熟且開箱即用。

## 建立 MVC API 專案的指令

若您想從頭建立一個與本專案類似的 MVC API 專案，可以使用以下 .NET CLI 指令：

```bash
# 建立一個名為 MyMvcApiProject 的 Web API 專案 (預設使用 Controller 架構)
dotnet new webapi -n MyMvcApiProject --use-controllers
```
*(備註：在較新的 .NET SDK 版本中，預設的 `webapi` 範本可能會使用 Minimal API，透過加上 `--use-controllers` 參數可以強制建立基於 Controller 的 MVC API 專案)*

## 環境要求
- [.NET 10.0 SDK](https://dotnet.microsoft.com/download) (或專案對應版本的 .NET SDK)

## 如何啟動專案

> **💡 提示：** 微軟已宣佈自 Visual Studio 2022 之後的版本不再推出 Mac 版（Visual Studio for Mac 已停止支援）。因此，對於在 Mac 上開發最新的 .NET 專案，強烈推薦使用 `.NET CLI` (例如 `dotnet run`) 來進行建置與啟動。

要啟動這個專案，請按照以下步驟進行：

1. **開啟終端機 (Terminal)**
2. **切換到專案的根目錄**：
   ```bash
   cd /Users/parkerchen/Desktop/code/DotNetMvcAPI
   ```
3. **執行專案**：
   - **一般執行模式：**
   ```bash
   dotnet run
   ```

   - **開發者模式 (熱重載 Hot Reload)：**
   （推薦使用此模式，當你修改程式碼並存檔時，API 伺服器會自動重新載入，無須手動重啟）
   ```bash
   dotnet watch run
   ```

### 啟動後的存取方式
專案成功啟動後，終端機會顯示伺服器正在監聽的 URL (例如：`http://localhost:5xxx`)。

在開發環境 (Development) 下，專案預設啟用了 Scalar 做為 API 文件。您可以打開瀏覽器，前往：
```
http://localhost:<您的port>/scalar/v1
```
透過 Scalar UI，您可以方便地查看所有的 API 介面並進行測試。

> 註：由於這是一個純 API 專案，根目錄 (`/`) 預設不會有任何網頁內容，因此直接訪問 `http://localhost:<您的port>/` 會顯示 404 找不到網頁，請直接訪問 API 路由或 `/scalar/v1`。

## 開發工具推薦
您可以選擇以下任一編輯器來開發此專案：
- **Visual Studio Code** (推薦安裝 C# Dev Kit 擴充功能)
- **JetBrains Rider**
- **Visual Studio (Windows)**
- ~~**Visual Studio for Mac**~~ (微軟已停止支援，建議改用 VS Code 或 Rider)

## 跨平台開發：使用 Visual Studio (Windows) 開啟注意事項

由於本專案是透過 `.NET CLI` 建立，如果您在 Windows 平台上使用一般的 Visual Studio 應用程式開啟此專案，請注意以下幾點：

1. **方案檔 (.sln)**：
   CLI 建立專案時預設只產生了專案檔 (`.csproj`) 而沒有方案檔 (`.sln`)。在 Visual Studio 中，請直接點選「開啟專案或方案」，並選取 `DotNetMvcAPI.csproj`。Visual Studio 開啟後會在您儲存時自動產生對應的 `.sln` 方案檔。
2. **啟動設定檔 (Launch Profile)**：
   在 Windows 的 Visual Studio 中，預設的啟動選項通常會是 `IIS Express`。為了與跨平台 CLI 環境下 (`dotnet run`) 的執行行為 (使用 Kestrel 伺服器) 保持一致，建議您在上方啟動按鈕的下拉選單中，將啟動選項切換為專案名稱 (**DotNetMvcAPI**)。
