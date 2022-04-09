using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Blog.Models
{
    public partial class BlogContext : DbContext
    {
        //public BlogContext()
        //{
        //}

        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options)
        {
        }
        
        public virtual DbSet<AccessRight> AccessRights { get; set; }
        public virtual DbSet<AccessRightGroup> AccessRightGroups { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostCategory> PostCategories { get; set; }
        public virtual DbSet<PostTag> PostTags { get; set; }
        public virtual DbSet<PostUser> PostUsers { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=SARMADSPC\\SQLEXPRESS;Initial Catalog=Blog;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<AccessRight>(entity =>
            {
                entity.ToTable("AccessRight");

                entity.Property(e => e.Created).HasColumnType("date");

                entity.Property(e => e.Creater)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.LastEdit).HasColumnType("date");

                entity.Property(e => e.LastEditor)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<AccessRightGroup>(entity =>
            {
                entity.HasKey(e => new { e.AccessRightId, e.GroupId });

                entity.ToTable("AccessRight_Group");

                entity.HasOne(d => d.AccessRight)
                    .WithMany(p => p.AccessRightGroups)
                    .HasForeignKey(d => d.AccessRightId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AcessRight_User_Group");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.AccessRightGroups)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Group_AccessRight_Group");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Created).HasColumnType("date");

                entity.Property(e => e.Creater)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.LastEdit).HasColumnType("date");

                entity.Property(e => e.LastEditor)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Group");

                entity.Property(e => e.Created).HasColumnType("date");

                entity.Property(e => e.Creater)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.LastEdit).HasColumnType("date");

                entity.Property(e => e.LastEditor)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.Created).HasColumnType("date");

                entity.Property(e => e.Creater)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.LastEdit).HasColumnType("date");

                entity.Property(e => e.LastEditor)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Published).HasColumnType("date");

                entity.Property(e => e.Text).IsRequired();
            });

            modelBuilder.Entity<PostCategory>(entity =>
            {
                entity.HasKey(e => new { e.PostId, e.CategoryId })
                    .HasName("PK_PostCategory");

                entity.ToTable("Post_Category");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.PostCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostCategory_Category");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostCategories)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostCategory_Post");
            });

            modelBuilder.Entity<PostTag>(entity =>
            {
                entity.HasKey(e => new { e.PostId, e.TagId })
                    .HasName("PK_PostTag");

                entity.ToTable("Post_Tag");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostTags)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostTag_Post");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.PostTags)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostTag_Tag");
            });

            modelBuilder.Entity<PostUser>(entity =>
            {
                entity.HasKey(e => new { e.PostId, e.UserId })
                    .HasName("PK_PostUser");

                entity.ToTable("Post_User");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostUsers)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostUser_Post");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PostUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostUser_User");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("Tag");

                entity.Property(e => e.Created).HasColumnType("date");

                entity.Property(e => e.Creater)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.LastEdit).HasColumnType("date");

                entity.Property(e => e.LastEditor)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Created).HasColumnType("date");

                entity.Property(e => e.Creater)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.LastEdit).HasColumnType("date");

                entity.Property(e => e.LastEditor)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<UserGroup>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.GroupId });

                entity.ToTable("User_Group");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.UserGroups)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Group_User_Group");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserGroups)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_User_Group");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
