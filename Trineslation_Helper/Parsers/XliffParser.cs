using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Trineslation_Helper.Parsers
{
    internal static class Constants
    {
        #region XLIFF CONSTANTS
        public const string XliffVersion = "1.2";
        public const string XliffNamespace = "urn:oasis:names:tc:xliff:document:1.2";
        public const string XliffDatatype = "htmlbody";
        #endregion

        #region FILE DIALOGS
        public const string FileDialogDefaultExt = ".xliff";
        public const string FileDialogFilter = "XLIFF documents (*.xlf;*.xliff)|*.xlf;*.xliff";
        #endregion

        #region XML NODES & ATTRIBUTES
        public const string XmlNodeRoot = "xliff";
        public const string XmlNodeFile = "file";
        public const string XmlNodeBody = "body";
        public const string XmlNodeTranslationUnit = "trans-unit";
        public const string XmlNodeSource = "source";
        public const string XmlNodeTarget = "target";

        public const string XmlAttributeVersion = "version";
        public const string XmlAttributeNamespace = "xmlns";
        public const string XmlAttributeFileIdentifier = "original";
        public const string XmlAttributeSourceLanguage = "source-language";
        public const string XmlAttributeDatatype = "datatype";
        public const string XmlAttributeIdentifier = "id";
        #endregion
    }

    public class TranslationUnit
    {
        public string Identifier { get; set; }
        public string Source { get; set; }
        public string Target { get; set; }
    }

    public class XliffParser
    {
        private const string NamespacePrefix = "ns";

        private XmlNamespaceManager XmlNamespaceManager { get; set; }
        private XmlDocument XmlDocument { get; } = new XmlDocument();
        private string SourceLanguage { get; }
        private string FileNameIdentifier { get; }

        public XliffParser() { }

        public XliffParser(string sourceLanguage, string fileName)
        {
            SourceLanguage = sourceLanguage;
            FileNameIdentifier = fileName;
        }

        public List<TranslationUnit> GetTranslationUnitsFromFile(string filePath)
        {
            string text;
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                text = streamReader.ReadToEnd();
            }

            XmlDocument.LoadXml(text);

            XmlNamespaceManager = new XmlNamespaceManager(XmlDocument.NameTable);
            XmlNamespaceManager.AddNamespace(NamespacePrefix, GetNamespace());
            return GetTranslationUnits();
        }

        private string GetNamespace()
        {
            return XmlDocument.DocumentElement?.NamespaceURI;
        }

        private List<TranslationUnit> GetTranslationUnits()
        {
            XmlNodeList translationUnitNodes = XmlDocument.DocumentElement?.SelectNodes($"//{NamespacePrefix}:{Constants.XmlNodeTranslationUnit}", XmlNamespaceManager);

            List<TranslationUnit> translationUnits = new List<TranslationUnit>();

            if (translationUnitNodes != null)
                foreach (XmlNode translationUnitNode in translationUnitNodes)
                {
                    string identifier =
                        translationUnitNode.Attributes?.GetNamedItem(Constants.XmlAttributeIdentifier)?.Value ??
                        string.Empty;
                    string source =
                        EncodeAndCleanValue(translationUnitNode
                                                .SelectSingleNode($"{NamespacePrefix}:{Constants.XmlNodeSource}",
                                                    XmlNamespaceManager)?.InnerXml ?? string.Empty);
                    string target =
                        EncodeAndCleanValue(translationUnitNode
                                                .SelectSingleNode($"{NamespacePrefix}:{Constants.XmlNodeTarget}",
                                                    XmlNamespaceManager)?.InnerXml ?? string.Empty);

                    translationUnits.Add(new TranslationUnit()
                    {
                        Identifier = identifier,
                        Source = source,
                        Target = target
                    });
                }

            return translationUnits;
        }

        private string EncodeAndCleanValue(string str)
        {
            str = str.Replace(" xmlns=\"urn:oasis:names:tc:xliff:document:1.2\" ", string.Empty);

            return str;
        }

        public XmlDocument CreateXliffDocument(List<TranslationUnit> translationUnitsCollection)
        {
            XmlDocument xmlDocument = new XmlDocument();

            if (translationUnitsCollection.Count == 0)
            {
                translationUnitsCollection.Add(new TranslationUnit
                {
                    Identifier = "blank",
                    Source = ""
                });
            }
            return CreateXliffDocument(xmlDocument, translationUnitsCollection);
        }

        private XmlDocument CreateXliffDocument(XmlDocument xmlDocument, IEnumerable translationUnits)
        {
            XmlNode rootNode = xmlDocument.CreateElement(Constants.XmlNodeRoot);

            XmlAttribute versionAttribute = xmlDocument.CreateAttribute(Constants.XmlAttributeVersion);
            versionAttribute.Value = Constants.XliffVersion;
            rootNode.Attributes?.Append(versionAttribute);

            XmlAttribute namespaceAttribute = xmlDocument.CreateAttribute(Constants.XmlAttributeNamespace);
            namespaceAttribute.Value = Constants.XliffNamespace;
            rootNode.Attributes?.Append(namespaceAttribute);

            XmlNode fileNode = xmlDocument.CreateElement(Constants.XmlNodeFile);

            XmlAttribute fileIdentifierAttribute = xmlDocument.CreateAttribute(Constants.XmlAttributeFileIdentifier);
            fileIdentifierAttribute.Value = FileNameIdentifier;
            fileNode.Attributes?.Append(fileIdentifierAttribute);

            XmlAttribute datatypeAttribute = xmlDocument.CreateAttribute(Constants.XmlAttributeDatatype);
            datatypeAttribute.Value = Constants.XliffDatatype;
            fileNode.Attributes?.Append(datatypeAttribute);

            if (string.IsNullOrEmpty(SourceLanguage) == false)
            {
                XmlAttribute sourceLanguageAttribute = xmlDocument.CreateAttribute(Constants.XmlAttributeSourceLanguage);
                sourceLanguageAttribute.Value = SourceLanguage;
                fileNode.Attributes?.Append(sourceLanguageAttribute);
            }

            XmlNode bodyNode = xmlDocument.CreateElement(Constants.XmlNodeBody);

            foreach (TranslationUnit translationUnit in translationUnits)
            {
                if (string.IsNullOrEmpty(translationUnit.Identifier)) continue;

                XmlNode translationUnitNode = xmlDocument.CreateElement(Constants.XmlNodeTranslationUnit);

                XmlAttribute identifierAttribute = xmlDocument.CreateAttribute(Constants.XmlAttributeIdentifier);
                identifierAttribute.Value = translationUnit.Identifier;
                translationUnitNode.Attributes?.Append(identifierAttribute);

                XmlNode sourceNode = xmlDocument.CreateElement(Constants.XmlNodeSource);
                sourceNode.InnerText = translationUnit.Source;
                translationUnitNode.AppendChild(sourceNode);

                if (string.IsNullOrEmpty(translationUnit.Target) == false)
                {
                    XmlNode targetNode = xmlDocument.CreateElement(Constants.XmlNodeTarget);
                    targetNode.InnerText = translationUnit.Target;
                    translationUnitNode.AppendChild(targetNode);
                }

                bodyNode.AppendChild(translationUnitNode);
            }

            fileNode.AppendChild(bodyNode);
            rootNode.AppendChild(fileNode);
            xmlDocument.AppendChild(rootNode);

            return xmlDocument;
        }

        public static async void Save(string targetPath, XmlDocument document)
        {
            string targetFolder = Path.GetDirectoryName(targetPath);
            if (Directory.Exists(targetFolder) == false)
                Directory.CreateDirectory(targetFolder ?? throw new InvalidOperationException());

            StringWriter stringWriter = new StringWriter();
            document.Save(stringWriter);
            string documentString = stringWriter.ToString();
            using (StreamWriter streamWriter = new StreamWriter(targetPath, false, Encoding.Unicode))
            {
                await streamWriter.WriteAsync(documentString);
            }
        }
    }
}
