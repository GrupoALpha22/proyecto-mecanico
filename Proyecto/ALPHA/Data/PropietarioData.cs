using System.Data.SqlClient;
using ALPHA.Models;
using System.Data;
using System.Collections.Generic;
using System;

namespace ALPHA.Data
{
    public class PropietarioData
    {

        public List<PropietarioModel> Listar()
        {
            var olista = new List<PropietarioModel>();
            var cn = new Connection();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ListarPropietario", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        olista.Add(new PropietarioModel()
                        {
                            Idpropietario = Convert.ToInt32(dr["Idpropietario"]),
                            Idpersona = Convert.ToInt32(dr["Idpersona"]),
                            Identificacion = dr["Identificacion"].ToString(),
                            Nombre = dr["Nombre"].ToString(),
                            Apellido = dr["Apellido"].ToString(),
                            anacimiento = dr["anacimiento"].ToString(),
                            Ciudad = dr["Ciudad"].ToString(),
                            Email = dr["Email"].ToString(),
                        });
                    }
                }
            }
            return olista;

        }

        public PropietarioModel Obtener(int Idpersona)
        {
            var oPropietario = new PropietarioModel();
            var cn = new Connection();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ObtenerPropietario", conexion);
                cmd.Parameters.AddWithValue("Idpersona", Idpersona);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {

                        oPropietario.Idpersona = Convert.ToInt32(dr["Idpersona"]);
                        oPropietario.Identificacion = dr["Identificacion"].ToString();
                        oPropietario.Nombre = dr["Nombre"].ToString();
                        oPropietario.Apellido = dr["Apellido"].ToString();
                        oPropietario.anacimiento = dr["anacimiento"].ToString();
                        oPropietario.Ciudad = dr["Ciudad"].ToString();
                        oPropietario.Email = dr["Email"].ToString();

                    }
                }
            }
            return oPropietario;

        }
        public bool Guardar(PropietarioModel opropietario)
        {
            bool rpta;
            try
            {
                var cn = new Connection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_GuardarPropietario", conexion);
                    cmd.Parameters.AddWithValue("Identificacion", opropietario.Identificacion);
                    cmd.Parameters.AddWithValue("Nombre", opropietario.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", opropietario.Apellido);
                    cmd.Parameters.AddWithValue("anacimiento", opropietario.anacimiento);
                    cmd.Parameters.AddWithValue("Ciudad", opropietario.Ciudad);
                    cmd.Parameters.AddWithValue("Email", opropietario.Email);
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

        public bool Editar(PropietarioModel opropietario)
        {
            bool rpta;
            try
            {
                var cn = new Connection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EditarPropietario", conexion);
                    cmd.Parameters.AddWithValue("Idpersona", opropietario.Idpersona);
                    cmd.Parameters.AddWithValue("Identificacion", opropietario.Identificacion);
                    cmd.Parameters.AddWithValue("Nombre", opropietario.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", opropietario.Apellido);
                    cmd.Parameters.AddWithValue("anacimiento", opropietario.anacimiento);
                    cmd.Parameters.AddWithValue("Ciudad", opropietario.Ciudad);
                    cmd.Parameters.AddWithValue("Email", opropietario.Email);
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
                    SqlCommand cmd = new SqlCommand("sp_EliminarPropietario", conexion);
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
