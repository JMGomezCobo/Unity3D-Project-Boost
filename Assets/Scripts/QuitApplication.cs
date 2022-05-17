using UnityEngine;

namespace ProjectBoost
{
    public class QuitApplication : MonoBehaviour
    {
        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;
            
            Application.Quit();
        }
    }
}