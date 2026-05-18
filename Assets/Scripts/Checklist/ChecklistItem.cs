using UnityEngine;

[CreateAssetMenu(fileName = "ChecklistItem", menuName = "Checklist/Checklist", order = 0)]
public class ChecklistItem : ScriptableObject
{
    public string ingredientName;
    public bool isCheckedOff;
}
