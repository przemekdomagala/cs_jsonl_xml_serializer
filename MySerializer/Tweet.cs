public class Tweet{
    private String ?text;
    public String ?Text{ get => this.text; set => this.text = value;}
    private String ?userName;
    public String ?UserName{ get => this.userName; set => this.userName = value;}
    private String ?linkToTweet;
    public String ?LinkToTweet{ get => this.linkToTweet; set => this.linkToTweet = value;}
    private String ?firstLinkUrl;
    public String ?FirstLinkUrl{ get => this.firstLinkUrl; set => this.firstLinkUrl = value;}
    private String ?createdAt;
    public String ?CreatedAt{ get => this.createdAt; set => this.createdAt = value;}
    private String ?tweetEmbedCode;
    public String ?TweetEmbedCode{ get => this.tweetEmbedCode; set => this.tweetEmbedCode = value;}


    public Tweet(String text, String userName, String linkToTweet, String firstLinkUrl, 
    String createdAt, String tweetEmbedCode){
        this.text = text;
        this.userName = userName;
        this.linkToTweet = linkToTweet;
        this.firstLinkUrl = firstLinkUrl;
        this.createdAt = createdAt;
        this.tweetEmbedCode = tweetEmbedCode;
        
    }

    private Tweet(){}

    public override string ToString()
    {
        return "Tweet Content: "+this.text+"\nUser Name: "+this.userName+
        "\nLink To Tweet: "+this.linkToTweet+"\nFirst Link URL: "+this.firstLinkUrl
        +"\nCreated At: "+this.createdAt+"\nTweet Embed Code: "+this.tweetEmbedCode;
    }
}