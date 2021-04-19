using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.GameScripts
{
    public interface ISwipeableObject
    {
        void Swipe(float x, float y);
        void SetSpeed(float speed);
        GameObject SwipeableObject { get; }
    }
}
