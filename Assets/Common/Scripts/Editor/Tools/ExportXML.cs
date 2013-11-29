using System;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;
using UnityEditor;

class ExportXML : EditorWindow
{
    private static string _xml;

    [MenuItem("Sirius/Export XML")]
    public static void Execute()
    {
        Type[] types = typeof(Sirius).Assembly.GetTypes();

        for(int i = 0; i < types.Length; i++)
        {
            _AppendConfigXML(types[i]);
        }

        Debug.Log(_xml);
    }

    public static void _AppendConfigXML(Type type)
    {
        FieldInfo info = type.GetField("config");

        if(info != null)
        {
            XmlSerializer xml = new XmlSerializer(info.FieldType);

            StringWriter writer = new StringWriter();
            xml.Serialize(writer, info.GetValue(null));
            string text = writer.ToString();

            _xml += "@" + type.Name + "\r\n" + text + "\r\n\r\n";
        }
    }
}