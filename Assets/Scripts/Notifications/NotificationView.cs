using System.Collections;
using TMPro;
using UnityEngine;

namespace SoulsLike
{
    public class NotificationView : MonoBehaviour
    {
        public float fadeOutTime = 2.0f;

        public TextMeshProUGUI notification;

        private void Start()
        {
            StartCoroutine(NotificationCheck());
        }

        IEnumerator NotificationCheck()
        {
            while(true)
            {
                yield return new WaitForSeconds(fadeOutTime / 4);
                if (Notification.notifications.Count > 0)
                {
                    string message = Notification.notifications.Dequeue();
                    notification.SetText(message);
                    notification.CrossFadeAlpha(1, fadeOutTime, false);
                    yield return new WaitForSeconds(fadeOutTime + 0.1f);
                    notification.CrossFadeAlpha(0, fadeOutTime, false);
                    yield return new WaitForSeconds(fadeOutTime + 0.1f);
                }
            }
        }
    }
}
