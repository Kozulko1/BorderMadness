using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.GameScripts
{
    public interface IClickableObject
    {
        void Ability();
        void SetSpeed(float speed);
        GameObject PlayableObject { get; }
    }
}
