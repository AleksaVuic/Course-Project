namespace Finalni_Test.Migrations
{
    using Finalni_Test.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Finalni_Test.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Finalni_Test.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //



            context.Jedinice.AddOrUpdate(
                new OrganizacionaJedinica() { Id = 1, Ime = "Administracija", GodinaOsnivanja = 2010}, // PR PLATA: 2000
                new OrganizacionaJedinica() { Id = 2, Ime = "Racunovodstvo", GodinaOsnivanja = 2012 }, // PR PLATA: 2000
                new OrganizacionaJedinica() { Id = 3, Ime = "Razvoj", GodinaOsnivanja = 2013 } // PR PLATA: 
                );
            context.SaveChanges();



            context.Zaposleni.AddOrUpdate(
                new Zaposlen() { Id = 1, ImeIPrezime = "Pera Peric", Rola = "Direktor", GodinaRodjenja = 1980, GodinaZaposlenja = 2010, Plata = 3000, JedinicaId = 1},
                new Zaposlen() { Id = 2, ImeIPrezime = "Mika Mikic", Rola = "Sekretar", GodinaRodjenja = 1985, GodinaZaposlenja = 2011, Plata = 1000, JedinicaId = 1 },
                new Zaposlen() { Id = 3, ImeIPrezime = "Iva Ivic", Rola = "Racunovodja", GodinaRodjenja = 1981, GodinaZaposlenja = 2012, Plata = 2000, JedinicaId = 2 },
                new Zaposlen() { Id = 4, ImeIPrezime = "Zika Zikic", Rola = "Inzinjer", GodinaRodjenja = 1982, GodinaZaposlenja = 2013, Plata = 2500, JedinicaId = 3 },
                new Zaposlen() { Id = 5, ImeIPrezime = "Ana Anic", Rola = "Inzinjer", GodinaRodjenja = 1984, GodinaZaposlenja = 2014, Plata = 2500, JedinicaId = 3 },
                new Zaposlen() { Id = 6, ImeIPrezime = "Jelena Jelic", Rola = "Inzjiner", GodinaRodjenja = 1987, GodinaZaposlenja = 2016, Plata = 2300, JedinicaId = 3 }
                );
            context.SaveChanges();
            
        }
    }
}
