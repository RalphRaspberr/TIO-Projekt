using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Web;
using StatService.Model;
using LiteDB;

namespace StatService.Repositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly string DatabasePath = "C:\\statistics";

        internal StatisticDB ConvertStatisticToStatisticDB(Statistic statistic)
        {
            if (statistic == null)
                return null;
            return new StatisticDB()
            {
                Id = statistic.Id,
                ViewDate = statistic.ViewDate,
                UserIp = statistic.UserIp
            };
        }

        internal Statistic ConvertStatisticDBToStatistic(StatisticDB statisticDb)
        {
            if (statisticDb == null)
                return null;
            return new Statistic()
            {
                Id = statisticDb.Id,
                ViewDate = statisticDb.ViewDate,
                UserIp = statisticDb.UserIp
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

        public List<Statistic> findAll()
        {
            using (var database = new LiteDatabase(this.DatabasePath))
            {
                var repository = database.GetCollection<StatisticDB>("statistics");
                var listStatisticsDb = repository.FindAll();
                return listStatisticsDb.Select(x => ConvertStatisticDBToStatistic(x)).ToList();
            }
        }

        public List<Statistic> findAllById(int id)
        {
            using (var database = new LiteDatabase(this.DatabasePath))
            {
                var repository = database.GetCollection<StatisticDB>("statistics");
                var allStatistics = findAll();
                return allStatistics.Where(x => x.Id == id).ToList();
            }
        }
    }
}