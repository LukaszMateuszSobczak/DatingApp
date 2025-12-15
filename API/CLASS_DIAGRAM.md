# Diagram klas projektu DatingApp API

## Diagram Mermaid

```mermaid
classDiagram
    %% Entities
    class AppUser {
        +string Id
        +string DisplayName
        +string Email
        +byte[] PasswordHash
        +byte[] PasswordSalt
    }

    %% DTOs (Data Transfer Objects)
    class RegisterDto {
        +string DisplayName
        +string Email
        +string Password
    }

    class LoginDto {
        +string Email
        +string Password
    }

    class UserDto {
        +string Id
        +string Email
        +string DisplayName
        +string? ImageUrl
        +string Token
    }

    %% Controllers
    class BaseApiController {
        <<abstract>>
    }

    class AccountController {
        -AppDbContext context
        -ITokenService tokenService
        +Register(RegisterDto) Task~ActionResult~UserDto~~
        +Login(LoginDto) Task~ActionResult~UserDto~~
        -EmailExists(string) Task~bool~
    }

    class MembersController {
        -AppDbContext context
        +GetMembers() Task~ActionResult~List~AppUser~~~
        +GetMemberById(string) Task~ActionResult~AppUser~~
        +DeleteMemberById(string) Task~ActionResult~
    }

    %% Data Layer
    class AppDbContext {
        +DbSet~AppUser~ Users
    }

    %% Services & Interfaces
    class ITokenService {
        <<interface>>
        +CreateToken(AppUser) string
    }

    class TokenService {
        -IConfiguration config
        +CreateToken(AppUser) string
    }

    %% Relations - Inheritance
    BaseApiController <|-- AccountController : extends
    BaseApiController <|-- MembersController : extends
    ITokenService <|.. TokenService : implements

    %% Relations - Dependencies
    AccountController ..> RegisterDto : uses
    AccountController ..> LoginDto : uses
    AccountController ..> UserDto : returns
    AccountController --> AppDbContext : uses
    AccountController --> ITokenService : uses
    
    MembersController --> AppDbContext : uses
    MembersController ..> AppUser : returns
    
    AppDbContext --> AppUser : manages
    
    TokenService ..> AppUser : uses
    
    AccountController ..> AppUser : creates

    %% Annotations
    note for AppUser "Encja użytkownika w bazie danych"
    note for RegisterDto "DTO dla rejestracji nowego użytkownika"
    note for LoginDto "DTO dla logowania użytkownika"
    note for UserDto "DTO zwracane po autoryzacji (z tokenem JWT)"
    note for AppDbContext "Entity Framework DbContext - dostęp do bazy"
    note for TokenService "Serwis generujący JWT tokeny"
```

---

## Legenda

### Typy relacji:
- **`<|--`** - Dziedziczenie (extends)
- **`<|..`** - Implementacja interfejsu (implements)
- **`-->`** - Użycie/Zależność silna (pole w klasie)
- **`..>`** - Użycie/Zależność słaba (parametr metody, zwracany typ)

### Typy klas:
- **Entities** - Modele bazy danych (AppUser)
- **DTOs** - Data Transfer Objects (RegisterDto, LoginDto, UserDto)
- **Controllers** - Endpointy API (AccountController, MembersController)
- **Data** - DbContext (AppDbContext)
- **Services** - Logika biznesowa (TokenService)
- **Interfaces** - Kontrakty (ITokenService)

---

## Opis przepływu danych

### Rejestracja użytkownika
```
RegisterDto → AccountController → AppUser (nowy) → AppDbContext → Baza danych
                    ↓
              TokenService.CreateToken(AppUser) → JWT Token
                    ↓
                UserDto (z tokenem) → Response
```

### Logowanie użytkownika
```
LoginDto → AccountController → AppDbContext.Users → AppUser (pobrany)
                    ↓
           Weryfikacja hasła (HMACSHA512)
                    ↓
           TokenService.CreateToken(AppUser) → JWT Token
                    ↓
           UserDto (z tokenem) → Response
```

### Pobieranie listy członków
```
MembersController → AppDbContext.Users → List<AppUser> → Response
```

### Pobieranie członka po ID
```
MembersController → AppDbContext.Users.FindAsync(id) → AppUser → Response
```

### Usuwanie członka
```
MembersController → AppDbContext.Users.FindAsync(id) → Remove → SaveChangesAsync
```

---

## Struktura projektu

```
API/
├── Controllers/
│   ├── BaseApiController.cs       (Klasa bazowa)
│   ├── AccountController.cs       (Rejestracja, logowanie)
│   └── MembersController.cs       (CRUD użytkowników)
│
├── Entities/
│   └── AppUser.cs                 (Model użytkownika)
│
├── DTOs/
│   ├── RegisterDto.cs             (Dane rejestracji)
│   ├── LoginDto.cs                (Dane logowania)
│   └── UserDto.cs                 (Odpowiedź z tokenem)
│
├── Data/
│   └── AppDbContext.cs            (EF Core DbContext)
│
├── Services/
│   └── TokenService.cs            (Generowanie JWT)
│
└── Interfaces/
    └── ITokenService.cs           (Kontrakt serwisu tokenów)
```

---

## Jak wyświetlić diagram?

### 1. W VS Code
Zainstaluj rozszerzenie: **Markdown Preview Mermaid Support**

### 2. Online
Skopiuj kod Mermaid do: https://mermaid.live/

### 3. GitHub
Diagram automatycznie renderuje się w plikach .md

---

Ostatnia aktualizacja: 15 grudnia 2025
