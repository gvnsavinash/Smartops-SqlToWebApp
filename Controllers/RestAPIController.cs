/***********************************************************************************************************************************************/
/*
 Project Name : SmartOps phase2
 Version      : 1.1
 Language Used: C#
 Devoloped by : Avinash
 Created as of: 17/05/2022
 Module       : RestAPI Controller
 
/***********************************************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;


namespace SqlToWebApp.Controllers
{
    // Authenticate with Azure cors in cloud to access Azure web app
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RestAPIController : ApiController
    {
        /*[HttpGet]
        // Get HTTP response for Plant view
        public string Plant()
        {
            return Helper.Plantsummary();
        }
        */
        [HttpGet]
        // Get HTTP response for Workcell view
        public string Workcell()
        {
            return HelperSql.Workcellsummary();
        }
        [HttpGet]
        // Get HTTP response for Plant shift view
        public string PlantShift()
        {
            return HelperSql.PlantShiftsummary();
        }
        [HttpGet]
        // Get HTTP response for Machine shift view
        public string MachineShift()
        {
            return HelperSql.MachineShiftsummary();

        }
        /*  [HttpGet]
          // Get HTTP response for Machine shift view
          public string PlantShiftByWeek()
          {
              return Helper.PlantShiftsummary_PerWeek();

          }
        */
        [HttpGet]
        public string PlantMerge()
        {
            return HelperSql.PlantExperimental();

        }



    }
}