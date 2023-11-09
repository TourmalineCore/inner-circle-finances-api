using System.Reflection.Emit;

namespace SalaryService.Domain;

public class CompensationType
{
    public long Id { get; set; }

    public string Label { get; set; }

    public string Value { get; set; }

    public CompensationType(long id, string label)
    {
        Id = id;
        Label = label;
        Value = ToValue(label);
    }

    static string ToValue(string label)
    {
        string[] words = label.Split(' ');

        for (int i = 1; i < words.Length; i++)
        {
            words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1);
        }

        var value = string.Join("", words);

        return char.ToLower(value[0]) + value.Substring(1);
    }
}
