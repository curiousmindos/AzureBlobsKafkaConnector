using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace BlobsFunctionApp;

public class MessageModel
{
    public string FileName { get; set; }
    public long FileLength { get; set; }

    public string Message { get; set; }
}

public interface IFileStreamParser
{
    MessageModel Parse(string filename, Stream fileStream);
}

public class FileStreamParser : IFileStreamParser
{
    /// <summary>
    /// Sample to parse Csv file
    /// </summary>
    /// <param name="filename"></param>
    /// <param name="fileStream"></param>
    /// <returns></returns>
    public MessageModel Parse(string filename, Stream fileStream)
    {
        using StreamReader sr = new StreamReader(fileStream);
        string fileContent = sr.ReadToEnd();
        var csvParsing = CsvParser(fileContent);

        return new MessageModel()
        {
            FileName = filename,
            FileLength = fileStream.Length,
            Message = string.Join(",",csvParsing.FirstOrDefault())
        };
    }

    private List<string[]> CsvParser(string fileContent, string separator = "\",\"")
    {
        List<string[]> result = new();
        
        foreach (string line in Regex.Split(fileContent, System.Environment.NewLine)
            .ToList().Where(s => !string.IsNullOrEmpty(s)))
        {
            string[] values = Regex.Split(line, separator);

            for (int i = 0; i < values.Length; i++)
            {
                //Trim values
                values[i] = values[i].Trim('\"');
            }

            result.Add(values);
        }
        return result;
    }
}