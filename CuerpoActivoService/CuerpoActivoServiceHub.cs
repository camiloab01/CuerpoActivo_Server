using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CuerpoActivoService
{
    public class CuerpoActivoServiceHub : Hub
    {
        private readonly CuerpoActivoService _cuerpoActivoService;

        public CuerpoActivoServiceHub() :
            this(CuerpoActivoService.Instance)
        {

        }

        public CuerpoActivoServiceHub(CuerpoActivoService cuerpoActivoService)
        {
            _cuerpoActivoService = cuerpoActivoService;
        }
    }
}
