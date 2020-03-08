using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotOM;
using SpeckleCore;
using Newtonsoft.Json;

namespace SpeckleRobotClient
{
    public static class SpeckleClientsSchema
    {
        public static IRobotParamSchema GetSchema(IRobotProject doc)
        {
            IRobotParamSchemaMngr schemaMngr = doc.Structure.ExtParams.Schemas;

            if (schemaMngr.Exist("SpeckleClientStorage"))
                return schemaMngr.GetByName("SpeckleClientStorage");

            IRobotParamSchema schema = schemaMngr.Create("SpeckleClientStorage");

            schema.Def.AddSimpleParam("clients", null);

            return schema;
        }
    }

    public static class SpeckleClientsStorageManager
    {
        /// <summary>
        /// Returns the speckle clients present in the current document.
        /// </summary>
        public static SpeckleClientsWrapper ReadClients(IRobotProject doc)
        {
            RobotParamCollection paramCollection = GetParamCollection(doc);
            if (paramCollection == null) 
                return null;

            string clientsParam = paramCollection.GetValue(paramCollection.Find("clients", "SpeckleClientStorage"));
            var clientsList = clientsParam.Split(',').ToList();

            var mySpeckleClients = new SpeckleClientsWrapper();
            mySpeckleClients.SetClients(clientsList);

            return mySpeckleClients;
        }

        public static void WriteClients(IRobotProject doc, SpeckleClientsWrapper wrap)
        {
            //not sure what this data looks like so this might not work; just throwin this in for the mo
            string clientsString = string.Join(",", wrap.GetStringList() as IList<string>);

            IRobotParamSchema clientSchema = SpeckleClientsSchema.GetSchema(doc);

            //temp node to assign param to. will think of something better later
            IRobotNodeServer nodeServer = doc.Structure.Nodes;
            if (nodeServer.Exist(SpeckleUiBindingsRobot.node_id) == 0) nodeServer.Create(SpeckleUiBindingsRobot.node_id, 0, 0, 0);

            IRobotDataObject speckNode = nodeServer.Get(SpeckleUiBindingsRobot.node_id);
            speckNode.SetLabel((IRobotLabelType)(-1), "SpeckleNodePlaceholder");

            clientSchema.SetParam(nodeServer.GetUniqueId(SpeckleUiBindingsRobot.node_id), "clients", clientsString);

        }

        public static RobotParamCollection GetParamCollection(IRobotProject doc)
        {
            RobotParamCollection paramCollection = null;
            IRobotNodeServer nodeServer = doc.Structure.Nodes;

            doc.Structure.ExtParams.GetAllParamsForSchema(nodeServer.GetUniqueId(SpeckleUiBindingsRobot.node_id),
                "SpeckleClientStorage", paramCollection);

            return paramCollection;
        }
    }
}
