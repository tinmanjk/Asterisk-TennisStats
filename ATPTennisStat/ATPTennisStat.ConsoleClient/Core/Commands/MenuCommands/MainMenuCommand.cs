﻿using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.ConsoleClient.Core.Utilities;
using System;
using System.Collections.Generic;

namespace ATPTennisStat.ConsoleClient.Core.Commands.MenuCommands
{
    public class MainMenuCommand : ICommand
    {
        private IWriter writer;

        public MainMenuCommand(IWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("Writer cannot be null!");
            }

            this.writer = writer;
        }

        public string Execute()
        {
            this.writer.Clear();
            return
                Messages.GenerateWelcomeMessage() +
                Messages.GenerateMainMenu();
        }
        public string Execute(IList<string> parameters)
        {
            if (parameters.Count == 0)
            {
                return this.Execute();
            }
            else
            {
                throw new ArgumentException(Messages.ParametersWarning);
            }
        }
    }
}
