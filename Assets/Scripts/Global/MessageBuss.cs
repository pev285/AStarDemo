using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStarDemo.Global
{
    public static class MessageBuss
    {
        public static class Global
        {
            public static Action<SystemState> SystemStateChanged = (a) => { };
        }

        public static class Input
        {
            public static Func<bool> GetTouch = () => false;
            public static Func<bool> GetTouchDown = () => false;

            public static Func<Vector2> GetTouchPosition = () => Vector2.zero;

            public static Action NextStage = () => { };
            public static Action StartOver = () => { };
        }

        public static class Screen
        {
            public static Func<float> GetWorldWidth = () => 0;
            public static Func<float> GetWorldHeight = () => 0;
            public static Func<float> GetMinDimension = () => 0;
        }

        public static class Map
        {
            public static Action<Vector2Int> StartPositionChosen = (a) => { };
            public static Action<Vector2Int> DestinationPositionChosen = (a) => { };
        }
    }
}
