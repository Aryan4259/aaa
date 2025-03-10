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
    public partial class by_whom_report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string MNO = Session["MobileNumber"].ToString();
                string conn_str = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Expense-Tracker\\Expense-Tracker\\App_Data\\Database1.mdf;Integrated Security=True";
                string query = "select DISTINCT by_whom from [transaction] where mno="+MNO+" order by by_whom";
                SqlConnection conn = new SqlConnection(conn_str);
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);
                DropDownList1.DataSource = table;
                DropDownList1.DataTextField = "by_whom";
                DropDownList1.DataValueField = "by_whom";
                DropDownList1.DataBind();
            }
        }

        protected void btnAddIncome_Click(object sender, EventArgs e)
        {
            string MNO = Session["MobileNumber"].ToString();
            string conn_str = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Expense-Tracker\\Expense-Tracker\\App_Data\\Database1.mdf;Integrated Security=True";
            string query = "select * from [transaction] where by_whom='"+DropDownList1.SelectedItem.Text+"' and mno="+MNO+"";

            using (SqlConnection conn = new SqlConnection(conn_str))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable transaction = new DataTable();
                    decimal balance = 0;
                    foreach (DataRow row in transaction.Rows)
                    {
                        if (row["income"].ToString() != "")
                            balance += Convert.ToDecimal(row["income"]);
                        if (row["expense"].ToString() != "")
                            balance += Convert.ToDecimal(row["expense"]);
                        Session["ByWhomId"] = row["id"];
                    }
                    IncomeRepeater.DataSource = reader;
                    IncomeRepeater.DataBind();
                }
            }
        }
        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            Button deletebutton = (Button)sender;
            string id = deletebutton.CommandArgument;
            string conn_str = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Expense-Tracker\\Expense-Tracker\\App_Data\\Database1.mdf;Integrated Security=True";
            string query = "delete from [transaction] where id = @id";

            using (SqlConnection conn = new SqlConnection(conn_str))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        protected void EditButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("by-whom-report-edit.aspx");
        }
    }
}