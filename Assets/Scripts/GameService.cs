using Assets.Scripts.Enemy;
using Assets.Scripts.Player;
using Assets.Scripts.Utilities.Events;
using UnityEngine;

public class GameService : MonoBehaviour
{

    #region Refrences
    [SerializeField] PlayerView playerView;
    [SerializeField] EnemyController enemy;
    #endregion

    private EventService EventService;
    private PlayerService PlayerService;
    private EnemyService EnemyService;

    private void Awake()
    {
        EventService = new EventService();
        PlayerService = new PlayerService(EventService,playerView,enemy.transform);
        EnemyService = new EnemyService(EventService, playerView.transform, enemy);

    }
}
