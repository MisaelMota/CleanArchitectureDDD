using CleanArchitecture.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infraestructure.Persistence
{
    public class StreamerDbContextSeed
    {
        public static async Task SeedAsync(StreamerDbContext context,ILogger<StreamerDbContextSeed> logger)
        {
            if (!context.Streamers!.Any())
            {
                context.Streamers!.AddRange(GetPreconfiguredStreamer());
               await context.SaveChangeAsync();
                logger.LogInformation("Estamos insertando nuevos records al db {context}",typeof(StreamerDbContext).Name);
            }
        }

        private static IEnumerable<Streamer> GetPreconfiguredStreamer()
        {
            return new List<Streamer>
            {
                new Streamer
                {
                    CreatedBy="Misael",
                    Nombre="Misael HBP",
                    Url="http://www.hbp.com"
                },
                
                new Streamer
                {
                    CreatedBy="Misael",
                    Nombre="Amazon HBP",
                    Url="http://www.AmazonHbp.com"
                }
            };
        }
    }
}
