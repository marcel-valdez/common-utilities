namespace CommonUtilities
{
    using System.IO;
    using System.IO.Compression;

    public static class CompressorExtensions
    {
        /// <summary>
        /// Compresses the specified uncompressed string, and gives the result.
        /// </summary>
        /// <param name="uncompressedstring">The uncompressed string.</param>
        /// <returns>A byte array with the compressed content</returns>
        public static byte[] Compress(this string uncompressedstring)
        {
            return uncompressedstring.ToUtf8ByteArray().Compress();
        }

        /// <summary>
        /// Compresses the specified uncompressed bytes and gives the result.
        /// </summary>
        /// <param name="uncompressedbytes">The uncompressed bytes.</param>
        /// <returns>A byte array with the compressed content</returns>
        public static byte[] Compress(this byte[] uncompressedbytes)
        {
            byte[] compressed = null;

            using (MemoryStream output = new MemoryStream())
            {
                using (GZipStream compressor = new GZipStream(output, CompressionMode.Compress))
                {
                    using (MemoryStream input = new MemoryStream(uncompressedbytes, true))
                    {
                        input.WriteTo(compressor);
                    }
                }
                compressed = output.ToArray();
            }

            return compressed;
        }


        /// <summary>
        /// Decompresses the specified compressed String and gives the result
        /// </summary>
        /// <param name="compressedstring">The compressed String.</param>
        /// <returns>A byte array with the decompressed content</returns>
        public static byte[] Decompress(this string compressedstring)
        {
            return compressedstring.ToUtf8ByteArray().Decompress();
        }

        /// <summary>
        /// Decompresses the specified compressed bytes.
        /// </summary>
        /// <param name="compressed">The compressed.</param>
        /// <returns>A byte array with the decompressed content</returns>
        public static byte[] Decompress(this byte[] compressedbytes)
        {
            byte[] decompressed = null;

            using (MemoryStream writer = new MemoryStream(compressedbytes, true))
            {
                using (GZipStream input = new GZipStream(writer, CompressionMode.Decompress))
                {
                    using (MemoryStream output = new MemoryStream())
                    {
                        int currentValue = input.ReadByte();
                        if (currentValue == -1)
                        {
                            ///Marcel Valdez:
                            ///Cuando no se termina correctamente el GZipStream, el primer valor es un -1
                            ///indicando que no se terminó de leer, para esto se intentará recuperar los 
                            ///datos del GZipStream con la información recibida.
                            currentValue = input.ReadByte();
                        }

                        while (currentValue != -1)
                        {
                            output.WriteByte((byte)(currentValue & 0xFF));
                            currentValue = input.ReadByte();
                        }

                        decompressed = output.ToArray();
                    }
                }
            }

            return decompressed;
        }

    }
}
