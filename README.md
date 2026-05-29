# DotNetMvcAPI

這是一個用於學習和練習的 .NET (ASP.NET Core Web API) 專案。

## 專案簡介
本專案的主要目的是作為學習 .NET API 開發的練習場。在這裡可以學習和測試如何建立 RESTful API、設計 Controller、處理 Model 等後端開發技巧。

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
   ```bash
   dotnet run
   ```

### 啟動後的存取方式
專案成功啟動後，終端機會顯示伺服器正在監聽的 URL (例如：`http://localhost:5xxx`)。

在開發環境 (Development) 下，專案預設啟用了 Swagger。您可以打開瀏覽器，前往：
```
http://localhost:<您的port>/swagger
```
透過 Swagger UI，您可以方便地查看所有的 API 介面並進行測試。

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
