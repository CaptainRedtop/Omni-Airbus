using System.Text.Json;

namespace Omni_Airbus.Model.FIDS
{
    public class FIDSDisplay
    {
        Queue<FIDSItem> FIDSItems { get; set; }
        public FIDSDisplay()
        {
            FIDSItems = new Queue<FIDSItem>();
            Thread thread = new Thread(UpdateFIDSItems);
            thread.Start();
        }

        public void UpdateFIDSItems()
        {
            while (true)
            {
                //todo: contact database.
                //todo: get fIDSItems from database.
                //todo: Create x new fIDSItems and place them in the fIDSItems list.
                FIDSItems.Clear();
                FIDSItems.Enqueue(new FIDSItem(DateTime.Now, "cph", 1, "Norwegian Air 1"));
                FIDSItems.Enqueue(new FIDSItem(DateTime.Now, "cph", 1, "Norwegian Air 2"));
                FIDSItems.Enqueue(new FIDSItem(DateTime.Now, "cph", 1, "Norwegian Air 3"));
                FIDSItems.Enqueue(new FIDSItem(DateTime.Now, "cph", 1, "Norwegian Air 4"));
                FIDSItems.Enqueue(new FIDSItem(DateTime.Now, "cph", 1, "Norwegian Air 5"));
                FIDSItems.Enqueue(new FIDSItem(DateTime.Now, "cph", 1, "Norwegian Air 6"));
                FIDSItems.Enqueue(new FIDSItem(DateTime.Now, "cph", 1, "Norwegian Air 7"));
                FIDSItems.Enqueue(new FIDSItem(DateTime.Now, "cph", 1, "Norwegian Air 8"));
                FIDSItems.Enqueue(new FIDSItem(DateTime.Now, "cph", 1, "Norwegian Air 9"));
                FIDSItems.Enqueue(new FIDSItem(DateTime.Now, "cph", 1, "Norwegian Air 10"));

                var jsonObject = new
                {
                    data = FIDSItems
                };

                string data = JsonSerializer.Serialize(jsonObject);
                File.WriteAllText(Path.Combine(FIDSWebServer.BASE_PATH, "departures.json"), data);
                File.WriteAllText(Path.Combine(FIDSWebServer.BASE_DEBUG_PATH, "departures.json"), data);
                Thread.Sleep(5000);
                Console.WriteLine("updated departures.json");
            }
        }
    }
}