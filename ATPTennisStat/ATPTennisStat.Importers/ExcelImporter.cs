﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using System.IO;
using ATPTennisStat.Importers.Contracts;
using ATPTennisStat.SQLServerData;
using ATPTennisStat.Models;
using ATPTennisStat.Factories;
using System.Data;

namespace ATPTennisStat.Importers
{
    public class ExcelImporter : IImporter
    {
        private readonly string solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
        private string playersFilePath;
        private string matchesFilePath;

        private SqlServerDataProvider dataProvider;
        private ModelsFactory modelsFactory;

        public ExcelImporter(SqlServerDataProvider dataProvider, ModelsFactory modelsFactory)
        {
            this.dataProvider = dataProvider;
            this.modelsFactory = modelsFactory;


            this.playersFilePath = this.solutionDirectory + "\\Data\\Excel\\Players-Full-Data.xlsx";
            this.matchesFilePath = this.solutionDirectory + "\\Data\\Excel\\Matches-Full-Data.xlsx";
        }

        /// <summary>
        /// Expects a file with data in the first worksheet
        /// </summary>
        public IXLTableRange GenerateTableRangeFromFile(string filePath)
        {
            try
            {
                var workbook = new XLWorkbook(filePath);
                var ws = workbook.Worksheets.First();

                var dataRange = ws.RangeUsed().AsTable().DataRange;

                return dataRange;
            }
            catch (Exception ex)
            {

                //throw new ArgumentException("File opened by another program");
                Console.WriteLine("File opened by another program");
                return null;
            }
            

        }

        public void ImportMatches()
        {

            var dataRange = GenerateTableRangeFromFile(this.matchesFilePath);

            if (dataRange == null)
            {
                //another exception handling possible
                return;
            }

            var matches = dataRange.Rows()
                       .Select(row => new
                       {
                           DatePlayed = row.Field("DatePlayed").GetString().Trim(),
                           Winner = row.Field("Winner").GetString().Trim(),
                           Loser = row.Field("Loser").GetString().Trim(),
                           Result = row.Field("Result").GetString().Trim(),
                           TournamentName = row.Field("Tournament").GetString().Trim(),
                           Round = row.Field("Round").GetString().Trim()
                       })
                        .ToList();

            foreach (var m in matches)
            {
                try
                {
                    var newMatch = modelsFactory.CreateMatch(
                         m.DatePlayed,
                         m.Winner,
                         m.Loser,
                         m.Result,
                         m.TournamentName,
                         m.Round
                     );

                    this.dataProvider.Matches.Add(newMatch);

                }
                catch (ArgumentException ex)
                {

                    Console.WriteLine("Excel import problem: " + ex.Message);
                }

            }

            //this.dataProvider.UnitOfWork.Finished();

            var a = 3;
        }

        public void ImportPlayers()
        {
            var dataRange = GenerateTableRangeFromFile(this.playersFilePath);

            var players = dataRange.Rows()
                .Select(row => new
                {
                    FirstName = row.Field("FirstName").GetString().Trim(),
                    LastName = row.Field("LastName").GetString().Trim(),
                    Ranking = row.Field("Ranking").GetString().Trim(),
                    BirthDate = row.Field("BirthDate").GetString().Trim(),
                    Height = row.Field("Height").GetString().Trim(),
                    Weight = row.Field("Weight").GetString().Trim(),
                    City = row.Field("City").GetString().Trim(),
                    Country = row.Field("Country").GetString().Trim()

                })
                .ToList();


            foreach (var p in players)
            {
                try
                {
                    var newPlayer = modelsFactory.CreatePlayer(
                     p.FirstName,
                     p.LastName,
                     p.Ranking,
                     p.BirthDate,
                     p.Height,
                     p.Weight,
                     p.City,
                     p.Country);

                    this.dataProvider.Players.Add(newPlayer);

                }
                catch (ArgumentException ex)
                {

                    Console.WriteLine("Excel import problem: " + ex.Message);
                }

            }

            this.dataProvider.UnitOfWork.Finished();
        }

        public void Write()
        {




            //var city = this.modelsFactory.CreateCity("Paris", "France");
            //this.dataProvider.Cities.Add(city);

            ////this.dataProvider.UnitOfWork.Finished();

            //var city1 = modelsFactory.CreateCity("Nant", "France");


            //this.dataProvider.Cities.Add(city1);

            //this.dataProvider.UnitOfWork.Finished();

        }


    }
}
