using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations.Infrastructure;
using SPARK.Model;

namespace SPARKMigrations
{
    [ContextType(typeof(SPARKDbContext))]
    partial class SPARKDbContextModelSnapshot : ModelSnapshot
    {
        public override void BuildModel(ModelBuilder builder)
        {
            builder
                .Annotation("ProductVersion", "7.0.0-beta6-13815");

            builder.Entity("SPARK.Model.Owner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("Surname");

                    b.Property<string>("Username");

                    b.Key("Id");
                });

            builder.Entity("SPARK.Model.Parking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<int>("Capacity");

                    b.Property<double>("CoordX");

                    b.Property<double>("CoordY");

                    b.Property<int>("MinCredits");

                    b.Property<double>("MonthlyProfit");

                    b.Property<string>("Name");

                    b.Property<int>("NumTakenSpaces");

                    b.Property<double>("Price");

                    b.Property<double>("TodaysProfit");

                    b.Property<int>("Zone");

                    b.Key("Id");
                });

            builder.Entity("SPARK.Model.PaymentMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Key("Id");
                });

            builder.Entity("SPARK.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("Surname");

                    b.Property<string>("Username");

                    b.Key("Id");
                });
        }
    }
}
