using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

class Program
{
    static async Task ProcessRepositoriesAsync(HttpClient client, string itemid) 
    { 
        string url = "https://secure.runescape.com/m=itemdb_oldschool/api/catalogue/detail.json?item=" + itemid;
        var stringTask = client.GetStringAsync(url);

        var msg = await stringTask;
        JObject o = JObject.Parse(msg);
        var exchangeprice = o["item"]["current"]["price"];
        var name = o["item"]["name"];
        Console.WriteLine("Current GE price of " + name+ " is " + exchangeprice);
        
    }

    

    static async Task Main(string[] args)
    {
        Dictionary<string, List<string>> itemLibrary = 
            new Dictionary<string, List<string>>();
        
        void createItemLibraryEntry(string name, string id)
        {
            List<string> list = new List<string>(); list.Add(id);
            itemLibrary.Add(name, list);
        }

        // Brewing items
        
        createItemLibraryEntry("barleymalt", "6008");
        createItemLibraryEntry("barley", "6006");
        createItemLibraryEntry("aleyeast", "5767");
        createItemLibraryEntry("pot", "1931");
        createItemLibraryEntry("bucketofwater", "1929");
        createItemLibraryEntry("bucket", "1925");
        createItemLibraryEntry("calquatfruit", "5980");
        createItemLibraryEntry("calquatkeg", "5769");
        createItemLibraryEntry("beerglass", "1919");
        //createItemLibraryEntry("thestuff", "8988"); CANNOT BE BOUGHT ON THE GRAND EXCHANGE

        // Cider
        createItemLibraryEntry("cider", "5763");
        createItemLibraryEntry("maturecider", "5765");
        createItemLibraryEntry("ciderkeg", "5849");
        createItemLibraryEntry("matureciderkeg", "5929");
        createItemLibraryEntry("applemush", "5992");

        // Dwarven stout
        createItemLibraryEntry("dwarvenstout", "1913");
        createItemLibraryEntry("maturedwarvenstout", "5747");
        createItemLibraryEntry("dwarvenstoutkeg", "5777");
        createItemLibraryEntry("maturedwarvenstoutkeg", "5857");
        createItemLibraryEntry("hammerstonehops", "5994");

        // Asgarnian ale
        createItemLibraryEntry("asgarnianale", "1905");
        createItemLibraryEntry("matureasgarnianale", "5739");
        createItemLibraryEntry("asgarnianalekeg", "5785");
        createItemLibraryEntry("matureasgarnianalekeg", "5857");
        createItemLibraryEntry("asgarnianhops", "5996");

        // Greenman's ale
        createItemLibraryEntry("greenmansale", "1909");
        createItemLibraryEntry("maturegreenmansale", "5743");
        createItemLibraryEntry("greenmansalekeg", "5793");
        createItemLibraryEntry("maturegreenmansalekeg", "5873");
        createItemLibraryEntry("grimyharralander", "205");
        createItemLibraryEntry("harralander", "255");

        // Wizard's mind bomb
        createItemLibraryEntry("wizardsmindbomb", "1907");
        createItemLibraryEntry("maturewizardsmindbomb", "5741");
        createItemLibraryEntry("wizardsmindbombkeg", "5801");
        createItemLibraryEntry("maturewizardsmindbombkeg", "5881");
        createItemLibraryEntry("yanillianhops", "5998");

        // Dragon bitter
        createItemLibraryEntry("dragonbitter", "1911");
        createItemLibraryEntry("maturedragonbitter", "5745");
        createItemLibraryEntry("dragonbitterkeg", "5809");
        createItemLibraryEntry("maturedragonbitterkeg", "5889");
        createItemLibraryEntry("krandorianhops", "6000");

        // Moonlight mead
        createItemLibraryEntry("moonlightmead", "2955");
        createItemLibraryEntry("maturemoonlightmead", "5749");
        createItemLibraryEntry("moonlightmeadkeg", "5817");
        createItemLibraryEntry("maturemoonlightmeadkeg", "5897");
        createItemLibraryEntry("mushroom", "6004");

        // Axeman's folly
        createItemLibraryEntry("axemansfolly", "5751");
        createItemLibraryEntry("matureaxemansfolly", "5753");
        createItemLibraryEntry("axemansfollykeg", "5825");
        createItemLibraryEntry("matureaxemansfollykeg", "5905");
        createItemLibraryEntry("oakroots", "6043");
        
        // Chef's delight
        createItemLibraryEntry("chefsdelight", "5755");
        createItemLibraryEntry("maturechefsdelight", "5757");
        createItemLibraryEntry("chefsdelightkeg", "5833");
        createItemLibraryEntry("maturechefsdelightkeg", "5913");
        createItemLibraryEntry("chocolatedust", "1975");
        createItemLibraryEntry("chocolatebar", "1937");
        
        // Slayer's respite
        createItemLibraryEntry("slayersrespite", "5759");
        createItemLibraryEntry("matureslayersrespite", "5761");
        createItemLibraryEntry("slayersrespitekeg", "5841");
        createItemLibraryEntry("matureslayersrespitekeg", "5921");
        createItemLibraryEntry("wildbloodhops", "6002");

        using HttpClient client = new();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.vs+json"));
        client.DefaultRequestHeaders.Add("User-Agent", ".Net Foundation Repository Reporter");

        foreach (KeyValuePair<string, List<string>> item in itemLibrary)
        {
            while(true)
            {
                try 
                {
                    await ProcessRepositoriesAsync(client, item.Value[0]);
                    break;
                }
                catch
                {
                    System.Threading.Thread.Sleep(2000); // wait 2 seconds to try api request again
                }
            }
        }
    }
}