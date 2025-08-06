using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Models;

namespace TaskFlow.Data
{
    /// <summary>
    /// Represents the database context for the TaskFlow application.
    /// Inherits from IdentityDbContext to include ASP.NET Core Identity tables.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet for core domain models
        public DbSet<Workspace> Workspaces { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<List> Lists { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<BoardUser> BoardUsers { get; set; }
        public DbSet<CardUser> CardUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure string properties for MySQL compatibility
            modelBuilder.Entity<Card>()
                .Property(c => c.Description)
                .HasColumnType("longtext");

            modelBuilder.Entity<Comment>()
                .Property(co => co.Content)
                .HasColumnType("longtext");

            // Configure many-to-many relationship for Board and ApplicationUser
            modelBuilder.Entity<BoardUser>()
                .HasKey(bu => new { bu.BoardId, bu.UserId });

            modelBuilder.Entity<BoardUser>()
                .HasOne(bu => bu.Board)
                .WithMany(b => b.BoardUsers)
                .HasForeignKey(bu => bu.BoardId);

            modelBuilder.Entity<BoardUser>()
                .HasOne(bu => bu.User)
                .WithMany(u => u.BoardUsers)
                .HasForeignKey(bu => bu.UserId);

            // Configure many-to-many relationship for Card and ApplicationUser
            modelBuilder.Entity<CardUser>()
                .HasKey(cu => new { cu.CardId, cu.UserId });

            modelBuilder.Entity<CardUser>()
                .HasOne(cu => cu.Card)
                .WithMany(c => c.CardUsers)
                .HasForeignKey(cu => cu.CardId);

            modelBuilder.Entity<CardUser>()
                .HasOne(cu => cu.User)
                .WithMany(u => u.CardUsers)
                .HasForeignKey(cu => cu.UserId);

            // Configure one-to-many relationship for Workspace and Board
            modelBuilder.Entity<Board>()
                .HasOne(b => b.Workspace)
                .WithMany(w => w.Boards)
                .HasForeignKey(b => b.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete boards when a workspace is deleted

            // Configure one-to-many relationship for Board and List
            modelBuilder.Entity<List>()
                .HasOne(l => l.Board)
                .WithMany(b => b.Lists)
                .HasForeignKey(l => l.BoardId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete lists when a board is deleted

            // Configure one-to-many relationship for List and Card
            modelBuilder.Entity<Card>()
                .HasOne(c => c.List)
                .WithMany(l => l.Cards)
                .HasForeignKey(c => c.ListId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete cards when a list is deleted

            // Configure one-to-many relationship for Card and Comment
            modelBuilder.Entity<Comment>()
                .HasOne(co => co.Card)
                .WithMany(ca => ca.Comments)
                .HasForeignKey(co => co.CardId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete comments when a card is deleted

            // Configure one-to-many relationship for ApplicationUser and Comment
            modelBuilder.Entity<Comment>()
                .HasOne(co => co.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(co => co.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Restrict deletion of user if comments exist
        }
    }
}