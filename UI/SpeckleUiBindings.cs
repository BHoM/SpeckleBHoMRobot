using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Newtonsoft.Json;
using RobotOM;
using SpeckleCore;
using SpeckleUiBase;

namespace SpeckleRobotClient
{
    public partial class SpeckleUiBindingsRobot : SpeckleUIBindings
    {
        public static IRobotApplication RobotApp;
        public static IRobotProject Project { get => RobotApp.Project; }

        public static SpeckleUiWindow SpeckleWindow;

        public List<Action> Queue;

        //public ExternalEvent Executor;

        // temp SpeckleNodePlaceholder to attach params for the moment.
        // will replace with something else later
        public static int node_id = 420420;

        public Timer SelectionTimer;

        /// <summary>
        /// Holds the current project's clients
        /// </summary>
        public SpeckleClientsWrapper ClientListWrapper;

        public List<SpeckleStream> LocalState;

        public SpeckleUiBindingsRobot(IRobotApplication _RobotApp) : base()
        {
            RobotApp = _RobotApp;
            Queue = new List<Action>();
            ClientListWrapper = new SpeckleClientsWrapper();
        }

        public void InitialiseBinding()
        {
            // LOCAL STATE
            LocalState = new List<SpeckleStream>();

            //Queue.Add(new Action(() =>
            //{
            //}));
            LocalState = SpeckleStateManager.ReadState(Project);
            InjectStateInKits();

            SelectionTimer = new Timer(1400) { AutoReset = true, Enabled = true };
            SelectionTimer.Elapsed += SelectionTimer_Elapsed;
        }

        private void SelectionTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (Project == null) return;
            var selectedObjectsCount = 0;
            foreach (var i in Enumerable.Range(-2,10))
            {
                selectedObjectsCount += Project.Structure.Selections.Get((IRobotObjectType)(i)).Count;
            }

            NotifyUi("update-selection-count", JsonConvert.SerializeObject(new
            {
                selectedObjectsCount
            }));
        }

        /// <summary>
        /// Injects the current lolcal state in any speckle kit initialiser class that has a "LocalRobotState" property defined. 
        /// This can then be used to determine what existing speckle baked objects exist in the current doc and either modify/delete whatever them in the conversion methods.
        /// </summary>
        public void InjectStateInKits()
        {
            var assemblies = SpeckleCore.SpeckleInitializer.GetAssemblies();
            foreach (var ass in assemblies)
            {
                var types = ass.GetTypes();
                foreach (var type in types)
                {
                    if (type.GetInterfaces().Contains(typeof(SpeckleCore.ISpeckleInitializer)))
                    {
                        if (type.GetProperties().Select(p => p.Name).Contains("LocalRobotState"))
                        {
                            type.GetProperty("LocalRobotState").SetValue(null, LocalState);
                        }
                    }
                }
            }
        }

        public override void AddObjectsToSender(string args)
        {
            throw new NotImplementedException();
        }

        public override void AddReceiver(string args)
        {
            var client = JsonConvert.DeserializeObject<dynamic>(args);
            ClientListWrapper.clients.Add(client);

            SpeckleClientsStorageManager.WriteClients(Project, ClientListWrapper);
        }

        public override void AddSelectionToSender(string args)
        {
            throw new NotImplementedException();
        }

        public override void AddSender(string args)
        {
            throw new NotImplementedException();
        }

        public override string GetApplicationHostName()
        {
            return "Robot";
        }

        public override string GetDocumentId()
        {
            return Project.UniqueId;
        }

        public override string GetDocumentLocation()
        {
            return Project.FileName;
        }

        public override string GetFileClients()
        {
            var myReadClients = SpeckleClientsStorageManager.ReadClients(Project);
            if (myReadClients == null)
                myReadClients = new SpeckleClientsWrapper();

            // Set them up in the class so we're aware of them
            ClientListWrapper = myReadClients;

            return JsonConvert.SerializeObject(myReadClients.clients);
        }

        public override string GetFileName()
        {
            return Project.Name;
        }

        public override List<ISelectionFilter> GetSelectionFilters()
        {
            throw new NotImplementedException();
        }

        public override void PushSender(string args)
        {
            throw new NotImplementedException();
        }

        public override void RemoveClient(string args)
        {
            var client = JsonConvert.DeserializeObject<dynamic>(args);
            var index = ClientListWrapper.clients.FindIndex(cl => cl.clientId == client.clientId);

            if (index == -1) return;

            ClientListWrapper.clients.RemoveAt(index);

            var lsIndex = LocalState.FindIndex(x => x.StreamId == (string)client.streamId);
            if (lsIndex != -1) LocalState.RemoveAt(lsIndex);

            SpeckleStateManager.WriteState(Project, LocalState);
            SpeckleClientsStorageManager.WriteClients(Project, ClientListWrapper);
        }

        public override void RemoveObjectsFromSender(string args)
        {
            throw new NotImplementedException();
        }

        public override void RemoveSelectionFromSender(string args)
        {
            throw new NotImplementedException();
        }

        public override void SelectClientObjects(string args)
        {
            throw new NotImplementedException();
        }

        public override void UpdateSender(string args)
        {
            throw new NotImplementedException();
        }
    }
}
