using System;
using System.Collections.Generic;
// Afegits
using System.Data.SqlClient;

/// <summary>
/// Descripción breve de clsGeneres
/// </summary>
public class clsGeneres
{
    public static List<Dictionary<string, string>> ObtenirGeneres(string ordenacio, string ascdesc)
    {
        // Al ser un mètode estatic no podem fer servir cap atribut no-estatic, així que farem servir variables locals
        SqlConnection oConnexio = new SqlConnection();
        SqlDataReader oSqlDataReader = null;
        SqlCommand oSqlCommand = null;
        List<Dictionary<string, string>> llistaGeneres = null;
        Dictionary<string, string> dictGeneres = null;

        oConnexio.ConnectionString = @"Data Source = localhost\SQLEXPRESS; Initial Catalog = headhacks; Integrated Security = true;";
        oConnexio.Open();
        try
        {
            oSqlCommand = new SqlCommand("SELECT genere, COUNT(genere) AS num_pelicules FROM pelicules GROUP BY genere ORDER BY +@ordenacio +@ascdesc;", oConnexio);
            oSqlCommand.Parameters.Add(new SqlParameter("@ordenacio", ordenacio)); //ordenacio
            oSqlCommand.Parameters.Add(new SqlParameter("@ascdesc", ascdesc));
            //Definim el parametre utilitzat en l'objecte SqlCommand i l'afegim
            oSqlDataReader = oSqlCommand.ExecuteReader();

            llistaGeneres = new List<Dictionary<string, string>>();
            while (oSqlDataReader.Read())
            {
                dictGeneres = new Dictionary<string, string>();
                for (int i = 0; i < oSqlDataReader.FieldCount; i++)
                {
                    dictGeneres.Add(oSqlDataReader.GetName(i), Convert.ToString(oSqlDataReader[i]));
                }
                llistaGeneres.Add(dictGeneres);
            }
            if (oSqlDataReader.HasRows)
                return llistaGeneres;
            else
                return null;
        }
        catch (Exception)
        {
            return null;
        }
        finally
        {
            if (oSqlDataReader != null)
            {
                oSqlDataReader.Close();
            }
            if (oConnexio != null)
            {
                oConnexio.Close();
            }
        }

    }
}