


using ReadCSV.Class;

namespace ReadClientCSV.Services
{
    public class WebReviewServices
    { 
        private readonly string _path;

        public WebReviewServices(string path)
        {
            _path = path;
        }

        public  List<WebReview> Read()
        {
            List<WebReview> webReviews = new List<WebReview>();
            if (!File.Exists(_path))
            {
                Console.WriteLine("File not found.");
                return webReviews;
            }
            if (string.IsNullOrEmpty(_path))
            {
                Console.WriteLine("Path is null or empty.");
                return webReviews;
            }
            try
            {
                using var csv = new StreamReader(_path);
                using var reader = new CsvHelper.CsvReader(csv, System.Globalization.CultureInfo.InvariantCulture);
                var records = reader.GetRecords<WebReview>().ToList();

                var query = from date in records
                            where date.Rating >= 4 && date.Comentario!.Equals("Excelente")
                            select date;

                webReviews = query.ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return webReviews;
        }
    }
}
