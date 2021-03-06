﻿using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.ConsoleClient.Core.Utilities;
using System;
using System.Collections.Generic;

namespace ATPTennisStat.ConsoleClient.Core.Commands.MenuCommands
{
    public class TicketMenuCommand : ICommand
    {
        private IWriter writer;

        public TicketMenuCommand(IWriter writer)
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
                Messages.GenerateTicketMenu();
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
