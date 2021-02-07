using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormularioDePrueba
{
    public partial class Form1 : Form
    {
        private int sep = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.Text = "Letra: " + e.KeyChar;
        }

        private void btnCambioPos_Click(object sender, EventArgs e)
        {
            labelTextBox1.Posicion = labelTextBox1.Posicion == DI_Tema5_Ejer1_Componente.ePosicion.DERECHA ? labelTextBox1.Posicion = DI_Tema5_Ejer1_Componente.ePosicion.IZQUIERDA : labelTextBox1.Posicion = DI_Tema5_Ejer1_Componente.ePosicion.DERECHA;
        }

        private void btnCambioSep(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Name == "btnMas")
            {
                labelTextBox1.Separacion = sep++;
            }
            else
            {
                if (sep > 0)
                {
                    labelTextBox1.Separacion = sep--;
                }
                else
                {
                    label1.Text = "la separacón no debe ser más pequeña de 0";
                }
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            label1.Text = "KeyUp: "+(char)e.KeyValue;
        }

        private void LabelTextBox_CambiaPosición(object sender, EventArgs e)
        {
            this.Text = labelTextBox1.Posicion == DI_Tema5_Ejer1_Componente.ePosicion.DERECHA ? "derecha" : "izquierda";
        }

        private void LabelTextBox_CambiarSeparacion(object sender, EventArgs e)
        {
            label1.Text = string.Format("La separación es de: {0} entre label y textbox", sep);
        }

        private void labelTextBox1_TxtChanged(object sender, EventArgs e)
        {
            label2.Text = "El texto cambia";
        }
    }
}
