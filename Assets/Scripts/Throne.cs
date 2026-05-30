using UnityEngine;

public class Throne : MonoBehaviour
{
    public Transform seatPosition;

    public void SitOnThrone()
    {
        Player.Instance.transform.position = seatPosition.position;
        Controller.Instance.enabled = false;
        PlayerVisual.Instance.animator.SetBool("Sitting", true);
        

        MonsterPlant.Instance.animator.SetBool("Eating", true);
    }
}
