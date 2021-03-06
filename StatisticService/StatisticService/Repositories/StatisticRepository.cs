﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using LiteDB;
using StatisticService.Model;

namespace StatisticService.Repositories
{
    public class StatisticRepository : IStatisticRepository
    {
        private readonly string DatabasePath = System.AppDomain.CurrentDomain.BaseDirectory+@"\statistics";

        internal StatisticDB ConvertStatisticToStatisticDB(Statistic statistic)
        {
            if (statistic == null)
                return null;
            return new StatisticDB()
            {
                Id = statistic.Id,
                ImageId = statistic.ImageId,
                ViewDate = statistic.ViewDate,
                Author = statistic.Author             
            };
        }

        internal Statistic ConvertStatisticDBToStatistic(StatisticDB statisticDb)
        {
            if (statisticDb == null)
                return null;
            return new Statistic()
            {
                Id = statisticDb.Id,
                ImageId = statisticDb.ImageId,
                ViewDate = statisticDb.ViewDate,
                Author = statisticDb.Author
            };
        }

        public int Add(Statistic statistic)
        {
            using (var database = new LiteDatabase(this.DatabasePath))
            {
                var statisticDB = ConvertStatisticToStatisticDB(statistic);
                var repository = database.GetCollection<StatisticDB>("statistics");
                return repository.Insert(statisticDB);
            }
        }
        
        public List<Statistic> FindAll()
        {
            using (var database = new LiteDatabase(this.DatabasePath))
            {
                var repository = database.GetCollection<StatisticDB>("statistics");
                var listStatisticsDb = repository.FindAll();
                return listStatisticsDb.Select(x => ConvertStatisticDBToStatistic(x)).ToList();
            }
        }

        public IEnumerable<Statistic> FindImageStats(string imageId, string authorName)
        {
            using (var database = new LiteDatabase(this.DatabasePath))
            {
                var repository = database.GetCollection<StatisticDB>("statistics");
                var allStatistics = FindAll();
                return allStatistics.Where(x => x.ImageId.Equals(imageId) 
                                            && x.Author.Equals(authorName));
            }
        }
    }
}