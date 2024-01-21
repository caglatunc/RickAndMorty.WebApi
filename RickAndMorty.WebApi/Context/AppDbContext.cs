using Microsoft.EntityFrameworkCore;
using RickAndMorty.WebApi.Models;
using System.Reflection;

namespace RickAndMorty.WebApi.Context;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Episode> Episodes { get; set; }
    public DbSet<EpisodeCharacter> EpisodeCharacters { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Origin> Origins { get; set; }
    public DbSet<Character> Characters { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EpisodeCharacter>()
            .HasKey(x=> new { x.EpisodeId, x.CharacterId });//Composite key

        modelBuilder.Entity<Episode>().HasMany(p => p.EpisodeCharacters).WithOne();

        modelBuilder.Entity<EpisodeCharacter>().HasOne<Episode>().WithMany(p => p.EpisodeCharacters).HasForeignKey(p => p.EpisodeId);

        modelBuilder.Entity<EpisodeCharacter>().HasOne(p=>p.Character).WithMany().HasForeignKey(p => p.CharacterId);

        modelBuilder.Entity<Character>().HasMany<EpisodeCharacter>().WithOne(p => p.Character).OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Character>().HasOne(p => p.Location).WithMany().HasForeignKey(p => p.LocationId);

        modelBuilder.Entity<Location>().HasMany<Character>().WithOne(p=>p.Location).OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Character>().HasOne(p => p.Origin).WithMany().HasForeignKey(p => p.OriginId);

        modelBuilder.Entity<Origin>().HasMany<Character>().WithOne(p => p.Origin).OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Character>().HasIndex(i=>i.Url).IsUnique();

        modelBuilder.Entity<Episode>().HasIndex(i => new {i.EpisodeNumber}).IsUnique();

    }
}
