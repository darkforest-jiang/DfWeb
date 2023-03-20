using System;
using System.Collections.Generic;
using DfConfig.Service.Entities;
using Microsoft.EntityFrameworkCore;

namespace DfConfig.Service.Context;

public partial class MysqlContext : DbContextBase
{
    public MysqlContext()
    {
    }

    public MysqlContext(DbContextOptions<MysqlContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TApp> Tapps { get; set; }

    public virtual DbSet<TAppClient> Tappclients { get; set; }

    public virtual DbSet<TEnv> Tenvs { get; set; }

    public virtual DbSet<TKv> Tkvs { get; set; }

    public virtual DbSet<TNamespace> Tnamespaces { get; set; }

    public virtual DbSet<TUser> Tusers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<TApp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tapp");

            entity.Property(e => e.AppKey)
                .HasMaxLength(32)
                .HasComment("主键 应用的Id");
            entity.Property(e => e.AppName)
                .HasMaxLength(50)
                .HasComment("应用名称");
        });

        modelBuilder.Entity<TAppClient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tappclient");

            entity.Property(e => e.Id)
                .HasComment("主键Id 自增")
                .HasColumnName("id");
            entity.Property(e => e.AppId).HasComment("应用Id");
            entity.Property(e => e.ClientIp)
                .HasMaxLength(20)
                .HasComment("客户端Ip");
            entity.Property(e => e.ClientPort).HasComment("客户端端口号");
            entity.Property(e => e.EnvId).HasComment("环境Id");
        });

        modelBuilder.Entity<TAppNamespace>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tappnamespace");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("主键Id");
            entity.Property(e => e.AppId).HasComment("应用Id");
            entity.Property(e => e.NsId).HasComment("命名空间Id");
        });

        modelBuilder.Entity<TEnv>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tenv");

            entity.HasIndex(e => e.Env, "uk1").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("主键Id");
            entity.Property(e => e.Env)
                .HasMaxLength(10)
                .HasComment("运行环境名称");
        });

        modelBuilder.Entity<TKv>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tkv");

            entity.HasIndex(e => new { e.AppId, e.EnvId, e.Key, e.NsId }, "uk1").IsUnique();

            entity.Property(e => e.Id).HasComment("主键Id");
            entity.Property(e => e.AppId).HasComment("应用表主键Id");
            entity.Property(e => e.EnvId).HasComment("环境表主键Id");
            entity.Property(e => e.Key)
                .HasMaxLength(32)
                .HasComment("Key");
            entity.Property(e => e.Notes)
                .HasMaxLength(32)
                .HasComment("注释");
            entity.Property(e => e.NsId).HasComment("命名空间Id");
            entity.Property(e => e.Status).HasComment("发布状态 0-未发布 已发布");
            entity.Property(e => e.Value)
                .HasComment("Value")
                .HasColumnType("text");
        });

        modelBuilder.Entity<TNamespace>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tnamespace");

            entity.Property(e => e.Id).HasComment("主键Id");
            entity.Property(e => e.AppId).HasComment("AppId");
            entity.Property(e => e.IsPublic).HasComment("命名空间属性 公共=1 私有=0");
            entity.Property(e => e.NameSpaceKey)
                .HasMaxLength(32)
                .HasComment("命名空间");
            entity.Property(e => e.Notes)
                .HasMaxLength(64)
                .HasComment("备注");
        });

        modelBuilder.Entity<TUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tuser");

            entity.Property(e => e.Id).HasComment("主键Id 自增");
            entity.Property(e => e.IsAdmin).HasComment("是否管理员");
            entity.Property(e => e.LoginId)
                .HasMaxLength(10)
                .HasComment("登录账号");
            entity.Property(e => e.Password)
                .HasMaxLength(64)
                .HasComment("登录密码");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
