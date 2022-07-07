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
namespace Tienda_el_Chulo
{
    public partial class FrmUsuarios : Form
    {
        public FrmUsuarios()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text != "")
            {
                try
                {


                    SqlConnection cone = new SqlConnection();
                    cone.ConnectionString = Conexion.CONEXIONMAESTRA.CONEXION;
                    cone.Open();
                    SqlCommand cmd = new SqlCommand();

                    cmd = new SqlCommand("insertar_usuarios", cone);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombres", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@Login", txtUsuario.Text);
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text);
                    cmd.Parameters.AddWithValue("@Rol", txtRol.Text);
                    //cadena para transformar las imagenes de binarios a un formato sql
                    //Esta funcion permite almacenar en la DataBase las imagenes que el usuario elija
                    MemoryStream ms = new MemoryStream();
                    imgPerfil.Image.Save(ms, imgPerfil.Image.RawFormat);


                    cmd.Parameters.AddWithValue("@Icono", ms.GetBuffer());
                    cmd.Parameters.AddWithValue("@Nombre_Icono", lblNumeroIcono.Text);
                    cmd.Parameters.AddWithValue("@Estado","Activo");

                    cmd.ExecuteNonQuery();
                    cone.Close();

                    Mostrar();
                    panelAgregarUser.Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }

        private void Mostrar()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;

                SqlConnection cone = new SqlConnection();
                cone.ConnectionString = Conexion.CONEXIONMAESTRA.CONEXION;
                cone.Open();

                da = new SqlDataAdapter("mostrar_usuario", cone);
                da.Fill(dt);
                dataListUsers.DataSource = dt;
                cone.Close();
                //ocultando las columnas que no se van a mostrar (id,icono,etc)
                dataListUsers.Columns[1].Visible = false;
                dataListUsers.Columns[5].Visible = false;
                dataListUsers.Columns[6].Visible = false;
                dataListUsers.Columns[7].Visible = false;
                dataListUsers.Columns[8].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void lblAnuncioIcono_Click(object sender, EventArgs e)
        {
            panelIconos.Visible = true;
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            imgPerfil.Image = pictureBox2.Image;
            lblNumeroIcono.Text = "0";
            lblAnuncioIcono.Visible = false;
            panelIconos.Visible = false;
        }


        private void pictureBox3_Click(object sender, EventArgs e)
        {
            imgPerfil.Image = pictureBox3.Image;
            lblNumeroIcono.Text = "1";
            lblAnuncioIcono.Visible = false;
            panelIconos.Visible = false;
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            imgPerfil.Image = pictureBox4.Image;
            lblNumeroIcono.Text = "2";
            lblAnuncioIcono.Visible = false;
            panelIconos.Visible = false;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            imgPerfil.Image = pictureBox5.Image;
            lblNumeroIcono.Text = "3";
            lblAnuncioIcono.Visible = false;
            panelIconos.Visible = false;
        }
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            imgPerfil.Image = pictureBox6.Image;
            lblNumeroIcono.Text = "4";
            lblAnuncioIcono.Visible = false;
            panelIconos.Visible = false;
        }
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            imgPerfil.Image = pictureBox7.Image;
            lblNumeroIcono.Text = "5";
            lblAnuncioIcono.Visible = false;
            panelIconos.Visible = false;
        }
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            imgPerfil.Image = pictureBox8.Image;
            lblNumeroIcono.Text = "6";
            lblAnuncioIcono.Visible = false;
            panelIconos.Visible = false;
        }
        private void pictureBox9_Click(object sender, EventArgs e)
        {
            imgPerfil.Image = pictureBox9.Image;
            lblNumeroIcono.Text = "7";
            lblAnuncioIcono.Visible = false;
            panelIconos.Visible = false;
        }
        private void pictureBox10_Click(object sender, EventArgs e)
        {
            imgPerfil.Image = pictureBox10.Image;
            lblNumeroIcono.Text = "8";
            lblAnuncioIcono.Visible = false;
            panelIconos.Visible = false;
        }
        private void pictureBox11_Click(object sender, EventArgs e)
        {
            imgPerfil.Image = pictureBox11.Image;
            lblNumeroIcono.Text = "9";
            lblAnuncioIcono.Visible = false;
            panelIconos.Visible = false;
        }
        private void pictureBox12_Click(object sender, EventArgs e)
        {
            imgPerfil.Image = pictureBox12.Image;
            lblNumeroIcono.Text = "10";
            lblAnuncioIcono.Visible = false;
            panelIconos.Visible = false;
        }
        private void pictureBox13_Click(object sender, EventArgs e)
        {
            imgPerfil.Image = pictureBox13.Image;
            lblNumeroIcono.Text = "11";
            lblAnuncioIcono.Visible = false;
            panelIconos.Visible = false;
        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            //Esta seccion es cuando carga el Formulario
            panelAgregarUser.Visible = false;
            panelIconos.Visible = false;
            Mostrar();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            panelAgregarUser.Visible = true;
            lblAnuncioIcono.Visible = true;
            txtNombre.Text = "";
            txtUsuario.Text = "";
            txtPassword.Text = "";
            txtCorreo.Text = "";
            txtRol.Text = "";
            txtNombre.Focus();
            btnGuardar.Visible = true;
            btnGuardarCambios.Visible = false;
        }

        private void dataListUsers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //    try
            //    {
            lblIdUser.Text = dataListUsers.SelectedCells[1].Value.ToString();
            txtNombre.Text = dataListUsers.SelectedCells[2].Value.ToString();
            txtUsuario.Text = dataListUsers.SelectedCells[3].Value.ToString();
            txtPassword.Text = dataListUsers.SelectedCells[4].Value.ToString();
            //tratamiento de icono
            imgPerfil.BackgroundImage = null;
            byte[] b = (Byte[])dataListUsers.SelectedCells[5].Value;
            //byte[] imagen = (Byte[])dataListUsers.SelectedCells[5].Value;
            //MemoryStream ms = new MemoryStream(imagen);
            MemoryStream ms = new MemoryStream(b);
            imgPerfil.Image = Image.FromStream(ms);


            //imgPerfil.Image = System.Drawing.Bitmap.FromStream(ms);


            imgPerfil.SizeMode = PictureBoxSizeMode.StretchImage;

            //MemoryStream Ms = new MemoryStream(b.ToArray());
            //Bitmap Im = new Bitmap(Ms);
            //imgPerfil.Image = Im;
            //imgPerfil.Image = Image.FromStream(ms);

            lblAnuncioIcono.Visible = false;

            lblNumeroIcono.Text = dataListUsers.SelectedCells[6].Value.ToString();
            txtCorreo.Text = dataListUsers.SelectedCells[7].Value.ToString();
            txtRol.Text = dataListUsers.SelectedCells[8].Value.ToString();
            panelAgregarUser.Visible = true;
            btnGuardar.Visible = false;
            btnGuardarCambios.Visible = true;

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}




        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            panelAgregarUser.Visible = false;
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text != "")
            {
                try
                {


                    SqlConnection cone = new SqlConnection();
                    cone.ConnectionString = Conexion.CONEXIONMAESTRA.CONEXION;
                    cone.Open();
                    SqlCommand cmd = new SqlCommand();

                    cmd = new SqlCommand("editar_usuarios", cone);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_User", lblIdUser.Text);
                    cmd.Parameters.AddWithValue("@Nombres", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@Login", txtUsuario.Text);
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text);
                    cmd.Parameters.AddWithValue("@Rol", txtRol.Text);
                    //cadena para transformar las imagenes de binarios a un formato sql
                    //Esta funcion permite almacenar en la DataBase las imagenes que el usuario elija
                    MemoryStream ms = new MemoryStream();
                    imgPerfil.Image.Save(ms, imgPerfil.Image.RawFormat);


                    cmd.Parameters.AddWithValue("@Icono", ms.GetBuffer());
                    cmd.Parameters.AddWithValue("@Nombre_Icono", lblNumeroIcono.Text);

                    cmd.ExecuteNonQuery();
                    cone.Close();

                    Mostrar();
                    panelAgregarUser.Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void imgPerfil_Click(object sender, EventArgs e)
        {
            panelIconos.Visible = true;
        }

        private void dataListUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.dataListUsers.Columns["btnDeleted"].Index)
            {
                DialogResult result;
                result = MessageBox.Show("¿Realmente Desea eliminar este Usuario?", "Eliminando Reguistros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    SqlCommand cmd;
                    try
                    {
                        foreach (DataGridViewRow row in dataListUsers.SelectedRows)
                        {

                        
                            int OneKey = Convert.ToInt32(row.Cells["id_User"].Value);
                            string Usuario = Convert.ToString(row.Cells["login"].Value);

                            try
                            {
                                SqlConnection cone = new SqlConnection();
                                cone.ConnectionString = Conexion.CONEXIONMAESTRA.CONEXION;
                                cone.Open();

                                cmd = new SqlCommand("eliminar_usuraio", cone);
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@idUser", OneKey);
                                cmd.Parameters.AddWithValue("@login", Usuario);
                                cmd.ExecuteNonQuery();

                                cone.Close();

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }

                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                Mostrar();
            }
        }
    }
}
