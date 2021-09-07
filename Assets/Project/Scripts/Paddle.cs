using UnityEngine;

namespace Project.Scripts
{
    public class Paddle : MonoBehaviour
    {
        [Range(1, 2)]
        public int playerNumber = 1;
        public float speed = 1f;

        private string _axisName;
        
        // Start is called before the first frame update
        void Start()
        {
            _axisName = $"Vertical{playerNumber}";
        }

        // Update is called once per frame
        void Update()
        {
            var verticalMovement = Input.GetAxis(_axisName);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, verticalMovement * speed);
        }
    }
}
