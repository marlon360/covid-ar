//-----------------------------------------------------------------------
// <copyright originalFile="AndyPlacementManipulator.cs" company="Google">
// <renamed file="ARPlacementInteractable.cs">
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


using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.Interaction.Toolkit.AR;

/// <summary>
/// Controls the placement of Andy objects via a tap gesture.
/// </summary>
public class SimpleARPlacementInteractable : ARBaseGestureInteractable
{
    [SerializeField]
    [Tooltip("A GameObject to place when a raycast from a user touch hits a plane.")]
    GameObject m_PlacementPrefab;
    /// <summary>
    /// A GameObject to place when a raycast from a user touch hits a plane.
    /// </summary>
    public GameObject placementPrefab { get { return m_PlacementPrefab; } set { m_PlacementPrefab = value; } }

    [SerializeField, Tooltip("Called when the this interactable places a new GameObject in the world.")]
    UnityEvent m_OnObjectPlaced = new UnityEvent();
    /// <summary>Gets or sets the event that is called when the this interactable places a new GameObject in the world.</summary>
    public UnityEvent onObjectPlaced { get { return m_OnObjectPlaced; } set { m_OnObjectPlaced = value; } }

    private GameObject _spawnedObject;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    static GameObject s_TrackablesObject;

    /// <summary>
    /// Returns true if the manipulation can be started for the given gesture.
    /// </summary>
    /// <param name="gesture">The current gesture.</param>
    /// <returns>True if the manipulation can be started.</returns>
    protected override bool CanStartManipulationForGesture(TapGesture gesture)
    {
        // Allow for test planes
        if (gesture.TargetObject == null || gesture.TargetObject.layer == 9)
            return true;

        return false;
    }

    /// <summary>
    /// Function called when the manipulation is ended.
    /// </summary>
    /// <param name="gesture">The current gesture.</param>
    protected override void OnEndManipulation(TapGesture gesture)
    {
        if (_spawnedObject != null)
            return;

        if (gesture.WasCancelled)
            return;

        // If gesture is targeting an existing object we are done.
        // Allow for test planes
        if (gesture.TargetObject != null && gesture.TargetObject.layer != 9)
            return;

        // Raycast against the location the player touched to search for planes.
        if (GestureTransformationUtility.Raycast(gesture.StartPosition, s_Hits, TrackableType.PlaneWithinPolygon))
        {
            var hit = s_Hits[0];

            _spawnedObject = Instantiate(placementPrefab, hit.pose.position, hit.pose.rotation);

            _spawnedObject.transform.LookAt(new Vector3(
                Camera.main.transform.position.x,
                _spawnedObject.transform.position.y,
                Camera.main.transform.position.z
            ));

            if (m_OnObjectPlaced != null)
                m_OnObjectPlaced.Invoke();
        }
    }
}
