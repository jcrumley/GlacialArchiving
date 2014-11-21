using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;

namespace GlacialArchiving.ClerkService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();

            this.BeforeInstall += new InstallEventHandler(ProjectInstaller_BeforeInstall);
        }

        protected void ProjectInstaller_BeforeInstall(object sender, InstallEventArgs e)
        {
            try
            {
                if (this.Context.Parameters.ContainsKey("name"))
                {
                    string name = this.Context.Parameters["name"].ToString();

                    this.serviceInstaller1.ServiceName = name;
                    this.serviceInstaller1.DisplayName = name;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("No name was provided in the parameters", ex);
            }
        }
    }
}
