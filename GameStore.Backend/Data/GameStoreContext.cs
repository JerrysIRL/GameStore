﻿using GamesWebApp.Entities;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GamesWebApp.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(
            new {Id = 1, Name = "Fighting"},
            new {Id = 2, Name = "Sports"},
            new {Id = 3, Name = "Racing"}
        );
    }
}