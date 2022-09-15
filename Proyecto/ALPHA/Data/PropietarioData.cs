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
        //public PersonModel Obtener(int Idpersona)
        //{
        //    var oPersona = new PersonModel();
        //    var cn = new Connection();
        //    using (var conexion = new SqlConnection(cn.getCadenaSQL()))
        //    {
        //        conexion.Open();
        //        SqlCommand cmd = new SqlCommand("sp_Obtener", conexion);
        //        cmd.Parameters.AddWithValue("Idpersona", Idpersona);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        using (var dr = cmd.ExecuteReader())
        //        {
        //            while (dr.Read())
        //            {

        //                oPersona.Idpersona = Convert.ToInt32(dr["Idpersona"]);
        //                oPersona.Identificacion = dr["Identificacion"].ToString();
        //                oPersona.Nombre = dr["Nombre"].ToString();
        //                oPersona.Apellido = dr["Apellido"].ToString();
        //                oPersona.anacimiento = dr["anacimiento"].ToString();

        //            }
        //        }
        //    }
        //    return oPersona;

        //}

        //public bool Guardar(PersonModel opersona)
        //{
        //    bool rpta;
        //    try
        //    {
        //        var cn = new Connection();
        //        using (var conexion = new SqlConnection(cn.getCadenaSQL()))
        //        {
        //            conexion.Open();
        //            SqlCommand cmd = new SqlCommand("sp_Guardar", conexion);
        //            cmd.Parameters.AddWithValue("Identificacion", opersona.Identificacion);
        //            cmd.Parameters.AddWithValue("Nombre", opersona.Nombre);
        //            cmd.Parameters.AddWithValue("Apellido", opersona.Apellido);
        //            cmd.Parameters.AddWithValue("anacimiento", opersona.anacimiento);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.ExecuteNonQuery();
        //        }
        //        rpta = true;


        //    }
        //    catch (Exception e)
        //    {
        //        string error = e.Message;
        //        rpta = false;

        //    }

        //    return rpta;
        //}
        //public bool Editar(PersonModel opersona)
        //{
        //    bool rpta;
        //    try
        //    {
        //        var cn = new Connection();
        //        using (var conexion = new SqlConnection(cn.getCadenaSQL()))
        //        {
        //            conexion.Open();
        //            SqlCommand cmd = new SqlCommand("sp_Editar", conexion);
        //            cmd.Parameters.AddWithValue("Idpersona", opersona.Idpersona);
        //            cmd.Parameters.AddWithValue("Identificacion", opersona.Identificacion);
        //            cmd.Parameters.AddWithValue("Nombre", opersona.Nombre);
        //            cmd.Parameters.AddWithValue("Apellido", opersona.Apellido);
        //            cmd.Parameters.AddWithValue("anacimiento", opersona.anacimiento);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.ExecuteNonQuery();
        //        }
        //        rpta = true;


        //    }
        //    catch (Exception e)
        //    {
        //        string error = e.Message;
        //        rpta = false;

        //    }

        //    return rpta;
        //}
        //public bool Eliminar(int Idpersona)
        //{
        //    bool rpta;
        //    try
        //    {
        //        var cn = new Connection();
        //        using (var conexion = new SqlConnection(cn.getCadenaSQL()))
        //        {
        //            conexion.Open();
        //            SqlCommand cmd = new SqlCommand("sp_Eliminar", conexion);
        //            cmd.Parameters.AddWithValue("Idpersona", Idpersona);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.ExecuteNonQuery();
        //        }
        //        rpta = true;


        //    }
        //    catch (Exception e)
        //    {
        //        string error = e.Message;
        //        rpta = false;

        //    }

        //    return rpta;
        //}

    }
}
