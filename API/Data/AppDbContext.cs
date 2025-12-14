using System;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

// public class AppDbContext : DbContext // klasa musi dziedziczyć z DbContext dla funkcjonalności entity framework
// {
//     **musimy przekazać do DbContext konfiguracje naszej bazy, to jest stary sposób na zrobienie tego
//     public AppDbContext(DbContextOptions options) : base(options)
//     {
        
//     }

    

// }

public class AppDbContext(DbContextOptions options) : DbContext(options) //* primary constructor
{
    public DbSet<AppUser> Users { get; set; } //* Nazwa tabeli: Users, Kolumny: Id - primry key, kolejne to właściwości klasy AppUser
}
