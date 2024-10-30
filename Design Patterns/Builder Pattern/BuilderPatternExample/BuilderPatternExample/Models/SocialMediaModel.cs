namespace BuilderPatternExample.Models;

public class SocialMediaModel
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string Author { get; set; }
    public List<string> Tags { get; set; }
    public DateTime DatePosted { get; set; }
    public Uri ImageUrl { get; set; }
    public List<Uri> Links { get; set; }
}