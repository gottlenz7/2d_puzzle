using System.Net.NetworkInformation;
using UnityEngine;

public class Throne : MonoBehaviour
{
    public Transform seatPosition, player;

    private PlayerView view;

    private void Awake()
    {
        view = FindObjectOfType<PlayerView>();
    }

    public void SitOnThrone()
    {
        view.transform.position = seatPosition.position;
        Controller.Instance.enabled = false;
        view.animator.SetBool("Sitting", true);

        MonsterPlant.Instance.animator.SetBool("Eating", true);
    }
}
