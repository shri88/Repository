using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Configuration.Provider;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;

namespace NG_Portal
{
    public partial class RID : System.Web.UI.Page
    {
        string con = ConfigurationManager.ConnectionStrings["DeploymentReportConnection"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            btnSendMail.Visible = false;
            tblRFC.Visible = false;
            tbltxtReason.Text = "";
        }

        private void Gridview_Populate(string sqlQuery)
        {
            gdvNgCodes.Visible = true;
            gdvPCMVariables.Visible = false;
            SqlConnection sqlCon = new SqlConnection(con);
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
            SqlCommandBuilder sqlCmd = new SqlCommandBuilder(sqlDa);
            DataTable dt = new DataTable();
            sqlDa.Fill(dt);
            gdvNgCodes.DataSource = dt;
            gdvNgCodes.DataBind();
            gdvNgCodes.EmptyDataText = "No Records Found..!!";

        }

        protected void btnOperaLink_Click(object sender, EventArgs e)
        {
            string sqlQuery = "Select * from NG_Lab_Code";
            Gridview_Populate(sqlQuery);
        }

        protected void btnNGLABCodes_Click(object sender, EventArgs e)
        {
            string sqlQuery = "select * from NG_Codes";
            Gridview_Populate(sqlQuery);
        }

        protected void btnPCMVariables_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnExportVar_Click(object sender, EventArgs e)
        {
            ExportData();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void SendMail()
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("Shriram.88@gmail.com");
            mail.Subject = "RID";
            mail.To.Add("Shriramlakshminarayanan@eurofins.com");
            mail.Body = "Message Body";

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential
            ("shriram.88@gmail.com", txtPassword.Text.ToString());

            smtp.EnableSsl = true;
            smtp.Send(mail);
        }

        protected void btnSendMail_Click(object sender, EventArgs e)
        {
            SendMail();
        }

        protected void gdvNgCodes_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gdvNgCodes.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void gdvNgCodes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            
        }

        protected void btnRFCForm_Click(object sender, EventArgs e)
        {
            gdvNgCodes.Visible = false;
            gdvPCMVariables.Visible = false;
            btnSendMail.Visible = true;
            tblRFC.Visible = true;
        }

        protected void gdvPCMVariables_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gdvPCMVariables_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gdvPCMVariables.EditIndex = e.NewEditIndex;
            this.BindGrid();
        }

        protected void gdvPCMVariables_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gdvPCMVariables.EditIndex = -1;
            this.BindGrid();
        }

        protected void gdvPCMVariables_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gdvPCMVariables.Rows[e.RowIndex];
            string strVariable = (row.FindControl("lblVariable") as Label).Text;
            string strExp = (row.FindControl("txtExpValue") as TextBox).Text;
            string strLab = (row.FindControl("txtLab") as TextBox).Text;

            SqlConnection sqlCon = new SqlConnection(con);
            SqlCommand cmd = new SqlCommand("SP_PCM_VARIABLES_UPDATE", sqlCon);
            SqlDataAdapter sqlDa = new SqlDataAdapter();
            cmd.CommandType = CommandType.StoredProcedure;
            sqlCon.Open();
            cmd.Parameters.AddWithValue("@variable", strVariable);
            cmd.Parameters.AddWithValue("@expValue", strExp);
            cmd.Parameters.AddWithValue("@lab", strLab);
            
            cmd.ExecuteNonQuery();
            sqlCon.Close();
            gdvPCMVariables.EditIndex = -1;
            this.BindGrid();
        }

        protected void gdvPCMVariables_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {

        }

        protected void gdvPCMVariables_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        private void BindGrid()
        {
            gdvNgCodes.Visible = false;
            gdvPCMVariables.Visible = true;
            string strQuery = "Select * from NG_RID_PCM_Variables";
            SqlConnection sqlCon = new SqlConnection(con);
            SqlDataAdapter sqlDa = new SqlDataAdapter(strQuery, sqlCon);
            SqlCommandBuilder sqlCmd = new SqlCommandBuilder(sqlDa);
            DataTable dt = new DataTable();
            sqlDa.Fill(dt);
            gdvPCMVariables.DataSource = dt;
            gdvPCMVariables.DataBind();
            gdvPCMVariables.EmptyDataText = "No Records Found..!!";
        }

        private void ExportData()
        {
            gdvPCMVariables.AllowPaging = false;
            BindGrid();
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=PCM Variables.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htW = new HtmlTextWriter(sw);
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            gdvPCMVariables.RenderControl(htW);
            Response.Output.Write(sw.ToString());
            Response.End();
        }
    }
}