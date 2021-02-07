using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DI_Tema5_Ejer1_Componente
{
    public enum ePosicion
    {
        IZQUIERDA, DERECHA
    }

    public partial class LabelTextBox : UserControl
    {
        public LabelTextBox()
        {
            InitializeComponent();
        }

        /*=========================================================================
            88""Yb 88""Yb  dP"Yb  88""Yb 88 888888 8888b.     db    8888b.  888888 .dP"Y8
            88__dP 88__dP dP   Yb 88__dP 88 88__    8I  Yb   dPYb    8I  Yb 88__   `Ybo."
            88"""  88"Yb  Yb   dP 88"""  88 88""    8I  dY  dP__Yb   8I  dY 88""   o.`Y8b
            88     88  Yb  YbodP  88     88 888888 8888Y"  dP""""Yb 8888Y"  888888 8bodP'

         ========================================================================== */

        //Propiedad Apariencia ====================================================

        private ePosicion posicion = ePosicion.IZQUIERDA;
        [Category("Appearance")]
        [Description("Indica si la label se situa a la izquiera del textbox " +
            "o a la derecha")]
        public ePosicion Posicion
        {
            //set y get
            set
            {
                if (Enum.IsDefined(typeof(ePosicion), value))
                {
                    //si el valor(value) que le llega es un Enum es de tipo ePosicion
                    posicion = value;
                    recolocar();
                    if (CambiaPosición != null)
                    {
                        CambiaPosición(this, new EventArgs());
                        /*Si en el formulario, se mete el evento de CambiaPosición lo invocará
                         por ejemplo, en el form ponemos un onClick que al pulsar cambie la posición
                         cada vez que se cambia la posición llegamos a este set, si a parte hemos programado
                         un evento CambiaPosición en el que cambie el título del formulario, al cambiar la posicion
                         entrará en set y llamará a la funcion CambiaPosición que cambiará el titulo*/
                    }
                }
                else
                {
                    throw new InvalidEnumArgumentException();
                }
            }

            get
            {
                return posicion;
            }
        }


        //Propiedad Diseño ====================================================

        private int separacion = 0; //variable privada
        [Category("Design")]
        [Description("Los pixels de separación entre el label y el texbox")]
        public int Separacion //setters y getters
        {
            set
            {
                if (value >= 0) //menos seria uno encima del otro
                {
                    separacion = value;
                    recolocar();
                    if (CambiarSeparacion != null)
                    {
                        CambiarSeparacion(this, new EventArgs());
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            get
            {
                return separacion;
            }
        }
        /*===========================================================
          88""Yb 88   88 88""Yb 88     88  dP""b8    db    .dP"Y8
          88__dP 88   88 88__dP 88     88 dP   `"   dPYb   `Ybo."
          88"""  Y8   8P 88""Yb 88  .o 88 Yb       dP__Yb  o.`Y8b
          88     `YbodP' 88oodP 88ood8 88  YboodP dP""""Yb 8bodP'
         ============================================================*/
        //Propiedades públicas que dan acceso a las propiedades que por defecto estan privadas
        //pero que queremos que puedan acceder a ellas por ejemplo la propiedad Text
        //De quererlo, tendríamos que hacer lo mismo para color, font, size etc.
        [Category("Appearance")]
        [Description("Texto asociado a la label del control")]
        public string TextLbl
        {
            set
            {
                label1.Text = value;
                recolocar();
            }
            get
            {
                return label1.Text;
            }
        }

        [Category("Appearance")]
        [Description("Texto asociado a la textox del control")]
        public string TextTxt
        {
            set
            {
                textBox1.Text = value;
               
            }
            get
            {
                return textBox1.Text;
            }
        }

        private char pswChr;
        [Category("Desing")]
        [Description("enlaza con la propiedad PasswordChar del Textbox interno.")]
        public char PswChr
        {
            set
            {
                textBox1.PasswordChar = value;
            }
            get
            {
                return textBox1.PasswordChar;
            }
        }


        private void recolocar()
        {
            switch (posicion)
            {
                case ePosicion.DERECHA:
                    textBox1.Location = new Point(0, 0);
                    textBox1.Width = this.Width - label1.Width - separacion; //lael tiene ancho fijo
                    label1.Location = new Point(textBox1.Width + separacion, 0);
                    this.Height = Math.Max(textBox1.Height, label1.Height);//this hace referencia al recuadro que engloba todo
                    break;
                case ePosicion.IZQUIERDA:
                    label1.Location = new Point(0, 0);
                    textBox1.Location = new Point(label1.Width + separacion, 0);
                    textBox1.Width = this.Width - label1.Width - separacion;
                    this.Height = Math.Max(textBox1.Height, label1.Height);
                    break;
            }
        }

        /*=======================================================================
            88        db    88b 88 8888P    db    88""Yb
            88       dPYb   88Yb88   dP    dPYb   88__dP
            88  .o  dP__Yb  88 Y88  dP    dP__Yb  88"Yb 
            88ood8 dP""""Yb 88  Y8 d8888 dP""""Yb 88  Yb
        
            888888 Yb    dP 888888 88b 88 888888  dP"Yb  .dP"Y8
            88__    Yb  dP  88__   88Yb88   88   dP   Yb `Ybo."
            88""     YbdP   88""   88 Y88   88   Yb   dP o.`Y8b
            888888    YP    888888 88  Y8   88    YbodP  8bodP'
        =========================================================================*/

        private void LabelTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //De esta manera, el usuario al pulsar una tecla lanza el evento KeyPress del TextBox,
            // y la función provoca que se lance el evento KeyPress del LabelTextBox.
            this.OnKeyPress(e);
        }

        private void LabelTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            this.OnKeyUp(e);
        }

        private void Txt_change(object sender, EventArgs e)
        {
            if (TxtChanged != null)
            {
                TxtChanged(this, new EventArgs());
            }
        }

        /*=======================================================================
            dP""b8 88""Yb 888888    db    88""Yb
           dP   `" 88__dP 88__     dPYb   88__dP
           Yb      88"Yb  88""    dP__Yb  88"Yb 
            YboodP 88  Yb 888888 dP""""Yb 88  Yb

           888888 Yb    dP 888888 88b 88 888888  dP"Yb  .dP"Y8
           88__    Yb  dP  88__   88Yb88   88   dP   Yb `Ybo."
           88""     YbdP   88""   88 Y88   88   Yb   dP o.`Y8b
           888888    YP    888888 88  Y8   88    YbodP  8bodP'
       =========================================================================*/

        [Category("La propiedad cambió")]
        [Description("Se lanza cuando la propiedad Posición cambia")]
        public event EventHandler CambiaPosición;

        [Category("")]
        [Description("Se lanza cuando la propiedad Separación cambia")]
        public event EventHandler CambiarSeparacion;

        [Category("Cuando cambia el texto")]
        [Description("Se lanza cuando sucede el evento TextChanged del textbox")]
        public event EventHandler TxtChanged;
        /*=======================================================================
             dP"Yb  888888 88""Yb  dP"Yb  .dP"Y8
            dP   Yb   88   88__dP dP   Yb `Ybo."
            Yb   dP   88   88"Yb  Yb   dP o.`Y8b
             YbodP    88   88  Yb  YbodP  8bodP'
       =========================================================================*/

        // Esta función has de enlazarla con el evento SizeChanged.
        // Sería necesario también tener en cuenta otros eventos como FontChanged
        // que aquí nos saltamos.
        private void LabelTextBox_SizeChanged(object sender, EventArgs e)
        {
            recolocar();
        }

    }


}
