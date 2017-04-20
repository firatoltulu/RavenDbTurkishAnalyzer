#  Turkish Analyzer FOR RavenDb

**Support Turkish Characters FOR RavenDB.


I was looking for a turkish analyzer for RavenDB but couldn't find any except https://github.com/tugberkugurlu/LuceneAnalyzers 
this analyzer is basically just composed of the already existing KeywordAnalyzer and TurkishAsciiFolding in Lucene.Net



```csharp
public class Users : AbstractIndexCreationTask<User>
{
    public Users()
    {
        Map = users => from user in users
                       select new
                       {
                           user.Name
                       };
        Analyzers.Add(x => x.Name, typeof(TurkishAnalyzer).AssemblyQualifiedName); //this here important
    }
}

