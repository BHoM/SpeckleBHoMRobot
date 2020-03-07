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
    public static class SpeckleStateSchema
    {
        //not sure if Guid is necessary or if it's specific to revit (need it to construct revit schema)
        //readonly static Guid schemaGuid = new Guid("{5A447395-5833-4BE4-B79D-5006AD81D704}");
        public static IRobotParamSchema GetSchema( IRobotProject doc )
        {
            IRobotParamSchemaMngr schemaMngr = doc.Structure.ExtParams.Schemas;

            if (schemaMngr.Exist("SpeckleLocalStateStorage")) 
                return schemaMngr.GetByName("SpeckleLocalStateStorage");
         
            IRobotParamSchema schema = schemaMngr.Create("SpeckleLocalStateStorage");
            //Robot param can only have string params (no list params); will just needa convert every time?
            schema.Def.AddSimpleParam("streams", null);

            return schema;
        }
    }

    static class UniqueSchemaLocalState
    {
        public static IRobotParamSchema GetSchema(IRobotProject doc)
        {
            IRobotParamSchemaMngr schemaMngr = doc.Structure.ExtParams.Schemas;

            if (schemaMngr.Exist("UniqueSchemaLocalState"))
                return schemaMngr.GetByName("UniqueSchemaLocalState");

            IRobotParamSchema schema = schemaMngr.Create("UniqueSchemaLocalState");

            schema.Def.AddSimpleParam("guid", null);

            return schema;
        }
    }

    public static class SpeckleStateManager
    {
        readonly static Guid ID = new Guid("{C44D581C-DECC-492A-8CEC-DA3F7D3A2802}");
        private static IRobotParamCollection paramCollection;

        public static void WriteState(IRobotProject doc, List<SpeckleStream> state)
        {
            //not sure what this data looks like so this might not work; just throwin this in for the mo
            string ls = string.Join(",", state.Select(stream => JsonConvert.SerializeObject(stream)).ToList());

            IRobotParamSchema stateSchema = SpeckleStateSchema.GetSchema(doc);

            //oh nooo a prob here -- can't set to whole proj b/c doc ID is str while structure ID is long 😑
            //needa think of somewhere else to put this
            //stateSchema.SetParam(doc.UniqueId, "streams", ls);

            //this'll do for now -- making a temp node to attach the local state to
            IRobotNodeServer nodeServer = doc.Structure.Nodes;
            if (nodeServer.Exist(SpeckleUiBindingsRobot.node_id) == 0) nodeServer.Create(SpeckleUiBindingsRobot.node_id, 0, 0, 0);

            IRobotDataObject speckNode = nodeServer.Get(SpeckleUiBindingsRobot.node_id);
            speckNode.SetLabel( (IRobotLabelType)(-1), "SpeckleNodePlaceholder");

            stateSchema.SetParam(nodeServer.GetUniqueId(SpeckleUiBindingsRobot.node_id), "streams", ls);

        }

        public static List<SpeckleStream> ReadState(IRobotProject doc)
        {
            //will this work? no idea; tbd
            string streamParam = paramCollection.GetValue(paramCollection.Find("streams", "SpeckleLocalStateStorage"));
            if (streamParam == null) 
                return new List<SpeckleStream>();

            var streamList = streamParam.Split(',').ToList();
            var myState = streamList.Select(str => JsonConvert.DeserializeObject<SpeckleStream>(str)).ToList();

            return myState != null ? myState : new List<SpeckleStream>();
        }

        //not sure where this fits yet; `GetStateEntity` in revit plugin, but there isn't really an entity equiv in robot
        public static IRobotParamSchema GetParamSchema(IRobotProject doc)
        {
            IRobotParamSchema stateParam = SpeckleStateSchema.GetSchema(doc);

            return null;
        }
    }

}
