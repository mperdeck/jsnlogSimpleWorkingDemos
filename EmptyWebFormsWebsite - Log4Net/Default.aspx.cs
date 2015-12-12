using System;
using log4net;
using log4net.Config;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        XmlConfigurator.Configure();

        ILog log = LogManager.GetLogger("serverlogger");
        log.Info("Info Message generated on server");


    }
}