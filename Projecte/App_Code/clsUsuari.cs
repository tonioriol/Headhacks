using System;
using System.Collections.Generic;
// Afegits
using System.Data.SqlClient;

/// <summary>
/// Descripción breve de clsUsuari
/// </summary>
public class clsUsuari
{
    #region Atributs
    private string idUsuari;
    private string nom;
    private string cognoms;
    private DateTime dataNaixement;
    private string foto;
    private string correuElectronic;
    private string nomUsuari;
    private string contrasenya;
    private string prestigi;
    private Connexio oConnexio;
    private string msgError;
    #endregion

    #region Propietats
    public string Id
    {
        get
        {
            return this.idUsuari;
        }
        set
        {
            this.idUsuari = value;
        }
    }

    public string Foto
    {
        get
        {
            return this.foto;
        }
        set
        {
            this.foto = value;
        }
    }

    public string Nom
    {
        get
        {
            return this.nom;
        }
        set
        {
            this.nom = value;
        }
    }

    public string Cognoms
    {
        get
        {
            return this.cognoms;
        }
        set
        {
            this.cognoms = value;
        }
    }

    public DateTime DataNaixement
    {
        get
        {
            return this.dataNaixement;
        }
        set
        {
            this.dataNaixement = value;
        }
    }

    public string CorreuElectronic
    {
        get
        {
            return this.correuElectronic;
        }
        set
        {
            this.correuElectronic = value;
        }
    }

    public string NomUsuari
    {
        get
        {
            return this.nomUsuari;
        }
        set
        {
            this.nomUsuari = value;
        }
    }

    public string Contrasenya
    {
        get
        {
            return this.contrasenya;
        }
        set
        {
            this.contrasenya = value;
        }
    }

    public string Prestigi
    {
        get
        {
            return this.prestigi;
        }
        set
        {
            this.prestigi = value;
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
    #endregion;

    #region Mètodes Constructors
    // Constructor per a nou usuari
    /*
    public clsUsuari(string nom, string cognoms, DateTime dataNaixement, string foto, string correuElectronic, string nomUsuari, string contrasenya)
    {
        this.nom = nom;
        this.cognoms = cognoms;
        this.dataNaixement = dataNaixement;
        this.foto = foto;
        this.correuElectronic = correuElectronic;
        this.nomUsuari = nomUsuari;
        this.contrasenya = contrasenya;
        this.prestigi = "5"; // Ho fem així per que des de l'sql server tenim un "default = 5", es a dir que quan creem un nou usuari, per defete "naix" amb 5 de prestigi 
        oConnexio = new Connexio();

        SqlCommand oSqlCommand = null;
        oConnexio.Obrir();
        try
        {
            oSqlCommand = new SqlCommand("INSERT INTO usuaris (nom, cognoms, data_naixement, foto, correu_electronic, nomUsuari, contrasenya, prestigi) VALUES (@nom, @cognoms, @dataNaixement, @foto, @correuElectronic, @nomUsuari, @contrasenya, @prestigi);", oConnexio.Con);

            oSqlCommand.Parameters.Add(new SqlParameter("@nom", nom));
            oSqlCommand.Parameters.Add(new SqlParameter("@cognoms", cognoms));
            oSqlCommand.Parameters.Add(new SqlParameter("@dataNaixement", dataNaixement));
            oSqlCommand.Parameters.Add(new SqlParameter("@foto", foto));
            oSqlCommand.Parameters.Add(new SqlParameter("@correuElectronic", correuElectronic));
            oSqlCommand.Parameters.Add(new SqlParameter("@nomUsuari", nomUsuari));
            oSqlCommand.Parameters.Add(new SqlParameter("@contrasenya", contrasenya));

            oSqlCommand.ExecuteNonQuery();

            //Com que acabem d'afegir un nou usuari, i l'idUsuari ve donat automaticamen per l'sql server, fem 
            // una consulta per a assignlarlo dintre de la classe
            oSqlCommand = new SqlCommand("SELECT * FROM usuaris WHERE nom = @nom;", oConnexio.Con);
            oSqlCommand.Parameters.Add(new SqlParameter("@nom", this.nom));
            this.idUsuari = Convert.ToString(oSqlCommand.ExecuteScalar());
            //
        }
        finally
        {
            if (oConnexio != null)
            {
                oConnexio.Tancar();
            }
        }
    }
     * */

    // Constructor per a usuari existent
    public clsUsuari(string nomUsuari)
    {
        oConnexio = new Connexio();
        SqlDataReader oSqlDataReader = null;

        idUsuari = ObtenirIdUsuari(nomUsuari);

        oConnexio.Obrir();
        try
        {
            SqlCommand oSqlCommand;

            oSqlCommand = new SqlCommand("SELECT * FROM aspnet_membership WHERE userid = @id_usuari;", oConnexio.Con);
            oSqlCommand.Parameters.Add(new SqlParameter("@id_usuari", idUsuari));

            oSqlDataReader = oSqlCommand.ExecuteReader();

            oSqlDataReader.Read();
            nom = Convert.ToString(oSqlDataReader["nom"]);
            cognoms = Convert.ToString(oSqlDataReader["cognoms"]);
            if (oSqlDataReader["data_naixement"] != DBNull.Value)
            {
                dataNaixement = Convert.ToDateTime(oSqlDataReader["data_naixement"]);
            }
            foto = Convert.ToString(oSqlDataReader["foto"]);
            correuElectronic = Convert.ToString(oSqlDataReader["Email"]);
            this.nomUsuari = nomUsuari;
            prestigi = Convert.ToString(oSqlDataReader["prestigi"]);
        }
        catch (Exception)
        {
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

    public clsUsuari(string idUsuari, string dummy)
    {
        oConnexio = new Connexio();
        SqlDataReader oSqlDataReader = null;

        this.nomUsuari = ObtenirNomUsuari(idUsuari);

        oConnexio.Obrir();
        try
        {
            SqlCommand oSqlCommand;

            oSqlCommand = new SqlCommand("SELECT * FROM aspnet_membership WHERE userid = @id_usuari;", oConnexio.Con);
            oSqlCommand.Parameters.Add(new SqlParameter("@id_usuari", idUsuari));

            oSqlDataReader = oSqlCommand.ExecuteReader();

            oSqlDataReader.Read();
            nom = Convert.ToString(oSqlDataReader["nom"]);
            cognoms = Convert.ToString(oSqlDataReader["cognoms"]);
            if (oSqlDataReader["data_naixement"] != DBNull.Value)
            {
                dataNaixement = Convert.ToDateTime(oSqlDataReader["data_naixement"]);
            }
            foto = Convert.ToString(oSqlDataReader["foto"]);
            correuElectronic = Convert.ToString(oSqlDataReader["Email"]);
            prestigi = Convert.ToString(oSqlDataReader["prestigi"]);
        }
        catch (Exception)
        {
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
    public static bool Existeix(string nomUsuari)
    {
        // Al ser un mètode estatic no podem fer servir cap atribut no-estatic, així que farem servir variables locals
        SqlConnection oConnexio = new SqlConnection();
        SqlDataReader oSqlDataReader = null;
        SqlCommand oSqlCommand = null;

        oConnexio.ConnectionString = @"Data Source = localhost\SQLEXPRESS; Initial Catalog = headhacks; Integrated Security = true;";
        oConnexio.Open();
        try
        {
            oSqlCommand = new SqlCommand("SELECT * FROM aspnet_users WHERE username = @nomusuari;", oConnexio);
            oSqlCommand.Parameters.Add(new SqlParameter("@nomusuari", nomUsuari));

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

    private string ObtenirNomUsuari(string idUsuari)
    {
        msgError = "";

        SqlCommand oSqlCommand = null;
        SqlDataReader oSqlDataReader = null;

        oConnexio.Obrir();
        try
        {
            oSqlCommand = new SqlCommand("SELECT UserName FROM aspnet_Users WHERE UserId = @idUsuari;", oConnexio.Con);
            oSqlCommand.Parameters.Add(new SqlParameter("@idUsuari", idUsuari));

            oSqlDataReader = oSqlCommand.ExecuteReader();
            oSqlDataReader.Read();
            return Convert.ToString(oSqlDataReader["UserName"]);
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

    private string ObtenirIdUsuari(string nomUsuari)
    {
        msgError = "";
        SqlCommand oSqlCommand = null;
        SqlDataReader oSqlDataReader = null;
        oConnexio.Obrir();
        try
        {
            oSqlCommand = new SqlCommand("SELECT UserId FROM aspnet_Users WHERE UserName = @nomUsuari;", oConnexio.Con);
            oSqlCommand.Parameters.Add(new SqlParameter("@nomUsuari", nomUsuari));

            oSqlDataReader = oSqlCommand.ExecuteReader();
            oSqlDataReader.Read();
            return Convert.ToString(oSqlDataReader["UserId"]);
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

    public bool EsContacte(string nomUsuari)
    {
        msgError = "";
        SqlCommand oSqlCommand = null;
        SqlDataReader oSqlDataReader = null;
        string idContacte = null;

        idContacte = ObtenirIdUsuari(nomUsuari);

        oConnexio.Obrir();
        try
        {
            oSqlCommand = new SqlCommand("SELECT * FROM contactes WHERE id_usuari1 = @id1 AND id_usuari2 = @id2 OR id_usuari1 = @id2 AND id_usuari2 = @id1;", oConnexio.Con);
            oSqlCommand.Parameters.Add(new SqlParameter("@id1", idUsuari));
            oSqlCommand.Parameters.Add(new SqlParameter("@id2", idContacte));

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
        catch (Exception ex)
        {
            msgError = ex.Message;
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
                oConnexio.Tancar();
            }
        }
    }

    public List<Dictionary<string, string>> ObtenirContactes()
    {
        msgError = "";
        SqlCommand oSqlCommand = null;
        SqlDataReader oSqlDataReader = null;
        List<Dictionary<string, string>> llistaContactes = null;
        Dictionary<string, string> dictContacte = null;

        oConnexio.Obrir();
        try
        {
            oSqlCommand = new SqlCommand(
                "SELECT m.foto, username, nom, cognoms, data_naixement, prestigi " +
                "FROM contactes c, aspnet_Membership m, aspnet_users u " +
                "WHERE u.userid = m.userid AND m.userid = c.id_usuari2 AND c.id_usuari1 = @idUsuari " +
                "OR u.userid = m.userid AND m.userid = c.id_usuari1 AND c.id_usuari2 = @idUsuari;"
                , oConnexio.Con);

            //Definim el parametre utilitzat en l'objecte SqlCommand i l'afegim
            oSqlCommand.Parameters.Add(new SqlParameter("@idUsuari", idUsuari));

            oSqlDataReader = oSqlCommand.ExecuteReader();

            llistaContactes = new List<Dictionary<string, string>>();


            while (oSqlDataReader.Read())
            {
                dictContacte = new Dictionary<string, string>();

                for (int i = 0; i < oSqlDataReader.FieldCount; i++)
                {
                    dictContacte.Add(oSqlDataReader.GetName(i), Convert.ToString(oSqlDataReader[i]));
                }
                llistaContactes.Add(dictContacte);
            }
            return llistaContactes;
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

    public List<Dictionary<string, string>> ObtenirPeliculesVistes()
    {
        msgError = "";
        SqlCommand oSqlCommand = null;
        SqlDataReader oSqlDataReader = null;
        List<Dictionary<string, string>> llistaPeliculesVistes = null;
        Dictionary<string, string> dictPeliculaVista = null;

        oConnexio.Obrir();
        try
        {
            oSqlCommand = new SqlCommand("SELECT * FROM pelicules p, pelicules_vistes v WHERE p.id = v.id_pelicula AND v.id_usuari = @idUsuari;", oConnexio.Con);

            //Definim el parametre utilitzat en l'objecte SqlCommand i l'afegim
            oSqlCommand.Parameters.Add(new SqlParameter("@idUsuari", idUsuari));

            oSqlDataReader = oSqlCommand.ExecuteReader();

            llistaPeliculesVistes = new List<Dictionary<string, string>>();

            while (oSqlDataReader.Read())
            {
                dictPeliculaVista = new Dictionary<string, string>();

                for (int i = 0; i < oSqlDataReader.FieldCount; i++)
                {
                    dictPeliculaVista.Add(oSqlDataReader.GetName(i), Convert.ToString(oSqlDataReader[i]));
                }
                llistaPeliculesVistes.Add(dictPeliculaVista);
            }
            return llistaPeliculesVistes;
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

    public List<Dictionary<string, string>> ObtenirPeliculesVeure()
    {
        msgError = "";
        SqlCommand oSqlCommand = null;
        SqlDataReader oSqlDataReader = null;
        List<Dictionary<string, string>> llistaPeliculesVeure = null;
        Dictionary<string, string> dictPeliculaVeure = null;

        oConnexio.Obrir();
        try
        {
            oSqlCommand = new SqlCommand("SELECT * FROM pelicules p, pelicules_a_veure v WHERE p.id = v.id_pelicula AND v.id_usuari = @idUsuari;", oConnexio.Con);

            //Definim el parametre utilitzat en l'objecte SqlCommand i l'afegim
            oSqlCommand.Parameters.Add(new SqlParameter("@idUsuari", idUsuari));

            oSqlDataReader = oSqlCommand.ExecuteReader();

            llistaPeliculesVeure = new List<Dictionary<string, string>>();


            while (oSqlDataReader.Read())
            {
                dictPeliculaVeure = new Dictionary<string, string>();

                for (int i = 0; i < oSqlDataReader.FieldCount; i++)
                {
                    dictPeliculaVeure.Add(oSqlDataReader.GetName(i), Convert.ToString(oSqlDataReader[i]));
                }
                llistaPeliculesVeure.Add(dictPeliculaVeure);
            }
            return llistaPeliculesVeure;
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

    public List<Dictionary<string, string>> ObtenirComentarisPerfil()
    {
        msgError = "";
        SqlCommand oSqlCommand = null;
        SqlDataReader oSqlDataReader = null;
        List<Dictionary<string, string>> llistaComentarisPerfil = null;
        Dictionary<string, string> dictComentariPerfil = null;

        oConnexio.Obrir();
        try
        {
            oSqlCommand = new SqlCommand("SELECT foto, nom, cognoms, username, data_hora, comentari FROM comentaris_perfil c, aspnet_membership m, aspnet_users u WHERE id_comentat = @idUsuari AND id_comentador = m.userid AND id_comentador = u.userid;", oConnexio.Con);

            //Definim el parametre utilitzat en l'objecte SqlCommand i l'afegim
            oSqlCommand.Parameters.Add(new SqlParameter("@idUsuari", idUsuari));

            oSqlDataReader = oSqlCommand.ExecuteReader();

            llistaComentarisPerfil = new List<Dictionary<string, string>>();

            while (oSqlDataReader.Read())
            {
                dictComentariPerfil = new Dictionary<string, string>();

                for (int i = 0; i < oSqlDataReader.FieldCount; i++)
                {
                    dictComentariPerfil.Add(oSqlDataReader.GetName(i), Convert.ToString(oSqlDataReader[i]));
                }
                llistaComentarisPerfil.Add(dictComentariPerfil);
            }
            return llistaComentarisPerfil;
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

    public void AfegirContacte(string nomUsuari)
    {
        msgError = "";

        SqlCommand oSqlCommand = null;
        string iIdContacte = null;

        iIdContacte = ObtenirIdUsuari(nomUsuari);

        oConnexio.Obrir();
        try
        {
            oSqlCommand = new SqlCommand("INSERT INTO contactes (id_usuari1, id_usuari2) VALUES (@id1, @id2);", oConnexio.Con);

            //Definim els parametres utilitzats en l'objecte SqlCommand i els afegim
            oSqlCommand.Parameters.Add(new SqlParameter("@id1", idUsuari));
            oSqlCommand.Parameters.Add(new SqlParameter("@id2", iIdContacte));

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

    public void AfegirPeliculaVista(string iIdPelicula)
    {
        msgError = "";
        SqlCommand oSqlCommand = null;

        // Primer que res borrem la pelicula de la ataula pelicules_a_veure, si es que esta
        EliminarPeliculaAVeure(iIdPelicula);

        oConnexio.Obrir();
        try
        {
            oSqlCommand = new SqlCommand("INSERT INTO pelicules_vistes (id_usuari, id_pelicula) VALUES (@id1, @id2);", oConnexio.Con);

            //Definim els parametres utilitzats en l'objecte SqlCommand i els afegim
            oSqlCommand.Parameters.Add(new SqlParameter("@id1", idUsuari));
            oSqlCommand.Parameters.Add(new SqlParameter("@id2", iIdPelicula));

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

    public void AfegirPeliculaVeure(string iIdPelicula)
    {
        msgError = "";
        SqlCommand oSqlCommand = null;

        // Primer que res borrem la pelicula de la ataula pelicules_vistes, si es que esta
        EliminarPeliculaVista(iIdPelicula);

        oConnexio.Obrir();
        try
        {
            // Ara fem tot el procediemt de afegir la pelicula

            oSqlCommand = new SqlCommand("INSERT INTO pelicules_a_veure (id_usuari, id_pelicula) VALUES (@id1, @id2);", oConnexio.Con);

            //Definim els parametres utilitzats en l'objecte SqlCommand i els afegim
            oSqlCommand.Parameters.Add(new SqlParameter("@id1", idUsuari));
            oSqlCommand.Parameters.Add(new SqlParameter("@id2", iIdPelicula));

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

    public void AfegirComentariPerfil(string nomComentador, string sComentari)
    {
        msgError = "";
        SqlCommand oSqlCommand = null;
        string idComentador = null;

        idComentador = ObtenirIdUsuari(nomComentador);

        oConnexio.Obrir();
        try
        {
            oSqlCommand = new SqlCommand("INSERT INTO comentaris_perfil (id_comentador, id_comentat, comentari, data_hora) VALUES (@id_comentador, @id_comentat, @comentari, GETDATE());", oConnexio.Con);

            //Definim els parametres utilitzats en l'objecte SqlCommand i els afegim
            oSqlCommand.Parameters.Add(new SqlParameter("@id_comentat", idUsuari));
            oSqlCommand.Parameters.Add(new SqlParameter("@id_comentador", idComentador));
            oSqlCommand.Parameters.Add(new SqlParameter("@comentari", sComentari));

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

    public void ActualitzarDadesUsuari()
    {
        msgError = "";
        SqlCommand oSqlCommand = null;
        oConnexio.Obrir();
        try
        {   //Correu, contrasenya
            oSqlCommand = new SqlCommand("UPDATE aspnet_Membership SET nom = @nom, cognoms = @cognoms, data_naixement = @dataNaixement, foto = @foto, Email = @correuElectronic WHERE UserId = @idUsuari;", oConnexio.Con);

            //Definim els parametres utilitzats en l'objecte SqlCommand i els afegim
            oSqlCommand.Parameters.Add(new SqlParameter("@idUsuari", idUsuari));
            oSqlCommand.Parameters.Add(new SqlParameter("@nom", nom));
            oSqlCommand.Parameters.Add(new SqlParameter("@cognoms", cognoms));
            oSqlCommand.Parameters.Add(new SqlParameter("@dataNaixement", dataNaixement));
            oSqlCommand.Parameters.Add(new SqlParameter("@foto", foto));
            oSqlCommand.Parameters.Add(new SqlParameter("@correuElectronic", correuElectronic));

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

    public void ActualitzarPrestigi()
    {
        msgError = "";

        // Actualitzem el prestigi (sempre que aquest sigui inferior a 100, que es el maxim)
        if (Convert.ToInt32(prestigi) < 100)
        {
            prestigi = Convert.ToString(Convert.ToInt32(prestigi) + 1);

            SqlCommand oSqlCommand = null;
            oConnexio.Obrir();
            try
            {
                oSqlCommand = new SqlCommand("UPDATE aspnet_membership SET prestigi = @prestigi WHERE userid = @idUsuari;", oConnexio.Con);
                oSqlCommand.Parameters.Add(new SqlParameter("@idUsuari", idUsuari));
                oSqlCommand.Parameters.Add(new SqlParameter("@prestigi", prestigi));

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
    }

    public void EliminarContacte(string nomUsuari)
    {
        msgError = "";
        SqlCommand oSqlCommand = null;
        string iIdContacte;

        iIdContacte = ObtenirIdUsuari(nomUsuari);

        oConnexio.Obrir();
        try
        {
            oSqlCommand = new SqlCommand("DELETE FROM contactes WHERE id_usuari1 = @id1 AND id_usuari2 = @id2 OR id_usuari1 = @id2 AND id_usuari2 = @id1;", oConnexio.Con);

            //Definim els parametres utilitzats en l'objecte SqlCommand i els afegim
            oSqlCommand.Parameters.Add(new SqlParameter("@id1", idUsuari));
            oSqlCommand.Parameters.Add(new SqlParameter("@id2", iIdContacte));

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

    public void EliminarPeliculaVista(string iIdPelicula)
    {
        msgError = "";
        SqlCommand oSqlCommand = null;
        oConnexio.Obrir();
        try
        {
            oSqlCommand = new SqlCommand("DELETE FROM pelicules_vistes WHERE id_usuari = @idusuari AND id_pelicula = @idpelicula;", oConnexio.Con);

            //Definim els parametres utilitzats en l'objecte SqlCommand i els afegim
            oSqlCommand.Parameters.Add(new SqlParameter("@idusuari", idUsuari));
            oSqlCommand.Parameters.Add(new SqlParameter("@idpelicula", iIdPelicula));

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

    public void EliminarPeliculaAVeure(string iIdPelicula)
    {
        msgError = "";
        SqlCommand oSqlCommand = null;
        oConnexio.Obrir();
        try
        {
            oSqlCommand = new SqlCommand("DELETE FROM pelicules_a_veure WHERE id_usuari = @idusuari AND id_pelicula = @idpelicula;", oConnexio.Con);

            //Definim els parametres utilitzats en l'objecte SqlCommand i els afegim
            oSqlCommand.Parameters.Add(new SqlParameter("@idusuari", idUsuari));
            oSqlCommand.Parameters.Add(new SqlParameter("@idpelicula", iIdPelicula));

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

    public bool HaVist(string idPelicula)
    {
        msgError = "";
        SqlCommand oSqlCommand = null;
        SqlDataReader oSqlDataReader = null;

        oConnexio.Obrir();
        try
        {

            oSqlCommand = new SqlCommand("SELECT * FROM pelicules_vistes WHERE id_usuari = @id_usuari AND id_pelicula = @id_pelicula;", oConnexio.Con);
            oSqlCommand.Parameters.Add(new SqlParameter("@id_usuari", idUsuari));
            oSqlCommand.Parameters.Add(new SqlParameter("@id_pelicula", idPelicula));

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
        catch (Exception ex)
        {
            msgError = ex.Message;
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
                oConnexio.Tancar();
            }
        }
    }

    public bool VolVeure(string idPelicula)
    {
        msgError = "";
        SqlCommand oSqlCommand = null;
        SqlDataReader oSqlDataReader = null;

        oConnexio.Obrir();
        try
        {

            oSqlCommand = new SqlCommand("SELECT * FROM pelicules_a_veure WHERE id_usuari = @id_usuari AND id_pelicula = @id_pelicula;", oConnexio.Con);
            oSqlCommand.Parameters.Add(new SqlParameter("@id_usuari", idUsuari));
            oSqlCommand.Parameters.Add(new SqlParameter("@id_pelicula", idPelicula));

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
        catch (Exception ex)
        {
            msgError = ex.Message;
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
                oConnexio.Tancar();
            }
        }
    }
    #endregion
}