using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using System.IO;

namespace Tabela_e_DataBase
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void botaoCriarTabela_Click(object sender, EventArgs e)
        {
            string pathBd = Application.StartupPath + @"\BD\BancoSqlCde.sdf";
            string strConection = @"DataSource =" + pathBd + "; PassWord = '123'";

            SqlCeConnection conexao = new SqlCeConnection(strConection);

            try
            {
                conexao.Open(); // abre conexão
                SqlCeCommand comando = new SqlCeCommand(); // objeto de conexão
                comando.Connection = conexao;
                comando.CommandText = "CREATE TABLE pessoas (id INT NOT " + "NULL PRIMARY KEY, nome NVARCHAR(50), email NVARCHAR(50))";
                comando.ExecuteNonQuery();
                MessageBox.Show("Tabela criada, segue o lider");
                comando.Dispose();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro " + erro.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        private void botaoConectar_Click_1(object sender, EventArgs e)
        {
            string pathBd = Application.StartupPath + @"\BD\BancoSqlCde.sdf";
            string strConection = @"DataSource =" + pathBd + "; PassWord = '123'";

            SqlCeEngine bd = new SqlCeEngine(strConection);

            if (!File.Exists(pathBd))
            {
                bd.CreateDatabase();
            }
            bd.Dispose();

            SqlCeConnection conexao = new SqlCeConnection(strConection);

            try
            {
                conexao.Open();
                MessageBox.Show("Conexão estabelecida");
            }

            catch (Exception ex)
            {
                MessageBox.Show("Erro ao estabelecer conexão: " + ex);
            }
        }

        private void botaoInserir_Click(object sender, EventArgs e)
        {
            string pathBd = Application.StartupPath + @"\BD\BancoSqlCde.sdf";
            string strConection = @"DataSource =" + pathBd + "; PassWord = '123'";

            SqlCeConnection conexao = new SqlCeConnection(strConection);

            try
            {
                conexao.Open();
                SqlCeCommand comando = new SqlCeCommand();
                comando.Connection = conexao;

                int id = new Random().Next(0, 1000);
                string nome = textNome.Text;
                string email = textEmail.Text;

                comando.CommandText = "INSERT INTO pessoas VALUES " +
                    "(" + id +",'" + nome + "','" + email + "')";
                comando.ExecuteNonQuery();
                MessageBox.Show("Dados inseridos na tabela criados com sucessso");
                comando.Dispose();
            }

            catch (Exception er)
            {
                MessageBox.Show("Erro " + er.Message);
            }

            finally
            {
                conexao.Close();
            }
        }
    }
}
