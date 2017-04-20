using Raven.Client.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurkishAnalyzerTest.Index
{
    public class UserIndex : AbstractIndexCreationTask<Domain.User>
    {
        public UserIndex()
        {
            Map = users => from user in users
                           select new
                           {
                               user.Name
                           };
            
            Analyzers.Add(x => x.Name, typeof(TurkishAnalyzer.TurkishAnalyzer).AssemblyQualifiedName);
        }
    }
}
