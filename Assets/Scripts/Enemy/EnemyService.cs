using System.Collections;
using Assets.Scripts.Player;
using Assets.Scripts.Utilities;
using Assets.Scripts.Utilities.Events;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyService
    {
        private EnemyController enemy;
        private EventService eventService;
        private Transform playerTransform;
        private CoroutineRunner coroutineRunner;

        public EnemyService(EventService eventService, Transform playerTransform, EnemyController enemy)
        {
            this.eventService = eventService;
            this.playerTransform = playerTransform;
            this.enemy = enemy;
            enemy.SetService(eventService, playerTransform);
            coroutineRunner = CoroutineRunner.Instance;
            AddEventListeners();
        }

        private void AddEventListeners()
        {
            eventService.OnStartRound.AddListener(OnStartRound);
            eventService.OnNextRound.AddListener(OnNextRound);
        }

        private void OnStartRound(int lvl)
        {
            coroutineRunner.StartCoroutine(EnablePlayer());
        }
        private void OnNextRound(int win)
        {
            enemy.isActive = false;
        }

        private IEnumerator EnablePlayer()
        {
            enemy.OnRoundStart();
            yield return new WaitForSeconds(2);
            enemy.isActive = true;
        }
    }
}