using System.Xml.Serialization;
using Newtonsoft.Json;

public class Program{
    public static void Main(){
        String jsonString = File.ReadAllText("jsonl_files\\favorite-tweets.jsonl");
        Tweets tweets = tweetsReader(jsonString);
        var path = "xml_files\\tweets.xml";
        File.WriteAllText(path, "");
        xmlSerializer(tweets);
        
        tweets.sortByUsernames();
        Console.WriteLine("Alphabetically last user's tweet:\n"+tweets.Data[tweets.Data.Count-1].ToString()+"\n");
        Console.WriteLine("Alphabetically first user's tweet:\n"+tweets.Data[0].ToString()+"\n");
        
        
        tweets.sortByDate();
        Console.WriteLine("Newest tweet:\n"+tweets.Data[tweets.Data.Count-1].ToString()+"\n");
        Console.WriteLine("Oldest tweet:\n"+tweets.Data[0].ToString()+"\n");
        
        TweetsDict tweetsDict = new TweetsDict(tweets);
        Console.WriteLine("Random user's all tweets: \n");
        foreach(Tweet t in tweetsDict.TweetsDictionatry.Values.ElementAt(6)){
            Console.WriteLine(t.UserName+" "+t.CreatedAt);
        }

        Console.WriteLine("Most frequent words with their frequencies:\n");
        tweets.wordsFrequency(true);
        Console.WriteLine("IDF calculated for ten highest values:\n");
        tweets.idfCalculator();
    }

    public static Tweets tweetsReader(String content){
        Tweets tweets = new Tweets();
        var jsonReader = new JsonTextReader(new StringReader(content)){
            SupportMultipleContent = true
        };

        var jsonSerializer = new JsonSerializer();
        while (jsonReader.Read())
        {
            Tweet ?tweet = jsonSerializer.Deserialize<Tweet>(jsonReader);
            tweets.Data.Add(tweet);
        }
        return tweets;
    }

    public static void xmlSerializer(Tweets ?tweets = null, bool readFromFile = false, int index = -1){
        XmlSerializer x = new XmlSerializer(typeof(Tweets));
        if(readFromFile){
            using(StreamReader reader = new StreamReader("xml_files\\tweets.xml")){
                Tweets tweetsRead = (Tweets)x.Deserialize(reader);
                if(index == -1){
                    Console.WriteLine(tweetsRead.ToString());
                }
                else{
                    Console.WriteLine(tweetsRead.Data[index].ToString());
                }
            }
        }
        else{
            using(StreamWriter writer = File.CreateText("xml_files\\tweets.xml")){
                x.Serialize(writer, tweets);
            }
        }
    }
}
