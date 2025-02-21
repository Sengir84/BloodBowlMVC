using System;
using System.Collections.Generic;
using BloodBowlMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace BloodBowlMVC.Data;

public partial class BloodBowlDbContext : DbContext
{
    public BloodBowlDbContext()
    {
    }

    public BloodBowlDbContext(DbContextOptions<BloodBowlDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Roster> Rosters { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<Trainer> Trainers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-68GFSRK\\SENGIRSSERVER;Database=BloodBowl;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.PlayerIdPk).HasName("PK__Players__007D39B554724A85");

            entity.Property(e => e.PlayerIdPk).HasColumnName("playerID_PK");
            entity.Property(e => e.PlayerAgility).HasColumnName("playerAgility");
            entity.Property(e => e.PlayerArmorValue).HasColumnName("playerArmorValue");
            entity.Property(e => e.PlayerMovement).HasColumnName("playerMovement");
            entity.Property(e => e.PlayerName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("playerName");
            entity.Property(e => e.PlayerPosition)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("playerPosition");
            entity.Property(e => e.PlayerRosterIdFk).HasColumnName("playerRosterID_FK");
            entity.Property(e => e.PlayerStatus)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasDefaultValue("Active")
                .HasColumnName("playerStatus");
            entity.Property(e => e.PlayerStrength).HasColumnName("playerStrength");
            entity.Property(e => e.PlayerTeamIdFk).HasColumnName("playerTeamID_FK");
            entity.Property(e => e.Playercost).HasColumnName("playercost");

            entity.HasOne(d => d.PlayerRosterIdFkNavigation).WithMany(p => p.Players)
                .HasForeignKey(d => d.PlayerRosterIdFk)
                .HasConstraintName("FK__Players__playerR__45F365D3");

            entity.HasOne(d => d.PlayerTeamIdFkNavigation).WithMany(p => p.Players)
                .HasForeignKey(d => d.PlayerTeamIdFk)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Players__playerT__46E78A0C");

            entity.HasMany(d => d.PlayerSkillsSkillIdFks).WithMany(p => p.PlayerSkillsPlayerIdFks)
                .UsingEntity<Dictionary<string, object>>(
                    "PlayerSkill",
                    r => r.HasOne<Skill>().WithMany()
                        .HasForeignKey("PlayerSkillsSkillIdFk")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PlayerSki__playe__4CA06362"),
                    l => l.HasOne<Player>().WithMany()
                        .HasForeignKey("PlayerSkillsPlayerIdFk")
                        .HasConstraintName("FK__PlayerSki__playe__4BAC3F29"),
                    j =>
                    {
                        j.HasKey("PlayerSkillsPlayerIdFk", "PlayerSkillsSkillIdFk").HasName("PK__PlayerSk__87F035D8CA8CC0C5");
                        j.ToTable("PlayerSkills");
                        j.IndexerProperty<int>("PlayerSkillsPlayerIdFk").HasColumnName("playerSkillsPlayerID_FK");
                        j.IndexerProperty<int>("PlayerSkillsSkillIdFk").HasColumnName("playerSkillsSkillID_FK");
                    });
        });

        modelBuilder.Entity<Roster>(entity =>
        {
            entity.HasKey(e => e.RosterIdPk).HasName("PK__Roster__5330EA5D38878BFB");

            entity.ToTable("Roster");

            entity.Property(e => e.RosterIdPk).HasColumnName("rosterID_PK");
            entity.Property(e => e.RosterAgility).HasColumnName("rosterAgility");
            entity.Property(e => e.RosterArmor).HasColumnName("rosterArmor");
            entity.Property(e => e.RosterCost).HasColumnName("rosterCost");
            entity.Property(e => e.RosterMovement).HasColumnName("rosterMovement");
            entity.Property(e => e.RosterPosition)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("rosterPosition");
            entity.Property(e => e.RosterRace)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("rosterRace");
            entity.Property(e => e.RosterStrength).HasColumnName("rosterStrength");

            entity.HasMany(d => d.RosterSkillsSkillsIdFks).WithMany(p => p.RosterSkillsRosterIdFks)
                .UsingEntity<Dictionary<string, object>>(
                    "RosterSkill",
                    r => r.HasOne<Skill>().WithMany()
                        .HasForeignKey("RosterSkillsSkillsIdFk")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__rosterSki__roste__5070F446"),
                    l => l.HasOne<Roster>().WithMany()
                        .HasForeignKey("RosterSkillsRosterIdFk")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__rosterSki__roste__4F7CD00D"),
                    j =>
                    {
                        j.HasKey("RosterSkillsRosterIdFk", "RosterSkillsSkillsIdFk").HasName("PK__rosterSk__EF3C90DE013EA400");
                        j.ToTable("rosterSkills");
                        j.IndexerProperty<int>("RosterSkillsRosterIdFk").HasColumnName("rosterSkillsRosterID_FK");
                        j.IndexerProperty<int>("RosterSkillsSkillsIdFk").HasColumnName("rosterSkillsSkillsID_FK");
                    });
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.SkillIdPk).HasName("PK__Skills__004BB7D6CF57DE3D");

            entity.Property(e => e.SkillIdPk).HasColumnName("skillID_PK");
            entity.Property(e => e.SkillName)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("skillName");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.TeamIdPk).HasName("PK__Teams__C97EF2DF7545369D");

            entity.Property(e => e.TeamIdPk).HasColumnName("teamID_PK");
            entity.Property(e => e.TeamApothecary)
                .HasDefaultValue(0)
                .HasColumnName("teamApothecary");
            entity.Property(e => e.TeamAssistantCoaches)
                .HasDefaultValue(0)
                .HasColumnName("teamAssistantCoaches");
            entity.Property(e => e.TeamCheerleaders)
                .HasDefaultValue(0)
                .HasColumnName("teamCheerleaders");
            entity.Property(e => e.TeamFanFactor)
                .HasDefaultValue(0)
                .HasColumnName("teamFanFactor");
            entity.Property(e => e.TeamName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("teamName");
            entity.Property(e => e.TeamRace)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("teamRace");
            entity.Property(e => e.TeamRating)
                .HasDefaultValue(0)
                .HasColumnName("teamRating");
            entity.Property(e => e.TeamRerolls)
                .HasDefaultValue(0)
                .HasColumnName("teamRerolls");
            entity.Property(e => e.TeamTrainerIdFk).HasColumnName("teamTrainerID_FK");
            entity.Property(e => e.TeamTreasury)
                .HasDefaultValue(1000)
                .HasColumnName("teamTreasury");

            entity.HasOne(d => d.TeamTrainerIdFkNavigation).WithMany(p => p.Teams)
                .HasForeignKey(d => d.TeamTrainerIdFk)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Teams__teamTrain__403A8C7D");
        });

        modelBuilder.Entity<Trainer>(entity =>
        {
            entity.HasKey(e => e.TrainerIdPk).HasName("PK__Trainers__AAA34613A6DB6FD3");

            entity.Property(e => e.TrainerIdPk).HasColumnName("trainerID_PK");
            entity.Property(e => e.TrainerName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("trainerName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
