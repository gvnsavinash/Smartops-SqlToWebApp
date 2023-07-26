using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.WebSockets;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;




namespace SqlToWebApp.Controllers
{
    public class WSController : ApiController
    {
        // GET: WS
        [HttpGet]
        public HttpResponseMessage GetMessage()
        {
            var status = HttpStatusCode.BadRequest;
            var context = HttpContext.Current;
            if (context.IsWebSocketRequest)
            {
                context.AcceptWebSocketRequest(ProcessRequest);
                status = HttpStatusCode.SwitchingProtocols;

            }

            return new HttpResponseMessage(status);

        }
        private async Task ProcessRequest(AspNetWebSocketContext context)
        {
            var ws = context.WebSocket;
            await Task.WhenAll(WriteTask(ws), ReadTask(ws));
        }

        // MUST read if we want the socket state to be updated
        private async Task ReadTask(WebSocket ws)
        {
            var buffer = new ArraySegment<byte>(new byte[1024]);
            while (true)
            {
                await ws.ReceiveAsync(buffer, CancellationToken.None).ConfigureAwait(false);
                if (ws.State != WebSocketState.Open) break;
            }
        }
        private async Task WriteTask(WebSocket ws)
        {
            while (true)
            {
                //var timeStr = DateTime.UtcNow.ToString("MMM dd yyyy HH:mm:ss.fff UTC", CultureInfo.InvariantCulture);
                var timeStr = Helper.Plantsummary();
                var buffer = Encoding.UTF8.GetBytes(timeStr);
                if (ws.State != WebSocketState.Open) break;
                var sendTask = ws.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                await sendTask.ConfigureAwait(false);
                if (ws.State != WebSocketState.Open) break;
                await Task.Delay(1000).ConfigureAwait(false); // this is NOT ideal
            }
        }

        
        [HttpGet]
        public HttpResponseMessage GetMessage1()
        {
            var status = HttpStatusCode.BadRequest;
            var context = HttpContext.Current;
            if (context.IsWebSocketRequest)
            {
                context.AcceptWebSocketRequest(ProcessRequest1);
                status = HttpStatusCode.SwitchingProtocols;

            }

            return new HttpResponseMessage(status);

        }
        private async Task ProcessRequest1(AspNetWebSocketContext context)
        {
            var ws = context.WebSocket;
            await Task.WhenAll(WriteTask1(ws), ReadTask1(ws));
        }

        // MUST read if we want the socket state to be updated
        private async Task ReadTask1(WebSocket ws)
        {
            var buffer = new ArraySegment<byte>(new byte[1024]);
            while (true)
            {
                await ws.ReceiveAsync(buffer, CancellationToken.None).ConfigureAwait(false);
                if (ws.State != WebSocketState.Open) break;
            }
        }
        private async Task WriteTask1(WebSocket ws)
        {
            while (true)
            {
                //var timeStr = DateTime.UtcNow.ToString("MMM dd yyyy HH:mm:ss.fff UTC", CultureInfo.InvariantCulture);
                var timeStr = Helper.Workcellsummary();
                var buffer = Encoding.UTF8.GetBytes(timeStr);
                if (ws.State != WebSocketState.Open) break;
                var sendTask = ws.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                await sendTask.ConfigureAwait(false);
                if (ws.State != WebSocketState.Open) break;
                await Task.Delay(1000).ConfigureAwait(false); // this is NOT ideal
            }
        }


        [HttpGet]
        public HttpResponseMessage GetMessage2()
        {
            var status = HttpStatusCode.BadRequest;
            var context = HttpContext.Current;
            if (context.IsWebSocketRequest)
            {
                context.AcceptWebSocketRequest(ProcessRequest2);
                status = HttpStatusCode.SwitchingProtocols;

            }

            return new HttpResponseMessage(status);

        }
        private async Task ProcessRequest2(AspNetWebSocketContext context)
        {
            var ws = context.WebSocket;
            await Task.WhenAll(WriteTask2(ws), ReadTask2(ws));
        }

        // MUST read if we want the socket state to be updated
        private async Task ReadTask2(WebSocket ws)
        {
            var buffer = new ArraySegment<byte>(new byte[1024]);
            while (true)
            {
                await ws.ReceiveAsync(buffer, CancellationToken.None).ConfigureAwait(false);
                if (ws.State != WebSocketState.Open) break;
            }
        }
        private async Task WriteTask2(WebSocket ws)
        {
            while (true)
            {
                //var timeStr = DateTime.UtcNow.ToString("MMM dd yyyy HH:mm:ss.fff UTC", CultureInfo.InvariantCulture);
                var timeStr = Helper.PlantShiftsummary();
                var buffer = Encoding.UTF8.GetBytes(timeStr);
                if (ws.State != WebSocketState.Open) break;
                var sendTask = ws.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                await sendTask.ConfigureAwait(false);
                if (ws.State != WebSocketState.Open) break;
                await Task.Delay(1000).ConfigureAwait(false); // this is NOT ideal
            }
        }

        [HttpGet]
        public HttpResponseMessage GetMessage3()
        {
            var status = HttpStatusCode.BadRequest;
            var context = HttpContext.Current;
            if (context.IsWebSocketRequest)
            {
                context.AcceptWebSocketRequest(ProcessRequest3);
                status = HttpStatusCode.SwitchingProtocols;

            }

            return new HttpResponseMessage(status);

        }
        private async Task ProcessRequest3(AspNetWebSocketContext context)
        {
            var ws = context.WebSocket;
            await Task.WhenAll(WriteTask3(ws), ReadTask3(ws));
        }

        // MUST read if we want the socket state to be updated
        private async Task ReadTask3(WebSocket ws)
        {
            var buffer = new ArraySegment<byte>(new byte[1024]);
            while (true)
            {
                await ws.ReceiveAsync(buffer, CancellationToken.None).ConfigureAwait(false);
                if (ws.State != WebSocketState.Open) break;
            }
        }
        private async Task WriteTask3(WebSocket ws)
        {
            while (true)
            {
                //var timeStr = DateTime.UtcNow.ToString("MMM dd yyyy HH:mm:ss.fff UTC", CultureInfo.InvariantCulture);
                var timeStr = Helper.MachineShiftsummary();
                var buffer = Encoding.UTF8.GetBytes(timeStr);
                if (ws.State != WebSocketState.Open) break;
                var sendTask = ws.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                await sendTask.ConfigureAwait(false);
                if (ws.State != WebSocketState.Open) break;
                await Task.Delay(1000).ConfigureAwait(false); // this is NOT ideal
            }
        }

        /*[HttpGet]
        public HttpResponseMessage GetMessage4()
        {
            var status = HttpStatusCode.BadRequest;
            var context = HttpContext.Current;
            if (context.IsWebSocketRequest)
            {
                context.AcceptWebSocketRequest(ProcessRequest4);
                status = HttpStatusCode.SwitchingProtocols;

            }

            return new HttpResponseMessage(status);

        }
        private async Task ProcessRequest4(AspNetWebSocketContext context)
        {
            var ws = context.WebSocket;
            await Task.WhenAll(WriteTask4(ws), ReadTask4(ws));
        }

        // MUST read if we want the socket state to be updated
        private async Task ReadTask4(WebSocket ws)
        {
            var buffer = new ArraySegment<byte>(new byte[1024]);
            while (true)
            {
                await ws.ReceiveAsync(buffer, CancellationToken.None).ConfigureAwait(false);
                if (ws.State != WebSocketState.Open) break;
            }
        }
        private async Task WriteTask4(WebSocket ws)
        {
            while (true)
            {
                //var timeStr = DateTime.UtcNow.ToString("MMM dd yyyy HH:mm:ss.fff UTC", CultureInfo.InvariantCulture);
                var timeStr = Helper.WorkcellGroup();
                var buffer = Encoding.UTF8.GetBytes(timeStr);
                if (ws.State != WebSocketState.Open) break;
                var sendTask = ws.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                await sendTask.ConfigureAwait(false);
                if (ws.State != WebSocketState.Open) break;
                await Task.Delay(1000).ConfigureAwait(false); // this is NOT ideal
            }
        }
        */
    }
}