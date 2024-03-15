using System;
using System.Security.Cryptography;

public class Tweets{
    List<Tweet> data;
    public List<Tweet> Data { get => this.data; set => this.data = value;}
    public Tweets(){
        this.data = new List<Tweet>();
    }

    public override string ToString()
    {
        string result = "";
        foreach(Tweet t in this.data){
            result += t.ToString() +"\n";
        }
        return result;
    }

    private int comparisonByUsernames(Tweet t1, Tweet t2){
        if(t1.UserName.CompareTo(t2.UserName) > 0){
            return 1;
        }
        else if(t1.UserName.CompareTo(t2.UserName) == 0){
            return 0;
        }
        else{
            return -1;
        }
    }

    private int comparisonByDate(Tweet t1, Tweet t2){
        DateTime date1 = DateTime.ParseExact(t1.CreatedAt, "MMMM dd, yyyy 'at' hh:mmtt", System.Globalization.CultureInfo.InvariantCulture);
        DateTime date2 = DateTime.ParseExact(t2.CreatedAt, "MMMM dd, yyyy 'at' hh:mmtt", System.Globalization.CultureInfo.InvariantCulture);
        if(date1>date2){ return 1; }
        else if(date1<date2){ return -1; }
        else { return 0; }
    }

    public void sortByUsernames(){
        data.Sort(comparisonByUsernames);
    }

    public void sortByDate(){
        data.Sort(comparisonByDate);
    }

    internal void wordsFrequency(bool tenMostFrequent = false){
        Dictionary<string, int> wordsFrequencyDict = new Dictionary<string, int>();
        foreach(Tweet t in this.data){
            var punctuation = t.Text.Where(Char.IsPunctuation).Distinct().ToArray();
            var words = t.Text.Split().Select(x => x.Trim(punctuation));
            foreach(string word in words){
                if(!wordsFrequencyDict.ContainsKey(word)){
                    wordsFrequencyDict.Add(word, 1);
                }
                else{
                    wordsFrequencyDict[word]++;
                }
            }
        }
        if(!tenMostFrequent){
            foreach(var valuePair in wordsFrequencyDict){
                Console.WriteLine("Word: "+valuePair.Key+" Frequency: "+valuePair.Value);
            }
        }
        else{
            var sortedDict = wordsFrequencyDict.OrderByDescending(x => x.Value);
            int counter = 0;
            foreach(var valuePair in sortedDict){
                if(valuePair.Key.Length >= 5 && counter < 10){
                    Console.WriteLine("Word: "+valuePair.Key+" Frequency: "+valuePair.Value);
                    counter++;
                }
            }
        }
    }

    internal void idfCalculator(){
        int numberOfDocuments = this.data.Count;
        Dictionary<string, double> idfDict = new Dictionary<string, double>();
        
        Dictionary<string, int> wordsFrequencyDict = new Dictionary<string, int>();
        foreach(Tweet t in this.data){
            var punctuation = t.Text.Where(Char.IsPunctuation).Distinct().ToArray();
            var words = t.Text.Split().Select(x => x.Trim(punctuation));
            var wordsUnique = words.Select(x => x).Distinct(); 
            foreach(string word in words){
                if(!wordsFrequencyDict.ContainsKey(word)){
                    wordsFrequencyDict.Add(word, 1);
                }
                else{
                    wordsFrequencyDict[word]++;
                }
            }
        }

        foreach(var valuePair in wordsFrequencyDict){
            idfDict.Add(valuePair.Key, Math.Log(numberOfDocuments/valuePair.Value));
        }
        
        var sortedDict = idfDict.OrderByDescending(x => x.Value);
        int counter = 0;
        foreach(var valuePair in sortedDict){
            if(counter<80 && counter > 65){
                Console.WriteLine("Word: "+valuePair.Key+" IDF: "+valuePair.Value);
                counter++;
            }
            counter++;
        }
    }
}