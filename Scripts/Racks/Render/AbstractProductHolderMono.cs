using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RackScene
{
    public abstract class AbstractProductHolderMono : MonoBehaviour, IRender
    {
        //properties
        public abstract HolderType holderType { get; }

        //fileds
        public BoxCollider2D Borders;

        public abstract ProductHolder productHolderData { get; set; }
        public abstract void RenderDefault();

        public abstract void Clear();

        public abstract void SetAsMain();
    }

    public enum HolderType
    {
        Pin,
        Rack
    }
}
