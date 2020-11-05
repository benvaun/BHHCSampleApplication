using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SampleAPI.Controllers;
using SampleAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NUnitTestSampleAPI
{
    class TestReasonToHireController
    {
        private ReasonToHireContext _reasonToHireContext;
        [SetUp]
        public void Setup()
        {
            //create 5 reasons and insert them into the database for testing purposes
            var builder = new DbContextOptionsBuilder<ReasonToHireContext>().UseInMemoryDatabase("ReasonToHireDatabase");
            var context = new ReasonToHireContext(builder.Options);
            var reasons = Enumerable.Range(1, 5).Select(i => new ReasonToHire { Id = i, Name = $"Reason {i}", Text = $"This is reason #{i}." });
            context.ReasonsToHire.AddRange(reasons);
            int changed = context.SaveChanges();
            _reasonToHireContext = context;
        }
        [Test]
        public async Task TestGetAll()
        {
            var controller = new ReasonToHireController(_reasonToHireContext);
            ActionResult<IEnumerable<ReasonToHire>> result = await controller.GetReasonsToHire();
            IEnumerable<ReasonToHire> reasons = result.Value;
            Assert.IsNotNull(reasons);
            Assert.IsTrue(reasons.Count() > 0);
            //ensure that the reasons are deleted, otherwise we'll end up trying to insert more in the next test run
            _reasonToHireContext.Database.EnsureDeleted();
        }
        [Test]
        public async Task TestGetOne()
        {
            var controller = new ReasonToHireController(_reasonToHireContext);
            ActionResult<ReasonToHire> result = await controller.GetReasonToHire(1);
            ReasonToHire reason = result.Value;
            Assert.IsNotNull(reason);
            Assert.IsTrue(reason.Id == 1);
            //ensure that the reasons are deleted, otherwise we'll end up trying to insert more in the next test run
            _reasonToHireContext.Database.EnsureDeleted();
        }
    }
}