﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="payment-type-report-edit.aspx.cs" Inherits="Expense_Tracker.Expense_Tracker.payment_type_report_edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <meta charset="UFT-8" />
        <meta name="viewport" content="width=divice-width, initial-scale=1.0" />
        <title>Expense Tracker | Edit Items</title>
        <link rel="icon" type="image/png" href="#" sizes="32x32" />
        <!--<link rel="stylesheet" href="../Styles/Home.css" />-->
        <style>
            * 
            {
                padding: 0;
                margin: 0;
                box-sizing: border-box;
                font-family: 'Inter', Sans-Serif;
                list-style: none;
                text-decoration: none;
                scroll-behavior: smooth;
                border: none;
                outline: none;
            }

            :root 
            {
                --bg-color: #191919;
                --second-bg-color: #101010;
                --main-color: #fff;
                --text-color: #000;
                --second-color: #000;
                --other-color: #808080;
                --h1-font:7rem;
                --h2-font: 7rem;
                --p-font: 1.2rem;
            }

            body 
            {
                color: White;
            }
            .img 
            {
                width: 100%;
                height: 100%;
            }
            header 
            {
                position: fixed;
                top: 0;
                right: 0;
                z-index: 1000;
                width: 100%;
                display: flex;
                align-items: center;
                justify-content: space-between;
                background: white;
                opacity: 0.8;
                padding: 28px 11%;
                border-bottom: 1px solid transparent;
                transition: all ease .50s;  
            }

            .logo 
            {
               color: #000;
               font-size: 40px;
               font-weight: bold;    
            }

            .navlist 
            {
                display: flex;    
            }

            #menu-icon 
            {
                font-size: 42px;
                color: var(--text-color);
                z-index: 10001;
                cursor: pointer;
                display: none;
            }

            .nevlist a 
            {
                color: var(--second-color);
                font-size: 25px;
                font-weight: 600;
                margin: 0 20px;
                transition: all ease .40s;
                font-family: 'Special Elite', Serif;
            }

            .nevlist a:hover 
            {
                color:Black;
                font-size: 25px;
                transition; all ease .30s;
                border-bottom: #000 2px solid;
            }

            section 
            {
                padding: 105px 10% 100px;
            }

            .home
            {
                position: relative;
                min-height: 100vh;
                width: 100%;
                background-size: cover;
                background-position: center;
                display: flex;
                align-items: center;
                justify-content: flex-start;
            }

            .text-center
            {
                font-family: 'Caveat', Cursive;
                font-size: 4rem;
                font-weight: 800;
                margin-bottom: 40px;
                color: #000;
                text-align: center;
            }

            .tran_main 
            {
                display: flex;
                justify-content: center;
                gap: 7rem;
                align-items: center;
            }

            .transaction_sub 
            {
                width: 400px;
                height: 430px;
                border-radius: 20px;
                padding: 35px 10px 10px 10px;
                background-color: #9EDFFF;
                text-align: center;
            }

            .text 
            {
                font-size: 40px;
                font-weight: 700;
                color: #000;
                font-family: 'Special Elite', Serif;
            }

            .transaction_sub b 
            {
                color: White;
            }

            .income_main 
            {
                margin-top: 5%;
            }

            .income_main input 
            {
                font-size: 20px;
                height: 35px;
                width: 250px;
                padding: 5px;
                margin-bottom: 5px;
                margin-top: -13px;
                border-radius: 8px;
            }

            .income_main:hover input 
            {
                cursor: pointer;
            }

            .income_btn 
            {
                background-color: #4689CE;
                color: #fff;
                font-weight: 600;
            }

            .income_btn:hover 
            {
                background-color: #407DBD;
                transition: all ease 0.5s;
            }

            .transaction_balance 
            {
                width: 200px;
                height: 150px;
                border-radius: 20px;
                padding: 15px 10px 10px 10px;
                background-color: #9edfff;
                text-align: center;
                font-size: 25px;
                border: lightgreen 5px solid;
            }

            .transaction_balance p 
            {
                margin-top: 30px;
                font-size: 25px;
                font-weight: 600;
            }

            .about_main 
            {
                display: flex;
                justify-content: center;
                gap: 18rem;
            }

            .sub1
            {
                width: 300px;
                height: 250px;
                border-radius: 20px;
                padding: 15px 10px 10px 10px;
                background-color: #9EDFFF;
                text-align:center;
                font-weight: 600;
                margin-bottom: 200px;
            }

            .dropdown 
            {
                float: left;
                overflow: hidden;
            }

            .dropdown
            .dropbtn 
            {
                font-size: 22px;
                border: none;
                outline: none;
                color: #000;
                background-color: inherit;
                font-family: inherit;
            }

            .dropdown-content 
            {
                display: none;
                position: absolute;
                background-color: #F9F9F9;
                min-width: 160px;
                box-shadow: 0px 8px 16px 0px rgba(0, 0, 0, 0.2);
                z-index: 1;
            }

            .dropdonwn-content a 
            {
                float: none;
                color: Black;
                padding: 12px 16px;
                text-decoration: none;
                display: block;
                text-align: left;
            }

            .dropdown-content a:hover 
            {
                background-color: #DDD;
            }

            .dropdown:hover .dropdown-content 
            {
                display: block;
            }
            .validation
            {
                font-size: 15px;
                margin-left: 140px;    
            }
        </style>
    </head>
    <body>
    <form id="Form1" runat="server">
        <!-- header section -->
        <header class="sticky">
            <span class="logo">Expense Tracker</span>
            <div class="fa-solid fa-bars" id="menu-icon"></div>
            <ul class="navlist">
                <li><a href="Home.aspx" style="color: #000000">Home</a></li>
                <li><a href="payment-type-report.aspx" style="color: #000000">/ Payment Type Report</a></li>
                <li><a>/ Edit</a></li>
            </ul>
        </header>

        <!--transaction section-->
        <section class="transaction" id="transaction">
            <div class="text-center">Edit Items</div>
            <div class="tran_main">
                <form runat="server" id="mainForm">
                    <div class="transaction_sub">
                        <span class="text">Edit Items</span>
                        <b>__________________________________</b>
                        <div class="income_main">
                            <asp:TextBox ID="txtIncome" runat="server" placeholder="Add Amount &#8377;"></asp:TextBox>
                            <div class="validation"><asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                                    runat="server" ErrorMessage="*Income is Required" ControlToValidate="txtIncome" 
                                    Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator></div>
                            <br />
                            <asp:TextBox ID="txtCategory" runat="server" placeholder="Category"></asp:TextBox>
                            <div class="validation"><asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
                                    runat="server" ErrorMessage="*Category is Required" 
                                    ControlToValidate="txtCategory" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator></div>
                            <br />
                            <asp:TextBox ID="txtPayType" runat="server" placeholder="Payment Type"></asp:TextBox>
                            <div class="validation"><asp:RequiredFieldValidator ID="RequiredFieldValidator3" 
                                    runat="server" ErrorMessage="*Payment type is Required" 
                                    ControlToValidate="txtPayType" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator></div>
                            <br />
                            <asp:TextBox ID="txtByWhom" runat="server" placeholder="To Whom"></asp:TextBox>
                            <div class="validation"><asp:RequiredFieldValidator ID="RequiredFieldValidator4" 
                                    runat="server" ErrorMessage="*By whom is Required" 
                                    ControlToValidate="txtByWhom" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator></div>
                            <br />
                            <asp:TextBox ID="txtDate" runat="server" type="date"></asp:TextBox>
                            <div class="validation"><asp:RequiredFieldValidator ID="RequiredFieldValidator5" 
                                    runat="server" ErrorMessage="*Date is Required" ControlToValidate="txtDate" 
                                    Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator></div>
                            <br />
                            <span>
                                <asp:Button ID="btnAddExpense" runat="server" Text="ADD" value="Add Expence" 
                                    class="income_btn" onclick="btnAddExpense_Click"></asp:Button>
                            </span>
                        </div>
                    </div>
                </form>
            </div>
        </section>
        </form>
    </body>
</html>
