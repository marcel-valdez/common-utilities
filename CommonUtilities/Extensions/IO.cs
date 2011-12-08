// ----------------------------------------------------------------------
// <copyright file="IO.cs" company="Route Manager de México">
//     Copyright Route Manager de México(c) 2011. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
namespace CommonUtilities
{
    using System.Diagnostics.Contracts;
    using System.IO;
    using System.Linq;
    using System.Text;

    public static class IO
    {
        /// <summary>
        /// Chops off the first bytes of a stream, changes the stream's Length and leaves the Position at the
        /// end of the stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="count">The count.</param>
        /// <returns>The number of bytes chopped off</returns>
        public static int ChopStreamStart(Stream stream, long count)
        {
            long oldlength = stream.Length;
            long newlength = oldlength - count;

            if (newlength <= 0)
            {
                newlength = 0;
            }
            else
            {
                byte[] data = new byte[newlength];
                stream.Position = count;
                newlength = stream.Read(data, 0, data.Length);
                stream.Position = 0;
                stream.Write(data, 0, data.Length);
            }
            stream.SetLength(newlength);

            return (int)(oldlength - newlength);
        }

        /// <summary>
        /// Reads the text file.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <returns>the content of the file</returns>
        public static string ReadTextFile(FileInfo info)
        {
            string content = null;
            if (info.Exists)
            {
                using (FileStream file = info.OpenRead())
                {
                    using (var reader = new StreamReader(file))
                    {
                        content = reader.ReadToEnd();
                    }
                }
            }

            return content;
        }

        /// <summary>
        /// Writes the text file. If the file does not exists, then it is created.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="content">The content.</param>
        /// <param name="truncate">if set to <c>true</c> [truncate].</param>
        /// <param name="encoding">The encoding.</param>
        public static void WriteTextFile(FileInfo file, string content, bool truncate = true, Encoding encoding = null)
        {
            if (!file.Exists)
            {
                file.Create().Close();
            }

            encoding = encoding ?? new UTF8Encoding(false, false);
            FileMode mode = truncate ? FileMode.Truncate : FileMode.Append;
            using (FileStream fileStream = file.Open(mode, FileAccess.Write))
            {
                byte[] contentBytes = encoding.GetBytes(content);
                fileStream.Write(contentBytes, 0, contentBytes.Length);
            }
        }

        /// <summary>
        /// Determines whether [is valid file path] [the specified xmlfilepath].
        /// </summary>
        /// <param name="xmlfilepath">The xmlfilepath.</param>
        /// <returns>
        ///   <c>true</c> if [is valid file path] [the specified xmlfilepath]; otherwise, <c>false</c>.
        /// </returns>
        [Pure]
        public static bool IsValidFilePath(string xmlfilepath)
        {
            Contract.Requires(!string.IsNullOrEmpty(xmlfilepath), "xmlfilepath is null or empty.");
            return xmlfilepath.ToCharArray().Intersect(Path.GetInvalidPathChars()).Count() == 0;
        }
    }
}
