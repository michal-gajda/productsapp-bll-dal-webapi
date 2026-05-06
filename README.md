# ProductsApp — ASP.NET Core WebAPI (BLL / DAL / UI)

Przykładowa aplikacja demonstrująca warstwową architekturę w ASP.NET Core 8.

## Architektura

```
┌─────────────────────────────────────────────────┐
│  UI  (WebAPI)                                   │
│  Controllers · Middleware · Swagger · Program.cs │
│  Zależność: → BLL                               │
├─────────────────────────────────────────────────┤
│  BLL  (Business Logic Layer)                    │
│  Services · DTOs · Mapping                      │
│  Zależność: → DAL                               │
├─────────────────────────────────────────────────┤
│  DAL  (Data Access Layer)                       │
│  Entities · DbContext · Repositories            │
│  Zależność: EF Core                             │
└─────────────────────────────────────────────────┘
```

## Struktura projektu

```text
ProductsApp-BLL-DAL-WebAPI.slnx
├── src/
│   ├── DAL/                          # Data Access Layer
│   │   ├── Entities/Product.cs       # Model encji
│   │   ├── Data/AppDbContext.cs      # DbContext + seed data
│   │   ├── Repositories/
│   │   │   ├── IRepository.cs        # Generyczny interfejs repozytorium
│   │   │   ├── Repository.cs         # Generyczna implementacja
│   │   │   ├── IProductRepository.cs # Specyficzny interfejs
│   │   │   └── ProductRepository.cs  # Specyficzna implementacja
│   │   └── DAL.csproj
│   ├── BLL/                          # Business Logic Layer
│   │   ├── Models/                   # DTO: CreateProduct, Product, UpdateProduct
│   │   ├── Mapping/                  # Mapowanie Entity ↔ DTO
│   │   ├── Services/
│   │   │   ├── IProductService.cs    # Interfejs serwisu
│   │   │   └── ProductService.cs     # Implementacja z logowaniem
│   │   ├── Validation/               # Walidatory i modele błędów
│   │   └── BLL.csproj
│   └── WebAPI/                       # UI / Warstwa prezentacji
│       ├── Controllers/ProductsController.cs
│       ├── Middleware/ExceptionHandlingMiddleware.cs
│       ├── Models/                   # Modele żądań i odpowiedzi API
│       ├── Program.cs                # Konfiguracja DI + pipeline
│       ├── appsettings.json
│       └── WebAPI.csproj
└── tests/
    └── BLL.FunctionalTests/          # Testy funkcjonalne BLL
        └── BLL.FunctionalTests.csproj
```

## Endpointy API

| Metoda   | Ścieżka                          | Opis                          |
|----------|-----------------------------------|-------------------------------|
| GET      | `/api/products`                   | Lista wszystkich produktów    |
| GET      | `/api/products/{id}`              | Produkt po ID                 |
| GET      | `/api/products/active`            | Tylko aktywne produkty        |
| GET      | `/api/products/category/{name}`   | Produkty z kategorii          |
| GET      | `/api/products/categories`        | Lista kategorii               |
| POST     | `/api/products`                   | Utwórz produkt                |
| PUT      | `/api/products/{id}`              | Aktualizuj produkt            |
| DELETE   | `/api/products/{id}`              | Usuń produkt                  |
| PATCH    | `/api/products/{id}/toggle-active`| Przełącz aktywność            |

## Uruchomienie

```bash
# Przywróć pakiety i uruchom
cd WebAPI
dotnet restore
dotnet run

# Swagger UI dostępny pod:
# https://localhost:5001/swagger
```

## Kluczowe wzorce

- **Repository Pattern** — generyczne repozytorium + specyficzne rozszerzenia
- **DTO Pattern** — separacja modeli API od encji bazodanowych
- **Dependency Injection** — rejestracja w `Program.cs`
- **Global Exception Handling** — middleware przechwytujący wyjątki
- **InMemory Database** — EF Core InMemory z danymi seed (łatwa zamiana na SQL Server)
- **Walidacja** — Data Annotations na DTO

## Zmiana bazy danych

Aby przejść na SQL Server, zmień w `Program.cs`:

```csharp
// Z:
options.UseInMemoryDatabase("ProductsDb");

// Na:
options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
```

I dodaj connection string w `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "Default": "Server=.;Database=ProductsDb;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```
