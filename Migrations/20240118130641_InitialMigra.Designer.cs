﻿// <auto-generated />
using System;
using FerreteriaJuanito.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FerreteriaJuanito.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240118130641_InitialMigra")]
    partial class InitialMigra
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.1");

            modelBuilder.Entity("FerreteriaJuanito.Clases.CarroCompra", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Cantidad")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdDespacho")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdMedioPago")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdProducto")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Total")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("CarroCompras");
                });

            modelBuilder.Entity("FerreteriaJuanito.Clases.Despacho", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DescDespacho")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Despachos");
                });

            modelBuilder.Entity("FerreteriaJuanito.Clases.Flujo.Login", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Emaillogin")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordLogin")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Logins");
                });

            modelBuilder.Entity("FerreteriaJuanito.Clases.MedioPago", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DescMedioPago")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MedioPagos");
                });

            modelBuilder.Entity("FerreteriaJuanito.Clases.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CantidadProducto")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DescProducto")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("IdProveedor")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ValorProducto")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("FerreteriaJuanito.Clases.Proveedor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DireccionProveedor")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NombreProveedor")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TelefonoProveedor")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Proveedors");
                });

            modelBuilder.Entity("FerreteriaJuanito.Clases.TipoUsuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DescTipoUsuario")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TipoUsuarios");
                });

            modelBuilder.Entity("FerreteriaJuanito.Clases.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DireccionUsuario")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("IdTipoUsuario")
                        .HasColumnType("INTEGER");

                    b.Property<string>("NombreUsuario")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TelefonoUsuario")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("FerreteriaJuanito.Clases.Utils.Logs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Clase")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaLog")
                        .HasColumnType("TEXT");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LogDescription")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LogName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Logss");
                });
#pragma warning restore 612, 618
        }
    }
}
