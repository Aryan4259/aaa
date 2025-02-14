using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace Expense_Tracker.Expense_Tracker
{
    public partial class by_whom_report_edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string id = Session["ByWhomId"].ToString();
                string constr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Expense-Tracker\\Expense-Tracker\\App_Data\\Database1.mdf;Integrated Security=True";
                string query = "select * from [transaction] where id=@id";
                using (SqlConnection con = new SqlConnection(constr)) {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con)) {
                        cmd.Parameters.AddWithValue("@id", id);
                        SqlDataReader reader = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        if (dt.Rows.Count > 0) {
                            txtIncome.Text = dt.Rows[0]["income"].ToString();
                            txtCategory.Text = dt.Rows[0]["category"].ToString();
                            txtPayType.Text = dt.Rows[0]["pay_type"].ToString();
                            txtByWhom.Text = dt.Rows[0]["by_whom"].ToString();
                            txtDate.Text = dt.Rows[0]["date"].ToString();
                        }
                    }
                }
            }
        }

        protected void btnAddExpense_Click(object sender, EventArgs e)
        {
            string id = Session["ByWhomId"].ToString();
            string constr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Expense-Tracker\\Expense-Tracker\\App_Data\\Database1.mdf;Integrated Security=True";
            string query = "update [transaction] set income=@income, category=@category, pay_type=@paytype, by_whom=@by_whom, date=@date where id=@id";
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@income", txtIncome.Text);
                    cmd.Parameters.AddWithValue("@category", txtCategory.Text);
                    cmd.Parameters.AddWithValue("@paytype", txtPayType.Text);
                    cmd.Parameters.AddWithValue("@by_whom", txtByWhom.Text);
                    cmd.Parameters.AddWithValue("@date", txtDate.Text);
                    cmd.ExecuteNonQuery();
                }
            }
            Response.Redirect("by-whom-report.aspx");
        }
    }
}