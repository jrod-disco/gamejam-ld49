using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UnstableDeck.GameObjects
{
    public class UIButtonToggle : UIButton
    {
        //
        public bool isSelected { get; set; }
        public Texture2D ToggleTexture;
        
        public UIButtonToggle(bool _isSelected)
        {
            isSelected = _isSelected;    
        }
        
        public void Toggle ()
        {
            isSelected = !isSelected;
            Texture = isSelected ? DefaultTexture : ToggleTexture;
        }

    }
}