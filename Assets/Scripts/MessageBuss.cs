using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStarDemo

{
    public static class MessageBuss
    {
        public static class Input
        {
            public static Func<bool> GetTouch = () => false;
            public static Func<bool> GetTouchDown = () => false;

            public static Func<Vector2> GetTouchPosition = () => Vector2.zero;

            public static Action OnObstacklesPlacementFinished = () => { };
            public static Action OnStartPlaced = () => { };
            public static Action OnFinishPlaced = () => { };
        }

        public static class Screen
        {
            public static Func<float> GetWorldWidth = () => 0;
            public static Func<float> GetWorldHeight = () => 0;
            public static Func<float> GetMinDimension = () => 0;
        }


    }
}


