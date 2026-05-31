using UnityEngine;

public class MonsterPlant : MonoBehaviour 
{
    public static MonsterPlant Instance { get; private set; }

    public Animator animator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        animator = GetComponent<Animator>();
    }
}
