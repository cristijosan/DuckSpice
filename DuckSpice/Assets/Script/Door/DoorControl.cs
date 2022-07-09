using System;
using System.Collections;
using UnityEngine;

namespace Script.Door
{
    [Serializable]
    public enum OpenDoor
    {
        Close,
        Open
    }
        
    public class DoorControl : MonoBehaviour
    {
        private OpenDoor currentState;
        private float openPosition = -70f;

        private void Start()
        {
            currentState = OpenDoor.Close;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.CompareTag("Player"))
                return;

            Debug.Log("Open / Close");

            if (currentState == OpenDoor.Close)
            {
                currentState = OpenDoor.Open;
                StartCoroutine(Open());
            }
            else
            {
                currentState = OpenDoor.Open;
                StartCoroutine(Close());
            }
        }

        private IEnumerator Open()
        {
            var finalRotation = Quaternion.Euler(0, openPosition, 0) * transform.rotation;
            
            do
            {
                yield return new WaitForSeconds (0.01f);
                transform.Rotate(Vector3.up, openPosition * Time.deltaTime / 1f);
            } while (transform.rotation != finalRotation);
        }

        private IEnumerator Close()
        {
            var finalRotation = Quaternion.Euler(0, 0, 0) * transform.rotation;
            
            do
            {
                yield return new WaitForSeconds (0.01f);
                transform.Rotate(Vector3.up, 0 * Time.deltaTime / 1f);
            } while (transform.rotation != finalRotation);
        }
    }
}
