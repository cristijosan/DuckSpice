using UnityEngine;
using UnityEngine.UI;

namespace Script
{
	public class FPSDisplay : MonoBehaviour {
		public Text fpsText;
	
		private float pollingTime = 1f;
		private float time;
		private int frameCount;

 
		void Update() {
			// Update time.
			time += Time.deltaTime;

			// Count this frame.
			frameCount++;

			if (!(time >= pollingTime))
				return;
			
			// Update frame rate.
			var frameRate = Mathf.RoundToInt((float)frameCount / time);
			fpsText.text = frameRate.ToString() + " fps";

			// Reset time and frame count.
			time -= pollingTime;
			frameCount = 0;
		}
	}
}