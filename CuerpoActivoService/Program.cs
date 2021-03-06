﻿using ServiceProcess.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.ServiceProcess;

namespace CuerpoActivoService
{
    static class Program
    {
        private static readonly List<ServiceBase> _servicesToRun = new List<ServiceBase>();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            _servicesToRun.Add(CuerpoActivoService.Instance);

            if (Environment.UserInteractive)
            {
                _servicesToRun.ToArray().LoadServices();
            }
            else
            {
                ServiceBase.Run(_servicesToRun.ToArray());
            }
        }
    }
}
