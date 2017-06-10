﻿using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.SQLServerData;
using System;
using System.Collections.Generic;

namespace ATPTennisStat.ConsoleClient.Core.Commands.DataCommands.DataShowCommands
{
    class AddCountryCommand : ICommand
    {
        private SqlServerDataProvider dp;
        private IWriter writer;

        public AddCountryCommand(SqlServerDataProvider sqlDP, IWriter writer)
        {
            this.dp = sqlDP;
            this.writer = writer;
        }

        public string Execute(IList<string> parameters)
        {
            throw new NotImplementedException();
        }
    }
}