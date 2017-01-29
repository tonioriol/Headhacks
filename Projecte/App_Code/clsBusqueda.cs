using System;
using System.Collections.Generic;
// Afegits
using System.Data.SqlClient;

/// <summary>
/// Descripción breve de clsBusqueda
/// </summary>
public class clsBusqueda
{
    public List<Dictionary<string, string>> Cercar(string ordenacio, string ascdesc, string paramBusqueda)
    {
        bool trobat = false;
        SqlDataReader oSqlDataReader = null;
        Connexio oConnexio = new Connexio();
        SqlCommand oSqlCommand = null;
        Dictionary<string, string> dictPelis = null;
        List<Dictionary<string, string>> llistaPelis = null;

        oConnexio.Obrir();
        try
        {
            string[] vectorParamBusqueda = paramBusqueda.Split(' ');
            llistaPelis = new List<Dictionary<string, string>>();

            if (vectorParamBusqueda.Length == 1 && paramBusqueda.Length > 3)
            {
                oSqlCommand = new SqlCommand("SELECT * FROM pelicules WHERE titol LIKE '%' + @paramBusqueda OR titol LIKE @paramBusqueda + '%' OR titol LIKE '%' + @paramBusqueda + '%' " +
                    "OR director LIKE '%' + @paramBusqueda OR director LIKE @paramBusqueda + '%' OR director LIKE '%' + @paramBusqueda + '%' ORDER BY +@ordenacio +@ascdesc;", oConnexio.Con);
                oSqlCommand.Parameters.Add(new SqlParameter("@paramBusqueda", paramBusqueda));
                oSqlCommand.Parameters.Add(new SqlParameter("@ordenacio", ordenacio)); //ordenacio
                oSqlCommand.Parameters.Add(new SqlParameter("@ascdesc", ascdesc));
                //Definim el parametre utilitzat en l'objecte SqlCommand i l'afegim
                oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    dictPelis = new Dictionary<string, string>();
                    for (int i = 0; i < oSqlDataReader.FieldCount; i++)
                    {
                        dictPelis.Add(oSqlDataReader.GetName(i), Convert.ToString(oSqlDataReader[i]));
                    }
                    // Algorisme de Cerca que busca si la llista conté la pelicula (diccionari ) afegida al for anterior 
                    int k = 0;
                    while (!trobat && k < llistaPelis.Count)
                    {
                        if (llistaPelis[k].Equals(dictPelis))
                            trobat = true;
                        else
                            k++;
                    }
                    if (!trobat)
                        llistaPelis.Add(dictPelis);
                }
            }
            else
            {
                string cad = null;
                for (int j = 0; j < vectorParamBusqueda.Length; j++)
                {
                    cad = (vectorParamBusqueda[j]);
                    if (cad.Length >= 3)
                    {
                        oSqlCommand = new SqlCommand("SELECT * FROM pelicules WHERE titol LIKE '%' + @cad OR titol LIKE @cad + '%' OR titol  LIKE '%' + @cad + '%' " +
                            "OR  director LIKE '%' + @cad OR director LIKE @cad + '%' OR director  LIKE '%' + @cad + '%' ORDER BY +@ordenacio +@ascdesc" + ";", oConnexio.Con);
                        oSqlCommand.Parameters.Add(new SqlParameter("@cad", cad));
                        oSqlCommand.Parameters.Add(new SqlParameter("@ordenacio", ordenacio));
                        oSqlCommand.Parameters.Add(new SqlParameter("@ascdesc", ascdesc));
                        oSqlDataReader = oSqlCommand.ExecuteReader();
                        while (oSqlDataReader.Read())
                        {
                            dictPelis = new Dictionary<string, string>();
                            for (int i = 0; i < oSqlDataReader.FieldCount; i++)
                            {
                                dictPelis.Add(oSqlDataReader.GetName(i), Convert.ToString(oSqlDataReader[i]));
                            }
                            // Algorisme de Cerca que busca si la llista conté la pelicula (diccionari ) afegida al for anterior 
                            int k = 0;
                            while (!trobat && k < llistaPelis.Count)
                            {
                                if (llistaPelis[k]["id"] == dictPelis["id"])
                                    trobat = true;
                                else
                                    k++;
                            }
                            if (!trobat)
                                llistaPelis.Add(dictPelis);
                        }
                        oSqlDataReader.Close();
                    }
                }
            }
            return llistaPelis;
        }
        catch (Exception)
        {
            return null;
        }
        finally
        {
            if (oSqlDataReader != null)
                oSqlDataReader.Close();
            if (oConnexio != null)
                oConnexio.Tancar();
        }
    }

    public List<Dictionary<string, string>> PerDirector(string ordenacio, string ascdesc, string paramBusqueda)
    {
        SqlDataReader oSqlDataReader = null;
        Connexio oConnexio = new Connexio();
        SqlCommand oSqlCommand = null;
        Dictionary<string, string> dictPelis = null;
        List<Dictionary<string, string>> llistaPelis = null;
        bool trobat = false;
        oConnexio.Obrir();
        try
        {
            string[] vectorParamBusqueda = paramBusqueda.Split(' ');
            llistaPelis = new List<Dictionary<string, string>>();

            if (vectorParamBusqueda.Length == 1)
            {
                oSqlCommand = new SqlCommand("SELECT * FROM pelicules WHERE director LIKE '%' + @paramBusqueda OR director LIKE @paramBusqueda + '%' OR director LIKE '%' + @paramBusqueda + '%' ORDER BY +@ordenacio +@ascdesc;", oConnexio.Con);
                oSqlCommand.Parameters.Add(new SqlParameter("@paramBusqueda", paramBusqueda));
                oSqlCommand.Parameters.Add(new SqlParameter("@ordenacio", ordenacio)); //ordenacio
                oSqlCommand.Parameters.Add(new SqlParameter("@ascdesc", ascdesc));
                //Definim el parametre utilitzat en l'objecte SqlCommand i l'afegim
                oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    dictPelis = new Dictionary<string, string>();
                    for (int i = 0; i < oSqlDataReader.FieldCount; i++)
                    {
                        dictPelis.Add(oSqlDataReader.GetName(i), Convert.ToString(oSqlDataReader[i]));
                    }
                    llistaPelis.Add(dictPelis);
                }
            }
            else
            {
                string cad = null;
                for (int j = 0; j < vectorParamBusqueda.Length; j++)
                {
                    cad = (vectorParamBusqueda[j]);
                    if (cad.Length >= 3)
                    {
                        oSqlCommand = new SqlCommand("SELECT * FROM pelicules WHERE director LIKE '%' + @cad OR director LIKE @cad + '%' OR director  LIKE '%' + @cad + '%' ORDER BY +@ordenacio +@ascdesc ;", oConnexio.Con);
                        oSqlCommand.Parameters.Add(new SqlParameter("@cad", cad));
                        oSqlCommand.Parameters.Add(new SqlParameter("@ordenacio", ordenacio));
                        oSqlCommand.Parameters.Add(new SqlParameter("@ascdesc", ascdesc));
                        oSqlDataReader = oSqlCommand.ExecuteReader();

                        while (oSqlDataReader.Read())
                        {
                            dictPelis = new Dictionary<string, string>();
                            for (int i = 0; i < oSqlDataReader.FieldCount; i++)
                            {
                                dictPelis.Add(oSqlDataReader.GetName(i), Convert.ToString(oSqlDataReader[i]));
                            }
                            // Algorisme de Cerca que busca si la llista conté la pelicula (diccionari ) afegida al for anterior 
                            int k = 0;
                            while (!trobat && k < llistaPelis.Count)
                            {
                                if (llistaPelis[k]["id"] == dictPelis["id"])
                                    trobat = true;
                                else
                                    k++;
                            }
                            if (!trobat)
                                llistaPelis.Add(dictPelis);
                        }
                    }
                    oSqlDataReader.Close();
                }
            }
            return llistaPelis;
        }
        catch (Exception)
        {
            return null;
        }
        finally
        {
            if (oSqlDataReader != null)
                oSqlDataReader.Close();
            if (oConnexio != null)
                oConnexio.Tancar();
        }
    }
}