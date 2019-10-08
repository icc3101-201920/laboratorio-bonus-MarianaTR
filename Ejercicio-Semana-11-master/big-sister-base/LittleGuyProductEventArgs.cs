using System;
using System.Collections.Generic;
using System.Text;

namespace big_sister_base
{
    public class LittleGuyProductEventArgs : EventArgs
    {
        public Cart cart;

        public LittleGuyProductEventArgs(Cart cart)
        {
            this.cart = cart;
        }

    }
}
