
# Project Interview

## Introduction

This project is designed to upload, parse, and display data from CSV files. The user can upload a CSV file containing market prices and the project will display the data in a table, along with some basic statistics and charts.

### Technologies Used

- **Backend**: .NET Core MVC (C#)
- **Frontend**: HTML/CSS, Jquery, Bootstrap
- **Data Format**: CSV

## Prerequisites

To run this project, you need the following:

- [.NET SDK 6.0 or later](https://dotnet.microsoft.com/download/dotnet) installed on your machine.
- A suitable IDE (e.g., [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)).
- Basic knowledge of C#, ASP.NET Core MVC, and how to work with CSV files.

## Steps to Set Up and Run the Project

Follow these steps to open and run the project:

### 1. Clone the repository

First, clone this repository to your local machine:

```bash
git clone https://github.com/yourusername/project-interview.git
```

Replace `yourusername` with your actual GitHub username or the correct repository path.

### 2. Open the project

Once the repository is cloned, navigate to the folder and open it in your IDE of choice.

For Visual Studio, simply double-click on the `.sln` file to open it.

### 3. Restore NuGet Packages

The project uses NuGet to manage dependencies. You need to restore the packages before running the project.

In Visual Studio:
- Open the solution and go to the **Tools** menu.
- Select **NuGet Package Manager** → **Restore NuGet Packages**.

Alternatively, you can use the .NET CLI:

```bash
dotnet restore
```

### 4. Configure the Project (if needed)

If your project requires any special configurations (e.g., database connections or API keys), make sure to set them up in the `appsettings.json` or as environment variables.

### 5. Run the Project

Now you are ready to run the project.

In Visual Studio:
- Press `F5` or click on the **Run** button to start the application.

Using the .NET CLI:

```bash
dotnet run
```

This will start the application locally, and it should be accessible at `http://localhost:5037`.


### 7. Upload a CSV File

- On the home page, you will see an option to upload a CSV file.
- Click **Choose File**, select a CSV file from your computer, and click **Upload**.

### 8. View the Results

After uploading the file, the page will display the following:
- **CSV Data**: The contents of the CSV file in a table format.
- **Market Price Statistics**: The minimum, maximum, and average values of the market price from the CSV.
- **Market Price Charts**: Graphical representation of the market price data.

## Project Structure

- **Controllers**: Contains the logic for handling HTTP requests (e.g., uploading the CSV and displaying the results).
- **Models**: Contains the data models, like `DataEntry`, which represents each row in the CSV.
- **Services**: Handles the business logic, such as parsing the CSV file and processing the data.
- **Views**: Contains Razor views to render the data in HTML format.

## Additional Notes

- **Error Handling**: If the file uploaded is not a valid CSV or there's an error while parsing, the user will see an error message on the page.
- **CSV Format**: The expected CSV format is simple, with two columns: `Date` and `MarketPrice`.

Example CSV format:

```
Date,MarketPrice
2023-01-01,100
2023-01-02,120
2023-01-03,110
```

### Author

This application was developed by **Nguyen The Hoang Linh**. If you have any questions or need further assistance, feel free to reach out.
