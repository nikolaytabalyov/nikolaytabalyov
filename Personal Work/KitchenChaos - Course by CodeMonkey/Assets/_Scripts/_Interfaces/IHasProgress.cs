using System;
using UnityEngine;

namespace NikolayTabalyov
{
    public interface IHasProgress {
        
        public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
        public class OnProgressChangedEventArgs : EventArgs {
            public float progressNormalized;
        }
        #region Methods
        
        #endregion
        
    }
}