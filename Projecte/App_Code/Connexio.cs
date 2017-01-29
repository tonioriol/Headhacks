using System;
// Afegits
using System.Data.SqlClient;

/// <summary>
/// Descripción breve de Connexio
/// </summary>
public class Connexio
{
    #region Atributs
    private SqlConnection oConnexio;
    private string msgError;
    #endregion

    #region Propietats
    public SqlConnection Con
    {
        get
        {
            return oConnexio;
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

    #region Mètode Constructor
    public Connexio()
    {
        msgError = "";
        try
        {
            oConnexio = new SqlConnection();
            oConnexio.ConnectionString = @"Data Source = localhost\SQLEXPRESS; Initial Catalog = headhacks; Integrated Security = true;";
        }
        catch (Exception ex)
        {
            msgError = ex.Message;
        }
    }
    #endregion

    #region Mètodes
    public void Obrir()
    {
        msgError = "";
        try
        {
            oConnexio.Open();
        }
        catch (Exception ex)
        {
            msgError = ex.Message;
        }
    }

    public void Tancar()
    {

        msgError = "";
        try
        {
            oConnexio.Close();
        }
        catch (Exception ex)
        {
            msgError = ex.Message;
        }
    }
    #endregion    
}