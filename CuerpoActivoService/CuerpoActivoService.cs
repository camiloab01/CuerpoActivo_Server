﻿using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin.Hosting;
using Newtonsoft.Json;
using SignalRBroadcastServiceSample.Domain;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.ServiceProcess;
using System.Threading;

namespace SignalRBroadcastServiceSample
{
    public partial class CuerpoActivoService : ServiceBase
    {
        private Thread mainThread;
        private bool isRunning = true;
        protected IDisposable _signalRApplication = null;
        private String uri = "http://dev-cuerpoactivo.lumenup.net/rest/reminder";
        private int lastReminderNotifiedId;

        protected override void OnStart(string[] args)
        {
            _signalRApplication = WebApp.Start("http://localhost:8084"); 

            // Start main thread
            mainThread = new Thread(new ParameterizedThreadStart(this.RunService));
            mainThread.Start(DateTime.MaxValue);
        }

        protected override void OnStop()
        {
            if (_signalRApplication != null)
            {
                _signalRApplication.Dispose();
            }
            _signalRApplication = null;
            mainThread.Join();
        }

        public void RunService(object timeToComplete)
        {
            DateTime dtTimeToComplete = timeToComplete != null ? Convert.ToDateTime(timeToComplete) : DateTime.MaxValue;

            while (isRunning && DateTime.UtcNow < dtTimeToComplete)
            {
                using (var webClient = new WebClient())
                {
                    var jsonData = string.Empty;

                    jsonData = webClient.DownloadString(uri);

                    var reminder = JsonConvert.DeserializeObject<Reminder>(jsonData);

                    if (reminder.Id != 0 && reminder.Id != lastReminderNotifiedId)
                    {
                        NotifyAllClients(reminder);
                        lastReminderNotifiedId = reminder.Id;
                    }
                }

                Thread.Sleep(60000);
            }
        }

        // This line is necessary to perform the broadcasting to all clients
        private void NotifyAllClients(Reminder reminder)
        {
            Clients.All.NotifyReminder(reminder);
        }

        #region "SignalR code"

        // Singleton instance
        private readonly static Lazy<CuerpoActivoService> _instance = new Lazy<CuerpoActivoService>(
            () => new CuerpoActivoService(GlobalHost.ConnectionManager.GetHubContext<CuerpoActivoServiceHub>().Clients));

        public CuerpoActivoService(IHubConnectionContext<dynamic> clients)
        {
            InitializeComponent();

            Clients = clients;
        }

        public static CuerpoActivoService Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private IHubConnectionContext<dynamic> Clients { get; set; }

   }

    #endregion
}