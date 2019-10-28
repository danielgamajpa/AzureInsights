using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Insights
{
    public partial class Form1 : Form
    {

        private const string CHAVE_INSTRUMENTACAO = "CHAVE_INSTRUMENTACAO";

        Microsoft.ApplicationInsights.TelemetryClient telemetria;

        string SessaoUsuario;

        Dictionary<string, string> propriedades;



        public Form1()
        {
            InitializeComponent();

            SessaoUsuario = Guid.NewGuid().ToString();

            telemetria = new Microsoft.ApplicationInsights.TelemetryClient();
            telemetria.InstrumentationKey = CHAVE_INSTRUMENTACAO;
            telemetria.Context.Session.Id = SessaoUsuario;

            EnviarEventosTelemetria("Sistema Iniciado");

            propriedades = new Dictionary<string, string>();
            propriedades.Add("VersaoApp", Application.ProductVersion);
            propriedades.Add("UF", "PB");
        }

        private void button1_Click(object sender, EventArgs e)
        {

            EnviarEventosTelemetria("Botao 1 Clicado");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EnviarEventosTelemetria("Botao 2 Clicado");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EnviarEventosTelemetria("Botao 3 Clicado");

            try
            {
                throw new Exception("Erro");
            }
            catch (Exception ex)
            {
                EnviarExceptionsTelemetria(ex);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            EnviarEventosTelemetria("Botao 4 Clicado");
        }

        //-------------------------------------
        //-------------------------------------
        //-------------------------------------
        private void EnviarEventosTelemetria(string NomeEvento)
        {
            telemetria.TrackEvent(NomeEvento, propriedades);

            telemetria.Flush();
        }

        private void EnviarExceptionsTelemetria(Exception ex)
        {
            telemetria.TrackException(ex, propriedades);

            telemetria.Flush();
        }
    }
}
