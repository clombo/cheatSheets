using BuilderPatternExample.Models;

namespace BuilderPatternExample.Builders;

/// <summary>
/// Builder class for the SocialMediaModel class containing all methods to build it using the Builder Pattern.
/// </summary>
public class SocialMediaModelBuilder
{
    private readonly SocialMediaModel _post = new SocialMediaModel();

    /// <summary>
    /// Assign title to Social Media Model
    /// </summary>
    /// <param name="title"></param>
    /// <returns></returns>
    public SocialMediaModelBuilder AddTitle(string title)
    {
        _post.Title = title;
        return this;
    }

    /// <summary>
    /// Method called after necessary property builder methods were called. This is normally called last.
    /// </summary>
    /// <returns></returns>
    public SocialMediaModel Build()
    {
        return _post;
    }
}