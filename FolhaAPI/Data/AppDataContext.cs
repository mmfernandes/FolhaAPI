using Microsoft.EntityFrameworkCore;
using FolhaAPI.Models;

namespace FolhaAPI.Data;

public class AppDataContext : DbContext
{
    public AppDataContext(DbContextOptions<AppDataContext> options) : base(options){}

    public DbSet<Funcionario> Funcionarios { get; set; } 
    public DbSet<Folha> Folhas { get; set; } 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}