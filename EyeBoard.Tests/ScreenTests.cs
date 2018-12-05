using EyeBoard.Logic.Events;
using EyeBoard.Logic.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Profilan.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EyeBoard.Tests
{
    [TestClass]
    public class ScreenTests
    {
        private Guid testGroupId = Guid.NewGuid();
        private Presentation testPresentation1;
        private Presentation testPresentation2;

        public ScreenTests()
        {
            DomainEvents.ClearCallbacks();

            testPresentation1 = Presentation.Create("testPresentation1", null, null, null, "media/medium1.mp4");
            testPresentation2 = Presentation.Create("testPresentation2", null, null, null, "media/medium2.mp4");
        }

        [TestMethod]
        public void AddPresentationToGroupListOfMedia()
        {
            var group = GetTestGroup();
            group.Media.Count().Should().Be(0);

            group.AddNewPresentation(testPresentation1);
            testPresentation1.Should().BeEquivalentTo(group.Media.First());
        }

        [TestMethod]
        public void ThrowExceptionIfSameExactPresentationIsAddedTwice()
        {
            var group = GetTestGroup();

            group.AddNewPresentation(testPresentation1);

            Action action = () => group.AddNewPresentation(testPresentation1);

            action.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void RaisePresentationAddedEvent()
        {
            // Arrange
            Guid addedPresentationId = Guid.Empty;
            DomainEvents.Register<PresentationAddedEvent>(ase => addedPresentationId = testPresentation1.Id);

            // Act
            var group = GetTestGroup();
            group.AddNewPresentation(testPresentation1);

            // Assert
            testPresentation1.Id.Should().Be(addedPresentationId);
        }

        private ScreenGroup GetTestGroup()
        {


            return ScreenGroup.Create("testGroup1");
        }
    }
}
