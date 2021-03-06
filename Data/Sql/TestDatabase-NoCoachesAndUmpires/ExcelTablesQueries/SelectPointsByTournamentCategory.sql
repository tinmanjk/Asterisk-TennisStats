/****** Script for SelectTopNRows command from SSMS  ******/
USE TennisStats

SELECT	t.Category,
		t.PlayersNumber,
		r.Name AS [Round Name],
		pd.Points
  FROM [TennisStats].[dbo].[PointDistributions] pd
	INNER JOIN TournamentCategories t
		ON pd.CategoryId = t.Id
	INNER JOIN Rounds r
		ON pd.RoundId  = r.id
  ORDER BY t.Category, t.PlayersNumber, pd.RoundId