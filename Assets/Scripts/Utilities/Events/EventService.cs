
namespace Assets.Scripts.Utilities.Events
{
	public class EventService
	{

		public EventController OnGameStart;
		public EventController<int> OnGameOver;
		public EventController<int> OnNextRound; // roundWinner
		public EventController<int> OnStartRound; // roundWinner

		public EventController<int> OnPlayer1Hit;
		public EventController<int> OnPlayer2Hit;


		public EventService()
		{
			OnGameStart = new EventController();
			OnGameOver = new EventController<int>();
			OnNextRound = new EventController<int>();
			OnStartRound = new EventController<int>();

			OnPlayer1Hit = new EventController<int>();
			OnPlayer2Hit = new EventController<int>();
		}
	}
}