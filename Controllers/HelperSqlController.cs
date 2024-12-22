
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.Json;
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
    class HelperSql
    {
        // Connecting to Azure SQL Server // 
        private static SqlConnection SqlConnection = null;
        private static SqlConnection GetSqlConnection()
        {
            if (SqlConnection == null)
            {
                using (SqlConnection conn = new SqlConnection())
                {

                    
                }
            }

            if (SqlConnection.State != System.Data.ConnectionState.Open)
            {
                SqlConnection.Open();
            }
            return SqlConnection;
            //SqlConnection.Close();
        }


        public class Plant_KPI
        {
            public string Plant_Name { get; set; }
            public decimal Machine_Speed { get; set; }
            public float Total_MachineCount { get; set; }
            public float DeclareBags { get; set; }
            public decimal Uptime_Percentage { get; set; }
            public decimal OEE { get; set; }
            public int Greater_TargetMachines { get; set; }
            public int Lesser_TargetMachines { get; set; }
        }




        public class WorkcellView
        {
            public string Plant_Name { get; set; }
            public string WorkCell_Name { get; set; }
            public string Machine_Id { get; set; }
            public string Machine_Status { get; set; }
            public decimal Machine_Speed { get; set; }
            public float DeclareBags { get; set; }
            public decimal Uptime_Percentage { get; set; }
            public decimal OEE { get; set; }

        }
        
        public class MachineView
        {
            public string Plant_Name { get; set; }

            public string Machine_Id { get; set; }
            public string ShiftNumber { get; set; }
            public decimal Machine_Speed { get; set; }
            public decimal Uptime_Percentage { get; set; }
            public decimal OEE { get; set; }

        }


        public class shiftView
        {
            public string Plant_Name { get; set; }
            public string Shiftwise { get; set; }
            public decimal Machine_speed { get; set; }
            public decimal Uptime_Percentage { get; set; }
            public decimal OEE { get; set; }

        }


        public static string PlantExperimental()
        {
            List<Plant_KPI> Plant_KPI = new List<Plant_KPI>();
            //string Exec_Sp_Ms1 = "select *from [dbo].[Plant_WebJob_24hr]; ";
            string Exec_Sp_Ms1 = "select *from Pregis.vPlantData_KPI ;";
            var cmd7 = new SqlCommand(Exec_Sp_Ms1, GetSqlConnection());
            SqlDataReader rdr7 = cmd7.ExecuteReader();
            while (rdr7.Read())
            {
                Plant_KPI.Add(new Plant_KPI()
                {

                    Plant_Name = rdr7["Plant_Name"].ToString(),
                    Total_MachineCount = int.Parse(rdr7["Total_MachineCount"].ToString()),
                    Greater_TargetMachines = int.Parse(rdr7["GreaterTarget_Machines"].ToString()),
                    Lesser_TargetMachines = int.Parse(rdr7["LesserTarget_Machines"].ToString()),
                    Machine_Speed = decimal.Parse(rdr7["Machine_Speed"].ToString()),
                    DeclareBags = float.Parse(rdr7["DeclareBags"].ToString()),
                    OEE = decimal.Parse(rdr7["OEE"].ToString()),
                    Uptime_Percentage = decimal.Parse(rdr7["Uptime_Percentage"].ToString())

                });


            }
            var json = System.Text.Json.JsonSerializer.Serialize(new { Plant_KPI }, new JsonSerializerOptions() { WriteIndented = true });

            rdr7.Close();
            return json;
        }




        public static string Workcellsummary()
        {

            List<WorkcellView> WorkcellView = new List<WorkcellView>();

            string query2 = "select Plant_Name,Workcell_Name,Machine_Id,Machine_Status,Machine_Speed,DeclareBags,Uptime_Percentage,OEE from Pregis.vWorkCellData_KPI;";

            var cmd2 = new SqlCommand(query2, GetSqlConnection());
            SqlDataReader rdr2 = cmd2.ExecuteReader();

            //int i = 0;
            while (rdr2.Read())
            {

                WorkcellView.Add(new WorkcellView()
                {
                    Plant_Name = rdr2["Plant_Name"].ToString(),
                    WorkCell_Name = rdr2["WorkCell_Name"].ToString(),
                    Machine_Id = rdr2["Machine_Id"].ToString(),
                    Machine_Status = rdr2["Machine_Status"].ToString(),
                    Machine_Speed = decimal.Parse(rdr2["Machine_Speed"].ToString()),
                    DeclareBags = float.Parse(rdr2["DeclareBags"].ToString()),
                    Uptime_Percentage = decimal.Parse(rdr2["Uptime_Percentage"].ToString()),
                    OEE = decimal.Parse(rdr2["OEE"].ToString())

                });
            }
            var json = System.Text.Json.JsonSerializer.Serialize(new { WorkcellView }, new JsonSerializerOptions() { WriteIndented = true });

            rdr2.Close();
            return json;


        }


        public static string PlantShiftsummary()

        {
            List<shiftView> shiftView = new List<shiftView>();

            string query3 = "select *from Pregis.vPlantShiftData_KPI;";
            var cmd3 = new SqlCommand(query3, GetSqlConnection());
            SqlDataReader rdr3 = cmd3.ExecuteReader();

            //int i = 0;
            while (rdr3.Read())
            {
                shiftView.Add(new shiftView()
                {
                    Plant_Name = rdr3["Plant_Name"].ToString(),
                    Shiftwise = rdr3["ShiftNumber"].ToString(),
                    Machine_speed = decimal.Parse(rdr3["Machine_speed"].ToString()),
                    Uptime_Percentage = decimal.Parse(rdr3["Uptime_Percentage"].ToString()),
                    OEE = decimal.Parse(rdr3["OEE"].ToString())

                });
            }
                var json = System.Text.Json.JsonSerializer.Serialize(new { shiftView }, new JsonSerializerOptions() { WriteIndented = true });

                rdr3.Close();
                return json;
            
        }


        public static string MachineShiftsummary()
        {

            List<MachineView> MachineView = new List<MachineView>();

            string query4 = "select *from Pregis.vMachineShiftData_KPI;";
            var cmd4 = new SqlCommand(query4, GetSqlConnection());
            SqlDataReader rdr4 = cmd4.ExecuteReader();

            while (rdr4.Read())
            {
                MachineView.Add(new MachineView()
                {
                    Plant_Name = rdr4["Plant_Name"].ToString(),
                    Machine_Id = rdr4["Machine_Id"].ToString(),
                    ShiftNumber = rdr4["ShiftNumber"].ToString(),
                    Machine_Speed = decimal.Parse(rdr4["Machine_speed"].ToString()),
                    Uptime_Percentage = decimal.Parse(rdr4["Uptime_Percentage"].ToString()),
                    OEE = decimal.Parse(rdr4["OEE"].ToString())

                });
            }
                var json = System.Text.Json.JsonSerializer.Serialize(new { MachineView }, new JsonSerializerOptions() { WriteIndented = true });

                rdr4.Close();
                return json;
            }
















        }
}
