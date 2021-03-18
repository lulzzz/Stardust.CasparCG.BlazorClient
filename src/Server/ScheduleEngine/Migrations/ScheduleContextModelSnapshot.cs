﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Stardust.Flux.ScheduleEngine.Models;

namespace Stardust.Flux.ScheduleEngine.Migrations
{
    [DbContext(typeof(ScheduleContext))]
    partial class ScheduleContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Stardust.Flux.ScheduleEngine.Models.Event", b =>
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
#pragma warning restore 612, 618
        }
    }
}
