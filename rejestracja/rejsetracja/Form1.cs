﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace rejsetracja
{
    public partial class Form1 : Form
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\git projekt\baza3\customers.mdf;Integrated Security=True;Connect Timeout=30;Integrated Security=True;Connect Timeout=30;User Instance=True");
        int CustomerId = 0;
        public Form1()
        {
            InitializeComponent();
        }

        List<Reservation> GetReservations(string roomNumber)
        {
            DataTable dt = new DataTable();
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
            SqlCommand query = new SqlCommand("SELECT * FROM tb_rezerwacje WHERE RoomNumber = "+roomNumber, sqlCon);
            SqlDataReader queryResult = query.ExecuteReader();
            dt.Load(queryResult);
            label8.Text = dt.Rows.Count.ToString();
            List<Reservation> reservations = new List<Reservation>();
            for(int i =0; i< dt.Rows.Count; i++)
            {
                DataRow dw = dt.Rows[i];
                Reservation reservation;
                reservation.customerID = int.Parse(dw[0].ToString());
                reservation.name = dw[1].ToString();
                reservation.surname = dw[2].ToString();
                reservation.mobileNumber = dw[3].ToString();
                reservation.bookingFrom = DateTime.Parse(dw[4].ToString());
                reservation.bookingUntil = DateTime.Parse(dw[5].ToString());
                reservation.roomNumber = dw[6].ToString();
                reservations.Add(reservation);

            }
            
            sqlCon.Close();
            return reservations;
        }

        /*private void przycisk(object sender, EventArgs e)
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                
            }
            catch (Exception rx)
            {
                MessageBox.Show(ex.Message, "błąd");
            }
            finally
            {
                sqlCon.Close()
            }
        }*/


        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!RoomChecker.isRoomAvailable(
                DateTime.Parse(txtReservationFrom.Text),
                DateTime.Parse(txtReservationUntil.Text),
                GetReservations(txtRoomNumber.Text)))
            {
                MessageBox.Show("W podanym terminie dany pokuj jest zajęty!");

                label7.Text ="zajęte";
            }
            else
            {
                try
                {
                    if (sqlCon.State == ConnectionState.Closed)
                        sqlCon.Open();
                    if (btnSave.Text == "Zapisz")
                    {
                        SqlCommand sqlCmd = new SqlCommand("ReservationAddOrEdit", sqlCon);
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@mode", "Add");
                        sqlCmd.Parameters.AddWithValue("@CustomerID", 0);
                        sqlCmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Surname", txtSurname.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@MobileNumber", txtMobileNumber.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Booking_from", txtReservationFrom.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Booking_until", txtReservationUntil.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@RoomNumber", txtRoomNumber.Text.Trim());
                        sqlCmd.ExecuteNonQuery();
                        MessageBox.Show("Zapisano pomyślnie");
                    }
                    else
                    {
                        SqlCommand sqlCmd = new SqlCommand("ReservationAddOrEdit", sqlCon);
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@mode", "Edit");
                        sqlCmd.Parameters.AddWithValue("@CustomerID", CustomerId);
                        sqlCmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Surname", txtSurname.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@MobileNumber", txtMobileNumber.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Booking_from", txtReservationFrom.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Booking_until", txtReservationUntil.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@RoomNumber", txtRoomNumber.Text.Trim());
                        sqlCmd.ExecuteNonQuery();
                        MessageBox.Show("Zaktualizowano pomyślnie");
                    }
                    Reset();
                    FillDataGridView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "błąd");

                }
                finally
                {
                    sqlCon.Close();
                }
            }


            
        }

        void FillDataGridView()
        {
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("ReservationViewOrSearch", sqlCon);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("@ReservationSurname", txtSearch.Text.Trim());
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            dataGridView.DataSource = dtbl;
            dataGridView.Columns[0].Visible = false;
            sqlCon.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            try
            {
                FillDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd");

            }


        }

  
        private void dataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow.Index != -1)
            {
                CustomerId = Convert.ToInt32(dataGridView.CurrentRow.Cells[0].Value.ToString());
                txtName.Text = dataGridView.CurrentRow.Cells[1].Value.ToString();
                txtSurname.Text = dataGridView.CurrentRow.Cells[2].Value.ToString();
                txtMobileNumber.Text = dataGridView.CurrentRow.Cells[3].Value.ToString();
                txtReservationFrom.Text = dataGridView.CurrentRow.Cells[4].Value.ToString();
                txtReservationUntil.Text = dataGridView.CurrentRow.Cells[5].Value.ToString();
                txtRoomNumber.Text = dataGridView.CurrentRow.Cells[6].Value.ToString();
                btnSave.Text = "Zaktualizuj";
                btnDelete.Enabled = true;
            }
        }

        void Reset()
        {
            txtName.Text = txtSurname.Text = txtMobileNumber.Text = txtReservationFrom.Text = 
                txtReservationUntil.Text = txtRoomNumber.Text = "";
            btnSave.Text = "Zapisz";
            CustomerId = 0;
            btnDelete.Enabled = false ;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                   if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                   SqlCommand sqlCmd = new SqlCommand("CustomerDeletion", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@CustomerId", CustomerId);
                    sqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Usunięto pomyślnie");
                    Reset();
                    FillDataGridView();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Błąd");
            }


        }

       

    }
}

