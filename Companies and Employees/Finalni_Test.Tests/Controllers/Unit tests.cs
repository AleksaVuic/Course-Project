﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Finalni_Test.Controllers;
using Moq;
using Finalni_Test.Interfaces;
using Finalni_Test.Models;
using System.Web.Http.Results;
using System.Web.Http;
using System.Collections.Generic;
using System.Linq;

namespace Finalni_Test.Tests.Controllers
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetReturnsOkAndObject()
        {
            var mockRepository = new Mock<IZaposlenRepository>();
            mockRepository.Setup(x => x.GetById(25)).Returns(new Zaposlen { Id = 25 });
            var controller = new ZaposleniController(mockRepository.Object);

            IHttpActionResult actionResult = controller.Get(25);
            var contentResult = actionResult as OkNegotiatedContentResult<Zaposlen>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(25, contentResult.Content.Id);
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<Zaposlen>));

        }

        [TestMethod]
        public void PutReturnsBadRequest()
        {
            var mockRepository = new Mock<IZaposlenRepository>();
            var controller = new ZaposleniController(mockRepository.Object);

            IHttpActionResult actionResult = controller.Put(25, new Zaposlen { Id = 35, ImeIPrezime = "Zaposlen35" });

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }


        [TestMethod]
        public void GetReturnsMultipleObjectsSorted()
        {
            List<Zaposlen> zaposleni = new List<Zaposlen>();
            zaposleni.Add(new Zaposlen { Id = 1, ImeIPrezime = "Zaposlen1", GodinaZaposlenja = 2015 });
            zaposleni.Add(new Zaposlen { Id = 2, ImeIPrezime = "Zaposlen2", GodinaZaposlenja = 2018 });
            

            var mockRepository = new Mock<IZaposlenRepository>();
            mockRepository.Setup(x => x.GetAll()).Returns(zaposleni.AsQueryable());
            var controller = new ZaposleniController(mockRepository.Object);

            IQueryable<Zaposlen> rezultat = controller.Get();

            Assert.IsNotNull(rezultat);
            Assert.AreEqual(zaposleni.Count, rezultat.ToList().Count);
            Assert.AreEqual(zaposleni.ElementAt(0), rezultat.ElementAt(0));
            Assert.AreEqual(zaposleni.ElementAt(1), rezultat.ElementAt(1));
        }

        [TestMethod]
        public void PostReturnsMultipleObjectsSorted()
        {
            List<Zaposlen> zaposleni = new List<Zaposlen>();
            zaposleni.Add(new Zaposlen { Id = 1, ImeIPrezime = "Zaposlen1", Plata = 2000m });
            zaposleni.Add(new Zaposlen { Id = 2, ImeIPrezime = "Zaposlen2", Plata = 3000m });
            zaposleni.Add(new Zaposlen { Id = 3, ImeIPrezime = "Zaposlen3", Plata = 1500m });
            zaposleni.Add(new Zaposlen { Id = 4, ImeIPrezime = "Zaposlen4", Plata = 2500m });
            GranicaPlate filter = new GranicaPlate() { Najmanje = 1800m, Najvise = 2600m };

            var mockRepository = new Mock<IZaposlenRepository>();
            mockRepository.Setup(x => x.GetAllSaPlatomIzmedju(filter.Najmanje, filter.Najvise)).Returns(zaposleni.AsQueryable());
            var controller = new ZaposleniController(mockRepository.Object);

            IQueryable<Zaposlen> rezultat = controller.PostPretraga(filter);

            Assert.IsNotNull(rezultat);
            Assert.AreEqual(zaposleni.Count, rezultat.ToList().Count);
            Assert.AreEqual(zaposleni.ElementAt(0), rezultat.ElementAt(0));
            Assert.AreEqual(zaposleni.ElementAt(1), rezultat.ElementAt(1));
        }


    }
}
