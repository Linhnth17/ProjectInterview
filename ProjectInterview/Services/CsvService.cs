using Microsoft.Extensions.Logging;
using ProjectInterview.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace ProjectInterview.Services
{
    public class CsvService : ICsvService
    {
        private readonly ILogger<CsvService> _logger;

        // Constructor that injects ILogger for logging
        public CsvService(ILogger<CsvService> logger)
        {
            _logger = logger;
        }

        // Parses the CSV file and returns a list of DataEntry objects
        public async Task<List<DataEntry>> ParseCsvFileAsync(Stream fileStream)
        {
            List<DataEntry> dataList = new List<DataEntry>();
            HashSet<string> seenDates = new HashSet<string>();

            try
            {
                // Reads the CSV file line by line
                using (var reader = new StreamReader(fileStream))
                {
                    // Read the entire file content asynchronously
                    string fileContent = await reader.ReadToEndAsync();

                    if (string.IsNullOrWhiteSpace(fileContent))
                    {
                        _logger.LogWarning("CSV file is empty.");
                        return dataList;
                    }

                    var lines = fileContent.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                    bool isHeader = true;
                    int lineNumber = 0;

                    // Iterate through each line in the CSV file
                    foreach (var line in lines)
                    {
                        lineNumber++;

                        // Skip the header line
                        if (isHeader)
                        {
                            isHeader = false;
                            continue;
                        }

                        var values = line.Split(',');

                        // Ensure the line has enough values to parse
                        if (values.Length >= 2)
                        {
                            string date = values[0].Trim();
                            decimal marketPrice = decimal.TryParse(values[1], out decimal price) ? price : 0;

                            if (!seenDates.Contains(date))
                            {
                                seenDates.Add(date);

                                dataList.Add(new DataEntry
                                {
                                    Date = date,
                                    MarketPrice = marketPrice
                                });
                            }
                            else
                            {
                                _logger.LogWarning("Duplicate entry ignored at line {LineNumber}: {Line}", lineNumber, line);
                            }
                        }
                    }

                    _logger.LogInformation("CSV file parsing completed. {RecordCount} records parsed.", dataList.Count);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while parsing the CSV file.");
            }

            return dataList; 
        }
    }
}
