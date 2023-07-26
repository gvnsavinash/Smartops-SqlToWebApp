//***************************************************************************************************************************************//
/*
  Project Name  :   SmartOps Phase-2
  Task          :   Reading and store data From Excel to Angular Dashbaord using Web App
  Developed on  :   Aug 2022
  Developed by  :   Avinash
  Version       :   1.2
  Module:       :   HygWS Controller (Get HYGHelper response to websocket for angular dashboard) 

 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Diagnostics;
using System.IO;
using Azure.Storage.Blobs;


namespace SqlToWebApp.Controllers
{
    public class HYGController : System.Web.Http.ApiController
    {
        //private readonly object blobClient;

        [HttpPost]

        public void getAutoDemo()
        {
            var path = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/uploads");
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] files = dir.GetFiles("*.csv");
            foreach (FileInfo file in files)
            {
                HYGHelper.LoadData(path + "/" + file.Name);
            }
        }

        [HttpGet]

        public void getDemoByStorageBlob()
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=funcmfgsmartopsstg;AccountKey=/2QhPy9+jzcVpNRXRplvTlll+KvTf9jcN+LQzHxIkKgCgGKY9cwWVAPQqb7b1WMhhRPfLfCOlUyb+AStMx2EzA==;EndpointSuffix=core.windows.net");
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("hyg");
            BlobClient blobClient1 = containerClient.GetBlobClient("HYGINPUTFILE_1_15_Apr.csv");
            MemoryStream memoryStream = new MemoryStream();
            blobClient1.DownloadTo(memoryStream);
            memoryStream.Position = 0;
            HYGHelper.LoadDatafromBlob(memoryStream);


            // HYGHelper.LoadData("https://connectedfactory.blob.core.windows.net/hyg/HYGINPUTFILE_1_15_Apr.csv");
        }




        [HttpGet]
        public String DeleteTables()
        {
            return HYGHelper.datacleanup();
        }

        [HttpGet]
        public string getAssetList()
        {
            return "RESPONSE : getAssetList";
        }


        [HttpGet]
        public string getOperatorEfficiencyList()
        {
            return HYGHelper.getOperatorEfficiencyList();
            /*return "{\"Plant\": \"Berea 1\", \"TotalOperatorsWithinTPCT\":\"14\",\"TotalOperatorsExceedingTPCT\":16,\"TotalOperatorsExceedingTPCT\":2," +
                "\"OperatorsInformation\": [{\"Id\": \"500203\", \"FirstName\": \"John\",\"Telephone\": \"+1-541-754-3017\",\"CycleTime\": 7,\"ActualTime\": 8.3,\"Efficiency\": 84}," +
                "{\"Id\": \"500204\", \"FirstName\": \"Sue\",\"Telephone\": \"+1-541-754-3017\",\"CycleTime\": 7.3,\"ActualTime\": 7.45,\"Efficiency\": 98}]}";
              */
        }

        [HttpGet]
        public string getWorkStationEfficiency()
        {
            return HYGHelper.getWorkstationEfficiency();
            //string response = "{\"Plant\": \"Berea 1\",  \"TotalEventsWithinCycletTime\": 47, \"TotalEventsExceedingCycletTime\": 8, \"EfficiencyTrend\": [{\"Date\": #DATE#, \"TotalEventsWithinCycletTime\": 47, \"TotalEventsExceedingCycletTime\": 8}]}";
            //return response.Replace("#DATE#", DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString());
        }

        [HttpGet]
        public string getWorkUnitStatus()
        {
            return HYGHelper.getWorkUnitStatus();
            //return "{\"Plant\": \"Berea 1\", \"WorkUnitStatus\": [{\"WorkUnitId\": \"20654\", \"TPCT\": 16, \"UTT\": 17}]}";
        }


        [HttpGet]
        public string getOOFStatus()
        {
            //string response = "{\"Floor_Id\": \"Shop Floor I\", \"OOF\": [{\"Date\": #DATE#, \"Count\": #COUNT#}]}";
            //return response.Replace("#DATE#", DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString()).Replace("#COUNT#", HygHelper.GetOOFStatus().ToString());
            return HYGHelper.GetOOFStatus();
            //JObject json = JObject.Parse(str);
        }

        [HttpGet]
        public string getAllHYGWorkOrder()
        {
            return HYGHelper.getAllHYGWorkOrder();
            // return "[{\"workorder_id\": \"WO_19234\",\"WorkStation\":{\"workstationId\": \"AB51030\",\"WorkStationDescription\": \"AB51030 DESC\", \"Zone\": {\"ZoneId\": \"Zone5\", \"ZoneDescription\": \"Zone\"}},\"WorkUnit\":{\"WorkUnitId\": \"261090\", \"WorkUnitDescription\": \"workUnit\", \"ScheduledDate\": \"\"},\"Process\":{\"ProcessId\": \"4-7T MAST MAINLINE\", \"ProcessDescription\": \"ProcessID\"},\"SOE\":{\"SOE_Code\": \"SOE\", \"SOEDescription\": \"SOE DESC\"},\"Event\":{\"Event_Number\": 30, \"EventDescription\": \"Operation_30\",\"EventTime\": 0},\"Operation\":{\"Operation_Reference\": \"Op Ref\", \"OperationDescription\": \"Op Desc\"},\"Part\":{\"PartID\": \"000\", \"PartDescription\": \"Part Desc\"},\"StartDateTime\": \"2020-04-1T05:24:33\",\"ActualEndDateTime\": \"2020-04-1T05:28:46\",\"OOF_Reason\": \"OOF Reason\",\"OOFID\": \"2020\",\"FeederStationId\": \"FEEDERSt\",\"RecordEntryDateTime\": \"2020-11-6T04:55:19\",\"OrderStatus\": \"C\"}," +
            //     "{\"workorder_id\": \"WO_19235\",\"WorkStation\":{\"workstationId\": \"AB52030\",\"WorkStationDescription\": \"AB52030 DESC\", \"Zone\": {\"ZoneId\": \"Zone6\", \"ZoneDescription\": \"Zone\"}},\"WorkUnit\":{\"WorkUnitId\": \"261090\", \"WorkUnitDescription\": \"workUnit\", \"ScheduledDate\": \"\"},\"Process\":{\"ProcessId\": \"4-7T MAST MAINLINE\", \"ProcessDescription\": \"ProcessID\"},\"SOE\":{\"SOE_Code\": \"SOE\", \"SOEDescription\": \"SOE DESC\"},\"Event\":{\"Event_Number\": 30, \"EventDescription\": \"Operation_30\",\"EventTime\": 0},\"Operation\":{\"Operation_Reference\": \"Op Ref\", \"OperationDescription\": \"Op Desc\"},\"Part\":{\"PartID\": \"000\", \"PartDescription\": \"Part Desc\"},\"StartDateTime\": \"2020-04-1T05:24:33\",\"ActualEndDateTime\": \"2020-04-1T05:28:46\",\"OOF_Reason\": \"OOF Reason\",\"OOFID\": \"2020\",\"FeederStationId\": \"FEEDERSt\",\"RecordEntryDateTime\": \"2020-11-6T04:55:19\",\"OrderStatus\": \"C\"}]";
        }
        [HttpGet]
        public string gethygstart()
        {
            return HYGHelper.getAllHYGWorkOrder();
            // return "[{\"workorder_id\": \"WO_19234\",\"WorkStation\":{\"workstationId\": \"AB51030\",\"WorkStationDescription\": \"AB51030 DESC\", \"Zone\": {\"ZoneId\": \"Zone5\", \"ZoneDescription\": \"Zone\"}},\"WorkUnit\":{\"WorkUnitId\": \"261090\", \"WorkUnitDescription\": \"workUnit\", \"ScheduledDate\": \"\"},\"Process\":{\"ProcessId\": \"4-7T MAST MAINLINE\", \"ProcessDescription\": \"ProcessID\"},\"SOE\":{\"SOE_Code\": \"SOE\", \"SOEDescription\": \"SOE DESC\"},\"Event\":{\"Event_Number\": 30, \"EventDescription\": \"Operation_30\",\"EventTime\": 0},\"Operation\":{\"Operation_Reference\": \"Op Ref\", \"OperationDescription\": \"Op Desc\"},\"Part\":{\"PartID\": \"000\", \"PartDescription\": \"Part Desc\"},\"StartDateTime\": \"2020-04-1T05:24:33\",\"ActualEndDateTime\": \"2020-04-1T05:28:46\",\"OOF_Reason\": \"OOF Reason\",\"OOFID\": \"2020\",\"FeederStationId\": \"FEEDERSt\",\"RecordEntryDateTime\": \"2020-11-6T04:55:19\",\"OrderStatus\": \"C\"}," +
            //     "{\"workorder_id\": \"WO_19235\",\"WorkStation\":{\"workstationId\": \"AB52030\",\"WorkStationDescription\": \"AB52030 DESC\", \"Zone\": {\"ZoneId\": \"Zone6\", \"ZoneDescription\": \"Zone\"}},\"WorkUnit\":{\"WorkUnitId\": \"261090\", \"WorkUnitDescription\": \"workUnit\", \"ScheduledDate\": \"\"},\"Process\":{\"ProcessId\": \"4-7T MAST MAINLINE\", \"ProcessDescription\": \"ProcessID\"},\"SOE\":{\"SOE_Code\": \"SOE\", \"SOEDescription\": \"SOE DESC\"},\"Event\":{\"Event_Number\": 30, \"EventDescription\": \"Operation_30\",\"EventTime\": 0},\"Operation\":{\"Operation_Reference\": \"Op Ref\", \"OperationDescription\": \"Op Desc\"},\"Part\":{\"PartID\": \"000\", \"PartDescription\": \"Part Desc\"},\"StartDateTime\": \"2020-04-1T05:24:33\",\"ActualEndDateTime\": \"2020-04-1T05:28:46\",\"OOF_Reason\": \"OOF Reason\",\"OOFID\": \"2020\",\"FeederStationId\": \"FEEDERSt\",\"RecordEntryDateTime\": \"2020-11-6T04:55:19\",\"OrderStatus\": \"C\"}]";
        }

        [HttpGet]
        public string gethygend()
        {
            return HYGHelper.getAllHYGWorkOrder();
            // return "[{\"workorder_id\": \"WO_19234\",\"WorkStation\":{\"workstationId\": \"AB51030\",\"WorkStationDescription\": \"AB51030 DESC\", \"Zone\": {\"ZoneId\": \"Zone5\", \"ZoneDescription\": \"Zone\"}},\"WorkUnit\":{\"WorkUnitId\": \"261090\", \"WorkUnitDescription\": \"workUnit\", \"ScheduledDate\": \"\"},\"Process\":{\"ProcessId\": \"4-7T MAST MAINLINE\", \"ProcessDescription\": \"ProcessID\"},\"SOE\":{\"SOE_Code\": \"SOE\", \"SOEDescription\": \"SOE DESC\"},\"Event\":{\"Event_Number\": 30, \"EventDescription\": \"Operation_30\",\"EventTime\": 0},\"Operation\":{\"Operation_Reference\": \"Op Ref\", \"OperationDescription\": \"Op Desc\"},\"Part\":{\"PartID\": \"000\", \"PartDescription\": \"Part Desc\"},\"StartDateTime\": \"2020-04-1T05:24:33\",\"ActualEndDateTime\": \"2020-04-1T05:28:46\",\"OOF_Reason\": \"OOF Reason\",\"OOFID\": \"2020\",\"FeederStationId\": \"FEEDERSt\",\"RecordEntryDateTime\": \"2020-11-6T04:55:19\",\"OrderStatus\": \"C\"}," +
            //     "{\"workorder_id\": \"WO_19235\",\"WorkStation\":{\"workstationId\": \"AB52030\",\"WorkStationDescription\": \"AB52030 DESC\", \"Zone\": {\"ZoneId\": \"Zone6\", \"ZoneDescription\": \"Zone\"}},\"WorkUnit\":{\"WorkUnitId\": \"261090\", \"WorkUnitDescription\": \"workUnit\", \"ScheduledDate\": \"\"},\"Process\":{\"ProcessId\": \"4-7T MAST MAINLINE\", \"ProcessDescription\": \"ProcessID\"},\"SOE\":{\"SOE_Code\": \"SOE\", \"SOEDescription\": \"SOE DESC\"},\"Event\":{\"Event_Number\": 30, \"EventDescription\": \"Operation_30\",\"EventTime\": 0},\"Operation\":{\"Operation_Reference\": \"Op Ref\", \"OperationDescription\": \"Op Desc\"},\"Part\":{\"PartID\": \"000\", \"PartDescription\": \"Part Desc\"},\"StartDateTime\": \"2020-04-1T05:24:33\",\"ActualEndDateTime\": \"2020-04-1T05:28:46\",\"OOF_Reason\": \"OOF Reason\",\"OOFID\": \"2020\",\"FeederStationId\": \"FEEDERSt\",\"RecordEntryDateTime\": \"2020-11-6T04:55:19\",\"OrderStatus\": \"C\"}]";
        }

        [HttpGet]
        public string hygsp()
        {
            return HYGHelper.getAllHYGWorkOrder();
            // return "[{\"workorder_id\": \"WO_19234\",\"WorkStation\":{\"workstationId\": \"AB51030\",\"WorkStationDescription\": \"AB51030 DESC\", \"Zone\": {\"ZoneId\": \"Zone5\", \"ZoneDescription\": \"Zone\"}},\"WorkUnit\":{\"WorkUnitId\": \"261090\", \"WorkUnitDescription\": \"workUnit\", \"ScheduledDate\": \"\"},\"Process\":{\"ProcessId\": \"4-7T MAST MAINLINE\", \"ProcessDescription\": \"ProcessID\"},\"SOE\":{\"SOE_Code\": \"SOE\", \"SOEDescription\": \"SOE DESC\"},\"Event\":{\"Event_Number\": 30, \"EventDescription\": \"Operation_30\",\"EventTime\": 0},\"Operation\":{\"Operation_Reference\": \"Op Ref\", \"OperationDescription\": \"Op Desc\"},\"Part\":{\"PartID\": \"000\", \"PartDescription\": \"Part Desc\"},\"StartDateTime\": \"2020-04-1T05:24:33\",\"ActualEndDateTime\": \"2020-04-1T05:28:46\",\"OOF_Reason\": \"OOF Reason\",\"OOFID\": \"2020\",\"FeederStationId\": \"FEEDERSt\",\"RecordEntryDateTime\": \"2020-11-6T04:55:19\",\"OrderStatus\": \"C\"}," +
            //     "{\"workorder_id\": \"WO_19235\",\"WorkStation\":{\"workstationId\": \"AB52030\",\"WorkStationDescription\": \"AB52030 DESC\", \"Zone\": {\"ZoneId\": \"Zone6\", \"ZoneDescription\": \"Zone\"}},\"WorkUnit\":{\"WorkUnitId\": \"261090\", \"WorkUnitDescription\": \"workUnit\", \"ScheduledDate\": \"\"},\"Process\":{\"ProcessId\": \"4-7T MAST MAINLINE\", \"ProcessDescription\": \"ProcessID\"},\"SOE\":{\"SOE_Code\": \"SOE\", \"SOEDescription\": \"SOE DESC\"},\"Event\":{\"Event_Number\": 30, \"EventDescription\": \"Operation_30\",\"EventTime\": 0},\"Operation\":{\"Operation_Reference\": \"Op Ref\", \"OperationDescription\": \"Op Desc\"},\"Part\":{\"PartID\": \"000\", \"PartDescription\": \"Part Desc\"},\"StartDateTime\": \"2020-04-1T05:24:33\",\"ActualEndDateTime\": \"2020-04-1T05:28:46\",\"OOF_Reason\": \"OOF Reason\",\"OOFID\": \"2020\",\"FeederStationId\": \"FEEDERSt\",\"RecordEntryDateTime\": \"2020-11-6T04:55:19\",\"OrderStatus\": \"C\"}]";
        }


        [HttpGet]
        public string getOperatorEfficiency()
        {
            return HYGHelper.getOperatorEfficiency();
            //string response = "{\"Plant\": \"Berea 1\", \"OperatorEfficiency\": [{\"CycleTime\": 65, \"Count\": 65}]}";
            //Console.WriteLine("###### result " + response);
            //return response;
            //WS.interactionList = response;
        }


        [HttpGet]
        public string queryWarnings()
        {
            return "{\"Warnings\": [{\"Timestamp\": \"1555314572000\", \"Asset_Id\": 121, \"EngineVitals\": \"Preventive maintenance is due for the Forklift 1 in next four days\"},{\"Timestamp\": \"1555234232000\", \"Asset_Id\": 122, \"EngineVitals\": \"Forklift 1, 2 are waiting for the parts to be ordered\"},{\"Timestamp\": \"1555415922000\", \"Asset_Id\": 123, \"EngineVitals\": \"Seat belt malfunction reported for Forklift 3\"},{\"Timestamp\": \"1555408722000\", \"Asset_Id\": 121, \"EngineVitals\": \"Overheating of Forklift 1 is observed. Fluid levels including fuel, hydraulics, and antifreeze need to be checked\"}]}";
        }

        [HttpGet]
        public string queryAnomalies()
        {
            return "{\"Anomalies\": [{\"Timestamp\": \"1555314572000\", \"Asset_Id\": 121, \"EngineVitals\": \"Coolant Level is expecting threshold range between Min: 0.66 gal to Max: 2.25 gal but observed low value as: 0.3 gal\"},{\"Timestamp\": \"1555234232000\", \"Asset_Id\": 122, \"EngineVitals\": \"Coolant Level is expecting threshold range between Min: 0.66 gal to Max: 2.25 gal but observed low value as: 0.4 gal\"},{\"Timestamp\": \"1555415922000\", \"Asset_Id\": 123, \"EngineVitals\": \"Coolant Level is expecting threshold range between Min: 0.66 gal to Max: 2.25 gal but observed low value as: 0.3 gal\"},{\"Timestamp\": \"1555408722000\", \"Asset_Id\": 121, \"EngineVitals\": \"Coolant Level is expecting threshold range between Min: 0.66 gal to Max: 2.25 gal but observed low value as: 0.3 gal\"}]}";
        }

        [HttpGet]
        public string queryIncidents()
        {
            string response = "{ \"Incidents\": [{ \"Timestamp\": 1555502322000, \"Asset_Id\": 121, \"EngineVitals\": \"Critical motion failure reported at Forklift 1. Possible reason: bearing malfunction\" }, " +
                "{ \"Timestamp\": 1555491642000, \"Asset_Id\": 122, \"EngineVitals\": \"Forklift 2 is loaded and standing idle, due to fault in engine\" }," +
                "{ \"Timestamp\": 1555408842000, \"Asset_Id\": 123, \"EngineVitals\": \"Strange noise is observed for Forklift 3 steering while turning the wheels. Possible reason: rust in the steering mechanism or issue in the hydraulics\" }, " +
                "{ \"Timestamp\": 1555401642000, \"Asset_Id\": 122, \"EngineVitals\": \"Oil reservoir leak detected for forklift 2\" }," +
                "{ \"Timestamp\": 1555329642000, \"Asset_Id\": 121, \"EngineVitals\": \"Break system for Forklift 1 is failed. Possible reason: damage of the wheel cylinder and hub seals, which keep the brake system properly lubricated\" }, " +
                "{ \"Timestamp\": 1555324242000, \"Asset_Id\": 122, \"EngineVitals\": \"Overhead guard for Forklift 2 is damage. Needs immediate attention\" }, " +
                "{ \"Timestamp\": 1555317042000, \"Asset_Id\": 122, \"EngineVitals\": \"Forklift 2 has a blown hose due to hydraulic leaks\" }] }";

            return response;
        }






    }
}