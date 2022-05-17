using UnityEngine;

namespace ProjectBoost
{
    public class Oscillator : MonoBehaviour
    {
        private Vector3 startingPosition;
        [SerializeField] private Vector3 movementVector;
        private float movementFactor;
        [SerializeField] private float period = 2f;
    
        // Start is called before the first frame update
        private void Start()
        {
            startingPosition = transform.position;
        }

        // Update is called once per frame
        private void Update()
        {
            if (period <= Mathf.Epsilon) { return; }
            var cycles = Time.time / period;  // continually growing over time
        
            const float tau = Mathf.PI * 2;  // constant value of 6.283
            var rawSinWave = Mathf.Sin(cycles * tau);  // going from -1 to 1

            movementFactor = (rawSinWave + 1f) / 2f;   // recalculated to go from 0 to 1 so its cleaner
        
            var offset = movementVector * movementFactor;
            transform.position = startingPosition + offset;
        }
    }
}
