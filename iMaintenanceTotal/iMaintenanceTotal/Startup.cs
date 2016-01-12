using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Owin;
using Owin;
using System;
using iMaintenanceTotal.Services;

[assembly: OwinStartupAttribute(typeof(iMaintenanceTotal.Startup))]
namespace iMaintenanceTotal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            GlobalConfiguration.Configuration.UseSqlServerStorage("DefaultConnection");
            app.UseHangfireDashboard();

            RecurringJob.AddOrUpdate(() => MessagingService.SendReminders(), Cron.Minutely);
        }
    }
}
