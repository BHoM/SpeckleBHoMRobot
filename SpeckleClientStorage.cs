﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotOM;
using SpeckleCore;
using Newtonsoft.Json;

namespace SpeckleRobotClient
{
    public static class SpeckleClientsStorageManager
    {
        private static IRobotParamCollection paramCollection;
        private static IRobotNodeServer nodeServer;
        /// <summary>
        /// Returns the speckle clients present in the current document.
        /// </summary>
        public static SpeckleClientsWrapper ReadClients(IRobotProject doc)
        {
            string clientsParam = paramCollection.GetValue(paramCollection.Find("clients", "SpeckleClientStorage"));
            if (clientsParam == null) return null;

            var clientsList = clientsParam.Split(',').ToList();

            var mySpeckleClients = new SpeckleClientsWrapper();
            mySpeckleClients.SetClients(clientsList);

            return mySpeckleClients;
        }

        public static void WriteClients(IRobotProject doc, SpeckleClientsWrapper wrap)
        {
            //not sure what this data looks like so this might not work; just throwin this in for the mo
            string clientsString = string.Join(",", wrap.GetStringList() as IList<string>);

            IRobotParamSchema clientSchema = SpeckleClientsSchema.GetSchema();

            //temp node to assign param to. will think of something better later
            if (nodeServer.Exist(SpeckleUiBindingsRobot.node_id) == 0) nodeServer.Create(SpeckleUiBindingsRobot.node_id, 0, 0, 0);

            IRobotDataObject speckNode = nodeServer.Get(SpeckleUiBindingsRobot.node_id);
            speckNode.SetLabel((IRobotLabelType)(-1), "SpeckleNodePlaceholder");

            clientSchema.SetParam(nodeServer.GetUniqueId(SpeckleUiBindingsRobot.node_id), "clients", clientsString);

        }
    }
}
