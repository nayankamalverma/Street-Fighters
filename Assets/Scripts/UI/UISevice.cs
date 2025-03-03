using Assets.Scripts.Utilities.Events;
using UnityEngine;

namespace Assets.Scripts.UI
{
	public class UISevice : MonoBehaviour
	{
		[SerializeField]
		private MainMenuUI mainMenuUI;
		[SerializeField]
		private GamePlayUI gamePlayUI;
		[SerializeField]
		private GameOverUI gameOverUI;

		private int level = 0;
		public EventService eventService {  get; private set; }

		public void SetService(EventService eventService) 
		{
			this.eventService = eventService;
			mainMenuUI.SetService(this);
			gamePlayUI.SetService(this);
			AddEventListeners();
		}

        private void AddEventListeners()
        {
			eventService.OnGameStart.AddListener(OnGameStart);
			eventService.OnStartRound.AddListener(OnStartRound);
		}

		private void OnGameStart()
		{
			mainMenuUI.gameObject.SetActive(false);
			gamePlayUI.gameObject.SetActive(true);
		}

		private void OnStartRound(int level){
			this.level = level;
			gamePlayUI.OnStartRound();
		}

		private void OnDestroy()
        {
			eventService.OnGameStart.RemoveListener(OnGameStart);
        }
    }
}