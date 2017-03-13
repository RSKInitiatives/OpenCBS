using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Free.iso8583;

namespace WebClient
{
    public partial class RequestForm : System.Web.UI.Page, IModelParsingReceiver
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            pnlInput.Visible = Session["Free.iso8583.Data"] == null;
            pnlResponse.Visible = Session["Free.iso8583.Data"] != null;
            if (Session["Free.iso8583.Data"] != null)
            {
                iso8583Data.InnerText = Session["Free.iso8583.Data"].ToString();
            } 
        }

        private System.Threading.AutoResetEvent _iso8593ResponseComesEvent = new System.Threading.AutoResetEvent(false);
        private String _output = "";
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Models.Request0800 req = new Models.Request0800
            {
                TransmissionDateTime = DateTime.Now,
                SystemAuditTraceNumber = int.Parse(txtTraceNumber.Text.Trim().PadLeft(1, '0')),
                AdditionalData = txtAdtData.Text,
                NetworkManagementInformationCode = txtNMICode.Text.PadLeft(3, '0')
            };

            _output = "==== Begin: Request ====" + Environment.NewLine
                + Util.GetReadableStringFromModel(req) + Environment.NewLine
                + "==== End: Request ====" + Environment.NewLine;

            MessageClient client = new MessageClient("localhost", 3107, req);
            client.Callback = this.PrintIso8583Response;
            client.SendModel();
            _iso8593ResponseComesEvent.WaitOne();

            Session["Free.iso8583.Data"] = _output;
            Response.Redirect("~/RequestForm.aspx");
        }

        private Object PrintIso8583Response(Object model)
        {
            if (model == null)
            {
                _output += "An error occured!! Cannot get response...";
            }
            else
            {
                _output += "==== Begin: Response ====" + Environment.NewLine
                    + MessageUtility.HexToReadableString(ParsedMessage.AllBytes) + Environment.NewLine
                    + Environment.NewLine
                    + Environment.NewLine
                    + Util.GetReadableStringFromModel(model) + Environment.NewLine
                    + "==== End: Response ====";
            }
            _iso8593ResponseComesEvent.Set();
            return null;
        }

        protected void linkBack_Click(object sender, EventArgs e)
        {
            Session.Remove("Free.iso8583.Data");
            Response.Redirect("~/RequestForm.aspx");
        }

        //Implements IModelParsingReceiver
        public IParsedMessage ParsedMessage
        {
            get;
            set;
        }
    }
}