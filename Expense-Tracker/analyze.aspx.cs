using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Expense_Tracker.Expense_Tracker
{
    public partial class analyze : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string MNO = Session["MobileNumber"].ToString();
                string conn_str = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Expense-Tracker\\Expense-Tracker\\App_Data\\Database1.mdf;Integrated Security=True";
                string query = "select DISTINCT pay_type from [transaction] where mno=" + MNO + "order by pay_type";
                SqlConnection conn = new SqlConnection(conn_str);
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);
                DropDownList1.DataSource = table;
                DropDownList1.DataTextField = "pay_type";
                DropDownList1.DataValueField = "pay_type";
                DropDownList1.DataBind();
            }
            income();
            expense();
            statement();
            
        }
        private void income()
        {
            string MNO = Session["MobileNumber"].ToString();
            string conn_str = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Expense-Tracker\\Expense-Tracker\\App_Data\\Database1.mdf;Integrated Security=True";
            string query = "select * from [transaction] where income > 0 and mno = @mno";

            using (SqlConnection conn = new SqlConnection(conn_str))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@mno", MNO);
                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    decimal balance = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["income"] != DBNull.Value)
                            balance += Convert.ToDecimal(row["income"]);
                        if (row["expense"] != DBNull.Value)
                            balance -= Convert.ToDecimal(row["expense"]);

                        row["balance"] = balance; // Update balance in the DataTable itself.
                    }

                    // Update the database after updating the DataTable.
                    foreach (DataRow row in dt.Rows)
                    {
                        string uQuery = "update [transaction] set balance = @balance where id = @id";
                        using (SqlCommand ucmd = new SqlCommand(uQuery, conn))
                        {
                            ucmd.Parameters.AddWithValue("@balance", row["balance"]);
                            ucmd.Parameters.AddWithValue("@id", row["id"]);
                            ucmd.ExecuteNonQuery();
                        }
                    }

                    IncomeRepeater.DataSource = dt;
                    IncomeRepeater.DataBind();
                }
            }
        }

        private void expense()
        {
            string MNO = Session["MobileNumber"].ToString();
            string conn_str = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Expense-Tracker\\Expense-Tracker\\App_Data\\Database1.mdf;Integrated Security=True";
            string query = "select * from [transaction] where expense > 0 and mno = @mno";

            using (SqlConnection conn = new SqlConnection(conn_str))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@mno", MNO);
                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    decimal balance = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["income"] != DBNull.Value)
                            balance += Convert.ToDecimal(row["income"]);
                        if (row["expense"] != DBNull.Value)
                            balance -= Convert.ToDecimal(row["expense"]);

                        row["balance"] = balance; // Update balance in the DataTable itself.
                    }

                    // Update the database after updating the DataTable.
                    foreach (DataRow row in dt.Rows)
                    {
                        string uQuery = "update [transaction] set balance = @balance where id = @id";
                        using (SqlCommand ucmd = new SqlCommand(uQuery, conn))
                        {
                            ucmd.Parameters.AddWithValue("@balance", row["balance"]);
                            ucmd.Parameters.AddWithValue("@id", row["id"]);
                            ucmd.ExecuteNonQuery();
                        }
                    }

                    ExpenseRepeater.DataSource = dt;
                    ExpenseRepeater.DataBind();
                }
            }
        }
        private void statement()
        {
            string MNO = Session["MobileNumber"].ToString();
            string conn_str = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Expense-Tracker\\Expense-Tracker\\App_Data\\Database1.mdf;Integrated Security=True";
            string query = "SELECT * from [transaction] where (expense>0 OR income>0) and mno=" + MNO + "";

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
                        if (row["income"].ToString() == "")
                            row["income"] = "0";
                        if (row["expense"].ToString() == "")
                            row["expense"] = "0";
                        Session["StatementId"] = row["id"];
                    }
                    statementRepeater.DataSource = reader;
                    statementRepeater.DataBind();
                }
            }
        }

        protected void btnAddIncome_Click(object sender, EventArgs e)
        {
            // Call the method to fetch and bind data
            bydate();
        }

        private void bydate()
        {
            if (Session["MobileNumber"] == null)
            {
                // Handle session null case
                return;
            }

            string MNO = Session["MobileNumber"].ToString();
            string conn_str = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Expense-Tracker\\Expense-Tracker\\App_Data\\Database1.mdf;Integrated Security=True";
            string query = @"SELECT * FROM [transaction] WHERE date BETWEEN @StartDate AND @EndDate AND mno = @mno";

            try
            {
                using (SqlConnection conn = new SqlConnection(conn_str))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        DateTime startDate, endDate;



                        // Validate and parse dates
                        if (DateTime.TryParse(txtStartDate.Text, out startDate) && DateTime.TryParse(txtEndDate.Text, out endDate))
                        {
                            cmd.Parameters.AddWithValue("@StartDate", startDate);
                            cmd.Parameters.AddWithValue("@EndDate", endDate);
                            cmd.Parameters.AddWithValue("@mno", MNO);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                DataTable dt = new DataTable();
                                dt.Load(reader);

                                if (dt.Rows.Count > 0)
                                {
                                    ByDateRepeater.DataSource = dt;
                                    ByDateRepeater.DataBind();
                                }
                                else
                                {
                                    // Handle no data found case
                                    ByDateRepeater.DataSource = null;
                                    ByDateRepeater.DataBind();
                                }
                            }
                            ScriptManager.RegisterStartupScript(this, GetType(), "ShowModal", "openModal('byDateModal');", true);
                        }
                        else
                        {
                            // Handle invalid date format case
                            // You can show a message to the user here
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (log them, show a message, etc.)
            }
        }

        protected void ch1_Click(object sender, EventArgs e)
        {
            
        }

        protected void ch2_Click(object sender, EventArgs e)
        {
           
        }

        protected void ch3_Click(object sender, EventArgs e)
        {
            
        }

        protected void ch4_Click(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("by-whom-report.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("to-whom-report.aspx");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("category-report.aspx");
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            Response.Redirect("overall-report.aspx");
        }

        

        protected void addptype_Click(object sender, EventArgs e)
        {
            string MNO = Session["MobileNumber"].ToString();
            string conn_str = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Expense-Tracker\\Expense-Tracker\\App_Data\\Database1.mdf;Integrated Security=True";
            string query = "select * from [transaction] where pay_type='" + DropDownList1.SelectedItem.Text + "' and mno=" + MNO + "";

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
                        Session["PaymentId"] = row["id"];
                    }
                    PaymentTypeRepeater.DataSource = reader;
                    PaymentTypeRepeater.DataBind();
                }
            }

        }

        protected void btnrange_Click(object sender, EventArgs e)
        {
            string MNO = Session["MobileNumber"]?.ToString();
            if (string.IsNullOrEmpty(MNO))
            {
                Response.Write("Session expired or mobile number not found.");
                return;
            }

            if (string.IsNullOrWhiteSpace(minbal.Text) || string.IsNullOrWhiteSpace(maxbal.Text))
            {
                Response.Write("Please enter both minimum and maximum balance.");
                return;
            }

            string conn_str = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Expense-Tracker\\Expense-Tracker\\App_Data\\Database1.mdf;Integrated Security=True";
            string query = "SELECT * FROM [transaction] WHERE balance BETWEEN @minBalance AND @maxBalance AND mno = @mno";

            using (SqlConnection conn = new SqlConnection(conn_str))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@minBalance", Convert.ToDecimal(minbal.Text));
                    cmd.Parameters.AddWithValue("@maxBalance", Convert.ToDecimal(maxbal.Text));
                    cmd.Parameters.AddWithValue("@mno", MNO);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable table = new DataTable();
                        table.Load(reader);
                        if (table.Rows.Count == 0)
                        {
                            Response.Write("No records found for the given range.");
                        }
                        NumberRangeRepeater.DataSource = table;
                        NumberRangeRepeater.DataBind();
                    }
                }
            }

            // Reopen modal after binding data
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowModal", "openModal('numberRangeModal');", true);

        }
    } 
}