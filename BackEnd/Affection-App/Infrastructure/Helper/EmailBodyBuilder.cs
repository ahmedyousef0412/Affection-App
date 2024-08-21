
namespace Affection.Infrastructure.Helper;
public static class EmailBodyBuilder
{

    public static string GenerateEmailBody(string templateName , Dictionary<string, string> templateValues)
    {
        var templatePath = $"{Directory.GetCurrentDirectory()}/Templates/{templateName}.html";

        var streamReader = new StreamReader(templatePath);

        var body = streamReader.ReadToEnd();
        streamReader.Close();

        foreach (var item in templateValues)
            body = body.Replace(item.Key, item.Value);

        return body;

    }


    //public static string GenerateEmailBody(string templateContent, Dictionary<string, string> templateModel)
    //{
    //    if (string.IsNullOrEmpty(templateContent))
    //        throw new ArgumentException("Template content cannot be null or empty.", nameof(templateContent));

    //    if (templateModel == null || !templateModel.Any())
    //        return templateContent;

    //    var body = templateContent;

    //    foreach (var item in templateModel)
    //        body = body.Replace(item.Key, item.Value);

    //    return body;
    //}
}
