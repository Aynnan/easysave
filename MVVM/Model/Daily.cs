

using System.Threading.Tasks;

namespace EasySave.MVVM.Model
{
    class Daily
    {
        public Daily(Timestamp timestamp) {

            DailyJSON dailyJson = new DailyJSON();
            
            Task task = Task.Run(() => dailyJson.AddDaily(timestamp));
            task.Wait();

            // new DailyJSON().AddDaily(timestamp);
            //new DailyXML().AddDaily(timestamp);
        }

    }
}
