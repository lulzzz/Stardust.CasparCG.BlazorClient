﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Stardust.Flux.ScheduleEngine.Models;

namespace Stardust.Flux.ScheduleEngine.Migrations
{
    [DbContext(typeof(ScheduleContext))]
    [Migration("20210216213723_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Stardust.Flux.ScheduleEngine.Models.RecordJob", b =>
                {
                    b.Property<string>("RecordJobId")
                        .HasColumnType("text");

                    b.Property<string>("CronExpression")
                        .HasColumnType("text");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("interval");

                    b.Property<string>("Filename")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsRecording")
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

                    b.Property<int>("RecordSlotId")
                        .HasColumnType("integer");

                    b.Property<string>("RecordType")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ScheduleAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("StartRecordJobId")
                        .HasColumnType("text");

                    b.Property<string>("StopRecordJobId")
                        .HasColumnType("text");

                    b.HasKey("RecordJobId");

                    b.ToTable("RecordJobs");
                });
#pragma warning restore 612, 618
        }
    }
}