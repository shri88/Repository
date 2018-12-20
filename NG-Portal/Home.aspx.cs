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

namespace NG_Portal
{
    public partial class _Default : Page
    {
        string DepCon = ConfigurationManager.ConnectionStrings["DeploymentReportConnection"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btnShowReleaseLog_Click(object sender, EventArgs e)
        {
            BindReleaselog();
        }

        protected void btnExportReleaseLog_Click(object sender, EventArgs e)
        {
            ExportData();
        }

        protected void btnExportFilterLog_Click(object sender, EventArgs e)
        {
            gdvDeployLog.AllowPaging = false;
            string strFromDate = txtFromDate.Text;
            string strToDate = txtToDate.Text;
            string sqlQuery = "Select ML.Milestone,PDL.Environment,PDL.Lab,PDL.[Site],PDL.[Scope],PDL.created_dt,PDL.Release_id,PDL.DeployedBy,PDL.startTime,PDL.endTime,DATEDIFF(MINUTE, startTime , endTime) AS MinuteDiff,PDL.Description from pcm_deployment_logs as PDL inner join Milestone as ML on ML.environment = PDL.Scope Where convert(varchar(30),created_dt,101)>='" + strFromDate + "' and convert(varchar(30),created_dt,101)<= '" + strToDate + "' order by PDL.created_dt desc";
            SqlConnection sqlCon = new SqlConnection(DepCon);
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
            SqlCommandBuilder sqlCmd = new SqlCommandBuilder(sqlDa);
            DataTable dt = new DataTable();
            sqlDa.Fill(dt);
            gdvDeployLog.DataSource = dt;
            gdvDeployLog.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=ExportGridData.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htW = new HtmlTextWriter(sw);
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            gdvDeployLog.RenderControl(htW);
            Response.Output.Write(sw.ToString());
            Response.End();
        }

        private void BindReleaselog()
        {
            string sqlQuery = "Select ML.Milestone,PDL.Environment,PDL.Lab,PDL.[Site],PDL.[Scope],PDL.created_dt,PDL.Release_id,PDL.DeployedBy,PDL.startTime,PDL.endTime,DATEDIFF(MINUTE, startTime , endTime) AS MinuteDiff,PDL.Description from pcm_deployment_logs as PDL inner join Milestone as ML on ML.environment = PDL.Scope order by PDL.created_dt desc";
            SqlConnection sqlCon = new SqlConnection(DepCon);
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
            SqlCommandBuilder sqlCmd = new SqlCommandBuilder(sqlDa);
            DataTable dt = new DataTable();
            sqlDa.Fill(dt);
            gdvDeployLog.DataSource = dt;
            gdvDeployLog.DataBind();
            gdvDeployLog.EmptyDataText = "No Records Found..!!";
        }

        private void DateFilteredLog()
        {
            string sqlQuery = "Select ML.Milestone,PDL.Environment,PDL.Lab,PDL.[Site],PDL.[Scope],PDL.created_dt,PDL.Release_id,PDL.DeployedBy,PDL.startTime,PDL.endTime,DATEDIFF(MINUTE, startTime , endTime) AS MinuteDiff,PDL.Description from pcm_deployment_logs as PDL inner join Milestone as ML on ML.environment = PDL.Scope order by PDL.created_dt desc";
            SqlConnection sqlCon = new SqlConnection(DepCon);
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
            SqlCommandBuilder sqlCmd = new SqlCommandBuilder(sqlDa);
            DataTable dt = new DataTable();
            sqlDa.Fill(dt);
            gdvDeployLog.DataSource = dt;
            gdvDeployLog.DataBind();
            gdvDeployLog.EmptyDataText = "No Records Found..!!";
        }

        protected void gdvDeployLog_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            
           
                gdvDeployLog.PageIndex = e.NewPageIndex;
                BindReleaselog();
            
        }
        private void ExportData()
        {
            gdvDeployLog.AllowPaging = false;
            BindReleaselog();
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=ExportGridData.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htW = new HtmlTextWriter(sw);
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            gdvDeployLog.RenderControl(htW);
            Response.Output.Write(sw.ToString());
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void ddlMilestone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMilestone.SelectedValue.ToString() == "Select")
            {
                ddlScope.Items.Clear();
                ddlLab.Items.Clear();
            }
            else
            {
                string strMilestone = ddlMilestone.SelectedValue.ToString();
                string sqlQuery1 = "Select distinct environment from Milestone where milestone='" + strMilestone + "'";
                string sqlQuery2 = "select distinct PCM.lab from PCM_Deployment_logs as PCM inner join Milestone as ML on ML.Environment = PCM.scope where ML.Milestone='" + strMilestone.ToString() + "'";
                SqlConnection sqlCon = new SqlConnection(DepCon);
                SqlDataAdapter sqlDa1 = new SqlDataAdapter(sqlQuery1, sqlCon);
                SqlDataAdapter sqlDa2 = new SqlDataAdapter(sqlQuery2, sqlCon);
                SqlCommandBuilder sqlCmd1 = new SqlCommandBuilder(sqlDa1);
                SqlCommandBuilder sqlCmd2 = new SqlCommandBuilder(sqlDa2);
                DataSet ds1 = new DataSet();
                DataSet ds2 = new DataSet();
                sqlDa1.Fill(ds1);
                sqlDa2.Fill(ds2);

                ddlScope.Items.Clear();
                ddlScope.DataSource = ds1;
                ddlScope.DataTextField = "environment";
                ddlScope.DataValueField = "environment";
                ddlScope.DataBind();

                ddlLab.Items.Clear();
                ddlLab.DataSource = ds2;
                ddlLab.DataTextField = "Lab";
                ddlLab.DataValueField = "lab";
                ddlLab.DataBind();

            }
        }

        protected void btnShowFilter_Click(object sender, EventArgs e)
        {
            string strFromDate = txtFromDate.Text;
            string strToDate = txtToDate.Text;
            string sqlQuery = "Select ML.Milestone,PDL.Environment,PDL.Lab,PDL.[Site],PDL.[Scope],PDL.created_dt,PDL.Release_id,PDL.DeployedBy,PDL.startTime,PDL.endTime,DATEDIFF(MINUTE, startTime , endTime) AS MinuteDiff,PDL.Description from pcm_deployment_logs as PDL inner join Milestone as ML on ML.environment = PDL.Scope Where ML.MileStone='" + ddlMilestone.SelectedValue.ToString() + "' and PDL.scope='" + ddlScope.SelectedValue.ToString() + "' and PDL.lab= '" + ddlLab.SelectedValue.ToString() + "' and convert(varchar(30),created_dt,101)>='" + strFromDate + "' and convert(varchar(30),created_dt,101)<= '" + strToDate + "' order by PDL.created_dt desc";
            SqlConnection sqlCon = new SqlConnection(DepCon);
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
            SqlCommandBuilder sqlCmd = new SqlCommandBuilder(sqlDa);
            DataTable dt = new DataTable();
            sqlDa.Fill(dt);
            gdvDeployLog.DataSource = dt;
            gdvDeployLog.DataBind();
            gdvDeployLog.EmptyDataText = "No Records Found..!!";
        }

        protected void btnRelId_Click(object sender, EventArgs e)
        {

        }

        protected void gdvDeployLog_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PowershellModule(string strScope, string strId, string strEnv, string strSite)
        {
            //string strBatchFile = "\\\\eu51fiv02\\ITR02_DE_elims_NG\\RDM\\AutomationScripts\\Deployment_Report\\Script_batch.bat";
            string strBatchFile = "c:\\scripts\\script_batch.bat";

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.FileName = System.Environment.GetEnvironmentVariable("COMSPEC");
            startInfo.Arguments = string.Format("/C {0} \"{1}\" \"{2}\" \"{3}\" \"{4}\" ", strBatchFile, strScope, strId, strEnv, strSite);
            process.StartInfo = startInfo;
            process.Start();
            //Session["Flag"]=process.ExitCode.ToString();


        }

        protected void btnloadComp_Click(object sender, EventArgs e)
        {
            Session["Flag"] = "1";
            Button clickedButton = (Button)sender;
            GridViewRow row = (GridViewRow)clickedButton.NamingContainer;
            int idx = row.RowIndex;
            string strEnv = gdvDeployLog.Rows[idx].Cells[0].Text;
            string strScope = gdvDeployLog.Rows[idx].Cells[2].Text;
            string strSite = gdvDeployLog.Rows[idx].Cells[3].Text;
            string strRelId = (row.FindControl("lblRelId") as Label).Text;
            Button btnView = row.FindControl("btnViewComp") as Button;
            Button btnLoad = row.FindControl("btnloadComp") as Button;
            Session.Timeout = 300;
            Session["strEnv"] = strEnv;
            Session["strScope"] = strScope;
            Session["strSite"] = strSite;
            Session["strRelId"] = strRelId;

            PowershellModule(strScope, strRelId, strEnv, strSite);
            btnLoad.Text = "Loaded";
            btnLoad.Enabled = false;
            btnView.Enabled = true;
            //if(Session["Flag"].ToString()=="0")
            //{ 
            //btnView.Enabled = true;
            //}
        }

        protected void btnDateFilter_Click(object sender, EventArgs e)
        {
            string strFromDate = txtFromDate.Text;
            string strToDate = txtToDate.Text;
            string sqlQuery = "Select ML.Milestone,PDL.Environment,PDL.Lab,PDL.[Site],PDL.[Scope],PDL.created_dt,PDL.Release_id,PDL.DeployedBy,PDL.startTime,PDL.endTime,DATEDIFF(MINUTE, startTime , endTime) AS MinuteDiff,PDL.Description from pcm_deployment_logs as PDL inner join Milestone as ML on ML.environment = PDL.Scope Where convert(varchar(30),created_dt,101)>='" + strFromDate + "' and convert(varchar(30),created_dt,101)<= '" + strToDate + "' order by PDL.created_dt desc";
            SqlConnection sqlCon = new SqlConnection(DepCon);
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
            SqlCommandBuilder sqlCmd = new SqlCommandBuilder(sqlDa);
            DataTable dt = new DataTable();
            sqlDa.Fill(dt);
            gdvDeployLog.DataSource = dt;
            gdvDeployLog.DataBind();
            gdvDeployLog.EmptyDataText = "No Records Found..!!";
        }
    }
}