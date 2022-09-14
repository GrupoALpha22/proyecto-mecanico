using System.Data.SqlClient;
using ALPHA.Models;
using System.Data;
using System.Collections.Generic;
using System;

namespace ALPHA.Data
{
    public class PersonData
    {

        public List<PersonModel> Listar()
        {
            var olista = new List<PersonModel>();
            var cn = new Connection();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Listar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        olista.Add(new PersonModel()
                        {
                            Idpersona = Convert.ToInt32(dr["Idpersona"]),
                            Identificacion = dr["Identificacion"].ToString(),
                            Nombre = dr["Nombre"].ToString(),
                            Apellido = dr["Apellido"].ToString(),
                            anacimiento = dr["anacimiento"].ToString(),
                        });
                    }
                }
            }
            return olista;

        }

        public PersonModel Obtener(int Idpersona)
        {
            var oPersona = new PersonModel();
            var cn = new Connection();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener", conexion);
                cmd.Parameters.AddWithValue("Idpersona", Idpersona);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {

                        oPersona.Idpersona = Convert.ToInt32(dr["Idpersona"]);
                        oPersona.Identificacion = dr["Identificacion"].ToString();
                        oPersona.Nombre = dr["Nombre"].ToString();
                        oPersona.Apellido = dr["Apellido"].ToString();
                        oPersona.anacimiento = dr["anacimiento"].ToString();

                    }
                }
            }
            return oPersona;

        }

        public bool Guardar(PersonModel opersona)
        {
            bool rpta;
            try
            {
                var cn = new Connection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Guardar", conexion);
                    cmd.Parameters.AddWithValue("Identificacion", opersona.Identificacion);
                    cmd.Parameters.AddWithValue("Nombre", opersona.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", opersona.Apellido);
                    cmd.Parameters.AddWithValue("anacimiento", opersona.anacimiento);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;


            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;

            }

            return rpta;
        }
        public bool Editar(PersonModel opersona)
        {
            bool rpta;
            try
            {
                var cn = new Connection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Editar", conexion);
                    cmd.Parameters.AddWithValue("Idpersona", opersona.Idpersona);
                    cmd.Parameters.AddWithValue("Identificacion", opersona.Identificacion);
                    cmd.Parameters.AddWithValue("Nombre", opersona.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", opersona.Apellido);
                    cmd.Parameters.AddWithValue("anacimiento", opersona.anacimiento);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;


            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;

            }

            return rpta;
        }
        public bool Eliminar(int Idpersona)
        {
            bool rpta;
            try
            {
                var cn = new Connection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Eliminar", conexion);
                    cmd.Parameters.AddWithValue("Idpersona", Idpersona);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;


            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;

            }

            return rpta;
        }

    }
}
