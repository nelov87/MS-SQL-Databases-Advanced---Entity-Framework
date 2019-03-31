using Microsoft.EntityFrameworkCore;
using P03_FootballBetting.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P03_FootballBetting.Data
{
    public class FootballBettingContext : DbContext
    {
        public DbSet<Bet> Bets { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<User> Users { get; set; }



        public FootballBettingContext()
        {

        }

        public FootballBettingContext(DbContextOptions dbContext) : base(dbContext)
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-533LOVH\\SQLEXPRESS;Database=SalesDb;Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasKey(t => t.TeamId);

                entity
                .HasOne(c => c.PrimaryKitColor)
                .WithMany(t => t.PrimaryKitTeams)
                .HasForeignKey(t => t.PrimaryKitColorId);

                entity
                .HasOne(c => c.SecondaryKitColor)
                .WithMany(t => t.SecondaryKitTeams)
                .HasForeignKey(t => t.SecondaryKitColorId);

                entity
                .HasOne(t => t.Town)
                .WithMany(e => e.Teams)
                .HasForeignKey(t => t.TownId);

                entity
                .HasMany(p => p.Players)
                .WithOne(t => t.Team)
                .HasForeignKey(e => e.PlayerId);

               
            });

            modelBuilder.Entity<Town>(entity =>
            {
                entity
                .HasKey(t => t.TownId);

                entity
                .HasOne(t => t.Country)
                .WithMany(c => c.Towns)
                .HasForeignKey(t => t.CountryId);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(c => c.CountryId);

                entity
                .HasMany(t => t.Towns)
                .WithOne(c => c.Country);
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.HasKey(g => g.GameId);

                entity
                .HasOne(t => t.HomeTeam)
                .WithMany(g => g.HomeGames)
                .HasForeignKey(g => g.HomeTeamId);

                entity
                .HasOne(t => t.AwayTeam)
                .WithMany(g => g.AwayGames)
                .HasForeignKey(t => t.AwayTeamId);
            });

            modelBuilder.Entity<PlayerStatistic>(entity =>
            {
                entity.HasKey(p => new { p.PlayerId, p.GameId });

                entity
                .HasOne(p => p.Player)
                .WithMany(s => s.PlayerStatistics)
                .HasForeignKey(e => e.PlayerId);

                entity
                .HasOne(g => g.Game)
                .WithMany(s => s.PlayerStatistics)
                .HasForeignKey(s => s.GameId);
            });
        }
    }
}
