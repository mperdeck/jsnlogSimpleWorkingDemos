using System;
using NLog;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Logger logger = LogManager.GetLogger("serverlogger");
        logger.Warn("Warn Message generated on server");

    }
}