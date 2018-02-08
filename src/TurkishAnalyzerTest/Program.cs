using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Extensions;
using Raven.Client.Indexes;
using System;
using System.Linq;
using TurkishAnalyzerTest.Domain;
using TurkishAnalyzerTest.Index;

namespace TurkishAnalyzerTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string DefaultDatabase = "TurkishAnalyzerTest";
            IDocumentStore store = new DocumentStore
            {
                Url = "http://localhost:8080",
                DefaultDatabase = DefaultDatabase
            }.Initialize();

            IndexCreation.CreateIndexes(typeof(UserIndex).Assembly, store);
            store.DatabaseCommands.EnsureDatabaseExists(DefaultDatabase);

            using (var ses = store.OpenSession())
            {
                ses.Store(new User { Name = "ırmak", Role = "Administrator" });
                ses.Store(new User { Name = "fırat", Role = "Administrator" });
                ses.Store(new User { Name = "ELİF", Role = "Administrator" });
                ses.Store(new User { Name = "DİDEM", Role = "Administrator" });
                ses.Store(new User { Name = "ALİ", Role = "Administrator" });

                ses.SaveChanges();
                var luceneUser1 = ses.Advanced.LuceneQuery<User>("UserIndex").Where("Name: FIRAT").FirstOrDefault();
                var user1 = ses.Query<User>("UserIndex").Where(usr => usr.Name == "FIRAT").FirstOrDefault();

                if (user1 == null)
                {
                    throw new ApplicationException("Not Found");
                }

                if (luceneUser1 == null)
                {
                    throw new ApplicationException("Not Found");
                }

                
            }
        }
    }
}