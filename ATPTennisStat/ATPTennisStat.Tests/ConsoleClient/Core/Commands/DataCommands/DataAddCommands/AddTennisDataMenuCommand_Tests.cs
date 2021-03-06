﻿using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.ConsoleClient.Core.Commands.MenuCommands;

namespace ATPTennisStat.Tests.ConsoleClient.Core.Commands.DataCommands.DataAddCommands
{
    [TestFixture]
    class AddTennisDataMenuCommand_Tests
    {
        [Test]
        public void ConstructorShould_ReturnInstanceOfAddCountryCommandClass_WhenThePassedValuesAreValid()
        {
            var writerMock = new Mock<IWriter>();
            var command = new AddTennisDataMenuCommand(writerMock.Object);

            Assert.IsInstanceOf<AddTennisDataMenuCommand>(command);
        }

        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenThePassedWriterIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new AddTennisDataMenuCommand(null));
        }

        [Test]
        public void ExecuteShould_ClearTheScreenOneTIme()
        {
            var writerMock = new Mock<IWriter>();
            var command = new AddTennisDataMenuCommand(writerMock.Object);

            command.Execute(new List<string>());

            writerMock.Verify(x => x.Clear(), Times.Once);
        }
    }
}
