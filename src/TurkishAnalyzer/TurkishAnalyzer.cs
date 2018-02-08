using Lucene.Net.Analysis;
using System.IO;

namespace TurkishAnalyzer
{
    public class TurkishAnalyzer : Analyzer
    {
        public override TokenStream TokenStream(string fieldName, TextReader reader)
        {
            var token = new KeywordTokenizer(reader);
            var turkishFilter = new LowerCaseFilter(new TurkishAsciiFoldingFilter(token));
            return turkishFilter;
        }
    }
}
