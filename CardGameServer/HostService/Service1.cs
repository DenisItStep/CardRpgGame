using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using CardGameServer;

namespace HostService
{
    public partial class Service1 : ServiceBase
    {
        private ServiceHost _host;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _host=new ServiceHost(typeof(Servicegame));
            _host.Open();
        }

        protected override void OnStop()
        {
            if (_host!= null)
            {
                _host.Close();
            }
        }
    }
}
