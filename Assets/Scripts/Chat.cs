using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Chat
{
    public string quest = "";
    public string answer1 = "";
    public string answer2 = "";
    public Chat branch1
    {
        get
        {
            if (chatList == null || chatList.Count == 0) return null;
            return chatList[0];
        }
    }

    public Chat branch2
    {
        get
        {
            if (chatList == null || chatList.Count == 0) return null;
            return chatList[1];
        }
    }
    public List<Chat> chatList;
}
