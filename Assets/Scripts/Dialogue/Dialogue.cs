using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue/Dialogue", order = 0)]
public class Dialogue : ScriptableObject {
    public string speaker;
    [TextArea]
    public string dialogueText;
    public Sprite farLeftImage;
    public Sprite centreLeftImage;
    public Sprite centreRightImage;
    public Sprite farRightImage;
    public DialoguePosition mainSpeaker;
}