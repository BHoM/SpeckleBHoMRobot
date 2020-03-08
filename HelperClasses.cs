using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotOM;
using Newtonsoft.Json;

namespace SpeckleRobotClient
{
    /// <summary>
    /// Wrapper class to manage the storage of speckle clients.
    /// </summary>
    public class SpeckleClientsWrapper
    {
        public List<dynamic> clients { get; set; }

        public SpeckleClientsWrapper() { clients = new List<dynamic>(); }

        public List<string> GetStringList()
        {
            var myList = new List<string>();
            foreach (dynamic el in clients)
            {
                myList.Add(JsonConvert.SerializeObject(el));
            }
            return myList;
        }

        public void SetClients(IList<string> stringList)
        {
            clients = stringList.Select(el => JsonConvert.DeserializeObject<dynamic>(el)).ToList();
        }
    }

}
