using UnityEngine;

namespace ProjectBoost.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _mainThrust = 1000f;
        [SerializeField] private float _rotationThrust = 200f;
        
        [SerializeField] private ParticleSystem mainEngineParticles;
        [SerializeField] private ParticleSystem leftThrusterParticles;
        [SerializeField] private ParticleSystem rightThrusterParticles;

        [SerializeField] private AudioClip mainEngine;
        private Rigidbody _rigidbody;
        private AudioSource audioSource;
        
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();
        }
        
        private void Update()
        {
            HandleRotation();
            HandleThrust();
        }

        private void HandleRotation()
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                RotateLeft();

            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                RotateRight();
            
            else
                StopRotating();
        }

        private void HandleThrust()
        {
            if (Input.GetKey(KeyCode.Space))
                StartThrusting();

            else
                StopThrusting();
        }

        private void StartThrusting()
        {
            _rigidbody.AddRelativeForce(Vector3.up * (_mainThrust * Time.deltaTime));
            
            if (!audioSource.isPlaying) 
                audioSource.PlayOneShot(mainEngine);

            if (!mainEngineParticles.isPlaying) 
                mainEngineParticles.Play();
        }

        private void StopThrusting()
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }

        private void RotateLeft()
        {
            ApplyRotation(_rotationThrust);
            
            if (!rightThrusterParticles.isPlaying)
            {
                rightThrusterParticles.Play();
            }
        }

        private void RotateRight()
        {
            ApplyRotation(-_rotationThrust);
            
            if (!leftThrusterParticles.isPlaying)
            {
                leftThrusterParticles.Play();
            }
        }

        private void StopRotating()
        {
            rightThrusterParticles.Stop();
            leftThrusterParticles.Stop();
        }

        private void ApplyRotation(float currentRotation)
        {
            _rigidbody.freezeRotation = true;  
            
            transform.Rotate(Vector3.forward * (currentRotation * Time.deltaTime));
            
            _rigidbody.freezeRotation = false;
        }
    }
}