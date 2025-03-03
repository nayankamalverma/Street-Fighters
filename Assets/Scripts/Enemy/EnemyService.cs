using Assets.Scripts.Utilities.Events;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyService
    {
        private EnemyController enemy;
        private EventService eventService;
        private Transform playerTransform;

        public EnemyService(EventService eventService, Transform playerTransform, EnemyController enemy)
        {
            this.eventService = eventService;
            this.playerTransform = playerTransform;
            this.enemy = enemy;
            enemy.SetService(eventService, playerTransform);
        }
    }
}