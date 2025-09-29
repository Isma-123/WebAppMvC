

using ReadCSV.Class;
using ReadCSV.NewFolder;
using System.Windows.Markup;

namespace ReadClientCSV.Services
{
    public class SurveryServices
    {
        private readonly string _path;

        public SurveryServices(string path)
        {
            _path = path;
        }

        public List<Survey> ReadSurvey()
        {
            List<Survey> surveys = new List<Survey>();
            var result = new Result();

            if(!File.Exists(_path))
            {
                result.Exist = false;
                result.Message= "File not found.";  

                return surveys;
            } 

            if(string.IsNullOrEmpty(_path))
            {
                result.Exist = false;
                result.Message = "Path is null or empty.";  
        
                return surveys;
            }   

            try
            {
                using var csv = new StreamReader(_path);    
                using var reader = new CsvHelper.CsvReader(csv, System.Globalization.CultureInfo.InvariantCulture); 
                var records = reader.GetRecords<Survey>().ToList();  

                // query services de forma positiva
                var query = from survey in records
                            where survey.Clasificación!.Equals("Excelente") && survey.PuntajeSatisfacción >= 4       
                            select survey;


                surveys = query.ToList();   

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }



            return surveys;

        }
    }
}
