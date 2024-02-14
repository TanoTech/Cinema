using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace Cinema
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CaricaSaleCinema();
                VisualizzaPrenotazioni();
            }
        }

        private void CaricaSaleCinema()
        {
            ddlSala.Items.Add(new ListItem("Sala Est", "SalaEst"));
            ddlSala.Items.Add(new ListItem("Sala Sud", "SalaSud"));
            ddlSala.Items.Add(new ListItem("Sala Nord", "SalaNord"));
        }

        protected void btnPrenota_Click(object sender, EventArgs e)
        {
            string nome = txtNome.Text;
            string cognome = txtCognome.Text;
            string salaSelezionata = ddlSala.SelectedValue;
            bool ridotto = chkPrezzoRidotto.Checked;

            string nomeTabella = $"dbo.[{salaSelezionata}]";

            SalvaPrenotazione(nomeTabella, nome, cognome, ridotto);
            VisualizzaPrenotazioni();
        }

        private void SalvaPrenotazione(string nomeTabella, string nome, string cognome, bool ridotto)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString;

            string query = $"INSERT INTO {nomeTabella} (Nome, Cognome, Ridotto) VALUES (@Nome, @Cognome, @Ridotto)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Nome", nome);
                    cmd.Parameters.AddWithValue("@Cognome", cognome);
                    cmd.Parameters.AddWithValue("@Ridotto", ridotto);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void VisualizzaPrenotazioni()
        {
            phPrenotazioni.Controls.Clear();

            foreach (string sala in new string[] { "SalaEst", "SalaSud", "SalaNord" })
            {
                Label lblSala = new Label();
                lblSala.Text = $"Prenotazioni per {sala.Replace("Sala", " Sala ")}:";
                phPrenotazioni.Controls.Add(new LiteralControl("<br/>"));
                phPrenotazioni.Controls.Add(lblSala);
                phPrenotazioni.Controls.Add(new LiteralControl("<br/>"));

                string connectionString = ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString;
                string query = $"SELECT Nome, Cognome, Ridotto FROM dbo.[{sala}]";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string nome = reader.GetString(0);
                                string cognome = reader.GetString(1);
                                bool ridotto = reader.GetBoolean(2);
                                string idUnico = $"{nome}{cognome}".Replace(" ", ""); 

                                Label lblPrenotazione = new Label();
                                lblPrenotazione.ID = $"lbl{idUnico}";
                                lblPrenotazione.Text = $"{nome} {cognome} - Prezzo ridotto: {(ridotto ? "Sì" : "No")}";
                                phPrenotazioni.Controls.Add(lblPrenotazione);
                                phPrenotazioni.Controls.Add(new LiteralControl("<br/>"));
                            }
                        }

                    }
                }
            }
        }

    }
}
