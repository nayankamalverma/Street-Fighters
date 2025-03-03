using Assets.Scripts.Utilities;
using Assets.Scripts.Utilities.Events;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerService
    {
        private PlayerController playerController;
        private PlayerView playerView;
        private EventService eventService;
        private CoroutineRunner coroutineRunner;

        public PlayerService(EventService eventService,PlayerView playerView, Transform enemy)
        {
            this.eventService = eventService;
            this.playerView = playerView;
            playerController = new PlayerController(eventService, playerView, enemy);
            coroutineRunner = CoroutineRunner.Instance;
            AddEventListeners();
        }

        private void AddEventListeners()
        {
            eventService.OnStartRound.AddListener(OnStartRound);
            eventService.OnNextRound.AddListener(OnNextRound);
        }

        private void OnStartRound(int lvl) {
            coroutineRunner.StartCoroutine(EnablePlayer());
        }
        private void OnNextRound(int win) {
            playerController.isActive =false;
        }

        private IEnumerator EnablePlayer() {
            playerController.OnRoundStart();
            yield return new WaitForSeconds(2);
            playerController.isActive = true;
        }
    }
}
