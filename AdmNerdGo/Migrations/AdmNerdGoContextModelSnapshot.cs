﻿// <auto-generated />
using System;
using AdmNerdGo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AdmNerdGo.Migrations
{
    [DbContext(typeof(AdmNerdGoContext))]
    partial class AdmNerdGoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AdmNerdGo.Models.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CategoriaPaiId");

                    b.Property<string>("Descricao");

                    b.Property<string>("Slug");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaPaiId");

                    b.ToTable("Categoria");
                });

            modelBuilder.Entity("AdmNerdGo.Models.Compare", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Link");

                    b.Property<int>("LojaId");

                    b.Property<string>("Parcelamento");

                    b.Property<double>("Preco");

                    b.Property<int>("ProdutoId");

                    b.HasKey("Id");

                    b.HasIndex("LojaId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("Compare");
                });

            modelBuilder.Entity("AdmNerdGo.Models.Loja", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Logo");

                    b.Property<string>("Nome");

                    b.HasKey("Id");

                    b.ToTable("Loja");
                });

            modelBuilder.Entity("AdmNerdGo.Models.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoriaId");

                    b.Property<string>("Descricao");

                    b.Property<bool>("Destaque");

                    b.Property<byte[]>("Imagem");

                    b.Property<double>("Preco");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.ToTable("Produto");
                });

            modelBuilder.Entity("AdmNerdGo.Models.Categoria", b =>
                {
                    b.HasOne("AdmNerdGo.Models.Categoria", "CategoriaPai")
                        .WithMany()
                        .HasForeignKey("CategoriaPaiId");
                });

            modelBuilder.Entity("AdmNerdGo.Models.Compare", b =>
                {
                    b.HasOne("AdmNerdGo.Models.Loja", "Loja")
                        .WithMany()
                        .HasForeignKey("LojaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AdmNerdGo.Models.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AdmNerdGo.Models.Produto", b =>
                {
                    b.HasOne("AdmNerdGo.Models.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
