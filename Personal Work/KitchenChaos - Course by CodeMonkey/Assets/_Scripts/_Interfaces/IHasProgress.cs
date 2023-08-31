using System;
using UnityEngine;

namespace NikolayTabalyov
{
    public interface IHasProgress {
        
        public event EventHandler<OnCuttingProgressChangedEventArgs> OnCuttingProgressChanged;
        public class OnCuttingProgressChangedEventArgs : EventArgs {
            public float cuttingProgressNormalized;
        }
        #region Methods
        
        #endregion
        
    }
}