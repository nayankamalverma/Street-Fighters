using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class GamePlayUI : MonoBehaviour
    {
        [SerializeField] 
        private Image player1Health;
        [SerializeField]
        private Image player2Health;

        private UISevice uISevice;
        private int level = 0;
        public float p1Healt = 1f;
        public float p2Healt = 1f;

        public void SetService(UISevice uIService)
        {
            this.uISevice = uIService;
            AddEventListeners();
        }
        private void AddEventListeners()
        {
            uISevice.eventService.OnPlayer1Hit.AddListener(UpdatePlayer1Health);
            uISevice.eventService.OnPlayer2Hit.AddListener(UpdatePlayer2Health);
        }

        public void OnStartRound(){
            level++;
            p1Healt =1; p2Healt =1;
            UpdateFillValue();
        }

        private void UpdateFillValue()
        {
            player1Health.fillAmount = p1Healt;
            player2Health.fillAmount = p2Healt;
        }

        private void UpdatePlayer1Health(int dmg){
             p1Healt -= dmg/100.0f;
            UpdateFillValue();
        }
        private void UpdatePlayer2Health(int dmg){
             p2Healt -= dmg/100;
            UpdateFillValue();
        }

        private void OnDestroy()
        {
            uISevice.eventService.OnPlayer1Hit.RemoveListener(UpdatePlayer1Health);
            uISevice.eventService.OnPlayer2Hit.RemoveListener(UpdatePlayer2Health);
        }

    }
}