using System;
using System.IO;
using System.Windows.Forms;
using PdfiumViewer;

namespace DPDF
{
    public partial class Form1 : Form
    {
        private PdfViewer pdfViewer;
        private Button btnCarregarPDF;

        public Form1()
        {
            InitializeComponent();
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            // Inicializa o PdfViewer
            pdfViewer = new PdfViewer();
            pdfViewer.Dock = DockStyle.Fill;

            // Adiciona o PdfViewer ao formulário
            Controls.Add(pdfViewer);

            // Adiciona uma linha na parte superior
            Panel linhaSuperior = new Panel();
            linhaSuperior.BackColor = System.Drawing.Color.White;
            linhaSuperior.Dock = DockStyle.Top;
            linhaSuperior.Height = 2; // Altura da linha
            Controls.Add(linhaSuperior);

            // Inicializa o botão
            btnCarregarPDF = new Button();
            btnCarregarPDF.Text = "Carregar PDF";

            // Agora, pegamos as coordenadas da linha superior
            btnCarregarPDF.Location = new System.Drawing.Point(linhaSuperior.Location.X + 10 + 100, linhaSuperior.Bottom + 5);

            // Adiciona o botão ao formulário
            Controls.Add(btnCarregarPDF);

            // Atualiza a posição do botão no índice de controle para sobrepor a linha superior
            Controls.SetChildIndex(btnCarregarPDF, 0);

            // Adiciona o manipulador de eventos para o clique do botão
            btnCarregarPDF.Click += BtnCarregarPDF_Click;
        }

        private void BtnCarregarPDF_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Arquivos PDF (*.pdf)|*.pdf|Todos os arquivos (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    // Verifica se o arquivo existe
                    if (!File.Exists(filePath))
                    {
                        MessageBox.Show("O arquivo PDF não foi encontrado.");
                        return;
                    }

                    // Carrega o PDF no PdfViewer
                    pdfViewer.Document?.Dispose(); // Descarta o documento existente, se houver
                    pdfViewer.Document = PdfDocument.Load(filePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro: {ex.Message}");
            }
        }
    }
}
