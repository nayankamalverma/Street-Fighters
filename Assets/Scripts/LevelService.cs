using Assets.Scripts.Utilities.Events;
using UnityEngine;

namespace Assets.Scripts
{
	public class LevelService
	{
		private short _level;
		private short player1 = 0;
		private short player2 = 0;
		
		private EventService eventService;
		public LevelService(EventService eventService) 
		{
			this.eventService = eventService;
			AddEventListeners();
		}

		private void AddEventListeners(){
			eventService.OnGameStart.AddListener(OnGameStart);
			eventService.OnNextRound.AddListener(OnNextRound);
		}
		private void OnGameStart() { 
			_level = 1;
			eventService.OnStartRound.Invoke(_level);
		}
		private void OnNextRound(int winner) { 
			_level++;
			if (winner == 1) player1++;
			else if(winner ==2)player2++;
			if(player1 ==2 || player2 ==2) eventService.OnGameOver.Invoke(player1==2?1:2); //input 1 if player 1 win and 2 for player 2
			eventService.OnStartRound.Invoke(_level);
        }
	}
}