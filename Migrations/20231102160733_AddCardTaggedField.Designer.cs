﻿// <auto-generated />
using System;
using Flashcards;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Flashcards.Migrations
{
    [DbContext(typeof(FlashcardsContext))]
    [Migration("20231102160733_AddCardTaggedField")]
    partial class AddCardTaggedField
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("Flashcards.Card", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Back")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreationTimestamp")
                        .HasColumnType("TEXT");

                    b.Property<int>("DeckId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Front")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Tagged")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DeckId");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("Flashcards.Deck", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreationTimestamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Decks");
                });

            modelBuilder.Entity("Flashcards.Card", b =>
                {
                    b.HasOne("Flashcards.Deck", "Deck")
                        .WithMany("Cards")
                        .HasForeignKey("DeckId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Deck");
                });

            modelBuilder.Entity("Flashcards.Deck", b =>
                {
                    b.Navigation("Cards");
                });
#pragma warning restore 612, 618
        }
    }
}
