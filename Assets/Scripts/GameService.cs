using Assets.Scripts.Utilities.Events;
using UnityEngine;

public class GameService : MonoBehaviour
{

    private EventService EventService;

    private void Awake()
    {
        EventService = new EventService();

    }
}
