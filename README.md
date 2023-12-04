# Log Analyzer README

This C# program serves as a simple log analyzer that processes log data from a specified file or command line arguments. The program extracts valuable insights from the logs, such as ranking the most visited URLs and the most active IP addresses.



## Getting Started

### Prerequisites

- .NET Core SDK

### Installation

1. Clone this repository:

    ```bash
    git clone https://github.com/LachlanEdwards/Mantel.Http.Analyser.git
    ```

2. Navigate to the project directory:

    ```bash
    cd Mantel.Http.Analyser
    ```

### Usage

Run the program using the following command:

```bash
dotnet run
```

By default, the program will attempt to retrieve logs from the file located at "./Asset/programming-task-example-data.log". You can also specify a different file path as a command line argument.

```bash
dotnet run ./path/to/your/logfile.log
```

### Functionality
The program performs the following actions:

1. **Retrieve Logs:** Attempts to retrieve logs from the specified file or command line arguments.

2. **Check for Valid Logs:** Checks if the logs are null or empty; if so, the program exits.

3. **Rank URLs:** Ranks URLs based on the number of times they appear in the logs, and displays the top 3 most visited URLs.

4. **Rank IP Addresses:** Ranks IP addresses based on their activity (frequency of appearance), and displays the top 3 most active IP addresses.

5. **Display Unique IP Addresses:** Displays the number of unique IP addresses in the logs.

### Behaviour
The program is designed to return more than the desired amount of results if the popularity of items in the result-set is equal. If, for-example, you requested the top 3 results and the result-set contains a 1 value that is the most popular, and 3 values that are the second most popular, then 4 results will be returned.