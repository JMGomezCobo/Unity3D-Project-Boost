using ProjectBoost.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectBoost
{
    public class CollisionHandler : MonoBehaviour
    {
        [SerializeField] private float levelLoadDelay = 2f;
        [SerializeField] private AudioClip success;
        [SerializeField] private AudioClip crash;

        [SerializeField] private ParticleSystem successParticles;
        [SerializeField] private ParticleSystem crashParticles;

        private AudioSource audioSource;

        private bool isTransitioning = false;
        private bool collisionDisabled = false;

        private void Start() 
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void Update() 
        {
            RespondToDebugKeys();    
        }

        private void RespondToDebugKeys()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                LoadNextLevel();
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                collisionDisabled = !collisionDisabled;  // toggle collision
            } 
        }

        private void OnCollisionEnter(Collision other) 
        {
            if (isTransitioning || collisionDisabled) { return; }
        
            switch (other.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("This thing is friendly");
                    break;
                case "Finish":
                    StartSuccessSequence();
                    break;
                default:
                    StartCrashSequence();
                    break;
            }
        }

        private void StartSuccessSequence()
        {
            isTransitioning = true;
            audioSource.Stop();
            audioSource.PlayOneShot(success);
            successParticles.Play();
            GetComponent<PlayerController>().enabled = false;
            Invoke("LoadNextLevel", levelLoadDelay);
        }

        private void StartCrashSequence()
        {
            isTransitioning = true;
            audioSource.Stop();
            audioSource.PlayOneShot(crash);
            crashParticles.Play();
            GetComponent<PlayerController>().enabled = false;
            Invoke("ReloadLevel", levelLoadDelay);
        }

        private void LoadNextLevel()
        {
            var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            var nextSceneIndex = currentSceneIndex + 1;
            
            if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 0;
            }
            SceneManager.LoadScene(nextSceneIndex);
        }

        private void ReloadLevel()
        {
            var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }

    }
}
