using ConwayGameOfLife.Infra.Data.Mapping;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwayGameOfLife.Infra.Data.Context
{
    public class GameOfLifeDbContext : DbContext
    {
        public GameOfLifeDbContext(DbContextOptions<GameOfLifeDbContext> options) : base(options) 
        { 
        }

        public DbSet<Board> Boards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BoardMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
}
