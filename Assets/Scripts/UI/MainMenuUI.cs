using Assets.Scripts.Utilities.Events;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button play;
        [SerializeField] private Button info;
        [SerializeField] private Button exit;
        [SerializeField] private Button close;
        [SerializeField] private GameObject infoPanel;

        private UISevice uISevice;

        public void SetService(UISevice uIService)
        {
            this.uISevice = uIService;
        }

        private void Awake()
        {
            play.onClick.AddListener(StartMatch);
            info.onClick.AddListener(Info);
            exit.onClick.AddListener(Exit);
            close.onClick.AddListener(InfoClose);
        }

        private void StartMatch()
        {
            uISevice.eventService.OnGameStart.Invoke();
            gameObject.SetActive(false);
        }
        private void Info()
        {
            infoPanel.SetActive(true);
        }
        private void InfoClose()
        {
            infoPanel.SetActive(false);
        }

        private void Exit()
        {
            Application.Quit();
        }

        private void OnDestroy()
        {
            play.onClick.RemoveListener(StartMatch);
            exit.onClick.RemoveListener(Exit);
        }

    }
}