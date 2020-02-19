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
        public static SpeckleUiWindow SpeckleWindow;

        public List<Action> Queue;

        //public ExternalEvent Executor;

        public Timer SelectionTimer;

        /// <summary>
        /// Holds the current project's clients
        /// </summary>
        public SpeckleClientsWrapper ClientListWrapper;

        public List<SpeckleStream> LocalState;

        public override void AddObjectsToSender(string args)
        {
            throw new NotImplementedException();
        }

        public override void AddReceiver(string args)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public override string GetDocumentId()
        {
            throw new NotImplementedException();
        }

        public override string GetDocumentLocation()
        {
            throw new NotImplementedException();
        }

        public override string GetFileClients()
        {
            throw new NotImplementedException();
        }

        public override string GetFileName()
        {
            throw new NotImplementedException();
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
