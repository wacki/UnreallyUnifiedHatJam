using UnityEngine;
using System.Collections;

namespace UU.GameHam
{
    /// <summary>
    /// Camera script that ensures all of the players are visible at all times
    /// </summary>
    public class GameCamera : MonoBehaviour
    {
        [Tooltip("Inner border, camera will try to keep players inside this range")]
        public Vector2 margin;

        public Transform[] tempTestTransforms;
        public float minDistance;

        private Camera _cam;

        void Awake()
        {
            _cam = GetComponent<Camera>();
        }

        private void Update()
        {
            // get midpoint for all tracked objects
            Vector3 midpoint = Vector3.zero;
            foreach (var trfrm in tempTestTransforms)
            {
                midpoint += trfrm.position;
            }

            midpoint /= tempTestTransforms.Length;
            var pos = transform.position;
            pos.x = midpoint.x;
            pos.y = midpoint.y;
            transform.position = pos;



            var vfov = _cam.fov;
            var radAngle = _cam.fieldOfView * Mathf.Deg2Rad;
            var radHFOV = 2 * Mathf.Atan(Mathf.Tan(radAngle / 2) * _cam.aspect);
            var hfov = Mathf.Rad2Deg * radHFOV;
            
            // assuming that our characters are on z=0
            float distanceTo2DPlane = Mathf.Abs(transform.position.z);

            // max character distance on the xy plane from the camera
            Vector2 maxDistance = Vector2.zero;

            // camera position on the xy plane
            Vector3 cameraXY = Vector3.ProjectOnPlane(transform.position, Vector3.back);

            // get max distance from camera center on xy 2d plane
            foreach(var trfrm in tempTestTransforms)
            {
                var distX = Mathf.Abs(cameraXY.x - trfrm.position.x);
                var distY = Mathf.Abs(cameraXY.y - trfrm.position.y);
                maxDistance.x = Mathf.Max(maxDistance.x, distX);
                maxDistance.y = Mathf.Max(maxDistance.y, distY);
            }

            // add the given margins to our results
            maxDistance += margin;
            
            //var 
            float distanceX = calcCameraDistance(maxDistance.x, hfov);
            float distanceY = calcCameraDistance(maxDistance.y, vfov);

            var oldPos = transform.position;
            oldPos.z = -Mathf.Max(distanceX, distanceY, minDistance);
            transform.position = oldPos;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="horizontalDistance">max distance of all units in given direction from camera center on xy plane</param>
        /// <param name="fov">full fov in given direction (horizontal or vertical)</param>
        /// <returns></returns>
        private float calcCameraDistance(float horizontalDistance, float fov)
        {
            return horizontalDistance / Mathf.Tan(Mathf.Deg2Rad * fov / 2.0f);
        }
    }

}