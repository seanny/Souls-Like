using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulsLike
{
    public static class Notification
    {
        public static Queue<string> notifications = new Queue<string>();

        public static void Add(string message)
        {
            notifications.Enqueue(message);
        }
    }
}