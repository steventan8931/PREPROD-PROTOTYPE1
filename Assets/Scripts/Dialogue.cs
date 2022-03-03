using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string m_Name;

    [TextArea(3,10)]
    public string[] m_Sentences;
}
