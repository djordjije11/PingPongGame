using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerScript : MonoBehaviour
    {
        public float movementSpeed;
        public Rigidbody2D playerRigid;
        protected readonly string X_AXIS_STRING;
        protected readonly string Y_AXIS_STRING;
        protected Vector3 playerInitialPosition;
        protected PlayerScript(string x_axis_string, string y_axis_string)
        {
            X_AXIS_STRING = x_axis_string;
            Y_AXIS_STRING = y_axis_string;
        }

        protected void Start()
        {
            playerRigid = GetComponent<Rigidbody2D>();
            playerInitialPosition = transform.position;
        }
        protected void FixedUpdate()
        {
            var horizontal = Input.GetAxisRaw(X_AXIS_STRING);
            var vertical = Input.GetAxisRaw(Y_AXIS_STRING);
            playerRigid.velocity = new Vector2(horizontal, vertical) * movementSpeed;
        }
        public void RestartPlayerPosition()
        {
            transform.position = playerInitialPosition;
        }
    }
}
