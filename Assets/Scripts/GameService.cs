using Assets.Scripts.Player;
using Assets.Scripts.Utilities.Events;
using UnityEngine;

public class GameService : MonoBehaviour
{

    #region Refrences
    [SerializeField] PlayerView playerView;
    [SerializeField] Transform enemy;
    #endregion

    private EventService EventService;
    private PlayerService PlayerService;

    private void Awake()
    {
        EventService = new EventService();
        PlayerService = new PlayerService(EventService,playerView,enemy);

    }
}
