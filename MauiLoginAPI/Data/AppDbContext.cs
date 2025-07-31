// File: Data/AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using MauiLoginAPI.Models;

namespace MauiLoginAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
