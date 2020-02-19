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
    class SpeckleUiBindingsRobot : SpeckleUIBindings
    {
        public static IRobotApplication RobotApp;
        public static IRobotProject Project { get => RobotApp.Project; }

        public static SpeckleUiWindow SpeckleWindow;

        public List<Action> Queue;

        //public ExternalEvent Executor;

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

        public override void AddObjectsToSender(string args)
        {
            throw new NotImplementedException();
        }

        public override void AddReceiver(string args)
        {
            var client = JsonConvert.DeserializeObject<dynamic>(args);
            ClientListWrapper.clients.Add(client);
        }

        public override void AddSelectionToSender(string args)
        {
            throw new NotImplementedException();
        }

        public override void AddSender(string args)
        {
            throw new NotImplementedException();
        }

        public override void BakeReceiver(string args)
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
