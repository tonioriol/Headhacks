using System;
using System.Collections.Generic;
// Afegits
using System.Data.SqlClient;

/// <summary>
/// Descripción breve de clsPelicules
/// </summary>
public class clsPelicules
{
    public static List<Dictionary<string, string>> ObtenirPelis(string ordenacio, string ascdesc)
    {
        // Al ser un mètode estatic no podem fer servir cap atribut no-estatic, així que farem servir variables locals
        SqlConnection oConnexio = new SqlConnection();
        SqlDataReader oSqlDataReader = null;
        SqlCommand oSqlCommand = null;
        List<Dictionary<string, string>> llistaPelis = null;
        Dictionary<string, string> dictPelis = null;

        oConnexio.ConnectionString = @"Data Source = localhost\SQLEXPRESS; Initial Catalog = headhacks; Integrated Security = true;";
        oConnexio.Open();
        try
        {
            oSqlCommand = new SqlCommand("SELECT * FROM pelicules ORDER BY +@ordenacio +@ascdesc;", oConnexio);
            oSqlCommand.Parameters.Add(new SqlParameter("@ordenacio", ordenacio)); //ordenacio
            oSqlCommand.Parameters.Add(new SqlParameter("@ascdesc", ascdesc));
            //Definim el parametre utilitzat en l'objecte SqlCommand i l'afegim
            oSqlDataReader = oSqlCommand.ExecuteReader();

            llistaPelis = new List<Dictionary<string, string>>();
            while (oSqlDataReader.Read())
            {
                dictPelis = new Dictionary<string, string>();
                for (int i = 0; i < oSqlDataReader.FieldCount; i++)
                {
                    dictPelis.Add(oSqlDataReader.GetName(i), Convert.ToString(oSqlDataReader[i]));
                }
                llistaPelis.Add(dictPelis);
            }
            if (oSqlDataReader.HasRows)
                return llistaPelis;
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
                oSqlDataReader.Close();
            if (oConnexio != null)
                oConnexio.Close();
        }
    }

    public static List<Dictionary<string, string>> ObtenirPeliculesGenere(string ordenacio, string ascdesc, string genere)
    {
        // Al ser un mètode estatic no podem fer servir cap atribut no-estatic, així que farem servir variables locals
        SqlConnection oConnexio = new SqlConnection();
        SqlDataReader oSqlDataReader = null;
        SqlCommand oSqlCommand = null;
        List<Dictionary<string, string>> llistaPelis = null;
        Dictionary<string, string> dictPelis = null;

        oConnexio.ConnectionString = @"Data Source = localhost\SQLEXPRESS; Initial Catalog = headhacks; Integrated Security = true;";
        oConnexio.Open();
        try
        {
            oSqlCommand = new SqlCommand("SELECT * FROM pelicules WHERE genere = @genere ORDER BY +@ordenacio +@ascdesc;", oConnexio);

            oSqlCommand.Parameters.Add(new SqlParameter("@genere", genere));
            oSqlCommand.Parameters.Add(new SqlParameter("@ordenacio", ordenacio)); //ordenacio
            oSqlCommand.Parameters.Add(new SqlParameter("@ascdesc", ascdesc));
            //Definim el parametre utilitzat en l'objecte SqlCommand i l'afegim
            oSqlDataReader = oSqlCommand.ExecuteReader();

            llistaPelis = new List<Dictionary<string, string>>();
            while (oSqlDataReader.Read())
            {
                dictPelis = new Dictionary<string, string>();
                for (int i = 0; i < oSqlDataReader.FieldCount; i++)
                {
                    dictPelis.Add(oSqlDataReader.GetName(i), Convert.ToString(oSqlDataReader[i]));
                }
                llistaPelis.Add(dictPelis);
            }
            if (oSqlDataReader.HasRows)
                return llistaPelis;
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
                oSqlDataReader.Close();
            if (oConnexio != null)
                oConnexio.Close();
        }
    }

    public static List<Dictionary<string, string>> ObtenirPeliculesAny(string ordenacio, string ascdesc, string any)
    {
        // Al ser un mètode estatic no podem fer servir cap atribut no-estatic, així que farem servir variables locals
        SqlConnection oConnexio = new SqlConnection();
        SqlDataReader oSqlDataReader = null;
        SqlCommand oSqlCommand = null;
        List<Dictionary<string, string>> llistaPelis = null;
        Dictionary<string, string> dictPelis = null;

        oConnexio.ConnectionString = @"Data Source = localhost\SQLEXPRESS; Initial Catalog = headhacks; Integrated Security = true;";
        oConnexio.Open();
        try
        {
            oSqlCommand = new SqlCommand("SELECT * FROM pelicules WHERE any_estrena = @any ORDER BY +@ordenacio +@ascdesc;", oConnexio);

            oSqlCommand.Parameters.Add(new SqlParameter("@any", any));
            oSqlCommand.Parameters.Add(new SqlParameter("@ordenacio", ordenacio)); //ordenacio
            oSqlCommand.Parameters.Add(new SqlParameter("@ascdesc", ascdesc));
            //Definim el parametre utilitzat en l'objecte SqlCommand i l'afegim
            oSqlDataReader = oSqlCommand.ExecuteReader();

            llistaPelis = new List<Dictionary<string, string>>();
            while (oSqlDataReader.Read())
            {
                dictPelis = new Dictionary<string, string>();
                for (int i = 0; i < oSqlDataReader.FieldCount; i++)
                {
                    dictPelis.Add(oSqlDataReader.GetName(i), Convert.ToString(oSqlDataReader[i]));
                }
                llistaPelis.Add(dictPelis);
            }
            if (oSqlDataReader.HasRows)
                return llistaPelis;
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
                oSqlDataReader.Close();
            if (oConnexio != null)
                oConnexio.Close();
        }
    }

    public static List<Dictionary<string, string>> ObtenirPeliculesDirector(string ordenacio, string ascdesc, string director)
    {
        // Al ser un mètode estatic no podem fer servir cap atribut no-estatic, així que farem servir variables locals
        SqlConnection oConnexio = new SqlConnection();
        SqlDataReader oSqlDataReader = null;
        SqlCommand oSqlCommand = null;
        List<Dictionary<string, string>> llistaPelis = null;
        Dictionary<string, string> dictPelis = null;

        oConnexio.ConnectionString = @"Data Source = localhost\SQLEXPRESS; Initial Catalog = headhacks; Integrated Security = true;";
        oConnexio.Open();
        try
        {
            oSqlCommand = new SqlCommand("SELECT * FROM pelicules WHERE director = @director ORDER BY +@ordenacio +@ascdesc;", oConnexio);

            oSqlCommand.Parameters.Add(new SqlParameter("@director", director));
            oSqlCommand.Parameters.Add(new SqlParameter("@ordenacio", ordenacio)); //ordenacio
            oSqlCommand.Parameters.Add(new SqlParameter("@ascdesc", ascdesc));
            //Definim el parametre utilitzat en l'objecte SqlCommand i l'afegim
            oSqlDataReader = oSqlCommand.ExecuteReader();

            llistaPelis = new List<Dictionary<string, string>>();
            while (oSqlDataReader.Read())
            {
                dictPelis = new Dictionary<string, string>();
                for (int i = 0; i < oSqlDataReader.FieldCount; i++)
                {
                    dictPelis.Add(oSqlDataReader.GetName(i), Convert.ToString(oSqlDataReader[i]));
                }
                llistaPelis.Add(dictPelis);
            }
            if (oSqlDataReader.HasRows)
                return llistaPelis;
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
                oSqlDataReader.Close();
            if (oConnexio != null)
                oConnexio.Close();
        }
    }

    public static List<Dictionary<string, string>> ObtenirUltimesPelicules()
    {
        // Al ser un mètode estatic no podem fer servir cap atribut no-estatic, així que farem servir variables locals
        SqlConnection oConnexio = new SqlConnection();
        SqlDataReader oSqlDataReader = null;
        SqlCommand oSqlCommand = null;
        List<Dictionary<string, string>> llistaPelis = null;
        Dictionary<string, string> dictPelis = null;

        oConnexio.ConnectionString = @"Data Source = localhost\SQLEXPRESS; Initial Catalog = headhacks; Integrated Security = true;";
        oConnexio.Open();
        try
        {
            oSqlCommand = new SqlCommand("select distinct top 6 p.titol, p.portada, m.nom, m.cognoms, u.username, r.data_hora "+
                                         "from pelicules p, registre_edicions r, aspnet_Membership m, aspnet_Users u "+
                                         "where r.id_pelicula = p.id "+
                                         "and m.UserId = u.UserId "+
                                         "and m.UserId = r.id_usuari "+
                                         "and u.UserId = r.id_usuari " +
                                         "group by p.titol, p.portada, m.nom, m.cognoms, u.username, r.data_hora " +
                                         "order by data_hora desc;", oConnexio);

            //Definim el parametre utilitzat en l'objecte SqlCommand i l'afegim
            oSqlDataReader = oSqlCommand.ExecuteReader();

            llistaPelis = new List<Dictionary<string, string>>();
            while (oSqlDataReader.Read())
            {
                dictPelis = new Dictionary<string, string>();
                for (int i = 0; i < oSqlDataReader.FieldCount; i++)
                {
                    dictPelis.Add(oSqlDataReader.GetName(i), Convert.ToString(oSqlDataReader[i]));
                }
                llistaPelis.Add(dictPelis);
            }
            if (oSqlDataReader.HasRows)
                return llistaPelis;
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
                oSqlDataReader.Close();
            if (oConnexio != null)
                oConnexio.Close();
        }
    }
}