using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mail {
    public string sender;
    public string receiver;
    public string subject;
    public string date;
    [TextArea(1,7)]public string text;
}
