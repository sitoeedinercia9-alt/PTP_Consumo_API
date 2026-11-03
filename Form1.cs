using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;
namespace Consumo_API_CSharp
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient cliente = new HttpClient();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {

            try
            {
                listBox1.Items.Clear();

                button1.Enabled = false;
                button1.Text = "A carregar...";

                HttpResponseMessage resposta = await cliente.GetAsync("https://dummyjson.com/quotes");

                resposta.EnsureSuccessStatusCode();

                string json = await resposta.Content.ReadAsStringAsync();

                RespostaApi dados = JsonConvert.DeserializeObject<RespostaApi>(json);

                if (dados != null && dados.Quotes != null)
                {
                    foreach (Citacao citacao in dados.Quotes)
                    {
                        listBox1.Items.Add(
                            citacao.Id + ". " + citacao.Texto + " - " + citacao.Autor
                        );
                    }
                }

                MessageBox.Show("Citações carregadas com sucesso!");
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao ligar à API: " + erro.Message);
            }
            finally
            {
                button1.Enabled = true;
                button1.Text = "Buscar Citações";
            }

        }
    }
}
public class RespostaApi
{
    [JsonProperty("quotes")]
    public List<Citacao> Quotes { get; set; }
}

public class Citacao
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("quote")]
    public string Texto { get; set; }

    [JsonProperty("author")]
    public string Autor { get; set; }
}