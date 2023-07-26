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



using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;


namespace SqlToWebApp.Controllers
{
    class HYGHelper
    {

        // GET: HYGHelper
        private static readonly int DATA_INSERT_INTERVAL = 5000;// ConfigurationManager.AppSettings["DATA_INSERT_INTERVAL"];
        private static SqlConnection SqlConnection = null;
        private static SqlConnection GetSqlConnection()
        {
            if (SqlConnection == null)
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    //conn.ConnectionString = "Server=covtest;Database=OEETest;Trusted_Connection=true;UserId=covadmin;Password=covtest@01";
                    SqlConnection = new SqlConnection(@"Server=tcp:ae-smartops-covtest.database.windows.net,1433;Initial Catalog=Smart_Mfgs;Persist Security Info=False;User ID=dbadmin;Password=MqVZtYFumuPNQ8uu;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                    //mySqlConnection = new MySqlConnection("Database = covidapp; Data Source = hyster.mysql.database.azure.com; User Id = hyster@hyster; Password = hyg@0123");
                }
            }

            if (SqlConnection.State != System.Data.ConnectionState.Open)
            {
                SqlConnection.Open();
            }
            return SqlConnection;
            //SqlConnection.Close();
        }
        // ################# truncate db ##################
        public static string datacleanup()
        {
            string query = "exec DataCleanUp";
            var cmd = new SqlCommand(query, GetSqlConnection());
            //using MySqlDataReader rdr = cmd.ExecuteReader();
            cmd.ExecuteNonQuery();
            var message = "Data deleted from iotstaging.hyg_workorder and iotstaging.hyg_eventmaster and iotstaging.workunit and iotstaging.hyg_workstation";
            return message;
        }



        // ################# OOF ##################
        public static string GetOOFStatus()
        {
            string query = "Select TotalOFFCount from GetOOFStatus";
            var cmd = new SqlCommand(query, GetSqlConnection());
            SqlDataReader rdr = cmd.ExecuteReader();
            int count = 0;
            while (rdr.Read())
            {
                count = rdr.GetInt32(0);
            }
            string response = "{\"Floor_Id\": \"Shop Floor I\", \"OOF\": [{\"Date\": #DATE#, \"Count\": #COUNT#}]}";
            Console.WriteLine("Result of oeUTT >>>>" + count);
            rdr.Close();
            return response.Replace("#DATE#", DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString()).Replace("#COUNT#", count.ToString());
            //return count;

        }

        // ################# Operator Efficiency ##################
        public static string getOperatorEfficiency()
        {
            string query = " Select OE_TpCT,OE_UTT from getOperatorEfficiency";
            var cmd = new SqlCommand(query, GetSqlConnection());
            SqlDataReader rdr = cmd.ExecuteReader();
            //int count = 0;
            int oeTpCT = 0;
            int oeUTT = 0;
            while (rdr.Read())
            {
                oeTpCT = rdr.GetInt32(0);
                oeUTT = rdr.GetInt32(1);
            }

            //int count = 0;

            //while (rdr.Read())
            //{
            /*
            oeTpCT = cmd.Parameters["@OE_TpCT"].Value.ToString();
            oeUTT = cmd.Parameters["@OE_UTT"].Value.ToString();

            //}
            if (oeTpCT.Trim().Length == 0) oeTpCT = "0";
            if (oeUTT.Trim().Length == 0) oeUTT = "0";
            //return oeTpCT;
            */
            string response = "{\"Plant\": \"Berea 1\", \"OperatorEfficiency\": {\"CycleTime\": #CYCLETIME#, \"ActualTime\": #COUNT#}}";
            rdr.Close();
            //string response = "{\"Floor_Id\": \"Shop Floor I\", \"OOF\": [{\"Date\": #DATE#, \"Count\": #COUNT#}]}";
            return response.Replace("#CYCLETIME#", oeTpCT.ToString()).Replace("#COUNT#", oeUTT.ToString());
            //Console.WriteLine("Result of oeTpCT >>>>" + oeTpCT);
            //Console.WriteLine("Result of oeUTT >>>>" + oeUTT);

        }
        // ################# Work station  Efficiency ##################
        public static string getWorkstationEfficiency()
        {
            string query = " select WE_within,WE_exceed from getWorkstationEfficiency";
            var cmd = new SqlCommand(query, GetSqlConnection());
            SqlDataReader rdr = cmd.ExecuteReader();
            //int count = 0;
            int WE_within = 0;
            int WE_exceed = 0;
            while (rdr.Read())
            {
                WE_within = rdr.GetInt32(0);
                WE_exceed = rdr.GetInt32(1);
            }


            string response = "{\"Plant\": \"Berea 1\",  \"TotalEventsWithinCycletTime\": #WE_within#, \"TotalEventsExceedingCycletTime\": #WE_exceed#, \"EfficiencyTrend\": [{\"Date\": #DATE#, \"TotalEventsWithinCycletTime\": #WE_within#, \"TotalEventsExceedingCycletTime\": #WE_exceed#}]}";
            rdr.Close();
            return response.Replace("#DATE#", DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString()).Replace("#WE_within#", WE_within.ToString()).Replace("#WE_exceed#", WE_exceed.ToString());
        }

        // ################# Work Unit Status ##################

        public static string getWorkUnitStatus()
        {
            string query = "select WUS_TpCT,WUS_UTT from getWorkUnitStatus";
            var cmd = new SqlCommand(query, GetSqlConnection());
            SqlDataReader rdr = cmd.ExecuteReader();
            int WUS_TpCT = 0;
            int WUS_UTT = 0;
            while (rdr.Read())
            {
                WUS_TpCT = rdr.GetInt32(0);
                WUS_UTT = rdr.GetInt32(1);
            }

            string response = "{\"Plant\": \"Berea 1\", \"WorkUnitStatus\": [{\"WorkUnitId\": \"20654\", \"TPCT\": #WUS_TpCT#, \"UTT\": #WUS_UTT#}]}";
            rdr.Close();
            return response.Replace("#WUS_TpCT#", WUS_TpCT.ToString()).Replace("#WUS_UTT#", WUS_UTT.ToString());

        }




        // ################# Operator Efficiency List ##################
        public static string getOperatorEfficiencyList()
        {
            string query = "select clocknumber,TimeTaken,TimePlanned from getOperatorEfficiencyList";
            var cmd = new SqlCommand(query, GetSqlConnection());

            SqlDataReader rdr = cmd.ExecuteReader();
            string clocknumber = "";
            string TimeTaken = "";
            string TimePlanned = "";

            while (rdr.Read())
            {
                //clocknumber = rdr.GetInt32(0);
                clocknumber = rdr["clocknumber"].ToString();
                TimeTaken = rdr["TimeTaken"].ToString();
                TimePlanned = rdr["TimePlanned"].ToString();
            }
            string response = "{\"Plant\": \"Berea 1\", \"TotalOperatorsWithinTPCT\":#TimePlanned#,\"TotalOperatorsExceedingTPCT\":#TimeTaken#," +
                "\"OperatorsInformation\": [{\"Id\": #clocknumber#, \"FirstName\": #clocknumber#,\"Telephone\": \"+1-541-754-3017\",\"CycleTime\": #TimePlanned#,\"ActualTime\": #TimeTaken#,\"Efficiency\": 84}]}";
            rdr.Close();
            return response.Replace("#clocknumber#", clocknumber.ToString()).Replace("#TimePlanned#", TimePlanned.ToString()).Replace("#TimeTaken#", TimeTaken.ToString());


        }



        // ################# All Workorder   ##################
        public static string getAllHYGWorkOrder()
        {
            string query = "select *from getAllHYGWorkOrder order by WorkOrderId";
            string response = "[";
            var cmd = new SqlCommand(query, GetSqlConnection());

            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                response += "{\"workorder_id\" : \"" +
                     (rdr.IsDBNull(rdr.GetOrdinal("WorkOrderId")) ? "\"\"" : rdr["WorkOrderId"].ToString()) + "\"," +
                     "\"WorkStation" + "\":{\"workstationId\" : \"" + (rdr.IsDBNull(rdr.GetOrdinal("WorkStationID")) ? "\"\"" : rdr["WorkStationID"].ToString()) + "\"," +
                     "\"WorkStationDescription\" : \"" + (rdr.IsDBNull(rdr.GetOrdinal("workstationDescription")) ? "\"\"" : rdr["workstationDescription"].ToString()) + "\"," +
                     "\"Zone" + "\":{\"ZoneId\" : \"" + (rdr.IsDBNull(rdr.GetOrdinal("Zone")) ? "\"\"" : rdr["Zone"].ToString()) + "\"," +
                     "\"ZoneDescription\" : \"" + (rdr.IsDBNull(rdr.GetOrdinal("Zone")) ? "\"\"" : rdr["Zone"].ToString()) + "\"}}," +
                     "\"WorkUnit" + "\":{\"WorkUnitId\" : \"" + (rdr.IsDBNull(rdr.GetOrdinal("WorkUnit_Id")) ? "\"\"" : rdr["WorkUnit_Id"].ToString()) + "\"," +
                     "\"WorkUnitDescription\" : \"" + (rdr.IsDBNull(rdr.GetOrdinal("WorkUnit_Id")) ? "\"\"" : rdr["WorkUnit_Id"].ToString()) + "\"," +
                     "\"ScheduledDate\" : \"" + (rdr.IsDBNull(rdr.GetOrdinal("WorkUnit_Id")) ? "\"\"" : rdr["WorkUnit_Id"].ToString()) + "\"}," +
                     "\"Process" + "\":{\"ProcessId\" : \"" + (rdr.IsDBNull(rdr.GetOrdinal("Process_Id")) ? "\"\"" : rdr["Process_Id"].ToString()) + "\"," +
                     "\"ProcessDescription\" : \"" + (rdr.IsDBNull(rdr.GetOrdinal("Process_Id")) ? "\"\"" : rdr["Process_Id"].ToString()) + "\"}," +
                     "\"SOE" + "\":{\"SOE_Code\" : " + (rdr.IsDBNull(rdr.GetOrdinal("SOE_Code")) ? "\"\"" : "\"" + rdr["SOE_Code"].ToString() + "\"") + "," +
                     "\"SOEDescription\" : " + (rdr.IsDBNull(rdr.GetOrdinal("SOE_Code")) ? "\"\"" : "\"" + rdr["SOE_Code"].ToString() + "\"") + "}," +
                     "\"Event" + "\":{\"Event_Number\" : \"" + (rdr.IsDBNull(rdr.GetOrdinal("Event_Number")) ? "\"\"" : rdr["Event_Number"].ToString()) + "\"," +
                     "\"EventDescription\" : \"" + (rdr.IsDBNull(rdr.GetOrdinal("eventDescription")) ? "\"\"" : rdr["eventDescription"].ToString()) + "\"," +
                     "\"EventTime\" : \"" + (rdr.IsDBNull(rdr.GetOrdinal("eventTime")) ? "\"\"" : rdr["eventTime"].ToString()) + "\"}," +
                     "\"Operation" + "\":{\"Operation_Reference\" : " + (rdr.IsDBNull(rdr.GetOrdinal("Operation_Reference")) ? "\"\"" : "\"" + rdr["Operation_Reference"].ToString() + "\"") + "," +
                      "\"OperationDescription\" : " + (rdr.IsDBNull(rdr.GetOrdinal("Operation_Reference")) ? "\"\"" : "\"" + rdr["Operation_Reference"].ToString() + "\"") + "}," +
                      "\"FlowLine" + "\":{\"FlowLine_Id\" : \"" + (rdr.IsDBNull(rdr.GetOrdinal("FlowLine_Id")) ? "\"\"" : rdr["FlowLine_Id"].ToString()) + "\"," +
                     "\"FlowLineDescription\" : \"" + (rdr.IsDBNull(rdr.GetOrdinal("FlowLine_Id")) ? "\"\"" : rdr["FlowLine_Id"].ToString()) + "\"}," +
                      "\"Part" + "\":{\"PartID\" : " + (rdr.IsDBNull(rdr.GetOrdinal("partid")) ? "\"\"" : "\"" + rdr["partid"].ToString() + "\"") + "," +
                      "\"PartDescription\" : " + (rdr.IsDBNull(rdr.GetOrdinal("partid")) ? "\"\"" : "\"" + rdr["partid"].ToString() + "\"") + "}," +
                     "\"StartDateTime\" : \"" +
                     (rdr.IsDBNull(rdr.GetOrdinal("start_DATE")) ? "\"\"" : rdr["start_DATE"].ToString()) + "\"," +
                     "\"PlannedEndDateTime\" : \"" +
                     (rdr.IsDBNull(rdr.GetOrdinal("planned_DATE")) ? "\"\"" : rdr["planned_DATE"].ToString()) + "\"," +
                     "\"ActualEndDateTime\" : " + (rdr.IsDBNull(rdr.GetOrdinal("actual_DATE")) ? "\"\"" : "\"" + rdr["actual_DATE"].ToString() + "\"") + "," +
                     "\"OOF_Reason\" : " + (rdr.IsDBNull(rdr.GetOrdinal("OOF_Reason")) ? "\"\"" : "\"" + rdr["OOF_Reason"].ToString() + "\"") + "," +
                     "\"FeederStationId\" : " + (rdr.IsDBNull(rdr.GetOrdinal("FeederStationId")) ? "\"\"" : "\"" + rdr["FeederStationId"].ToString() + "\"") + "," +
                      "\"RecordEntryDateTime\" :" + (rdr.IsDBNull(rdr.GetOrdinal("RecordEntry_DATE")) ? "\"\"" : "\"" + rdr["RecordEntry_DATE"].ToString() + "\"") + "," +
                     "\"OrderStatus\" : " + (rdr.IsDBNull(rdr.GetOrdinal("OrderStatus")) ? "\"\"" : "\"" + rdr["OrderStatus"].ToString() + "\"");


                //.....
                //.....
                response += "},";
            }
            response = response.Remove(response.Length - 1) + "]";
            rdr.Close();
            return response;
        }

        // Load Data from Blob

        public static void LoadDatafromBlob(Stream path)
        {
            using (var reader = new StreamReader(path))

            //            using (var reader = new StreamReader(@"C:\Users\202828\Documents\001\HYGINPUTFILE.csv"))
            {
                bool header = true;
                SqlConnection conn = GetSqlConnection();
                int idx = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (header)
                    {
                        header = false;
                        continue;
                    }

                    string query = "[dbo].[hyg_workorder_insert_update_Test_1]";
                    SqlCommand SqlCommand = new SqlCommand(query, GetSqlConnection());
                    var cmd = SqlCommand;
                    //cmd.CommandText = "workorder_insert_update";
                    cmd.CommandType = CommandType.StoredProcedure;

                    var values = line.Split('\t');
                    idx = 0;

                    var value = getTrimmedValue(values[idx++]);


                    if (value != null)
                    {
                        cmd.Parameters.Add("@Process_Id", SqlDbType.VarChar, 50).Value = value;  //ProcessID                     
                    }
                    else
                    {
                        cmd.Parameters.Add("@Process_Id", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    }

                    value = getTrimmedValue(values[idx++]);
                    if (value != null)
                    {
                        cmd.Parameters.Add("@Event_Number", SqlDbType.VarChar, 50).Value = value;  //OperationID
                    }
                    else
                    {
                        cmd.Parameters.Add("@Event_Number", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    }
                    value = getTrimmedValue(values[idx++]);
                    if (value != null)
                    {
                        cmd.Parameters.Add("@Build_Id", SqlDbType.VarChar, 50).Value = value;  //BuildID
                    }
                    else
                    {
                        cmd.Parameters.Add("@Build_Id", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    }
                    value = getTrimmedValue(values[idx++]);
                    if (value != null)
                    {
                        cmd.Parameters.Add("@WorkUnit_Id", SqlDbType.VarChar, 50).Value = value;  //WUnit
                    }
                    else
                    {
                        cmd.Parameters.Add("@WorkUnit_Id", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    }
                    value = getTrimmedValue(values[idx++]);
                    if (value != null)
                    {
                        cmd.Parameters.Add("@WorkStationID", SqlDbType.VarChar, 50).Value = value;  //StationID
                    }
                    else
                    {
                        cmd.Parameters.Add("@WorkStationID", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    }
                    value = getTrimmedValue(values[idx++]);
                    if (value != null)
                    {
                        cmd.Parameters.Add("@ClockNumber", SqlDbType.VarChar, 50).Value = value;  //ClockNumber
                    }
                    else
                    {
                        cmd.Parameters.Add("@ClockNumber", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    }
                    value = getTrimmedValue(values[idx++]);
                    if (value != null)
                    {
                        cmd.Parameters.Add("@StartDateTime", SqlDbType.DateTime).Value = value;  //ScanInTime
                    }
                    else
                    {
                        cmd.Parameters.Add("@StartDateTime", SqlDbType.DateTime).Value = DBNull.Value;
                    }
                    value = getTrimmedValue(values[idx++]);
                    if (value != null)
                    {
                        cmd.Parameters.Add("@ActualEndDateTime", SqlDbType.DateTime).Value = value;  //ScanOutTime
                    }
                    else
                    {
                        cmd.Parameters.Add("@ActualEndDateTime", SqlDbType.DateTime).Value = DBNull.Value;
                    }
                    //cmd.Parameters.Add("@Event_Number", SqlDbType.VarChar,50).Value = getTrimmedValue(values[idx++]);   //OperationID
                    //cmd.Parameters.Add("@Build_Id", SqlDbType.VarChar,50).Value = getTrimmedValue(values[idx++]); ;   //BuildID
                    //cmd.Parameters.Add("@WorkUnit_Id", SqlDbType.VarChar,50).Value = getTrimmedValue(values[idx++]);  //WUnit
                    //cmd.Parameters.Add("@WorkStationID", SqlDbType.VarChar,50).Value = getTrimmedValue(values[idx++]); ; //StationID
                    //cmd.Parameters.Add("@ClockNumber", SqlDbType.VarChar,50).Value = getTrimmedValue(values[idx++]); ;  //ClockNumber

                    //cmd.Parameters.Add("@StartDateTime", SqlDbType.VarChar,50).Value = getTrimmedValue(values[idx++]); ;  //ScanInTime
                    //cmd.Parameters.Add("@ActualEndDateTime", SqlDbType.VarChar,50).Value = getTrimmedValue(values[idx++]); ;  //ScanOutTime
                    //cmd.Parameters.Add("@IsOOF", SqlDbType.VarChar,50).Value = values[idx++];  //RedirectedAt
                    //string redir = getTrimmedValue(values[idx++]);
                    value = getTrimmedValue(values[idx++]);
                    if (value != null && "0".Equals(value.Trim()))

                    // if("1".Equals(value.Trim()))
                    {
                        cmd.Parameters.Add("@IsOOF", SqlDbType.VarChar, 50).Value = "N";  //RedirectedAt
                    }
                    else
                    {
                        cmd.Parameters.Add("@IsOOF", SqlDbType.VarChar, 50).Value = "Y";  //RedirectedAt
                    }
                    value = getTrimmedValue(values[idx++]);
                    if (value != null)
                    {
                        cmd.Parameters.Add("@Plant", SqlDbType.VarChar, 50).Value = value;  //Plant
                    }
                    else
                    {
                        cmd.Parameters.Add("@Plant", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    }
                    value = getTrimmedValue(values[idx++]);
                    if (value != null)
                    {
                        cmd.Parameters.Add("@FlowLine_Id", SqlDbType.VarChar, 50).Value = value;  //FlowLine
                    }
                    else
                    {
                        cmd.Parameters.Add("@FlowLine_Id", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    }
                    value = getTrimmedValue(values[idx++]);
                    if (value != null)
                    {
                        cmd.Parameters.Add("@SchDate", SqlDbType.Date).Value = value;  //ScheduleTime
                    }
                    else
                    {
                        cmd.Parameters.Add("@SchDate", SqlDbType.Date).Value = DBNull.Value;
                    }
                    value = getTrimmedValue(values[idx++]);
                    if (value != null)
                    {
                        cmd.Parameters.Add("@PlannedTime", SqlDbType.VarChar, 50).Value = value;  //PlannedTime
                    }
                    else
                    {
                        cmd.Parameters.Add("@PlannedTime", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    }
                    //cmd.Parameters.Add("@Plant", SqlDbType.VarChar,50).Value = values[idx++];  //Plant
                    //cmd.Parameters.Add("@FlowLine_Id", SqlDbType.VarChar,50).Value = values[idx++];  //FlowLine
                    //cmd.Parameters.Add("@PlannedTime", SqlDbType.VarChar,50).Value = values[idx++];  //PlannedTime

                    cmd.Parameters.Add("@FeederStationId", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    cmd.Parameters.Add("@SOE_Code", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    cmd.Parameters.Add("@partId", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    cmd.Parameters.Add("@Operation_Reference", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    //cmd.Parameters.Add("@Event_Number", SqlDbType.VarChar,50).Value = DBNull.Value;
                    cmd.Parameters.Add("@OOF_Reason", SqlDbType.VarChar, 50).Value = DBNull.Value;

                    //cmd.Parameters.Add("@ResponseStatus", SqlDbType.VarChar,50).Value = DBNull.Value;
                    //cmd.Parameters.Add("@ResponseStatus", SqlDbType.VarChar,50).Value = DBNull.Value;

                    cmd.Parameters.Add(new SqlParameter("@ResponseStatus", SqlDbType.VarChar, 50));
                    cmd.Parameters["@ResponseStatus"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(new SqlParameter("@EventType", SqlDbType.VarChar, 50));
                    cmd.Parameters["@EventType"].Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    System.Threading.Thread.Sleep(DATA_INSERT_INTERVAL);


                }
            }
        }





        public static void LoadData(String path)
        {
            using (var reader = new StreamReader(path))

            //            using (var reader = new StreamReader(@"C:\Users\202828\Documents\001\HYGINPUTFILE.csv"))
            {
                bool header = true;
                SqlConnection conn = GetSqlConnection();
                int idx = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (header)
                    {
                        header = false;
                        continue;
                    }

                    string query = "[dbo].[hyg_workorder_insert_update_Test_1]";
                    SqlCommand SqlCommand = new SqlCommand(query, GetSqlConnection());
                    var cmd = SqlCommand;
                    //cmd.CommandText = "workorder_insert_update";
                    cmd.CommandType = CommandType.StoredProcedure;

                    var values = line.Split('\t');
                    idx = 0;

                    var value = getTrimmedValue(values[idx++]);


                    if (value != null)
                    {
                        cmd.Parameters.Add("@Process_Id", SqlDbType.VarChar, 50).Value = value;  //ProcessID                     
                    }
                    else
                    {
                        cmd.Parameters.Add("@Process_Id", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    }

                    value = getTrimmedValue(values[idx++]);
                    if (value != null)
                    {
                        cmd.Parameters.Add("@Event_Number", SqlDbType.VarChar, 50).Value = value;  //OperationID
                    }
                    else
                    {
                        cmd.Parameters.Add("@Event_Number", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    }
                    value = getTrimmedValue(values[idx++]);
                    if (value != null)
                    {
                        cmd.Parameters.Add("@Build_Id", SqlDbType.VarChar, 50).Value = value;  //BuildID
                    }
                    else
                    {
                        cmd.Parameters.Add("@Build_Id", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    }
                    value = getTrimmedValue(values[idx++]);
                    if (value != null)
                    {
                        cmd.Parameters.Add("@WorkUnit_Id", SqlDbType.VarChar, 50).Value = value;  //WUnit
                    }
                    else
                    {
                        cmd.Parameters.Add("@WorkUnit_Id", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    }
                    value = getTrimmedValue(values[idx++]);
                    if (value != null)
                    {
                        cmd.Parameters.Add("@WorkStationID", SqlDbType.VarChar, 50).Value = value;  //StationID
                    }
                    else
                    {
                        cmd.Parameters.Add("@WorkStationID", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    }
                    value = getTrimmedValue(values[idx++]);
                    if (value != null)
                    {
                        cmd.Parameters.Add("@ClockNumber", SqlDbType.VarChar, 50).Value = value;  //ClockNumber
                    }
                    else
                    {
                        cmd.Parameters.Add("@ClockNumber", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    }
                    value = getTrimmedValue(values[idx++]);
                    if (value != null)
                    {
                        cmd.Parameters.Add("@StartDateTime", SqlDbType.DateTime).Value = value;  //ScanInTime
                    }
                    else
                    {
                        cmd.Parameters.Add("@StartDateTime", SqlDbType.DateTime).Value = DBNull.Value;
                    }
                    value = getTrimmedValue(values[idx++]);
                    if (value != null)
                    {
                        cmd.Parameters.Add("@ActualEndDateTime", SqlDbType.DateTime).Value = value;  //ScanOutTime
                    }
                    else
                    {
                        cmd.Parameters.Add("@ActualEndDateTime", SqlDbType.DateTime).Value = DBNull.Value;
                    }
                    //cmd.Parameters.Add("@Event_Number", SqlDbType.VarChar,50).Value = getTrimmedValue(values[idx++]);   //OperationID
                    //cmd.Parameters.Add("@Build_Id", SqlDbType.VarChar,50).Value = getTrimmedValue(values[idx++]); ;   //BuildID
                    //cmd.Parameters.Add("@WorkUnit_Id", SqlDbType.VarChar,50).Value = getTrimmedValue(values[idx++]);  //WUnit
                    //cmd.Parameters.Add("@WorkStationID", SqlDbType.VarChar,50).Value = getTrimmedValue(values[idx++]); ; //StationID
                    //cmd.Parameters.Add("@ClockNumber", SqlDbType.VarChar,50).Value = getTrimmedValue(values[idx++]); ;  //ClockNumber

                    //cmd.Parameters.Add("@StartDateTime", SqlDbType.VarChar,50).Value = getTrimmedValue(values[idx++]); ;  //ScanInTime
                    //cmd.Parameters.Add("@ActualEndDateTime", SqlDbType.VarChar,50).Value = getTrimmedValue(values[idx++]); ;  //ScanOutTime
                    //cmd.Parameters.Add("@IsOOF", SqlDbType.VarChar,50).Value = values[idx++];  //RedirectedAt
                    //string redir = getTrimmedValue(values[idx++]);
                    value = getTrimmedValue(values[idx++]);
                    if (value != null && "0".Equals(value.Trim()))

                    // if("1".Equals(value.Trim()))
                    {
                        cmd.Parameters.Add("@IsOOF", SqlDbType.VarChar, 50).Value = "N";  //RedirectedAt
                    }
                    else
                    {
                        cmd.Parameters.Add("@IsOOF", SqlDbType.VarChar, 50).Value = "Y";  //RedirectedAt
                    }
                    value = getTrimmedValue(values[idx++]);
                    if (value != null)
                    {
                        cmd.Parameters.Add("@Plant", SqlDbType.VarChar, 50).Value = value;  //Plant
                    }
                    else
                    {
                        cmd.Parameters.Add("@Plant", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    }
                    value = getTrimmedValue(values[idx++]);
                    if (value != null)
                    {
                        cmd.Parameters.Add("@FlowLine_Id", SqlDbType.VarChar, 50).Value = value;  //FlowLine
                    }
                    else
                    {
                        cmd.Parameters.Add("@FlowLine_Id", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    }
                    value = getTrimmedValue(values[idx++]);
                    if (value != null)
                    {
                        cmd.Parameters.Add("@SchDate", SqlDbType.Date).Value = value;  //ScheduleTime
                    }
                    else
                    {
                        cmd.Parameters.Add("@SchDate", SqlDbType.Date).Value = DBNull.Value;
                    }
                    value = getTrimmedValue(values[idx++]);
                    if (value != null)
                    {
                        cmd.Parameters.Add("@PlannedTime", SqlDbType.VarChar, 50).Value = value;  //PlannedTime
                    }
                    else
                    {
                        cmd.Parameters.Add("@PlannedTime", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    }
                    //cmd.Parameters.Add("@Plant", SqlDbType.VarChar,50).Value = values[idx++];  //Plant
                    //cmd.Parameters.Add("@FlowLine_Id", SqlDbType.VarChar,50).Value = values[idx++];  //FlowLine
                    //cmd.Parameters.Add("@PlannedTime", SqlDbType.VarChar,50).Value = values[idx++];  //PlannedTime

                    cmd.Parameters.Add("@FeederStationId", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    cmd.Parameters.Add("@SOE_Code", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    cmd.Parameters.Add("@partId", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    cmd.Parameters.Add("@Operation_Reference", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    //cmd.Parameters.Add("@Event_Number", SqlDbType.VarChar,50).Value = DBNull.Value;
                    cmd.Parameters.Add("@OOF_Reason", SqlDbType.VarChar, 50).Value = DBNull.Value;

                    //cmd.Parameters.Add("@ResponseStatus", SqlDbType.VarChar,50).Value = DBNull.Value;
                    //cmd.Parameters.Add("@ResponseStatus", SqlDbType.VarChar,50).Value = DBNull.Value;

                    cmd.Parameters.Add(new SqlParameter("@ResponseStatus", SqlDbType.VarChar, 50));
                    cmd.Parameters["@ResponseStatus"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(new SqlParameter("@EventType", SqlDbType.VarChar, 50));
                    cmd.Parameters["@EventType"].Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    System.Threading.Thread.Sleep(DATA_INSERT_INTERVAL);


                }
            }
        }

        private static string getTrimmedValue(string value)
        {
            return value != null && !"NULL".Equals(value.Trim().ToUpper()) ? value.Trim() : null;
        }
    }
}