using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class DialogueManager : MonoBehaviour
{
    private XmlDocument xmlDialogues;
    public List<Phrase> GetDialogue(NPC npc)
    {
        var dialogues = xmlDialogues.DocumentElement;
        foreach (XmlNode hero in dialogues)
            if (int.Parse(hero.Attributes.GetNamedItem("id").Value) == npc.NPCId)
                return FindRequiredDialogue(npc, hero);
        return null;
    }

    private List<Phrase> FindRequiredDialogue(NPC npc, XmlNode hero)
    {
        foreach (XmlNode dialogue in hero)
            if (int.Parse(hero.Attributes.GetNamedItem("id").Value) == npc.DialogueId)
                return GetPhrase(dialogue);
        return null;
    }

    private List<Phrase> GetPhrase(XmlNode dialogue)
    {
        var phrases = new List<Phrase>();
        foreach (XmlNode phrase in dialogue)
            phrases.Add(new Phrase(phrase.Attributes.GetNamedItem("name").Value, phrase.Value));
        return phrases;
    }

    private void Start()
    {
        xmlDialogues = new XmlDocument();
        xmlDialogues.Load("Dialogues.xml");
    }
}
