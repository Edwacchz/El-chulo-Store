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


        private void Cargar_Estado_de_Iconos()
        {
            //esta funcion oculta los las imagenes que ya se estan utilizando
            try
            {
                foreach (DataGridViewRow row in dataListUsers.Rows)
                    try
                    {
                        string imgPerfil = Convert.ToString(row.Cells["Nombre_de_icono"].Value);
                        if (imgPerfil == "0")
                        {
                           
                            pictureBox2.Visible = false;
                        }
                        if (imgPerfil == "1")
                        {

                            pictureBox3.Visible = false;
                        }
                        if (imgPerfil == "2")
                        {

                            pictureBox4.Visible = false;
                        }
                        if (imgPerfil == "3")
                        {

                            pictureBox5.Visible = false;
                        }
                        if (imgPerfil == "4")
                        {

                            pictureBox6.Visible = false;
                        }
                        if (imgPerfil == "5")
                        {

                            pictureBox7.Visible = false;
                        }
                        if (imgPerfil == "6")
                        {

                            pictureBox8.Visible = false;
                        }
                        if (imgPerfil == "7")
                        {

                            pictureBox9.Visible = false;
                        }
                        if (imgPerfil == "8")
                        {

                            pictureBox10.Visible = false;
                        }
                        if (imgPerfil == "9")
                        {

                            pictureBox11.Visible = false;
                        }
                        if (imgPerfil == "10")
                        {

                            pictureBox12.Visible = false;
                        }
                        if (imgPerfil == "11")
                        {

                            pictureBox2.Visible = false;
                        }


                    }
                    catch(Exception ex)
                    {
                      
                    }
            }
            catch
            {

            }
        }
        public bool validar_Mail(string sMail)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(sMail, @"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$");
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validar_Mail(txtCorreo.Text) == false)
            {
                MessageBox.Show("Direccion de Correo electronico , No Valida, el correo debe tener el formato : Nombre@Dominio.com "
                                + "Por Favor Escriba un correo electronico valido",
                                 "Validacion de Correo electronico", MessageBoxButtons.OK);

                txtCorreo.Focus();
                txtCorreo.SelectAll();
            }
            else
            {
            
            if (txtNombre.Text != "")
            {
                    if (txtRol.Text != "")
                    {
                        if (lblAnuncioIcono.Visible == false)
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
                                cmd.Parameters.AddWithValue("@Estado", "Activo");

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
                        else
                        {
                            MessageBox.Show("Elige una Imagen de Perfil" , "Registro", MessageBoxButtons.OK );
                        }
                    }
                    else
                    {
                        MessageBox.Show("Elige un Rol para el nuevo Usuario", "Registro", MessageBoxButtons.OK);
                    }
               
            }
            else
                {
                    MessageBox.Show("Completa el Campo Nombre ", " Registro", MessageBoxButtons.OK);
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

        #region eventos clic en imagenes por defecto
        private void fnocultarLblAnun_PanelIcono()
        {
            lblAnuncioIcono.Visible = false;
            panelIconos.Visible = false;
        }
        private void lblAnuncioIcono_Click(object sender, EventArgs e)
        {
            Cargar_Estado_de_Iconos();
            panelIconos.Visible = true;
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            imgPerfil.Image = pictureBox2.Image;
            lblNumeroIcono.Text = "0";
            fnocultarLblAnun_PanelIcono();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            imgPerfil.Image = pictureBox3.Image;
            lblNumeroIcono.Text = "1";
            fnocultarLblAnun_PanelIcono();
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            imgPerfil.Image = pictureBox4.Image;
            lblNumeroIcono.Text = "2";
            fnocultarLblAnun_PanelIcono();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            imgPerfil.Image = pictureBox5.Image;
            lblNumeroIcono.Text = "3";
            fnocultarLblAnun_PanelIcono();
        }
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            imgPerfil.Image = pictureBox6.Image;
            lblNumeroIcono.Text = "4";
            fnocultarLblAnun_PanelIcono();
        }
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            imgPerfil.Image = pictureBox7.Image;
            lblNumeroIcono.Text = "5";
            fnocultarLblAnun_PanelIcono();
        }
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            imgPerfil.Image = pictureBox8.Image;
            lblNumeroIcono.Text = "6";
            fnocultarLblAnun_PanelIcono();
        }
        private void pictureBox9_Click(object sender, EventArgs e)
        {
            imgPerfil.Image = pictureBox9.Image;
            lblNumeroIcono.Text = "7";
            fnocultarLblAnun_PanelIcono();
        }
        private void pictureBox10_Click(object sender, EventArgs e)
        {
            imgPerfil.Image = pictureBox10.Image;
            lblNumeroIcono.Text = "8";
            fnocultarLblAnun_PanelIcono();
        }
        private void pictureBox11_Click(object sender, EventArgs e)
        {
            imgPerfil.Image = pictureBox11.Image;
            lblNumeroIcono.Text = "9";
            fnocultarLblAnun_PanelIcono();
        }
        private void pictureBox12_Click(object sender, EventArgs e)
        {
            imgPerfil.Image = pictureBox12.Image;
            lblNumeroIcono.Text = "10";
            fnocultarLblAnun_PanelIcono();
        }
        private void pictureBox13_Click(object sender, EventArgs e)
        {
            imgPerfil.Image = pictureBox13.Image;
            lblNumeroIcono.Text = "11";
            fnocultarLblAnun_PanelIcono();
        }
      #endregion
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
            panelIconos.Visible = false;
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

        private void pboxAddImagenPerfil_Click(object sender, EventArgs e)
        {
            dlg.InitialDirectory = "";
            dlg.Filter = "imagenes|*.jpg;*png";
            dlg.FilterIndex = 2;
            dlg.Title = "Cargador de Imagenes de Perfil";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                imgPerfil.BackgroundImage = null;
                 imgPerfil.Image = new Bitmap(dlg.FileName);
                imgPerfil.SizeMode = PictureBoxSizeMode.Zoom;
                lblNumeroIcono.Text= Path.GetFileName(dlg.FileName);
                lblAnuncioIcono.Visible = false;
                panelIconos.Visible = false;
            }
        }
        private void fnBuscar_Usuario()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;

                SqlConnection cone = new SqlConnection();
                cone.ConnectionString = Conexion.CONEXIONMAESTRA.CONEXION;
                cone.Open();

                da = new SqlDataAdapter("buscar_usuario", cone);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Texto", txtBuscar.Text); 
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

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            fnBuscar_Usuario();
        }
    }
}
