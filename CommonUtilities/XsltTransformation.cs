// ----------------------------------------------------------------------
// <copyright file="XsltTransformation.cs" company="Route Manager de México">
//     Copyright Route Manager de México(c) 2011. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
namespace CommonUtilities.XML
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Xsl;
    using System.Xml;

    public class XsltTransformation
    {
        /// <summary>
        /// Cached Compiled Transform (for efficiency)
        /// </summary>
        private XslCompiledTransform cachedCompiledTransform = null;

        private XsltArgumentList extensionObjects = null;

        /// <summary>
        /// Loads the XSL compiled from xsltc content
        /// </summary>
        /// <param name="XsltContent">The xsltcontent.</param>
        /// <returns></returns>
        public static XslCompiledTransform LoadXslCompiled(string XsltContent)
        {
            XslCompiledTransform xslttran = null;
            using (TextReader xsltTxtReader = new StringReader(XsltContent))
            {
                var xsltXmlReader = XmlReader.Create(xsltTxtReader);
                XsltSettings xslt_settings = new XsltSettings 
                { 
                    EnableScript = true,
                    EnableDocumentFunction = true 
                };

                xslttran = new XslCompiledTransform(true);
                xslttran.Load(xsltXmlReader, xslt_settings, new XmlUrlResolver());
            }
            return xslttran;
        }

        public static string XSLTTransform(XslCompiledTransform XslCompiledTransform, string XmlContent)
        {
            return XSLTTransform(XslCompiledTransform, XmlContent, null);
        }

        /// <summary>
        /// Transform an Xml valid string, using the desired XslCompiledTransform object
        /// </summary>
        /// <param name="XslCompiledTransform">The XSLCompiledTransform.</param>
        /// <param name="XmlContent">The valid Xml Content.</param>
        /// <returns></returns>
        public static string XSLTTransform(XslCompiledTransform XslCompiledTransform, string XmlContent, XsltArgumentList args)
        {
            string result = "";
            using (TextReader xmlTxtReader = new StringReader(XmlContent))
            {
                XmlReader xmlXmlReader = XmlReader.Create(xmlTxtReader);
                StringBuilder sb = new StringBuilder();
                using (TextWriter xmlWriter = new StringWriter(sb))
                {
                    XmlWriter outXmlWriter = XmlWriter.Create(xmlWriter);
                    if (args != null)
                    {
                        XslCompiledTransform.Transform(xmlXmlReader, args, outXmlWriter);
                    }
                    else
                    {
                        XslCompiledTransform.Transform(xmlXmlReader, outXmlWriter);
                    }
                }
                result = sb.ToString();
            }
            return result;
        }

        /// <summary>
        /// XSLTs the transform.
        /// </summary>
        /// <param name="XmlContent">The xmlcontent.</param>
        /// <param name="XsltContent">The xsltcontent.</param>
        /// <returns></returns>
        public static string XSLTTransform(string XmlContent, string XsltContent)
        {
            return XSLTTransform(XmlContent, XsltContent, null);
        }

        /// <summary>
        /// Transforms the XmlContent using the Xslt Transform in XsltContent,
        /// with the XsltArgumentList 
        /// </summary>
        /// <param name="XmlContent">The xmlcontent.</param>
        /// <param name="XsltContent">The xsltcontent.</param>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        public static string XSLTTransform(string XmlContent, string XsltContent, XsltArgumentList args)
        {
            XslCompiledTransform xslTran = LoadXslCompiled(XsltContent);
            return XSLTTransform(xslTran, XmlContent, args);
        }

        /// <summary>
        /// Caches the compiled XSLT.
        /// </summary>
        /// <param name="XsltContent">The xsltcontent.</param>
        public void CacheCompiledXslt(string XsltContent)
        {
            var xslTran = LoadXslCompiled(XsltContent);
            cachedCompiledTransform = xslTran;
        }

        /// <summary>
        /// Transform the XmlContent using the XsltTransform loaded from XsltContent
        /// </summary>
        /// <param name="XmlContent">The xmlcontent.</param>
        /// <param name="XsltContent">The xsltcontent.</param>
        /// <param name="cacheCompiled">if set to <c>true</c> [cache compiled].</param>
        /// <returns></returns>
        public string XSLTTransform(string XmlContent, string XsltContent, bool cacheCompiled)
        {
            string result = null;
            XslCompiledTransform xslTran = LoadXslCompiled(XsltContent);
            if (cacheCompiled)
            {
                cachedCompiledTransform = xslTran;
            }
            result = XSLTTransform(xslTran, XmlContent, this.extensionObjects);
            return result;
        }

        /// <summary>
        /// Transforms the valid Xml Content using the cache'd compiled transform, if there
        /// is nothing in cache, then the result will be null.
        /// </summary>
        /// <param name="XmlContent">The xmlcontent.</param>
        /// <returns></returns>
        public string XSLTTransform(string XmlContent)
        {
            return XSLTTransform(cachedCompiledTransform, XmlContent, this.extensionObjects);
        }

        /// <summary>
        /// Adds the extension object.
        /// </summary>
        /// <param name="objnamespace">The objnamespace.</param>
        /// <param name="extobj">The extobj.</param>
        public void AddExtensionObject(string objnamespace, object extobj)
        {
            if (this.extensionObjects == null) 
            { 
                lock (this) 
                {
                    if (this.extensionObjects == null)
                    {
                        this.extensionObjects = new XsltArgumentList();
                    }
                }
            }

            this.extensionObjects.AddExtensionObject(objnamespace, extobj);
        }
    }
}