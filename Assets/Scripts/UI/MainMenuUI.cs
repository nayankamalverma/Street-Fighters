using Assets.Scripts.Utilities.Events;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button play;
        [SerializeField] private Button exit;

        private UISevice uISevice;

        public void SetService(UISevice uIService){
            this.uISevice = uIService;
        }

        private void Awake()
        {
            play.onClick.AddListener(StartMatch);
            exit.onClick.AddListener(Exit);
        }

        private void StartMatch(){
            uISevice.eventService.OnGameStart.Invoke();
            gameObject.SetActive(false);
        }
        
        private void Exit(){
            Application.Quit();
        }

        private void OnDestroy()
        {
            play.onClick.RemoveListener(StartMatch);
            exit.onClick.RemoveListener(Exit);
        }

    }
}