using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace Egress.Application.Services;

/// <summary>
/// CSV utilities
/// </summary>
public static class CsvUtils
{
    private static readonly CsvConfiguration _configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        HasHeaderRecord = true,
        Delimiter = ";"
    };

    /// <summary>
    /// Read CSV file
    /// </summary>
    /// <param name="stream">Stream file</param>
    /// <param name="configuration">CSV configuration</param>
    /// <returns>List of T</returns>
    public static IList<T> ReadCsvToList<T>(Stream stream, CsvConfiguration? configuration = null)
    {
        using var reader = new StreamReader(stream);

        using var csv = (configuration is null) ? new CsvReader(reader, _configuration) : new CsvReader(reader, configuration);

        return csv.GetRecords<T>().ToList();
    }
}
