using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using GotoHealth10.Models;

namespace GotoHealth10.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20160519164636_MyFirstMigration")]
    partial class MyFirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("GotoHealth10.Models.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Age");

                    b.Property<string>("Email");

                    b.Property<string>("Gender");

                    b.Property<string>("Height");

                    b.Property<string>("InitialWeigth");

                    b.Property<string>("Logged");

                    b.Property<string>("NickName");

                    b.Property<string>("TargetWeight");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("GotoHealth10.Models.WeighingModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Difference");

                    b.Property<string>("IMC");

                    b.Property<string>("UpDown");

                    b.Property<string>("Weight");

                    b.HasKey("Id");
                });
        }
    }
}
