using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Feathers
{
    public class JumpableFeather : MonoBehaviour
    {
        [SerializeField]private bool featherTest;
        [SerializeField]Vector2 spotPosition;

        public bool FeatherTest { get => featherTest; set => featherTest = value; }

        public void OnJumpable(Vector2 spotLocation, bool isFeatherActivate)
        {
            spotPosition = spotLocation;
            FeatherTest = isFeatherActivate;
        }
        private void Update()
        {
            if (FeatherTest)
            {
                Debug.Log("Colidiu");
                Vector3.MoveTowards(transform.position, spotPosition, 0.5f * Time.deltaTime);
            }

        }
    }
}