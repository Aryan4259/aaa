﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace Expense_Tracker.Expense_Tracker
{
    public partial class income_report_edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["id"] != null)
                    {
                        int id = Convert.ToInt32(Request.QueryString["id"]);
                        LoadTransactionDetails(id);
                          // 🚀 Auto-update when page loads
                    }
                    else
                    {
                        Response.Redirect("income-report.aspx");
                    }
                }
            }
           
        }
        private void LoadTransactionDetails(int id)
        {
            string conn_str = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Expense-Tracker\\Expense-Tracker\\App_Data\\Database1.mdf;Integrated Security=True";
            string query = "SELECT * FROM [transaction] WHERE ID = @ID";

            using (SqlConnection conn = new SqlConnection(conn_str))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtDate.Text = Convert.ToDateTime(reader["date"]).ToString("yyyy-MM-dd");
                        txtIncome.Text = reader["income"].ToString();
                        txtCategory.Text = reader["category"].ToString();
                        txtPayType.Text = reader["pay_type"].ToString();
                        txtByWhom.Text = reader["by_whom"].ToString();
                        txtDate.Text = reader["date"].ToString();
                    }
                }
            }
        }
        
        protected void btnAddExpense_Click(object sender, EventArgs e)
        {
            // Get ID from Query String
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("income-report.aspx");
                return;
            }

            int id = Convert.ToInt32(Request.QueryString["id"]);
            string constr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Expense-Tracker\\Expense-Tracker\\App_Data\\Database1.mdf;Integrated Security=True";
            string query = "UPDATE [transaction] SET income=@income, category=@category, pay_type=@paytype, by_whom=@by_whom, date=@date WHERE id=@id";

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

                    // Convert Date
                    DateTime date;
                    if (DateTime.TryParse(txtDate.Text, out date))
                    {
                        cmd.Parameters.AddWithValue("@date", date);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@date", DBNull.Value);
                    }

                    cmd.ExecuteNonQuery();
                }
            }

            // Redirect back to the income report page
            Response.Redirect("income-report.aspx");
        }
    }
}