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
        public List<DataEntry> ParseCsvFile(Stream fileStream)
        {
            List<DataEntry> dataList = new List<DataEntry>();

            try
            {
                // Reads the CSV file line by line
                using (var reader = new StreamReader(fileStream))
                {
                    bool isHeader = true;
                    int lineNumber = 0;

                    // Iterate through each line in the CSV file
                    while (!reader.EndOfStream)
                    {
                        lineNumber++;
                        var line = reader.ReadLine();

                        // Skip the header line
                        if (isHeader)
                        {
                            isHeader = false;
                            continue;
                        }

                        var values = line.Split(',');

                        // Log data being parsed
                        // Ensure the line has enough values to parse
                        if (values.Length >= 2)
                        {

                            // Add parsed data to the list (Date and MarketPrice)
                            dataList.Add(new DataEntry
                            {
                                Date = values[0],
                                MarketPrice = decimal.TryParse(values[1], out decimal price) ? price : 0
                            });

                        }
                        else
                        {
                            // Log warning for invalid data format
                            _logger.LogWarning("Invalid data format at line {LineNumber}: {Line}", lineNumber, line);
                        }
                    }
                }

                // Log the number of records parsed successfully
                _logger.LogInformation("CSV file parsing completed. {RecordCount} records parsed.", dataList.Count);
            }
            catch (Exception ex)
            {
                // Log any exceptions during the file parsing process
                _logger.LogError(ex, "Error occurred while parsing the CSV file.");
            }

            return dataList; // Return the list of parsed data
        }
    }
}
