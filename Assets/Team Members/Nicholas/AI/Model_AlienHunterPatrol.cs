using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Nicholas.AI
{
    public class Model_AlienHunterPatrol : MonoBehaviour
    {
        [SerializeField] private string patrolPointName = "PatrolPoint";

        [SerializeField] private float navMeshSampleDistance = 2.0f;

        private List<Light> patrolLights = new List<Light>();

        public Transform CurrentPatrolTarget { get; private set; }

        private int previousPatrolIndex = -1;

        private void Awake()
        {
            FindPatrolLights();
        }

        private void FindPatrolLights()
        {
            patrolLights.Clear();

            Light[] lights = FindObjectsByType<Light>(FindObjectsSortMode.None);

            for (int i = 0; i < lights.Length; i++)
            {
                if (lights[i] != null)
                {
                    patrolLights.Add(lights[i]);
                }
            }

            Debug.Log("Alien found " + patrolLights.Count + " patrol Lights.");
        }

        public Transform SelectRandomPatrolTarget()
        {
            if (patrolLights.Count == 0)
            {
                Debug.LogWarning("No Lights were found for Alien patrol.");

                return null;
            }

            int selectedIndex;

            if (patrolLights.Count == 1)
            {
                selectedIndex = 0;
            }
            else
            {
                do
                {
                    selectedIndex = Random.Range(0, patrolLights.Count);
                } while (selectedIndex == previousPatrolIndex);
            }

            previousPatrolIndex = selectedIndex;

            Light selectedLight = patrolLights[selectedIndex];

            Transform patrolPoint = GetPatrolPoint(selectedLight);

            if (patrolPoint == null)
            {
                return null;
            }

            CurrentPatrolTarget = patrolPoint;

            Debug.Log("Alien selected patrol Light: " + selectedLight.name);

            return CurrentPatrolTarget;
        }

        private Transform GetPatrolPoint(Light selectedLight)
        {
            Transform patrolPoint = selectedLight.transform.Find(patrolPointName);

            if (patrolPoint == null)
            {
                Debug.LogWarning(selectedLight.name + " has no PatrolPoint child.");

                return null;
            }

            NavMeshHit hit;

            bool foundNavMesh = NavMesh.SamplePosition(
                patrolPoint.position, out hit, navMeshSampleDistance, NavMesh.AllAreas);

            if (!foundNavMesh)
            {
                Debug.LogWarning("No valid NavMesh position found near " + selectedLight.name);

                return null;
            }

            return patrolPoint;
        }

        public void ClearPatrolTarget()
        {
            CurrentPatrolTarget = null;
        }
    }
}