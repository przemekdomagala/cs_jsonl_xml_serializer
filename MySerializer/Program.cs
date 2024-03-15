﻿using System.Xml.Serialization;
using System.Text.Json;
using Newtonsoft.Json;

public class Program{
    public static void Main(){
        String jsonString = File.ReadAllText("json_files\\data.json");
        Tweets tweets = JsonConvert.DeserializeObject<Tweets>(jsonString);
        Console.WriteLine(tweets.Data[0].ToString());
        
        var path = "xml_files\\tweets.xml";
        File.WriteAllText(path, "");
        Console.WriteLine("\nxmlSerializer Function works:\n");
        xmlSerializer(tweets);
        xmlSerializer(tweets, true, 5);
        
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
