﻿using System;
using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.ConsoleClient.Core.Commands.TicketCommands;
using ATPTennisStat.SQLServerData;
using ATPTennisStat.Factories.Contracts;
using ATPTennisStat.ConsoleClient.Core.Commands.MenuCommands;
using ATPTennisStat.ConsoleClient.Core.Commands.ReporterCommands;
using ATPTennisStat.ConsoleClient.Core.Commands.DataCommands.DataShowCommands;
using ATPTennisStat.PostgreSqlData;
using ATPTennisStat.ConsoleClient.Core.Commands.DataCommands.DataDeleteCommands;
using ATPTennisStat.ConsoleClient.Core.Commands.DataCommands.DataUdateCommands;
using ATPTennisStat.ReportGenerators.Contracts;
using ATPTennisStat.ConsoleClient.Core.Commands.ImportCommands;
using ATPTennisStat.Importers.Contracts;
using ATPTennisStat.SQLiteData;

namespace ATPTennisStat.ConsoleClient.Core.Factories
{
    class CommandFactory : ICommandFactory
    {
        private const string InvalidCommand = "Invalid command!";

        private readonly IPostgresDataProvider pgDp;
        private readonly ISqlServerDataProvider sqlDp;
        private readonly ISqliteDataProvider sqliteDp;
        private IReader reader;
        private IWriter writer;
        private ILogger logger;
        private IReportGenerator reporter;
        private IModelsFactory modelsFactory;
        private ITicketModelsFactory ticketFactory;
        private IExcelImporter excelImporter;

        public CommandFactory(IReportGenerator reporter,
                              ILogger logger,
                              IReader reader,
                              IWriter writer,
                              IPostgresDataProvider pgDp,
                              ISqlServerDataProvider sqlDp,
                              ISqliteDataProvider sqliteDp,
                              IModelsFactory modelsFactory,
                              ITicketModelsFactory ticketFactory,
                              IExcelImporter excelImporter)
        {
            this.pgDp = pgDp;
            this.sqlDp = sqlDp;
            this.sqliteDp = sqliteDp;
            this.reader = reader;
            this.writer = writer;
            this.logger = logger;
            this.reporter = reporter;
            this.modelsFactory = modelsFactory;
            this.ticketFactory = ticketFactory;
            this.excelImporter = excelImporter;
        }

        public ICommand CreateCommandFromString(string commandName)
        {
            switch (commandName.ToLower())
            {
                // main menu
                case "menu":
                    return this.MainMenuCommand();
                case "r":
                    return this.ReportersMenuCommand();
                case "i":
                    return this.ImportMenuCommand();
                case "s":
                    return this.TennisDataMenuCommand();
                case "t":
                    return this.TicketMenuCommand();
                case "l":
                    return this.ShowLogs();
                case "ld":
                    return this.ShowDetailedLogs();
                case "a":
                    return this.TeamInfoCommand();
                // tickets commands
                case "alle":
                    return this.ShowEventsCommand();
                case "allt":
                    return this.ShowTicketsCommand();
                case "buyt":
                    return this.BuyTicketsCommand();
                case "importtk":
                    return this.ImportTicketsListCommand();
                // reporter commands
                case "pdfm":
                    return this.CreateMatchesPdf();
                case "pdfr":
                    return this.CreateRankingPdf();
                // data menu
                case "show":
                    return this.ShowTennisDataMenuCommand();
                case "add":
                    return this.AddTennisDataMenuCommand();
                // data show
                case "showp":
                    return this.ShowPlayers();
                case "showt":
                    return this.ShowTournaments();
                case "showm":
                    return this.ShowMatches();
                // data add
                case "addco":
                    return this.AddCountry();
                case "addct":
                    return this.AddCity();
                case "addp":
                    return this.AddPlayer();
                case "addt":
                    return this.AddTournament();
                case "addm":
                    return this.AddMatch();
                // data update
                case "updatep":
                    return this.UpdatePlayer();
                // data delete
                case "delm":
                    return this.DeleteMatch();
                // import
                case "importsd":
                    return this.ImportSampleData();
                case "importp":
                    return this.ImportPlayers();
                case "importt":
                    return this.ImportTournaments();
                case "importm":
                    return this.ImportMatches();
                case "importpd":
                    return this.ImportPointDistributions();

                default:
                    //throw new ArgumentException(nameof(ICommand)); The bellow way is more informative!
                    throw new ArgumentException("InvalidCommand");
            }
        }

        // Menu commands
        public ICommand MainMenuCommand()
        {
            return new MainMenuCommand(writer);
        }

        public ICommand ReportersMenuCommand()
        {
            return new ReportersMenuCommand(writer);
        }

        public ICommand ImportMenuCommand()
        {
            return new ImportMenuCommand(writer);
        }

        public ICommand TicketMenuCommand()
        {
            return new TicketMenuCommand(writer);
        }

        public ICommand TeamInfoCommand()
        {
            return new TeamInfoCommand(writer);
        }

        // Reporters commands
        public ICommand CreateMatchesPdf()
        {
            return new CreateMatchesPdf(this.reporter, this.logger);
        }

        public ICommand CreateRankingPdf()
        {
            return new CreateRankingPdf(this.reporter, this.logger);
        }

        // Ticket store commands
        public ICommand ShowTicketsCommand()
        {
            return new ShowTicketsCommand(pgDp, writer);
        }

        public ICommand ShowEventsCommand()
        {
            return new ShowEventsCommand(pgDp, writer);
        }

        public ICommand BuyTicketsCommand()
        {
            return new BuyTicketsCommand(pgDp, writer);
        }

        public ICommand ImportTicketsListCommand()
        {
            return new ImportTicketsListCommand(pgDp, writer, ticketFactory);
        }

        // Tennis Data menu commands
        public ICommand TennisDataMenuCommand()
        {
            return new TennisDataMenuCommand(writer);
        }

        public ICommand ShowTennisDataMenuCommand()
        {
            return new ShowTennisDataMenuCommand(writer);
        }

        public ICommand AddTennisDataMenuCommand()
        {
            return new AddTennisDataMenuCommand(writer);
        }

        // Data Add Commands
        public ICommand AddCountry()
        {
            return new AddCountryCommand(sqlDp, writer);
        }

        public ICommand AddCity()
        {
            return new AddCityCommand(sqlDp, writer);
        }

        public ICommand AddPlayer()
        {
            return new AddPlayerCommand(sqlDp, writer, modelsFactory);
        }

        public ICommand AddTournament()
        {
            return new AddTournamentCommand(sqlDp, writer, modelsFactory);
        }

        public ICommand AddMatch()
        {
            return new AddMatchCommand(sqlDp, writer, modelsFactory);
        }

        // Data Show Commands
        public ICommand ShowTournaments()
        {
            return new ShowTournamentsCommand(sqlDp, writer);
        }

        public ICommand ShowMatches()
        {
            return new ShowMatchesCommand(sqlDp, writer);
        }

        public ICommand ShowLogs()
        {
            return new ShowLogsCommand(sqliteDp, writer);
        }

        public ICommand ShowDetailedLogs()
        {
            return new ShowDetailedLogsCommand(sqliteDp, writer);
        }

        public ICommand ShowPlayers()
        {
            return new ShowPlayersCommand(sqlDp, writer);
        }

        // Data Update Commands
        public ICommand UpdatePlayer()
        {
            return new UpdatePlayerCommand(sqlDp, writer);
        }

        // Data Delete Commands
        public ICommand DeleteMatch()
        {
            return new DeleteMatchCommand(sqlDp, reader, writer);
        }

        // Import Commands
        public ICommand ImportSampleData()
        {
            return new ImportSampleDataCommand(sqlDp, modelsFactory, excelImporter, writer, logger, this);
        }

        public ICommand ImportPlayers()
        {
            return new ImportPlayersCommand(sqlDp, modelsFactory, excelImporter, writer, logger);
        }

        public ICommand ImportTournaments()
        {
            return new ImportTournamentsCommand(sqlDp, modelsFactory, excelImporter, writer, logger);
        }

        public ICommand ImportMatches()
        {
            return new ImportMatchesCommand(sqlDp, modelsFactory, excelImporter, writer, logger);
        }

        public ICommand ImportPointDistributions()
        {
            return new ImportPointDistributionsCommand(sqlDp, modelsFactory, excelImporter, writer, logger);
        }
    }
}
