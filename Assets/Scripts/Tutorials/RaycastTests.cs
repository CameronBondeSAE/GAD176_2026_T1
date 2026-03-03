using UnityEngine;

namespace CameronBonde
{
	public class RaycastTests : MonoBehaviour
	{
		// Update is called once per frame
		void Update()
		{
			RaycastHit hitInfo; // This is EMPTY. Raycast fills it in via the 'out' keyword
			bool raycastHitSomething = false;
			Vector3 currentPoint = transform.position; // Initial start in this GO
			Vector3 currentDirection = transform.forward; // Initial start in this GO
			int maxRays = 100;
			int currentRays = 0;

			// Physics.SphereCastAll()


			do
			{
				raycastHitSomething = Physics.Raycast(currentPoint, currentDirection, out hitInfo);
				currentPoint = hitInfo.point;
				currentDirection = Vector3.Reflect(currentDirection, hitInfo.normal);
				currentRays++;

				if (currentRays >= maxRays)
				{
					break;
				}
			} while (raycastHitSomething);
		}
	}
}