using ReadCSV.Class;

public class ClientServices
{
    private string _path;

    public ClientServices(string path)
    {
        _path = path;
    }

    public List<Client> ReadClients()
    {

        List<Client> clients = new List<Client>();

        if(!File.Exists(_path))
        {
            Console.WriteLine("File not found.");
            return clients;
        }

        if(string.IsNullOrEmpty(_path))
        {
            Console.WriteLine("Path is null or empty.");
            return clients;
        }

        try
        {
            using var csv = new StreamReader(_path);
            using var reader = new CsvHelper.CsvReader(csv, System.Globalization.CultureInfo.InvariantCulture);
            var records = reader.GetRecords<Client>().ToList(); 
            clients = records;  
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return clients;
    }
    }
