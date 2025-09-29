

namespace ReadCSV
{
    internal class Program
    {
        static void Main(string[] args)
        {
           

            try
            {
                ClientServices clientServices = new ClientServices(@"D:\\ITLA\\Materias\\Big Data\\data proyecto de opiniones\\clients.csv");
                var clients = clientServices.ReadClients();

                foreach (var item in clients)
                {
                    Console.WriteLine($"ID: {item.IdCliente}, Nombre: {item.Nombre},  Email: {item.Email}");

                }
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("Error: "+ ex.Message);
            }
            catch (FileNotFoundException rex)
            {
                Console.WriteLine("Error: " +rex.Message);
            }
        }
  
    }
}   
