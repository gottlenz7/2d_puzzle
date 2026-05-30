using TMPro;
using UnityEngine;

public class Note : MonoBehaviour
{
    public SpriteRenderer note;
    public TextMeshProUGUI text;
    public bool isOpen;

    private void Awake()
    {
        note.enabled = false;
        text.enabled = false;
        isOpen = false;
    }

    public void ShowNote()
    {
        note.enabled = true;
        text.enabled = true;
        isOpen = true;
    }
    public void HideNote()
    {
        note.enabled = false;
        text.enabled = false;
        isOpen = false;
    }
}
