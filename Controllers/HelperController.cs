

/***********************************************************************************************************************************************/
/*
 Project Name : SmartOps phase2
 Version      : 1.1
 Language Used: C#
 Devoloped by : Avinash
 Created as of: 17/05/2022
 Module       : Helper Controller
 
/***********************************************************************************************************************************************/


// import libraries,dependence packages,libraries//



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
    class Helper
    {
        // Connecting to Azure SQL Server // 
        private static SqlConnection SqlConnection = null;
        private static SqlConnection GetSqlConnection()
        {
            if (SqlConnection == null)
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    
                    SqlConnection = new SqlConnection(@"Server=tcp:ae-smartops-covtest.database.windows.net,1433;Initial Catalog=Smart_Mfgs;Persist Security Info=False;User ID=dbadmin;Password=MqVZtYFumuPNQ8uu;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                }
            }

            if (SqlConnection.State != System.Data.ConnectionState.Open)
            {
                SqlConnection.Open();
            }
            return SqlConnection;
            //SqlConnection.Close();
        }

        //**********Testing all sql queries in one***********// 
        public class shiftView
        {
            public string Plant_Name { get; set; }
            public string Shiftwise { get; set; }
            public decimal Machine_speed { get; set; }
            public decimal Uptime_Percentage { get; set; }
            public decimal OEE { get; set; }

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
        public class PlantView
        {
            public string Plant_Name { get; set; }
            public decimal Machine_Speed { get; set; }
            public float Total_MachineCount { get; set; }
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

        public class Plantmachinespeed
        {
            public string Plant_Name { get; set; }

            public decimal Machine_Speed { get; set; }

        }
        public class Plantmachinecount
        {
            public string Plant_Name { get; set; }

            public float Total_MachineCount { get; set; }

        }
        public class PlantBagcount
        {
            public string Plant_Name { get; set; }
            public string WorkCell_Name { get; set; }
            public string Machine_Id { get; set; }
            public string Machine_Status { get; set; }
            public float DeclareBags { get; set; }

        }
        public class PlantUptime
        {
            public string Plant_Name { get; set; }
            public string WorkCell_Name { get; set; }
            public string Machine_Id { get; set; }
            public string Machine_Status { get; set; }
            public decimal Uptime_Percentage { get; set; }

        }
        public class PlantOEE
        {
            public string Plant_Name { get; set; }
            public string WorkCell_Name { get; set; }
            public string Machine_Id { get; set; }
            public string Machine_Status { get; set; }
            public decimal OEE { get; set; }

        }
        public class WorkcellBagcount
        {
            public string Plant_Name { get; set; }
            public string WorkCell_Name { get; set; }
            public string Machine_Id { get; set; }
            public string Machine_Status { get; set; }
            public decimal Machine_Speed { get; set; }
            public float DeclareBags { get; set; }
            public string Cell { get; set; }

        }
        public class WorkcellUptime
        {
            public string Plant_Name { get; set; }
            public string WorkCell_Name { get; set; }
            public string Machine_Id { get; set; }
            public string Machine_Status { get; set; }
            public decimal Uptime_Percentage { get; set; }
            public string Cell { get; set; }

        }
        public class WorkcellOEE
        {
            public string Plant_Name { get; set; }
            public string WorkCell_Name { get; set; }
            public string Machine_Id { get; set; }
            public string Machine_Status { get; set; }
            public decimal OEE { get; set; }
            public string Cell { get; set; }

        }
        public class WorkcellGroupView
        {
            public string Plant_Name { get; set; }
            public string WorkCell_Name { get; set; }
            public string Machine_Id { get; set; }
            public string Machine_Status { get; set; }
            public decimal Machine_Speed { get; set; }
            public float DeclareBags { get; set; }
            public decimal Uptime_Percentage { get; set; }
            public decimal OEE { get; set; }
            public string Cell { get; set; }

        }

        public class shiftSpeed
        {
            public string Plant_Name { get; set; }
            public string ShiftNumber { get; set; }
            public decimal Machine_speed { get; set; }

        }
        public class shiftUptime
        {
            public string Plant_Name { get; set; }
            public string ShiftNumber { get; set; }
            public decimal Uptime_Percentage { get; set; }

        }
        public class shiftOEE
        {
            public string Plant_Name { get; set; }
            public string ShiftNumber { get; set; }
            public decimal OEE { get; set; }

        }

        public class machineSpeed
        {

            public string Plant_Name { get; set; }
            public string Machine_Id { get; set; }
            public string ShiftNumber { get; set; }
            public decimal Machine_speed { get; set; }

        }
        public class machineUptime
        {
            public string Plant_Name { get; set; }
            public string Machine_Id { get; set; }
            public string ShiftNumber { get; set; }
            public decimal Uptime_Percentage { get; set; }

        }
        public class machineOEE
        {
            public string Plant_Name { get; set; }
            public string Machine_Id { get; set; }
            public string ShiftNumber { get; set; }
            public decimal OEE { get; set; }

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


        public static string PlantExperimental()
        {
            List<Plant_KPI> Plant_KPI = new List<Plant_KPI>();
            //string Exec_Sp_Ms1 = "select *from [dbo].[Plant_WebJob_24hr]; ";
            string Exec_Sp_Ms1 = "exec [Pregis].[Plant_KPI] ;";
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



        public static string Plantsummary()

        {

            List<Plantmachinespeed> Plantmachinespeed = new List<Plantmachinespeed>();
            List<Plantmachinecount> Plantmachinecount = new List<Plantmachinecount>();
            List<PlantBagcount> PlantBagcount = new List<PlantBagcount>();
            List<PlantUptime> PlantUptime = new List<PlantUptime>();
            List<PlantOEE> PlantOEE = new List<PlantOEE>();
            List<PlantView> PlantView = new List<PlantView>();

            string Exec_Sp_Ms = "EXEC [Pregis].Plant_Machine_speed_avg;EXEC [Pregis].Plant_Machine_Count;EXEC [Pregis].Plant_TotalBag_Count;EXEC [Pregis].Plant_UptimePercentage;EXEC [Pregis].Plant_OEEPercentage;";
            var cmd1 = new SqlCommand(Exec_Sp_Ms, GetSqlConnection());
            SqlDataReader rdr1 = cmd1.ExecuteReader();
            while (rdr1.Read())
            {
                // float Machine_Speed = 0;
                Plantmachinespeed.Add(new Plantmachinespeed()
                {

                    Plant_Name = rdr1["Plant_Name"].ToString(),
                    Machine_Speed = decimal.Parse(rdr1["Machine_Speed"].ToString())//;
                });

                Console.WriteLine(Plantmachinespeed);
            }
            rdr1.NextResult();
            while (rdr1.Read())
            {

                Plantmachinecount.Add(new Plantmachinecount()
                {
                    Plant_Name = rdr1["Plant_Name"].ToString(),
                    Total_MachineCount = int.Parse(rdr1["Total_MachineCount"].ToString()),
                });

            }

            rdr1.NextResult();
            while (rdr1.Read())
            {
                PlantBagcount.Add(new PlantBagcount()
                {
                    Plant_Name = rdr1["Plant_Name"].ToString(),
                    DeclareBags = float.Parse(rdr1["DeclareBags"].ToString())
                });

            }
            rdr1.NextResult();
            while (rdr1.Read())
            {
                PlantUptime.Add(new PlantUptime()
                {
                    Plant_Name = rdr1["Plant_Name"].ToString(),
                    Uptime_Percentage = decimal.Parse(rdr1["Uptime_Percentage"].ToString())
                });

            }
            rdr1.NextResult();
            while (rdr1.Read())
            {
                PlantOEE.Add(new PlantOEE()
                {
                    Plant_Name = rdr1["Plant_Name"].ToString(),
                    OEE = decimal.Parse(rdr1["OEE"].ToString())
                });

            }

            var joined =
                       from a in Plantmachinespeed
                       join b in Plantmachinecount
                           on a.Plant_Name equals b.Plant_Name
                       join c in PlantBagcount
                           on a.Plant_Name equals c.Plant_Name
                       join d in PlantUptime
                           on a.Plant_Name equals d.Plant_Name
                       join e in PlantOEE
                          on a.Plant_Name equals e.Plant_Name

                       select new { Plantmachinespeed = a, Plantmachinecount = b, PlantBagcount = c, PlantUptime = d, PlantOEE = e };

            foreach (var record in joined)
                //Console.WriteLine("{0}  {1} {2} {3}", record.PlantSpeed.Plant_Name, record.PlantSpeed, record.MachineUptime, record.BagCount);
                PlantView.Add(new PlantView()
                {
                    Plant_Name = record.Plantmachinespeed.Plant_Name,
                    Machine_Speed = record.Plantmachinespeed.Machine_Speed,
                    Total_MachineCount = record.Plantmachinecount.Total_MachineCount,
                    DeclareBags = record.PlantBagcount.DeclareBags,
                    Uptime_Percentage = record.PlantUptime.Uptime_Percentage,
                    OEE = record.PlantOEE.OEE

                });

            var json = System.Text.Json.JsonSerializer.Serialize(new { PlantView }, new JsonSerializerOptions() { WriteIndented = true });

            rdr1.Close();
            return json;
        }

        public static string Workcellsummary()
        {
            List<WorkcellBagcount> WorkcellBagcount = new List<WorkcellBagcount>();
            List<WorkcellUptime> WorkcellUptime = new List<WorkcellUptime>();
            List<WorkcellOEE> WorkcellOEE = new List<WorkcellOEE>();
            List<WorkcellView> WorkcellView = new List<WorkcellView>();

            string query2 = @"EXEC [Pregis].WC_MachineSpeed_DecBags;EXEC [Pregis].WC_Uptime;EXEC [Pregis].WC_OEE";


            var cmd2 = new SqlCommand(query2, GetSqlConnection());
            SqlDataReader rdr2 = cmd2.ExecuteReader();

            //int i = 0;
            while (rdr2.Read())
            {

                WorkcellBagcount.Add(new WorkcellBagcount()
                {

                    Plant_Name = rdr2["Plant_Name"].ToString(),
                    WorkCell_Name = rdr2["WorkCell_Name"].ToString(),
                    Machine_Id = rdr2["Machine_Id"].ToString(),
                    Machine_Status = rdr2["Machine_Status"].ToString(),
                    Machine_Speed = decimal.Parse(rdr2["Machine_Speed"].ToString()),
                    DeclareBags = float.Parse(rdr2["DeclareBags"].ToString())//;
                });
                Console.WriteLine(WorkcellBagcount);
            }
            rdr2.NextResult();
            while (rdr2.Read())
            {

                WorkcellUptime.Add(new WorkcellUptime()
                {

                    Plant_Name = rdr2["Plant_Name"].ToString(),
                    WorkCell_Name = rdr2["WorkCell_Name"].ToString(),
                    Machine_Id = rdr2["Machine_Id"].ToString(),
                    Machine_Status = rdr2["Machine_Status"].ToString(),
                    Uptime_Percentage = decimal.Parse(rdr2["Uptime_Percentage"].ToString())//;
                });
                Console.WriteLine(WorkcellUptime);
            }
            rdr2.NextResult();
            while (rdr2.Read())
            {

                WorkcellOEE.Add(new WorkcellOEE()
                {

                    Plant_Name = rdr2["Plant_Name"].ToString(),
                    WorkCell_Name = rdr2["WorkCell_Name"].ToString(),
                    Machine_Id = rdr2["Machine_Id"].ToString(),
                    Machine_Status = rdr2["Machine_Status"].ToString(),
                    OEE = decimal.Parse(rdr2["OEE"].ToString())//;
                });
                Console.WriteLine(WorkcellOEE);
            }
            var joined =
                       from a in WorkcellBagcount
                       join b in WorkcellUptime
                           on (a.Machine_Id, a.Plant_Name, a.WorkCell_Name, a.Machine_Status) equals (b.Machine_Id, b.Plant_Name, b.WorkCell_Name, b.Machine_Status)
                       join c in WorkcellOEE
                           on (a.Machine_Id, a.Plant_Name, a.WorkCell_Name, a.Machine_Status) equals (c.Machine_Id, c.Plant_Name, c.WorkCell_Name, c.Machine_Status)


                       select new { WorkcellBagcount = a, WorkcellUptime = b, WorkcellOEE = c };
            foreach (var record in joined)
                //Console.WriteLine("{0}  {1} {2} {3}", record.PlantSpeed.Plant_Name, record.PlantSpeed, record.MachineUptime, record.BagCount);
                WorkcellView.Add(new WorkcellView()
                {
                    Plant_Name = record.WorkcellBagcount.Plant_Name,
                    WorkCell_Name = record.WorkcellBagcount.WorkCell_Name,
                    Machine_Id = record.WorkcellBagcount.Machine_Id,
                    Machine_Status = record.WorkcellBagcount.Machine_Status,
                    Machine_Speed = record.WorkcellBagcount.Machine_Speed,
                    DeclareBags = record.WorkcellBagcount.DeclareBags,
                    Uptime_Percentage = record.WorkcellUptime.Uptime_Percentage,
                    OEE = record.WorkcellOEE.OEE

                });
            var json = System.Text.Json.JsonSerializer.Serialize(new { WorkcellView }, new JsonSerializerOptions() { WriteIndented = true });

            rdr2.Close();
            return json;
        }

        public static string PlantShiftsummary()

        {
            List<shiftSpeed> shiftSpeed = new List<shiftSpeed>();
            List<shiftUptime> shiftUptime = new List<shiftUptime>();
            List<shiftOEE> shiftOEE = new List<shiftOEE>();
            List<shiftView> shiftView = new List<shiftView>();

            string query3 = @"EXEC [Pregis].Plant_Shift_MachineSpeed;EXEC [Pregis].Plant_Shift_Uptime;EXEC [Pregis].Plant_Shift_OEE";
            var cmd3 = new SqlCommand(query3, GetSqlConnection());
            SqlDataReader rdr3 = cmd3.ExecuteReader();

            //int i = 0;
            while (rdr3.Read())
            {

                shiftSpeed.Add(new shiftSpeed()
                {

                    Plant_Name = rdr3["Plant_Name"].ToString(),
                    ShiftNumber = rdr3["ShiftNumber"].ToString(),
                    Machine_speed = decimal.Parse(rdr3["Machine_speed"].ToString())//;
                });
                //Console.WriteLine(shiftBagcount);
            }
            rdr3.NextResult();
            while (rdr3.Read())
            {

                shiftUptime.Add(new shiftUptime()
                {

                    Plant_Name = rdr3["Plant_Name"].ToString(),
                    ShiftNumber = rdr3["ShiftNumber"].ToString(),
                    Uptime_Percentage = decimal.Parse(rdr3["Uptime_Percentage"].ToString())//;
                });
                //Console.WriteLine(shiftUptime);
            }
            rdr3.NextResult();
            while (rdr3.Read())
            {

                shiftOEE.Add(new shiftOEE()
                {

                    Plant_Name = rdr3["Plant_Name"].ToString(),
                    ShiftNumber = rdr3["ShiftNumber"].ToString(),
                    OEE = decimal.Parse(rdr3["OEE"].ToString())//;
                });
                //Console.WriteLine(shiftOEE);
            }
            var joined =
                       from a in shiftSpeed
                       join b in shiftUptime
                           on (a.Plant_Name, a.ShiftNumber) equals (b.Plant_Name, b.ShiftNumber)
                       join c in shiftOEE
                           on (a.Plant_Name, a.ShiftNumber) equals (c.Plant_Name, c.ShiftNumber)


                       select new { shiftSpeed = a, shiftUptime = b, shiftOEE = c };
            foreach (var record in joined)
                //Console.WriteLine("{0}  {1} {2} {3}", record.PlantSpeed.Plant_Name, record.PlantSpeed, record.MachineUptime, record.BagCount);
                shiftView.Add(new shiftView()
                {
                    Plant_Name = record.shiftSpeed.Plant_Name,
                    Shiftwise = record.shiftSpeed.ShiftNumber,
                    Machine_speed = record.shiftSpeed.Machine_speed,
                    Uptime_Percentage = record.shiftUptime.Uptime_Percentage,
                    OEE = record.shiftOEE.OEE

                });
            var json = System.Text.Json.JsonSerializer.Serialize(new { shiftView }, new JsonSerializerOptions() { WriteIndented = true });

            rdr3.Close();
            return json;
        }

        public static string MachineShiftsummary()
        {
            List<machineSpeed> machineSpeed = new List<machineSpeed>();
            List<machineUptime> machineUptime = new List<machineUptime>();
            List<machineOEE> machineOEE = new List<machineOEE>();
            List<MachineView> MachineView = new List<MachineView>();

            string query4 = @"EXEC [Pregis].Machine_Shift_Speed;EXEC [Pregis].Machine_Shift_Uptime;EXEC [Pregis].Machine_Shift_OEE";
            var cmd4 = new SqlCommand(query4, GetSqlConnection());
            SqlDataReader rdr4 = cmd4.ExecuteReader();

            while (rdr4.Read())
            {

                machineSpeed.Add(new machineSpeed()
                {

                    Plant_Name = rdr4["Plant_Name"].ToString(),
                    Machine_Id = rdr4["Machine_Id"].ToString(),
                    ShiftNumber = rdr4["ShiftNumber"].ToString(),
                    Machine_speed = decimal.Parse(rdr4["Machine_speed"].ToString())//;
                });
                //Console.WriteLine(shiftBagcount);
            }
            rdr4.NextResult();
            while (rdr4.Read())
            {

                machineUptime.Add(new machineUptime()
                {

                    Plant_Name = rdr4["Plant_Name"].ToString(),
                    Machine_Id = rdr4["Machine_Id"].ToString(),
                    ShiftNumber = rdr4["ShiftNumber"].ToString(),
                    Uptime_Percentage = decimal.Parse(rdr4["Uptime_Percentage"].ToString())//;
                });
                //Console.WriteLine(shiftBagcount);
            }
            rdr4.NextResult();
            while (rdr4.Read())
            {

                machineOEE.Add(new machineOEE()
                {

                    Plant_Name = rdr4["Plant_Name"].ToString(),
                    Machine_Id = rdr4["Machine_Id"].ToString(),
                    ShiftNumber = rdr4["ShiftNumber"].ToString(),
                    OEE = decimal.Parse(rdr4["OEE"].ToString())//;
                });
                //Console.WriteLine(shiftBagcount);
            }
            var joined =
                       from a in machineSpeed
                       join b in machineUptime
                           on (a.Plant_Name, a.Machine_Id, a.ShiftNumber) equals (b.Plant_Name, b.Machine_Id, b.ShiftNumber)
                       join c in machineOEE
                           on (a.Plant_Name, a.Machine_Id, a.ShiftNumber) equals (c.Plant_Name, c.Machine_Id, c.ShiftNumber)


                       select new { machineSpeed = a, machineUptime = b, machineOEE = c };
            foreach (var record in joined)
                //Console.WriteLine("{0}  {1} {2} {3}", record.PlantSpeed.Plant_Name, record.PlantSpeed, record.MachineUptime, record.BagCount);
                MachineView.Add(new MachineView()
                {
                    Plant_Name = record.machineSpeed.Plant_Name,
                    Machine_Id = record.machineSpeed.Machine_Id,
                    ShiftNumber = record.machineSpeed.ShiftNumber,
                    Machine_Speed = record.machineSpeed.Machine_speed,
                    Uptime_Percentage = record.machineUptime.Uptime_Percentage,
                    OEE = record.machineOEE.OEE

                });
            var json = System.Text.Json.JsonSerializer.Serialize(new { MachineView }, new JsonSerializerOptions() { WriteIndented = true });

            rdr4.Close();
            return json;
        }
        /*
        public static string WorkcellGroup()
        {
            List<WorkcellBagcount> WorkcellBagcount = new List<WorkcellBagcount>();
            List<WorkcellUptime> WorkcellUptime = new List<WorkcellUptime>();
            List<WorkcellOEE> WorkcellOEE = new List<WorkcellOEE>();
            List<WorkcellGroupView> WorkcellGroupView = new List<WorkcellGroupView>();

            string query5 = @"EXEC CellGroup_Speed_Bag;EXEC CellGroup_Uptime;EXEC CellGroup_OEE";
            var cmd5 = new SqlCommand(query5, GetSqlConnection());
            SqlDataReader rdr5 = cmd5.ExecuteReader();

            //int i = 0;
            while (rdr5.Read())
            {

                WorkcellBagcount.Add(new WorkcellBagcount()
                {
                    Cell = rdr5["Cell"].ToString(),
                    Plant_Name = rdr5["Plant_Name"].ToString(),
                    WorkCell_Name = rdr5["WorkCell_Name"].ToString(),
                    Machine_Id = rdr5["Machine_Id"].ToString(),
                    Machine_Status = rdr5["Machine_Status"].ToString(),
                    Machine_Speed = decimal.Parse(rdr5["Machine_Speed"].ToString()),
                    DeclareBags = float.Parse(rdr5["DeclareBags"].ToString())//;
                });
                Console.WriteLine(WorkcellBagcount);
            }
            rdr5.NextResult();
            while (rdr5.Read())
            {

                WorkcellUptime.Add(new WorkcellUptime()
                {
                    Cell = rdr5["Cell"].ToString(),
                    Plant_Name = rdr5["Plant_Name"].ToString(),
                    WorkCell_Name = rdr5["WorkCell_Name"].ToString(),
                    Machine_Id = rdr5["Machine_Id"].ToString(),
                    Machine_Status = rdr5["Machine_Status"].ToString(),
                    Uptime_Percentage = decimal.Parse(rdr5["Uptime_Percentage"].ToString())//;
                });
                Console.WriteLine(WorkcellUptime);
            }
            rdr5.NextResult();
            while (rdr5.Read())
            {

                WorkcellOEE.Add(new WorkcellOEE()
                {
                    Cell = rdr5["Cell"].ToString(),
                    Plant_Name = rdr5["Plant_Name"].ToString(),
                    WorkCell_Name = rdr5["WorkCell_Name"].ToString(),
                    Machine_Id = rdr5["Machine_Id"].ToString(),
                    Machine_Status = rdr5["Machine_Status"].ToString(),
                    OEE = decimal.Parse(rdr5["OEE"].ToString())//;
                });
                Console.WriteLine(WorkcellOEE);
            }
            var joined =
                       from a in WorkcellBagcount
                       join b in WorkcellUptime
                           on (a.Cell, a.Machine_Id, a.Plant_Name, a.WorkCell_Name, a.Machine_Status) equals (b.Cell, b.Machine_Id, b.Plant_Name, b.WorkCell_Name, b.Machine_Status)
                       join c in WorkcellOEE
                           on (a.Cell, a.Machine_Id, a.Plant_Name, a.WorkCell_Name, a.Machine_Status) equals (c.Cell, c.Machine_Id, c.Plant_Name, c.WorkCell_Name, c.Machine_Status)


                       select new { WorkcellBagcount = a, WorkcellUptime = b, WorkcellOEE = c };
            foreach (var record in joined)
                //Console.WriteLine("{0}  {1} {2} {3}", record.PlantSpeed.Plant_Name, record.PlantSpeed, record.MachineUptime, record.BagCount);
                WorkcellGroupView.Add(new WorkcellGroupView()
                {
                    Cell = record.WorkcellBagcount.Cell,
                    Plant_Name = record.WorkcellBagcount.Plant_Name,
                    WorkCell_Name = record.WorkcellBagcount.WorkCell_Name,
                    Machine_Id = record.WorkcellBagcount.Machine_Id,
                    Machine_Status = record.WorkcellBagcount.Machine_Status,
                    Machine_Speed = record.WorkcellBagcount.Machine_Speed,
                    DeclareBags = record.WorkcellBagcount.DeclareBags,
                    Uptime_Percentage = record.WorkcellUptime.Uptime_Percentage,
                    OEE = record.WorkcellOEE.OEE

                });
            var json = System.Text.Json.JsonSerializer.Serialize(new { WorkcellGroupView }, new JsonSerializerOptions() { WriteIndented = true });

            rdr5.Close();
            return json;
        }*/
    }
}

