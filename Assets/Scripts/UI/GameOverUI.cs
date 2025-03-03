using UnityEngine;

namespace Assets.Scripts.UI
{
    public class GameOverUI : MonoBehaviour
    {
        private UISevice uISevice;

        public void SetService(UISevice uIService)
        {
            this.uISevice = uIService;
            AddEventListeners();
        }
        private void AddEventListeners()
        {
        }
    }
}