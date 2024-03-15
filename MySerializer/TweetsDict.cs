public class TweetsDict{
    private Dictionary<string, List<Tweet>> tweetsDict;
    public Dictionary<string, List<Tweet>> TweetsDictionatry { get => this.tweetsDict; set => this.tweetsDict = value; }
    public TweetsDict(Tweets tweets){
        tweetsDict = new Dictionary<string, List<Tweet>>();
        foreach(Tweet t in tweets.Data){
            if(!tweetsDict.ContainsKey(t.UserName)){
                List<Tweet> buffer = new List<Tweet>();
                buffer.Add(t);
                tweetsDict.Add(t.UserName, buffer);
            }
            else{
                List<Tweet> buffer = tweetsDict[t.UserName];
                buffer.Add(t);
                tweetsDict[t.UserName] = buffer;
            }
        }
    }
}