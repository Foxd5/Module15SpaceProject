using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MinimalShooting.ControllerPackage
{
  
    public class KeyboardController : MonoBehaviour
    {   
        private Vector3 _inputVector;
        public Vector3 InputVector
        {
            get
            {
                return _inputVector;
            }
        }


        void Update()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            // Update _inputVector for 2D movement (X and Y axes)
            this._inputVector = new Vector3(-v, h, 0.0f);  // Changed Z to Y for 2D from the original code
        }
    }
}
