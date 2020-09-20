﻿using System;
using Microsoft.JSInterop;

namespace MatBlazor
{
    public class MatSliderJsHelper
    {
        public MatSliderJsHelper()
        {
        }

        public event EventHandler<decimal> OnChangeEvent;

        [JSInvokable]
        public void OnChangeHandler(decimal value)
        {
            OnChangeEvent?.Invoke(this, value);
        }
    }
}