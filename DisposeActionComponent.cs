// <copyright file="DisposeActionComponent.cs" company="Lewis Moten">
// Copyright (c) Lewis Moten. All rights reserved.
// </copyright>

namespace LewisMoten.Sentencesmith
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Perform an anonymouse action when the component is disposed.
    /// </summary>
    internal class DisposeActionComponent : Component
    {
        /// <summary>
        /// The action to perform when this component is disposed.
        /// </summary>
        private Action<bool> dispose;

        /// <summary>
        /// Initializes a new instance of the <see cref="DisposeActionComponent"/> class.
        /// </summary>
        /// <param name="dispose">The dispose.</param>
        internal DisposeActionComponent(Action<bool> dispose)
        {
            this.dispose = dispose;
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Component"/> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (this.dispose != null)
            {
                this.dispose(disposing);
            }
        }
    }
}
