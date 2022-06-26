using System;
namespace LukaszRzasaW66078
{
    public class Ceil
    {
        protected bool _isShown = false;
        protected char _sign = ' ';

        public Ceil(char sign) {
            this._sign = sign;
        }

        public bool IsShown
        {
            get => this._isShown;
            set { this._isShown = value; }
        }

        public char Sign(bool forceShow)
        {
            return forceShow || this._isShown ? this._sign : ' ';
        }

    }
}
