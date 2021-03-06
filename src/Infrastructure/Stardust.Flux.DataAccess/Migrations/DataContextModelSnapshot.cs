// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Stardust.Flux.DataAccess;

namespace Stardust.Flux.DataAccess.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Stardust.Flux.DataAccess.Models.Event", b =>
                {
                    b.Property<string>("EventId")
                        .HasColumnType("text");

                    b.Property<string>("CronExpression")
                        .HasColumnType("text");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("interval");

                    b.Property<string>("EventType")
                        .HasColumnType("text");

                    b.Property<string>("ExtraParams")
                        .HasColumnType("text");

                    b.Property<bool>("IsStarted")
                        .HasColumnType("boolean");

                    b.Property<string>("LastError")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastExecution")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("NextExecution")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ParamType")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ScheduleAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("StartRecordJobId")
                        .HasColumnType("text");

                    b.Property<string>("StopRecordJobId")
                        .HasColumnType("text");

                    b.HasKey("EventId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Stardust.Flux.DataAccess.Models.YoutubeAccount", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("text");

                    b.Property<string>("AccessToken")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long?>("ExpiresInSeconds")
                        .HasColumnType("bigint");

                    b.Property<string>("IdToken")
                        .HasColumnType("text");

                    b.Property<DateTime?>("IssuedUtc")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<string>("Scope")
                        .HasColumnType("text");

                    b.Property<string>("TokenType")
                        .HasColumnType("text");

                    b.HasKey("Key");

                    b.ToTable("YoutubeAccounts");
                });

            modelBuilder.Entity("Stardust.Flux.DataAccess.Models.YoutubeUpload", b =>
                {
                    b.Property<string>("YoutubeUploadId")
                        .HasColumnType("text");

                    b.Property<long>("BytesSent")
                        .HasColumnType("bigint");

                    b.Property<string>("CategoryId")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Error")
                        .HasColumnType("text");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PrivacyStatus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("State")
                        .HasColumnType("text");

                    b.Property<string>("Tags")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("YoutubeAccountId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("YoutubeVideoId")
                        .HasColumnType("text");

                    b.HasKey("YoutubeUploadId");

                    b.HasIndex("YoutubeAccountId");

                    b.ToTable("YoutubeUploads");
                });

            modelBuilder.Entity("Stardust.Flux.DataAccess.Models.YoutubeUpload", b =>
                {
                    b.HasOne("Stardust.Flux.DataAccess.Models.YoutubeAccount", "YoutubeAccount")
                        .WithMany("YoutubeUploads")
                        .HasForeignKey("YoutubeAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("YoutubeAccount");
                });

            modelBuilder.Entity("Stardust.Flux.DataAccess.Models.YoutubeAccount", b =>
                {
                    b.Navigation("YoutubeUploads");
                });
#pragma warning restore 612, 618
        }
    }
}
