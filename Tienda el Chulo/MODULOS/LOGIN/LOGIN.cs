using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using Siticone.UI.WinForms;
using System.Net;
using System.Net.Mail;

namespace Tienda_el_Chulo.MODULOS
{
    public partial class LOGIN : Form
    {
        int contador;
        public LOGIN()
        {
            InitializeComponent();
        }
        public void DibujarUsuarios()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Conexion.CONEXIONMAESTRA.CONEXION;
            con.Open();
            
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("select * from Usuarios WHERE Estado = 'Activo' ", con);

            SqlDataReader sdr = cmd.ExecuteReader();
            //bucle 
            Int32 y = 0;
            while (sdr.Read())
            {
                //creando componentes
                Panel pan = new Panel();
                SiticonePictureBox PB = new SiticonePictureBox();
                Label lab = new Label();

                //Declarando propiedades a Label 
                lab.Text = sdr["login"].ToString();
                lab.Name = sdr["id_User"].ToString();
                lab.Size = new Size(162, 53);
                lab.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                lab.FlatStyle = FlatStyle.Flat;
                lab.ForeColor = Color.FromArgb(240, 240, 240);
                lab.Dock = DockStyle.Bottom;
                lab.TextAlign = ContentAlignment.MiddleCenter;
                lab.Cursor = Cursors.Hand;

                //Asignando propiedades al Panel
                pan.Size = new Size(162, 150);
                pan.BorderStyle = BorderStyle.None;
                pan.Cursor = Cursors.Hand;
                pan.BackColor = Color.FromArgb(20, 20, 20);

                //Asignando propiedades a PicturBox
                PB.Name = "pb"+ sdr["id_User"].ToString();
                PB.Size = new Size(162, 100);
                PB.Dock = DockStyle.Top;
                PB.BackgroundImage = null;
                //Convercion de byte a imagen
                byte[] bi = (Byte[])sdr["Icono"];
                MemoryStream ms = new MemoryStream(bi);
                PB.Image = Image.FromStream(ms);
                PB.SizeMode = PictureBoxSizeMode.Zoom;
                PB.Tag = sdr["login"].ToString();
                PB.Cursor= Cursors.Hand;

                pan.Controls.Add(lab);
                pan.Controls.Add(PB);
                //Bringtofrom sirve para traer al frente
                lab.BringToFront();
                flowLayoutPanelUsuarios.Controls.Add(pan);

                //Programando eventos
                lab.Click += new EventHandler(Eventolabel);
                PB.Click += new EventHandler(EventoPictureBox);
                y++;
            }
            con.Close();

        }
    

        private void EventoPictureBox(object sender, EventArgs eventArgs)
        {
            txtLogin.Text = ((PictureBox)sender).Tag.ToString();
            panelInicioSesion.Visible = true;
            flowLayoutPanelUsuarios.Visible = false;
            picImgInicioSesion.Image = ((PictureBox)sender).Image;
        }
        private void Eventolabel(object sender, EventArgs eventArgs)
        {
            var controlsPanel = flowLayoutPanelUsuarios.Controls.OfType<Panel>();
            foreach (Panel pn in controlsPanel)
            {
                var controlImg = pn.Controls.OfType<SiticonePictureBox>();
                foreach (SiticonePictureBox item in controlImg)
                {
                    if (item.Name == "pb" + ((Label)sender).Name)
                    {
                        picImgInicioSesion.Image = item.Image;
                    }
                }

            }
            
            txtLogin.Text = ((Label)sender).Name;
            //this.Image.Save()
            panelInicioSesion.Visible = true;
            flowLayoutPanelUsuarios.Visible = false;
            //picImgInicioSesion.Image = ((PictureBox)sender).Image;
        }

        private void LOGIN_Load(object sender, EventArgs e)
        {
            DibujarUsuarios();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            btnViewMostrar.IconChar = FontAwesome.Sharp.IconChar.EyeSlash;

            if (btnViewMostrar.IconChar == FontAwesome.Sharp.IconChar.EyeSlash)
            {

            }

            if (btnViewMostrar.IconChar == FontAwesome.Sharp.IconChar.Eye)
            {

            }
        }

        private void txtPassword__TextChanged(object sender, EventArgs e)
        {

            IniciarSesion_Correcto();
            
        }

        private void IniciarSesion_Correcto()
        {
            CargarUsuario();
            Contar();

            if (contador > 0)
            {
                //llamar y abrir formulari apertura de caja
                CAJA.frmAPERTURA_DE_CAJA FormularioAC = new CAJA.frmAPERTURA_DE_CAJA();

                //cerrar eel formulario actual
                //this.Close();
                this.Hide();
                FormularioAC.ShowDialog();

            }

        }

        private void Contar()
        {
            int x;
            x = dataListUsers.Rows.Count;
            //txtContador.Text = Convert.ToString(x);
            contador = (x);
        }
        private void CargarUsuario()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;

                SqlConnection cone = new SqlConnection();
                cone.ConnectionString = Conexion.CONEXIONMAESTRA.CONEXION;
                cone.Open();

                da = new SqlDataAdapter("validar_usuario", cone);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Password", txtPassword.Text);
                da.SelectCommand.Parameters.AddWithValue("@login", txtLogin.Text);

                da.Fill(dt);
                dataListUsers.DataSource = dt;
                cone.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

      


        private void label3_Click(object sender, EventArgs e)
        {
            panelInicioSesion.Visible = false;
            flowLayoutPanelUsuarios.Visible = true;
        }

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {
            panelRestaurarCuenta.Visible = false;
        }
        private void fnMostrarCorreo()

        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;

                SqlConnection cone = new SqlConnection();
                cone.ConnectionString = Conexion.CONEXIONMAESTRA.CONEXION;
                cone.Open();

                da = new SqlDataAdapter("select Correo from Usuarios where estado = 'Activo'", cone);
                

                da.Fill(dt);
                cbCorreo.DisplayMember = "Correo";
                cbCorreo.ValueMember = "Correo";
                cbCorreo.DataSource = dt;
                cone.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void rjButton1_Click(object sender, EventArgs e)
        {
            panelRestaurarCuenta.Visible = true;
            fnMostrarCorreo();
        }
        private void MostrarUsuarioXCorreo()
        {
            try
            {
                String Resultado;
               
               

                SqlConnection cone = new SqlConnection();

                cone.ConnectionString = Conexion.CONEXIONMAESTRA.CONEXION;

                SqlCommand da = new SqlCommand("MostrarUsuarioXCorreo", cone);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@Correo", cbCorreo.Texts);

                cone.Open();
                //lblResultadoPass.Text = ToString(da.ExecuteScalar());
                lblResultadoPass.Text = Convert.ToString(da.ExecuteScalar());
                cone.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rjButton2_Click(object sender, EventArgs e)
        {
            MostrarUsuarioXCorreo();
            RichTextBox1.Text =RichTextBox1.Text.Replace("@Pass",lblResultadoPass.Text) ;
            EnviarCorreo("Edwarworld247@gmail.com" , "rvcwbmfphtknzavf", RichTextBox1.Text , "Solicitud de contraseña", cbCorreo.Texts,"");
        }
        internal void EnviarCorreo(string emisor , string password ,string mensaje , string asunto , string destinatario , string Ruta)
        {
            try
            {
                MailMessage correos = new MailMessage();
                SmtpClient envios = new SmtpClient();
                correos.To.Clear();
                correos.Body = "";
                correos.Subject = "";
                correos.Body = mensaje;
                correos.Subject = asunto;
                correos.IsBodyHtml = true;
                correos.To.Add((destinatario));
                correos.From = new MailAddress(emisor);
                envios.Credentials = new NetworkCredential(emisor, password);

                envios.Host = "smtp.gmail.com";
                envios.Port = 587;
                envios.EnableSsl = true;

                envios.Send(correos);

                lblEstadoEnvio.Text = "ENVIADO";

                MessageBox.Show("Contraseña enviada correctamente , Porfavor revisar correo", "Contraseña enviada");
               panelRestaurarCuenta.Visible = false;



            }
            catch (Exception ex)

            {

                lblEstadoEnvio.Text = "Correo no registrado";
            }
        }

        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
