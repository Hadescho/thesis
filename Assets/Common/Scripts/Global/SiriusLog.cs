using System.Collections.Generic;
using UnityEngine;

class SiriusLog : MonoBehaviour
{
    exSpriteFont _console;

    void Awake()
    {
        _console = GameObject.Find("Console").GetComponent<exSpriteFont>();
        _console.text = "";

        DontDestroyOnLoad(_console.gameObject);

        Application.RegisterLogCallback(_Callback);
    }

    void _Callback(string text, string trace, LogType type)
    {
        string temp = _console.text;

        while(temp.Length > 1024)
        {
            temp = temp.Remove(0, temp.IndexOf('\n') + 1);
        }

        temp += text + "\n";

        if(type == LogType.Exception)
        {
            temp += " ** TRACE ** " + trace + "\n";
        }

        _console.text = temp;
    }
}