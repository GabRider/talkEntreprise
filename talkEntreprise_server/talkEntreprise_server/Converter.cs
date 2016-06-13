using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace talkEntreprise_server
{
   public class Converter
    {
       
       /////Champs/////
       
       private Controler _ctrl;
       /////propriétées/////
        public Controler Ctrl
        {
            get { return _ctrl; }
            set { _ctrl = value; }
        }
        /////Constructeur/////
       public Converter(Controler cont)
       {
           this.Ctrl = cont;
       }
       /////méthodes/////
       /// <summary>
       /// permet de convertire un nombre en hexadécimal
       /// </summary>
       /// <param name="number">nombre à convertire</param>
       /// <returns>nombre en hexadécimal</returns>
        public string NumberToHexadecimal(int number)
        {
            //aide pour la conversion : http://stackoverflow.com/questions/74148/how-to-convert-numbers-between-hexadecimal-and-decimal-in-c
            // aide pour le formattage : http://stackoverflow.com/questions/11618387/string-format-for-hex
            return string.Format("{0:x4}", number).ToUpper();
        }
       /// <summary>
       /// Convertire de l'héxadécimal en nombre
       /// </summary>
       /// <param name="hexa">nombre hexadécimal à convertire</param>
       /// <returns>nombre</returns>
        public long HexadecimalToNumber(string hexa) {
            return Convert.ToInt64(hexa,16);
        }
    }
}
