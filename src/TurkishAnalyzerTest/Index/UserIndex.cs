using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;
using System.Linq;

namespace TurkishAnalyzerTest.Index
{
    public class UserIndex : AbstractIndexCreationTask<Domain.User>
    {
        public class Result
        {
            public string Name { get; set; }

            public decimal Total { get; set; }
        }

        public UserIndex()
        {
            Map = users => from user in users
                           select new
                           {
                               user.Name,
                               user.Total,
                               user.Role
                           };

            Indexes.Add(x => x.Name, FieldIndexing.Analyzed);
            Analyzers.Add(x => x.Name, typeof(TurkishAnalyzer.TurkishAnalyzer).AssemblyQualifiedName);

        }


    }
}