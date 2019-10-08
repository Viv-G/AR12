//-----------------------------------------------------------------------
// <copyright file="DetectedPlaneGenerator.cs" company="Google">
//
// Copyright 2018 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------

namespace GoogleARCore.Examples.Common
{
    using System.Collections.Generic;
    using GoogleARCore;
    using UnityEngine;

    /// <summary>
    /// Manages the visualization of detected planes in the scene.
    /// </summary>
    public class DetectedPlaneGenerator : MonoBehaviour
    {
        /// <summary>
        /// A prefab for tracking and visualizing detected planes.
        /// </summary>
        public GameObject DetectedPlanePrefab;

        /// <summary>
        /// A list to hold new planes ARCore began tracking in the current frame. This object is
        /// used across the application to avoid per-frame allocations.
        /// </summary>
        private List<DetectedPlane> m_NewPlanes = new List<DetectedPlane>();

        /// <summary>
        /// PLANE CENTER AND NORMAL OF PLANE
        /// 
        /// </summary>
        public static Vector3 PlaneCenterSend = new Vector3();
        public static Vector3 PlaneNormalSend = new Vector3();

        /// <summary>
        /// The Unity Update method.
        /// </summary>
        public void Update()
        {
            // Check that motion tracking is tracking.
            if (Session.Status != SessionStatus.Tracking)
            {
                return;
            }

            // Iterate over planes found in this frame and instantiate corresponding GameObjects to
            // visualize them.
            Session.GetTrackables<DetectedPlane>(m_NewPlanes, TrackableQueryFilter.New);
            for (int i = 0; i < m_NewPlanes.Count; i++)
            {
                GameObject planeObject =
                    Instantiate(DetectedPlanePrefab, Vector3.zero, Quaternion.identity, transform);

           /*         Vector3 m_PlaneCenter = m_NewPlanes[i].CenterPose.position;
                    Vector3 planeNormal = m_NewPlanes[i].CenterPose.rotation * Vector3.up;
                if (i == 0)
                {
                    PlaneCenterSend = m_PlaneCenter;
                    PlaneNormalSend = planeNormal;
                }

                
                 if(m_PlaneCenter.y < PlaneCenterSend.y)
                    {
                        PlaneCenterSend = m_PlaneCenter;
                        PlaneNormalSend = planeNormal;
                    double Aeq = PlaneNormalSend.x;
                    double Beq = PlaneNormalSend.y;
                    double Ceq = PlaneNormalSend.z;
                    double Deq = (PlaneNormalSend.x * - PlaneCenterSend.x) + (PlaneNormalSend.y * -PlaneCenterSend.y) + (PlaneNormalSend.z * -PlaneCenterSend.z);
                    
                 //   string message = "PLANE EQUATION IS: " + Aeq + " , " + Beq + " , " + Ceq + " , " + Deq + "\n";
                 //   Debug.Log(message);

                    }

                double Aeq = PlaneNormalSend.x;
                double Beq = PlaneNormalSend.y;
                double Ceq = PlaneNormalSend.z;
                double Deq = (PlaneNormalSend.x * -PlaneCenterSend.x) + (PlaneNormalSend.y * -PlaneCenterSend.y) + (PlaneNormalSend.z * -PlaneCenterSend.z);

                string message = "PLANE EQUATION IS: " + Aeq + " , " + Beq + " , " + Ceq + " , " + Deq + "\n";
                Debug.Log(message); */

                // Instantiate a plane visualization prefab and set it to track the new plane. The
                // transform is set to the origin with an identity rotation since the mesh for our
                // prefab is updated in Unity World coordinates.

                planeObject.GetComponent<DetectedPlaneVisualizer>().Initialize(m_NewPlanes[i]);
            }
        }
    }
}
