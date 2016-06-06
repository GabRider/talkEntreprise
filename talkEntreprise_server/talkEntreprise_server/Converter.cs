using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace talkEntreprise_server
{
   public class Converter
    {
        Controler _ctrl;

        public Controler Ctrl
        {
            get { return _ctrl; }
            set { _ctrl = value; }
        }
       public Converter(Controler cont)
       {
           this.Ctrl = cont;
       }

        public string NumberToHexadecimal(int number)
        {
            //aide pour la conversion : http://stackoverflow.com/questions/74148/how-to-convert-numbers-between-hexadecimal-and-decimal-in-c
            // aide pour le formattage : http://stackoverflow.com/questions/11618387/string-format-for-hex
            return string.Format("{0:x4}", number).ToUpper();
        }
        public long HexadecimalToNumber(string hexa) {
            return Convert.ToInt64(hexa,16);
        }
    }
}
