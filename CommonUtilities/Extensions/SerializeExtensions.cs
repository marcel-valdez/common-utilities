namespace CommonUtilities
{
    using System;
    using System.Xml;
    using System.Text;
    using System.Diagnostics.Contracts;

    public static class SerializeExtensions
    {
        private readonly static UTF8Encoding Encoder = new UTF8Encoding(false, false);
        private readonly static UTF8Encoding EncoderWithBom = new UTF8Encoding(true, false);

        /// <summary>
        /// To convert a Byte Array of Unicode values (UTF-8 encoded) to a complete String.
        /// </summary>
        /// <param name="characters">Unicode Byte Array to be converted to String</param>
        /// <returns>String converted from Unicode Byte Array</returns>
        public static string Utf8BytesToString(this byte[] characters, bool useBOM = false)
        {
            UTF8Encoding temp = useBOM ? EncoderWithBom : Encoder;
            String constructedString = null;
            // Aparentemente, el método toString avienta un caracter inválido si no
            // se le quitan los 3 primeros bytes identificadores (preamble) del formato utf-8 en el
            // contenido del array de bytes.
            if (characters[0] == 0xEF && characters[1] == 0xBB && characters[2] == 0xBF)
            {
                constructedString = temp.GetString(characters, 3, characters.Length - 3);
            }
            else
            {
                constructedString = temp.GetString(characters);
            }

            return constructedString;
        }

        /// <summary>
        /// Converts the String to UTF8 Byte array and is used in De serialization
        /// </summary>
        /// <param name="pXmlString"></param>
        /// <returns></returns>
        public static byte[] ToUtf8ByteArray(this string pXmlString, bool useBOM = false)
        {
            UTF8Encoding temp = useBOM ? EncoderWithBom : Encoder;
            byte[] byteArray = temp.GetBytes(pXmlString);
            return byteArray;
        }
    }
}
