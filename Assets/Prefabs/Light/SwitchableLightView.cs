using Unity.Netcode;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

namespace Divij
{
	public class SwitchableLightView : MonoBehaviour
	{
		[SerializeField] private Light light;

		[SerializeField] private SwitchableLightModel model;

		private void OnEnable()
		{
			if (model != null)
			{
				model.isPowered.OnValueChanged += OnStateChanged;
				model.isSwitchedOn.OnValueChanged += OnStateChanged;

				UpdateLight();
			}
		}

		private void OnDisable()
		{
			if (model != null)
			{
				model.isPowered.OnValueChanged -= OnStateChanged;
				model.isSwitchedOn.OnValueChanged -= OnStateChanged;
			}
		}

		private void OnStateChanged(bool oldValue, bool newValue)
		{
			UpdateLight();
		}

		private void UpdateLight()
		{
			if (light == null)
			{
				Debug.LogWarning("Light needs to be assigned");
				return;
			}

			if (model.isPowered.Value && model.isSwitchedOn.Value)
			{
				light.enabled = true;
			}
			else
			{
				light.enabled = false;
			}
		}
	}
}
