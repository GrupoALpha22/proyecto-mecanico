using System.Data.SqlClient;
using ALPHA.Models;
using System.Data;
using System.Collections.Generic;
using System;

namespace ALPHA.Data
{
    public class MecanicoData
    {

        public List<MecanicoModel> Listar()
        {
            var olista = new List<MecanicoModel>();
            var cn = new Connection();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ListarMecanico", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        olista.Add(new MecanicoModel()
                        {
                            Idmecanico = Convert.ToInt32(dr["Idmecanico"]),
                            Idpersona = Convert.ToInt32(dr["Idpersona"]),
                            Identificacion = dr["Identificacion"].ToString(),
                            Nombre = dr["Nombre"].ToString(),
                            Apellido = dr["Apellido"].ToString(),
                            anacimiento = dr["anacimiento"].ToString(),
                            Direccion = dr["Direccion"].ToString(),
                            NivelEducativo = dr["NivelEducativo"].ToString(),
                        });
                    }
                }
            }
            return olista;

        }

        public MecanicoModel Obtener(int Idpersona)
        {
            var oMecanico = new MecanicoModel();
            var cn = new Connection();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ObtenerMecanico", conexion);
                cmd.Parameters.AddWithValue("Idpersona", Idpersona);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {

                        oMecanico.Idpersona = Convert.ToInt32(dr["Idpersona"]);
                        oMecanico.Identificacion = dr["Identificacion"].ToString();
                        oMecanico.Nombre = dr["Nombre"].ToString();
                        oMecanico.Apellido = dr["Apellido"].ToString();
                        oMecanico.anacimiento = dr["anacimiento"].ToString();
                        oMecanico.Direccion = dr["Direccion"].ToString();
                        oMecanico.NivelEducativo = dr["NivelEducativo"].ToString();

                    }
                }
            }
            return oMecanico;

        }
        public bool Guardar(MecanicoModel oMecanico)
        {
            bool rpta;
            try
            {
                var cn = new Connection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_GuardarMecanico", conexion);
                    cmd.Parameters.AddWithValue("Identificacion", oMecanico.Identificacion);
                    cmd.Parameters.AddWithValue("Nombre", oMecanico.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", oMecanico.Apellido);
                    cmd.Parameters.AddWithValue("anacimiento", oMecanico.anacimiento);
                    cmd.Parameters.AddWithValue("Direccion", oMecanico.Direccion);
                    cmd.Parameters.AddWithValue("NivelEducativo", oMecanico.NivelEducativo);
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

        public bool Editar(MecanicoModel oMecanico)
        {
            bool rpta;
            try
            {
                var cn = new Connection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EditarMecanico", conexion);
                    cmd.Parameters.AddWithValue("Idpersona", oMecanico.Idpersona);
                    cmd.Parameters.AddWithValue("Identificacion", oMecanico.Identificacion);
                    cmd.Parameters.AddWithValue("Nombre", oMecanico.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", oMecanico.Apellido);
                    cmd.Parameters.AddWithValue("anacimiento", oMecanico.anacimiento);
                    cmd.Parameters.AddWithValue("Direccion", oMecanico.Direccion);
                    cmd.Parameters.AddWithValue("NivelEducativo", oMecanico.NivelEducativo);
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
                    SqlCommand cmd = new SqlCommand("sp_EliminarMecanico", conexion);
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
