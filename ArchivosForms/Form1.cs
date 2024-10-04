using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArchivosForms
{
    public partial class Form1 : Form
    {
        private const string filePath = "datos.dat";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    using (FileStream mArchivoLector = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    using (BinaryReader Lector = new BinaryReader(mArchivoLector))
                    {
                        while (mArchivoLector.Position != mArchivoLector.Length)
                        {
                            int length = Lector.ReadInt32();
                            char[] nombreArray = Lector.ReadChars(length);
                            string nombre = new string(nombreArray);
                            int edad = Lector.ReadInt32();
                            int nota = Lector.ReadInt32();
                            char genero = Lector.ReadChar();

                            listBox1.Items.Add($"Nombre: {nombre}, Edad: {edad}, Nota: {nota}, Género: {genero}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbEdad_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbNota_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbGenero_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tbNombre.Text) ||
                    string.IsNullOrWhiteSpace(tbEdad.Text) ||
                    string.IsNullOrWhiteSpace(tbNota.Text) ||
                    string.IsNullOrWhiteSpace(tbGenero.Text))
                {
                    MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string nombre = tbNombre.Text;
                int edad = int.Parse(tbEdad.Text);
                int nota = int.Parse(tbNota.Text);
                char genero = char.Parse(tbGenero.Text);

                if (tbGenero.Text.Length != 1)
                {
                    MessageBox.Show("El campo género debe contener solo un carácter.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (FileStream mArchivoEscritor = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
                using (BinaryWriter Escritor = new BinaryWriter(mArchivoEscritor))
                {
                    mArchivoEscritor.Seek(0, SeekOrigin.End);

                    Escritor.Write(nombre.Length);
                    Escritor.Write(nombre.ToCharArray());
                    Escritor.Write(edad);
                    Escritor.Write(nota);
                    Escritor.Write(genero);
                }

                listBox1.Items.Add($"Nombre: {nombre}, Edad: {edad}, Nota: {nota}, Género: {genero}");

                tbNombre.Clear();
                tbEdad.Clear();
                tbNota.Clear();
                tbGenero.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
