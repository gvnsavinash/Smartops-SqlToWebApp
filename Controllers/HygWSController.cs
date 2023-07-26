//***************************************************************************************************************************************//
/*
  Project Name  :   SmartOps Phase-2
  Task          :   Reading and store data From Excel to Angular Dashbaord using Web App
  Developed on  :   Aug 2022
  Developed by  :   Avinash
  Version       :   1.2
  Module:       :   HygWS Controller (Get HYGHelper response to websocket for angular dashboard) 

 */



using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;

using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.WebSockets;


namespace SqlToWebApp.Controllers
{

    //[System.Web.Http.Route("/ws")]
    //[ApiController]
    public class HygWSController : ApiController
    {

        //public static string getOOFStatus = "";
        //public string getOOFStatusOldValue { get; set; }

        private static readonly int REFRESH_INTERVAL = 5000;// int.Parse(ConfigurationManager.AppSettings["REFRESH_INTERVAL"]);

        // ################# OOF ##################
        [HttpGet]
        public HttpResponseMessage hygoof()
        {
            if (HttpContext.Current.IsWebSocketRequest)
            {
                HttpContext.Current.AcceptWebSocketRequest(Hygoof);
            }
            return new HttpResponseMessage(HttpStatusCode.SwitchingProtocols);
        }
        private async Task Hygoof(AspNetWebSocketContext context)

        {
            WebSocket socket = context.WebSocket;

            while (true)
            {
                if (!socket.State.Equals(WebSocketState.Open))
                {
                    break;
                }

                ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[1024]);

                // (!getOOFStatus.Equals(getOOFStatusOldValue))
                //{

                // getOOFStatusOldValue = getOOFStatus;
                buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(HYGHelper.GetOOFStatus()));
                await socket.SendAsync(
                buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                System.Threading.Thread.Sleep(REFRESH_INTERVAL);

                //}

            }
        }

        // ################# Operator Efficiency ##################
        [HttpGet]
        public HttpResponseMessage hygoe()
        {
            if (HttpContext.Current.IsWebSocketRequest)
            {
                HttpContext.Current.AcceptWebSocketRequest(Hygoe);
            }
            return new HttpResponseMessage(HttpStatusCode.SwitchingProtocols);
        }
        //int SleepTime = 30000;
        private async Task Hygoe(AspNetWebSocketContext context)

        {
            WebSocket socket = context.WebSocket;

            while (true)
            {
                if (!socket.State.Equals(WebSocketState.Open))
                {
                    break;
                }

                ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[1024]);

                // (!getOOFStatus.Equals(getOOFStatusOldValue))
                //{

                // getOOFStatusOldValue = getOOFStatus;
                buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(HYGHelper.getOperatorEfficiency()));
                await socket.SendAsync(
                buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                //}
                System.Threading.Thread.Sleep(REFRESH_INTERVAL);
            }
        }

        // ################# Work station  Efficiency ##################
        [HttpGet]
        public HttpResponseMessage hygwse()
        {
            if (HttpContext.Current.IsWebSocketRequest)
            {
                HttpContext.Current.AcceptWebSocketRequest(Hygwse);
            }
            return new HttpResponseMessage(HttpStatusCode.SwitchingProtocols);
        }
        //int SleepTime = 30000;
        private async Task Hygwse(AspNetWebSocketContext context)

        {
            WebSocket socket = context.WebSocket;

            while (true)
            {
                if (!socket.State.Equals(WebSocketState.Open))
                {
                    break;
                }

                ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[1024]);

                // (!getOOFStatus.Equals(getOOFStatusOldValue))
                //{

                // getOOFStatusOldValue = getOOFStatus;
                buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(HYGHelper.getWorkstationEfficiency()));
                await socket.SendAsync(
                buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                //}
                System.Threading.Thread.Sleep(REFRESH_INTERVAL);
            }
        }

        // ################# Work Unit Status ##################
        [HttpGet]
        public HttpResponseMessage hygwus()
        {
            if (HttpContext.Current.IsWebSocketRequest)
            {
                HttpContext.Current.AcceptWebSocketRequest(Hygwus);
            }
            return new HttpResponseMessage(HttpStatusCode.SwitchingProtocols);
        }
        //int SleepTime = 30000;
        private async Task Hygwus(AspNetWebSocketContext context)

        {
            WebSocket socket = context.WebSocket;

            while (true)
            {
                if (!socket.State.Equals(WebSocketState.Open))
                {
                    break;
                }

                ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[1024]);

                // (!getOOFStatus.Equals(getOOFStatusOldValue))
                //{

                // getOOFStatusOldValue = getOOFStatus;
                buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(HYGHelper.getWorkUnitStatus()));
                await socket.SendAsync(
                buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                //}
                System.Threading.Thread.Sleep(REFRESH_INTERVAL);
            }
        }
        // ################# Operator Efficiency List ##################
        [HttpGet]
        public HttpResponseMessage hygoel()
        {
            if (HttpContext.Current.IsWebSocketRequest)
            {
                HttpContext.Current.AcceptWebSocketRequest(Hygoel);
            }
            return new HttpResponseMessage(HttpStatusCode.SwitchingProtocols);
        }
        //int SleepTime = 30000;
        private async Task Hygoel(AspNetWebSocketContext context)

        {
            WebSocket socket = context.WebSocket;

            while (true)
            {
                if (!socket.State.Equals(WebSocketState.Open))
                {
                    break;
                }

                ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[1024]);

                // (!getOOFStatus.Equals(getOOFStatusOldValue))
                //{

                // getOOFStatusOldValue = getOOFStatus;
                buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(HYGHelper.getOperatorEfficiencyList()));
                await socket.SendAsync(
                buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                //}
                System.Threading.Thread.Sleep(REFRESH_INTERVAL);
            }
        }
        // ################# All Workorder   ##################
        [HttpGet]
        public HttpResponseMessage hygstart()
        {
            if (HttpContext.Current.IsWebSocketRequest)
            {
                HttpContext.Current.AcceptWebSocketRequest(Hygstart);
            }
            return new HttpResponseMessage(HttpStatusCode.SwitchingProtocols);
        }
        //int SleepTime = 30000;
        private async Task Hygstart(AspNetWebSocketContext context)

        {
            WebSocket socket = context.WebSocket;

            while (true)
            {
                if (!socket.State.Equals(WebSocketState.Open))
                {
                    break;
                }

                ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[1024]);

                // (!getOOFStatus.Equals(getOOFStatusOldValue))
                //{

                // getOOFStatusOldValue = getOOFStatus;
                buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(HYGHelper.getAllHYGWorkOrder()));
                await socket.SendAsync(
                buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                //}
                System.Threading.Thread.Sleep(REFRESH_INTERVAL);
            }
        }
        // ################# All Workorder   ##################
        [HttpGet]
        public HttpResponseMessage hygend()
        {
            if (HttpContext.Current.IsWebSocketRequest)
            {
                HttpContext.Current.AcceptWebSocketRequest(Hygend);
            }
            return new HttpResponseMessage(HttpStatusCode.SwitchingProtocols);
        }
        //int SleepTime = 30000;
        private async Task Hygend(AspNetWebSocketContext context)

        {
            WebSocket socket = context.WebSocket;

            while (true)
            {
                if (!socket.State.Equals(WebSocketState.Open))
                {
                    break;
                }

                ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[1024]);

                // (!getOOFStatus.Equals(getOOFStatusOldValue))
                //{

                // getOOFStatusOldValue = getOOFStatus;
                buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(HYGHelper.getAllHYGWorkOrder()));
                await socket.SendAsync(
                buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                //}
                System.Threading.Thread.Sleep(REFRESH_INTERVAL);
            }
        }
        // ################# All Workorder   ##################
        [HttpGet]
        public HttpResponseMessage hygsp()
        {
            if (HttpContext.Current.IsWebSocketRequest)
            {
                HttpContext.Current.AcceptWebSocketRequest(Hygsp);
            }
            return new HttpResponseMessage(HttpStatusCode.SwitchingProtocols);
        }
        //int SleepTime = 30000;
        private async Task Hygsp(AspNetWebSocketContext context)

        {
            WebSocket socket = context.WebSocket;

            while (true)
            {
                if (!socket.State.Equals(WebSocketState.Open))
                {
                    break;
                }

                ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[1024]);

                // (!getOOFStatus.Equals(getOOFStatusOldValue))
                //{

                // getOOFStatusOldValue = getOOFStatus;
                buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(HYGHelper.getAllHYGWorkOrder()));
                await socket.SendAsync(
                buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                //}
                System.Threading.Thread.Sleep(REFRESH_INTERVAL);
            }
        }


    }
}