using System;
using System.Collections.Generic;
// Afegits
using System.Data.SqlClient;


/// <summary>
/// Descripción breve de clsPelicula
/// </summary>
public class clsPelicula
{
    #region Atributs
    private string idUsuari;
    private string id;
    private string titol;
    private string portada;
    private string any;
    private string duracio;
    private string pais;
    private string guio;
    private string musica;
    private string director;
    private string genere;
    private string interprets;
    private string trama;
    private string enllaçEnLinia;
    private string enllaçDescarrega;
    private Connexio oConnexio;
    private string msgError;
    #endregion

    #region Propietats
    public string Id
    {
        get
        {
            return this.id;
        }
        set
        {
            this.id = value;
        }
    }
    public string Portada
    {
        get
        {
            return this.portada;
        }
        set
        {
            this.portada = value;
        }
    }
    public string Titol
    {
        get
        {
            return this.titol;
        }
        set
        {
            this.titol = value;
        }
    }
    public string Any
    {
        get
        {
            return this.any;
        }
        set
        {
            this.any = value;
        }
    }
    public string Duracio
    {
        get
        {
            return this.duracio;
        }
        set
        {
            this.duracio = value;
        }
    }
    public string Pais
    {
        get
        {
            return this.pais;
        }
        set
        {
            this.pais = value;
        }
    }
    public string Guio
    {
        get
        {
            return this.guio;
        }
        set
        {
            this.guio = value;
        }
    }
    public string Musica
    {
        get
        {
            return this.musica;
        }
        set
        {
            this.musica = value;
        }
    }
    public string Director
    {
        get
        {
            return this.director;
        }
        set
        {
            this.director = value;
        }
    }
    public string Genere
    {
        get
        {
            return this.genere;
        }
        set
        {
            this.genere = value;
        }
    }
    public string Interprets
    {
        get
        {
            return this.interprets;
        }
        set
        {
            this.interprets = value;
        }
    }
    public string Trama
    {
        get
        {
            return this.trama;
        }
        set
        {
            this.trama = value;
        }
    }
    public string EnllaçEnLinia
    {
        get
        {
            return this.enllaçEnLinia;
        }
        set
        {
            this.enllaçEnLinia = value;
        }
    }
    public string EnllaçDescarrega
    {
        get
        {
            return this.enllaçDescarrega;
        }
        set
        {
            this.enllaçDescarrega = value;
        }
    }

    public Connexio OConnexio
    {
        get
        {
            return this.oConnexio;
        }
        set
        {
            this.oConnexio = value;
        }
    }

    public string MsgError
    {
        get
        {
            return msgError;
        }
    }

    public bool Error
    {
        get
        {
            return (MsgError != "");
        }
    }
    #endregion

    #region Mètodes Constructors
    // Constructor per a nova pelicula
    public clsPelicula(string sUsuari, string titol, string portada, string any, string duracio, string pais, string guio, string musica, string director, string genere, string interprets, string trama, string enllaçEnLinia, string enllaçDescarrega)
    {
        msgError = "";

        this.titol = titol;
        this.portada = portada;
        this.any = any;
        this.duracio = duracio;
        this.pais = pais;
        this.guio = guio;
        this.musica = musica;
        this.director = director;
        this.genere = genere;
        this.interprets = interprets;
        this.trama = trama;
        this.enllaçEnLinia = enllaçEnLinia;
        this.enllaçDescarrega = enllaçDescarrega;
        oConnexio = new Connexio();

        SqlCommand oSqlCommand = null;
        SqlDataReader oSqlDataReader = null;
        oConnexio.Obrir();
        try
        {
            // afegim la nova pelicula
            oSqlCommand = new SqlCommand("INSERT INTO pelicules (titol, portada, any_estrena, duracio, pais, guio, musica, director, genere, interprets, trama, enllaç_en_linia, enllaç_descarrega) VALUES (@titol, @portada, @any, @duracio, @pais, @guio, @musica, @director, @genere, @interprets, @trama, @enllaçEnLinia, @enllaçDescarrega);", oConnexio.Con);

            oSqlCommand.Parameters.Add(new SqlParameter("@titol", this.titol));
            oSqlCommand.Parameters.Add(new SqlParameter("@portada", this.portada));
            oSqlCommand.Parameters.Add(new SqlParameter("@any", this.any));
            oSqlCommand.Parameters.Add(new SqlParameter("@duracio", this.duracio));
            oSqlCommand.Parameters.Add(new SqlParameter("@pais", this.pais));
            oSqlCommand.Parameters.Add(new SqlParameter("@guio", this.guio));
            oSqlCommand.Parameters.Add(new SqlParameter("@musica", this.musica));
            oSqlCommand.Parameters.Add(new SqlParameter("@director", this.director));
            oSqlCommand.Parameters.Add(new SqlParameter("@genere", this.genere));
            oSqlCommand.Parameters.Add(new SqlParameter("@interprets", this.interprets));
            oSqlCommand.Parameters.Add(new SqlParameter("@trama", this.trama));
            oSqlCommand.Parameters.Add(new SqlParameter("@enllaçEnLinia", this.enllaçEnLinia));
            oSqlCommand.Parameters.Add(new SqlParameter("@enllaçDescarrega", this.enllaçDescarrega));

            oSqlCommand.ExecuteNonQuery();
            //


            //Com que acabem d'afegir una nova pelicula, i l'id ve donat automaticamen per l'sql server, fem una consulta per a assignlarlo dintre de la classe
            oSqlCommand = new SqlCommand("SELECT * FROM pelicules WHERE titol = @titol;", oConnexio.Con);
            oSqlCommand.Parameters.Add(new SqlParameter("@titol", titol));

            oSqlDataReader = oSqlCommand.ExecuteReader();

            oSqlDataReader.Read();
            this.id = Convert.ToString(oSqlDataReader["id"]);
            oSqlDataReader.Close();

            //Despres d'obtenir la pelicula, obtenim l'id de l'usuari actual (si es que esta autenticat)
            if (sUsuari != null)
            {
                oSqlCommand = new SqlCommand("SELECT UserId FROM aspnet_Users WHERE UserName = @usuari;", oConnexio.Con);
                oSqlCommand.Parameters.Add(new SqlParameter("@usuari", sUsuari));

                oSqlDataReader = oSqlCommand.ExecuteReader();

                oSqlDataReader.Read();
                idUsuari = Convert.ToString(oSqlDataReader["UserId"]);
                oSqlDataReader.Close();
            }
            else
            {
                idUsuari = null;
            }


            // Afegim el registre de la nova pelicula afegida
            oSqlCommand = new SqlCommand("INSERT INTO registre_edicions (id_usuari, id_pelicula, data_hora, titol, portada, any_estrena, duracio, pais, guio, musica, director, genere, interprets, trama, enllaç_en_linia, enllaç_descarrega) VALUES (@idUsuari, @id, GETDATE(), @titol, @portada, @any, @duracio, @pais, @guio, @musica, @director, @genere, @interprets, @trama, @enllaçEnLinia, @enllaçDescarrega);", oConnexio.Con);

            oSqlCommand.Parameters.Add(new SqlParameter("@idUsuari", this.idUsuari));
            oSqlCommand.Parameters.Add(new SqlParameter("@id", this.id));
            oSqlCommand.Parameters.Add(new SqlParameter("@titol", this.titol));
            oSqlCommand.Parameters.Add(new SqlParameter("@portada", this.portada));
            oSqlCommand.Parameters.Add(new SqlParameter("@any", this.any));
            oSqlCommand.Parameters.Add(new SqlParameter("@duracio", this.duracio));
            oSqlCommand.Parameters.Add(new SqlParameter("@pais", this.pais));
            oSqlCommand.Parameters.Add(new SqlParameter("@guio", this.guio));
            oSqlCommand.Parameters.Add(new SqlParameter("@musica", this.musica));
            oSqlCommand.Parameters.Add(new SqlParameter("@director", this.director));
            oSqlCommand.Parameters.Add(new SqlParameter("@genere", this.genere));
            oSqlCommand.Parameters.Add(new SqlParameter("@interprets", this.interprets));
            oSqlCommand.Parameters.Add(new SqlParameter("@trama", this.trama));
            oSqlCommand.Parameters.Add(new SqlParameter("@enllaçEnLinia", this.enllaçEnLinia));
            oSqlCommand.Parameters.Add(new SqlParameter("@enllaçDescarrega", this.enllaçDescarrega));

            oSqlCommand.ExecuteNonQuery();
            //
        }
        catch (Exception ex)
        {
            msgError = ex.Message;
        }
        finally
        {
            if (oSqlDataReader != null)
            {
                oSqlDataReader.Close();
            }
            if (oConnexio != null)
            {
                oConnexio.Tancar();
            }
        }
    }

    // Constructor per a pelicula existent
    public clsPelicula(string sUsuari, string sTitol)
    {
        msgError = "";
        oConnexio = new Connexio();
        SqlDataReader oSqlDataReader = null;
        oConnexio.Obrir();
        try
        {
            SqlCommand oSqlCommand;

            oSqlCommand = new SqlCommand("SELECT * FROM pelicules WHERE titol = @titol;", oConnexio.Con);
            oSqlCommand.Parameters.Add(new SqlParameter("@titol", sTitol));

            oSqlDataReader = oSqlCommand.ExecuteReader();

            oSqlDataReader.Read();

            id = Convert.ToString(oSqlDataReader["id"]);
            titol = Convert.ToString(oSqlDataReader["titol"]);
            portada = Convert.ToString(oSqlDataReader["portada"]);
            any = Convert.ToString(oSqlDataReader["any_estrena"]);
            duracio = Convert.ToString(oSqlDataReader["duracio"]);
            pais = Convert.ToString(oSqlDataReader["pais"]);
            guio = Convert.ToString(oSqlDataReader["guio"]);
            musica = Convert.ToString(oSqlDataReader["musica"]);
            director = Convert.ToString(oSqlDataReader["director"]);
            genere = Convert.ToString(oSqlDataReader["genere"]);
            interprets = Convert.ToString(oSqlDataReader["interprets"]);
            trama = Convert.ToString(oSqlDataReader["trama"]);
            enllaçEnLinia = Convert.ToString(oSqlDataReader["enllaç_en_linia"]);
            enllaçDescarrega = Convert.ToString(oSqlDataReader["enllaç_descarrega"]);

            //Despres d'obtenir la pelicula, obtenim l'id de l'usuari actual (si es que esta autenticat)
            if (sUsuari != null)
            {
                oSqlCommand = new SqlCommand("SELECT UserId FROM aspnet_Users WHERE UserName = @usuari;", oConnexio.Con);
                oSqlCommand.Parameters.Add(new SqlParameter("@usuari", sUsuari));

                oSqlDataReader.Close();

                oSqlDataReader = oSqlCommand.ExecuteReader();

                oSqlDataReader.Read();
                idUsuari = Convert.ToString(oSqlDataReader["UserId"]);
            }
            else
            {
                idUsuari = null;
            }
        }
        catch (Exception ex)
        {
            msgError = ex.Message;
        }
        finally
        {
            if (oSqlDataReader != null)
            {
                oSqlDataReader.Close();
            }
            if (oConnexio != null)
            {
                oConnexio.Tancar();
            }
        }
    }
    #endregion

    #region Mètodes
    public static bool Existeix(string sTitol)
    {
        // Al ser un mètode estatic no podem fer servir cap atribut no-estatic, així que farem servir variables locals
        SqlConnection oConnexio = new SqlConnection();
        SqlDataReader oSqlDataReader = null;
        SqlCommand oSqlCommand = null;

        oConnexio.ConnectionString = @"Data Source = localhost\SQLEXPRESS; Initial Catalog = headhacks; Integrated Security = true;";
        oConnexio.Open();
        try
        {
            oSqlCommand = new SqlCommand("SELECT * FROM pelicules WHERE titol = @titol;", oConnexio);
            oSqlCommand.Parameters.Add(new SqlParameter("@titol", sTitol));

            oSqlDataReader = oSqlCommand.ExecuteReader();

            if (oSqlDataReader.HasRows)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception)
        {
            return false;
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

    public List<Dictionary<string, string>> ObtenirComentarisPelicula()
    {
        msgError = "";

        SqlCommand oSqlCommand = null;
        SqlDataReader oSqlDataReader = null;
        List<Dictionary<string, string>> llistaComentaris = null;
        Dictionary<string, string> dictComentari = null;

        oConnexio.Obrir();
        try
        {
            oSqlCommand = new SqlCommand("SELECT foto, username, nom, cognoms, data_hora, comentari FROM comentaris_pelicules, aspnet_membership m, aspnet_users u WHERE id_pelicula = @id AND u.userid = id_usuari AND m.userid = id_usuari ORDER BY data_hora DESC;", oConnexio.Con);

            //Definim el parametre utilitzat en l'objecte SqlCommand i l'afegim
            oSqlCommand.Parameters.Add(new SqlParameter("@id", id));
            oSqlDataReader = oSqlCommand.ExecuteReader();

            llistaComentaris = new List<Dictionary<string, string>>();

            while (oSqlDataReader.Read())
            {
                dictComentari = new Dictionary<string, string>();

                for (int i = 0; i < oSqlDataReader.FieldCount; i++)
                {
                    dictComentari.Add(oSqlDataReader.GetName(i), Convert.ToString(oSqlDataReader[i]));
                }
                llistaComentaris.Add(dictComentari);
            }
            return llistaComentaris;
        }
        catch (Exception ex)
        {
            msgError = ex.Message;
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
                oConnexio.Tancar();
            }
        }
    }

    public List<Dictionary<string, string>> ObtenirEdicions()
    {
        msgError = "";

        SqlCommand oSqlCommand = null;
        SqlDataReader oSqlDataReader = null;
        List<Dictionary<string, string>> llistaEdicions = null;
        Dictionary<string, string> dictEdicions = null;

        oConnexio.Obrir();
        try
        {
            oSqlCommand = new SqlCommand("SELECT * FROM registre_edicions WHERE id_pelicula = @id_pelicula ORDER BY data_hora;", oConnexio.Con);

            oSqlCommand.Parameters.Add(new SqlParameter("@id_pelicula", id));
            //Definim el parametre utilitzat en l'objecte SqlCommand i l'afegim
            oSqlDataReader = oSqlCommand.ExecuteReader();

            llistaEdicions = new List<Dictionary<string, string>>();
            while (oSqlDataReader.Read())
            {
                dictEdicions = new Dictionary<string, string>();
                for (int i = 0; i < oSqlDataReader.FieldCount; i++)
                {
                    dictEdicions.Add(oSqlDataReader.GetName(i), Convert.ToString(oSqlDataReader[i]));
                }
                llistaEdicions.Add(dictEdicions);
            }
            if (oSqlDataReader.HasRows)
                return llistaEdicions;
            else
                return null;
        }
        catch (Exception ex)
        {
            msgError = ex.Message;
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
                oConnexio.Tancar();
            }
        }
    }

    public Dictionary<string, string> ObtenirEdicio(string idEdicio)
    {
        msgError = "";

        SqlCommand oSqlCommand = null;
        SqlDataReader oSqlDataReader = null;
        Dictionary<string, string> dictPelicula = null;

        oConnexio.Obrir();
        try
        {
            oSqlCommand = new SqlCommand("SELECT * FROM registre_edicions WHERE id_pelicula = @id_pelicula AND id = @id_edicio;", oConnexio.Con);

            oSqlCommand.Parameters.Add(new SqlParameter("@id_pelicula", id));
            oSqlCommand.Parameters.Add(new SqlParameter("@id_edicio", idEdicio));
            //Definim el parametre utilitzat en l'objecte SqlCommand i l'afegim
            oSqlDataReader = oSqlCommand.ExecuteReader();


            oSqlDataReader.Read();

            dictPelicula = new Dictionary<string, string>();
            for (int i = 0; i < oSqlDataReader.FieldCount; i++)
            {
                dictPelicula.Add(oSqlDataReader.GetName(i), Convert.ToString(oSqlDataReader[i]));
            }

            return dictPelicula;
        }
        catch (Exception ex)
        {
            msgError = ex.Message;
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
                oConnexio.Tancar();
            }
        }
    }

    public int AfegirComentariPelicula(string sComentari)
    {
        msgError = "";

        // Si l'usuari no esta autenticat
        if (idUsuari == null)
        {
            msgError = "usuari no autenticat";
            return -1;
        }

        SqlCommand oSqlCommand = null;
        oConnexio.Obrir();
        try
        {
            oSqlCommand = new SqlCommand("INSERT INTO comentaris_pelicules (id_pelicula, id_usuari, comentari, data_hora) VALUES (@idPelicula, @idUsuari, @comentari, GETDATE());", oConnexio.Con);

            //Definim els parametres utilitzats en l'objecte SqlCommand i els afegim
            oSqlCommand.Parameters.Add(new SqlParameter("@idPelicula", id));
            oSqlCommand.Parameters.Add(new SqlParameter("@idUsuari", idUsuari));
            oSqlCommand.Parameters.Add(new SqlParameter("@comentari", sComentari));

            oSqlCommand.ExecuteNonQuery();

            return 0;
        }
        catch (Exception ex)
        {
            msgError = ex.Message;
            return -1;
        }
        finally
        {
            if (oConnexio != null)
            {
                oConnexio.Tancar();
            }
        }
    }

    public int ActualitzarDadesPelicula()
    {
        msgError = "";

        SqlCommand oSqlCommand = null;
        oConnexio.Obrir();
        try
        {
            // Actualitzem les dades de la pelicula
            oSqlCommand = new SqlCommand("UPDATE pelicules SET titol = @titol, portada = @portada, any_estrena = @any, duracio = @duracio, pais = @pais, guio = @guio, musica = @musica, director = @director, genere = @genere, interprets = @interprets, trama = @trama, enllaç_en_linia = @enllaçEnLinia, enllaç_descarrega = @enllaçDescarrega WHERE id = @id;", oConnexio.Con);

            oSqlCommand.Parameters.Add(new SqlParameter("@id", id));
            oSqlCommand.Parameters.Add(new SqlParameter("@titol", titol));
            oSqlCommand.Parameters.Add(new SqlParameter("@portada", portada));
            oSqlCommand.Parameters.Add(new SqlParameter("@any", any));
            oSqlCommand.Parameters.Add(new SqlParameter("@duracio", duracio));
            oSqlCommand.Parameters.Add(new SqlParameter("@pais", pais));
            oSqlCommand.Parameters.Add(new SqlParameter("@guio", guio));
            oSqlCommand.Parameters.Add(new SqlParameter("@musica", musica));
            oSqlCommand.Parameters.Add(new SqlParameter("@director", director));
            oSqlCommand.Parameters.Add(new SqlParameter("@genere", genere));
            oSqlCommand.Parameters.Add(new SqlParameter("@interprets", interprets));
            oSqlCommand.Parameters.Add(new SqlParameter("@trama", trama));
            oSqlCommand.Parameters.Add(new SqlParameter("@enllaçEnLinia", enllaçEnLinia));
            oSqlCommand.Parameters.Add(new SqlParameter("@enllaçDescarrega", enllaçDescarrega));

            oSqlCommand.ExecuteNonQuery();
            //

            // Afegim el registre de la edició
            oSqlCommand = new SqlCommand("INSERT INTO registre_edicions (id_usuari, id_pelicula, data_hora, titol, portada, any_estrena, duracio, pais, guio, musica, director, genere, interprets, trama, enllaç_en_linia, enllaç_descarrega) VALUES (@idUsuari, @id, GETDATE(), @titol, @portada, @any, @duracio, @pais, @guio, @musica, @director, @genere, @interprets, @trama, @enllaçEnLinia, @enllaçDescarrega);", oConnexio.Con);

            oSqlCommand.Parameters.Add(new SqlParameter("@idUsuari", idUsuari));
            oSqlCommand.Parameters.Add(new SqlParameter("@id", id));
            oSqlCommand.Parameters.Add(new SqlParameter("@titol", titol));
            oSqlCommand.Parameters.Add(new SqlParameter("@portada", portada));
            oSqlCommand.Parameters.Add(new SqlParameter("@any", any));
            oSqlCommand.Parameters.Add(new SqlParameter("@duracio", duracio));
            oSqlCommand.Parameters.Add(new SqlParameter("@pais", pais));
            oSqlCommand.Parameters.Add(new SqlParameter("@guio", guio));
            oSqlCommand.Parameters.Add(new SqlParameter("@musica", musica));
            oSqlCommand.Parameters.Add(new SqlParameter("@director", director));
            oSqlCommand.Parameters.Add(new SqlParameter("@genere", genere));
            oSqlCommand.Parameters.Add(new SqlParameter("@interprets", interprets));
            oSqlCommand.Parameters.Add(new SqlParameter("@trama", trama));
            oSqlCommand.Parameters.Add(new SqlParameter("@enllaçEnLinia", enllaçEnLinia));
            oSqlCommand.Parameters.Add(new SqlParameter("@enllaçDescarrega", enllaçDescarrega));

            oSqlCommand.ExecuteNonQuery();
            //
            return 0;
        }
        catch (Exception ex)
        {
            msgError = ex.Message;
            return -1;
        }
        finally
        {
            if (oConnexio != null)
            {
                oConnexio.Tancar();
            }
        }
    }

    public void EliminarPelicula()
    {
        // colocar on delete cascade al sql (també als usuaris)
        msgError = "";

        SqlCommand oSqlCommand = null;
        oConnexio.Obrir();
        try
        {
            // Actualitzem les dades de la pelicula
            oSqlCommand = new SqlCommand("DELETE FROM pelicules WHERE id = @id;", oConnexio.Con);

            oSqlCommand.Parameters.Add(new SqlParameter("@id", id));

            oSqlCommand.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            msgError = ex.Message;
        }
        finally
        {
            if (oConnexio != null)
            {
                oConnexio.Tancar();
            }
        }
    }
    #endregion
}