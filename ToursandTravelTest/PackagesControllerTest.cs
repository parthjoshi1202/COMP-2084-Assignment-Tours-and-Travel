using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToursandTravel.Data;
using ToursandTravel.Controllers;
using ToursandTravel.Models;
using Microsoft.AspNetCore.Mvc;

namespace ToursandTravelTest
{
    [TestClass]
    public class PackagesControllerTest
    {
        //db reference 
        private ApplicationDbContext _context;

        //empty packages list for holding mock data
        List<Package> packages =new List<Package>();

        PackagesController controller;

        [TestInitialize]
        public void TestInitialize() {
            //instantiating db
            var options= new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

            _context =new ApplicationDbContext(options);

            //creating mock data
            var packageType =new PackageType { Id=108, Name="Test Special" };

            packages.Add(new Package { Id=15, Name="Test 4", Price=150, PackageType=packageType });
                        
            packages.Add(new Package { Id = 51, Name = "Test 72", Price = 500, PackageType = packageType });
            
            packages.Add(new Package { Id = 79, Name = "Test 99", Price = 900, PackageType = packageType });

            foreach(var p in packages)
            {
                _context.Packages.Add(p);
            }

            _context.SaveChanges();

            //calling constructor and pass in database object
            controller = new PackagesController(_context);
        }

        //Test methods for Index method

        [TestMethod]
        public void IndexViewLoads()
        {
            //act
            var result =controller.Index();
            
            var viewResult =(ViewResult)result.Result;
            
            //assert
            Assert.AreEqual("Index",viewResult.ViewName);
        }

        [TestMethod]
        public void IndexReturnsPackageData()
        {
            //act
            var result = controller.Index();
            
            var viewResult = (ViewResult)result.Result;
            
            //casting the result data model to list of packages to check it
            List<Package> model =(List<Package>)viewResult.Model;
            
            //assert
            CollectionAssert.AreEqual(packages.ToList(),model);
        }

        // Test Methods for Details 

        [TestMethod]
        public void DetailsNullId()
        {
            var result = controller.Details(null);

            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Error", viewResult.ViewName);

        }

        [TestMethod]
        public void DetailsInvalidId()
        {
            var result = controller.Details(9);

            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Error", viewResult.ViewName);

        }

        [TestMethod]
        public void DetailsValidId()
        {
            var result = controller.Details(51);

            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Details", viewResult.ViewName);

        }

        [TestMethod]
        public void DetailsValidLoadsPackage()
        {
            var result = controller.Details(15);
            
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Details", viewResult.ViewName);
        }

        [TestMethod]
        public void DetailsInvalidLoadsPackage()
        {
            var result = controller.Details(48);

            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Error", viewResult.ViewName);
        }

        //Test Methods for Create: GET

        [TestMethod]
        public void GetCreateViewLoads()
        {
            var result = controller.Create();

            var viewResult = (ViewResult)result;

            Assert.AreEqual("Create", viewResult.ViewName);
        }

        [TestMethod]
        public void GetCreateReturnsValidList()
        {
            var result = controller.Create();

            var viewData = controller.ViewData["TypeId"];

            Assert.IsNotNull(viewData);
        }

        //Test Methods for Create: POST

        [TestMethod]
        public void PostCreateReturnsIndex()
        {
            var packageType = new PackageType { Id = 12, Name = "Test Special 1" };

            var package = new Package { Id = 7, Name = "Some Test", Price = 89, PackageType = packageType };

            var result = controller.Create(package, null);

            var redirectResult = (RedirectToActionResult)result.Result;

            Assert.AreEqual("Index",redirectResult.ActionName);
        }

        [TestMethod]
        public void PostCreateDb()
        {
            var packageType = new PackageType { Id = 12, Name = "Test Special 1" };

            var package = new Package { Id = 7, Name = "Some Test", Price = 89, PackageType = packageType };

            _context.Packages.Add(package);

            _context.SaveChanges();

            Assert.AreEqual(package, _context.Packages.ToArray()[1]);
        }

        //Test methods for Edit: GET method

        [TestMethod]
        public void GetEditNullId() 
        {
            var result = controller.Edit(null);

            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Error", viewResult.ViewName);

        }

        [TestMethod]
        public void GetEditInvalidId()
        {
            var result = controller.Edit(8);

            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Error", viewResult.ViewName);

        }

        [TestMethod]
        public void GetEditValidId()
        {
            var result = controller.Edit(51);

            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Edit", viewResult.ViewName);

        }

        [TestMethod]
        public void GetEditLoadsCorrectModel()
        {
            var result = controller.Edit(15);
            
            var viewResult = (ViewResult)result.Result;
            
            Package model = (Package)viewResult.Model;

            Assert.AreEqual(_context.Packages.Find(15), model);
        }

        [TestMethod]
        public void GetEditLoadsViewData()
        {
            var result = controller.Edit(15);
            
            var viewResult = (ViewResult)result.Result;
            
            var viewData = viewResult.ViewData;

            Assert.AreEqual(viewData, viewResult.ViewData);
        }

        [TestMethod]
        public void GetEditLoadsErrorViewForInvalidModel()
        {
            var result = controller.Edit(48);
            
            var viewResult = (ViewResult)result.Result;
            
            Package model = (Package)viewResult.Model;

            Assert.AreNotEqual(_context.Packages.FindAsync(48), model);
        }



        //Test methods for Edit: POST method
        
        [TestMethod]
        public void PostEditReturnsId()
        {
            var result = controller.Edit(48,packages[0]);
            
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Error",viewResult.ViewName);
        }

        [TestMethod]
        public void PostEditSave()
        {
            var package = packages[1];
            
            package.Price = 77;
            
            var result = controller.Edit(package.Id, package);
            
            var redirectResult = (RedirectToActionResult)result.Result;

            Assert.AreEqual("Index", redirectResult.ActionName);
        }
    
        [TestMethod]
        public void PostEditDb()
        {
            var packageType = new PackageType { Id = 12, Name = "Test Special 1" };
            
            var checkPackage = new Package { Id=7, Name="Some Test", Price=89, PackageType=packageType  };
            
            var result = controller.Edit(7, checkPackage);
            
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Error", viewResult.ViewName);
        }


        //Test Methods for Delete: GET

        
        
        [TestMethod]
        public void GetDeleteNullId()
        {
            var result = controller.Delete(null);

            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Error", viewResult.ViewName);
        }

        [TestMethod]
        public void GetDeleteInvalidId()
        {
            var result = controller.Delete(9);

            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Error", viewResult.ViewName);
        }

        [TestMethod]
        public void GetDeleteValidId()
        {
            var result = controller.Delete(51);

            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Delete", viewResult.ViewName);
        }

        [TestMethod]
        public void GetDeleteInvalidIdLoadsPackage()
        {
            var result = controller.Delete(48);

            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Error", viewResult.ViewName);
        }

        [TestMethod]
        public void GetDeleteValidIdLoadsPackage()
        {
            var result = controller.Delete(51);

            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual(packages[1], viewResult.Model);
        }

        //Test Methods for Delete: POST

        [TestMethod]
        public void PostDeleteRedirect()
        {
            var result = controller.DeleteConfirmed(15);

            var redirectResult = (RedirectToActionResult)result.Result;

            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        [TestMethod]
        public void PostDeleteConfirmed()
        {
            var id = 7;

            var result = controller.DeleteConfirmed(id);

            var package =_context.Packages.Find(id);

            Assert.AreEqual(package,null);
        }

    }
}
