using System;
using System.DirectoryServices;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            var se = "LDAP://192.168.20.1";
            DirectoryEntry de = new DirectoryEntry(se ?? "LDAP://localhost");
            DirectorySearcher searcher = new DirectorySearcher(de);
            searcher.Filter = "(&(ObjectClass=User))";
            searcher.PropertiesToLoad.Add("distinguishedName");
            searcher.PropertiesToLoad.Add("sAMAccountName");
            searcher.PropertiesToLoad.Add("name");
            searcher.PropertiesToLoad.Add("objectSid");
            SearchResultCollection results = searcher.FindAll();
            int i = 1;
            foreach (SearchResult res in results)
            {
                Console.WriteLine("Result" + Convert.ToString(i++));
                DisplayProperties("distinguishedName", res);
                DisplayProperties("sAMAccouontName", res);
                DisplayProperties("name", res);
                DisplayProperties("objectSid", res);
                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        Console.ReadKey();
    }

    private static void DisplayProperties(string property, SearchResult res)
    {
        System.IO.File.AppendAllText("rr.txt", "\n" + property, System.Text.Encoding.UTF8);
        ResultPropertyValueCollection col = res.Properties[property];
        foreach (object o in col)
        {
            System.IO.File.AppendAllText("rr.txt", "\n" + o.ToString(), System.Text.Encoding.UTF8);
        }
    }
}