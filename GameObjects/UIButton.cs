using System;
using Microsoft.Xna.Framework;

namespace UnstableDeck.GameObjects
{
    public class UIButton : GameObject
    {
        //
        public Func<bool> ClickHandler;
        private bool _isPressed;

        public void Pressed (Vector2 _clickPos)
        {
            if(InClickBounds(_clickPos) && !_isPressed){ 
                _isPressed=true;
                ClickHandler();
            }
        }

        public void Released (Vector2 _clickPos)
        {
            if(InClickBounds(_clickPos) && _isPressed){ 
                _isPressed=false;
            }
        }

        private bool InClickBounds (Vector2 _clickPos)
        {
            var isInBounds = false;
            if(BoundingRectangle.Contains(_clickPos)) isInBounds=true;
            return isInBounds;
        }
    }
}   