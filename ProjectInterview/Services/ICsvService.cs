using ProjectInterview.Models;
using System.Collections.Generic;
using System.IO;

namespace ProjectInterview.Services
{
    // Interface for CSV service that defines the method contract for parsing CSV files.
    public interface ICsvService
    {
        // Method that takes a Stream (file data) and returns a List of DataEntry objects
        // after parsing the CSV file.
        List<DataEntry> ParseCsvFile(Stream fileStream);
    }
}
