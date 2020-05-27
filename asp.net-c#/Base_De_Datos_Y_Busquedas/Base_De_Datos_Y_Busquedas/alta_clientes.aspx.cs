﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Base_De_Datos_Y_Busquedas
{
    public partial class alta_clientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void BtnAceptar_Click(object sender, EventArgs e)
        {
            string query = @"INSERT INTO clientes (cliente,CUIT)
                            VALUES (@TB1,@TB2)";
            string queryValidar = @"SELECT * FROM clientes WHERE cliente LIKE @TB1";

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());// ESTABLECEMOS LA CONEXION

            try
            {
                SqlCommand commandValidation = new SqlCommand(queryValidar, conn); // CREAMOS EL COMANDO SQL A EJECUTAR
                commandValidation.Parameters.AddWithValue("TB1", TextBox1.Text);
                conn.Open();
                SqlDataReader leerbd = commandValidation.ExecuteReader();
                if (leerbd.Read()==true)
                {
                    Label1.Text = "Usuario existente";
                    conn.Close();
                }
                else
                {
                    try
                    {
                        conn.Close();
                        SqlCommand command = new SqlCommand(query, conn); // CREAMOS EL COMANDO SQL A EJECUTAR
                        command.Parameters.AddWithValue("TB1", TextBox1.Text);
                        command.Parameters.AddWithValue("TB2", TextBox2.Text);
                        conn.Open();//ABRIMOS LA CONEXION
                        command.ExecuteNonQuery(); // EJECUTAMOS EL COMANDO DEFINITIVO
                        conn.Close(); // CERRAMOS LA CONEXION
                        Label1.Text = "Se agregaron los registros correctamente.";
                    }
                    catch (Exception ex)
                    {
                        Label1.Text = ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;

            }
        }
    }
}