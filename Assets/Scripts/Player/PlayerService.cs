using Assets.Scripts.Utilities.Events;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerService
    {
        private PlayerController playerController;
        private PlayerView playerView;
        private EventService eventService;

        public PlayerService(EventService eventService,PlayerView playerView, Transform enemy)
        {
            this.eventService = eventService;
            this.playerView = playerView;
            playerController = new PlayerController(eventService, playerView,enemy);
        }

    }
}
