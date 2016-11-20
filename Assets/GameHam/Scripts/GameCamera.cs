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
        
        public float minDistance;

        public float smoothTime = 0.3F;

        public Vector2 offset;

        private Vector3 targetPos;
        private Camera _cam;
        private Vector3 velocity = Vector3.zero;


        void Awake()
        {
            _cam = GetComponent<Camera>();
        }

        private void Update()
        {
            // get midpoint for all tracked objects
            Vector3 midpoint = Vector3.zero;
            int totalCount = 0;
            bool first = true;
            Vector3 minPoint = Vector3.zero;
            Vector3 maxPoint = Vector3.zero;
            foreach (var character in GameManager.instance.characterInstances)
            {
                if (!character.GetComponent<CharacterStats>().isAlive)
                    continue;

                if(first)
                {
                    first = false;
                    minPoint = character.transform.position;
                    maxPoint = minPoint;
                }
                else
                {
                    minPoint.x = Mathf.Min(minPoint.x, character.transform.position.x);
                    minPoint.y = Mathf.Min(minPoint.y, character.transform.position.y);
                    minPoint.z = Mathf.Min(minPoint.z, character.transform.position.z);

                    maxPoint.x = Mathf.Max(maxPoint.x, character.transform.position.x);
                    maxPoint.y = Mathf.Max(maxPoint.y, character.transform.position.y);
                    maxPoint.z = Mathf.Max(maxPoint.z, character.transform.position.z);
                }

                totalCount++;
            }
            if (totalCount < 1)
                return;

            midpoint = minPoint + maxPoint;
            midpoint /= 2;
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
            Vector3 cameraXY = Vector3.ProjectOnPlane(transform.position + new Vector3(offset.x, offset.y, 0.0f), Vector3.back);


            // get max distance from camera center on xy 2d plane
            foreach (var character in GameManager.instance.characterInstances)
            {
                if (!character.GetComponent<CharacterStats>().isAlive)
                    continue;

                var trfrm = character.transform;
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

            targetPos = transform.position;
            targetPos.z = -Mathf.Max(distanceX, distanceY, minDistance);


            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime) + new Vector3(offset.x, offset.y, 0.0f); 
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