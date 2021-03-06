﻿using System;
using ATPTennisStat.Common;
using ATPTennisStat.SQLServerData;
using ATPTennisStat.ReportGenerators.Contracts;

namespace ATPTennisStat.ConsoleClient.Core.Contracts
{
    public interface ICommandFactory
    {
        ICommand CreateCommandFromString(string commandName);

        ICommand MainMenuCommand();

        ICommand ReportersMenuCommand();

        ICommand ImportMenuCommand();

        ICommand TicketMenuCommand();

        ICommand TeamInfoCommand();

        // Ticket data commands
        ICommand ShowEventsCommand();

        ICommand ShowTicketsCommand();

        ICommand BuyTicketsCommand();

        ICommand ImportTicketsListCommand();

        // Reporter commands

        ICommand CreateMatchesPdf();

        ICommand CreateRankingPdf();

        // Data menu commanda

        ICommand TennisDataMenuCommand();

        ICommand ShowTennisDataMenuCommand();

        ICommand AddTennisDataMenuCommand();

        // Create data commands 
        ICommand AddCountry();

        ICommand AddCity();

        ICommand AddPlayer();

        ICommand AddTournament();

        ICommand AddMatch();

        // Update data commands
        ICommand UpdatePlayer();

        // Delete data commands
        ICommand DeleteMatch();

        // Read data commands
        ICommand ShowTournaments();

        ICommand ShowMatches();

        ICommand ShowPlayers();

        // Log Commands

        ICommand ShowLogs();

        ICommand ShowDetailedLogs();

        //Import commands
        ICommand ImportSampleData();

        ICommand ImportPlayers();

        ICommand ImportTournaments();

        ICommand ImportMatches();

        ICommand ImportPointDistributions();

    }
}