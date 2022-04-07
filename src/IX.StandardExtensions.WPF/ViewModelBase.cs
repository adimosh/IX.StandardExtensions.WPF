// <copyright file="ViewModelBase.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using JetBrains.Annotations;

namespace IX.StandardExtensions.WPF;

/// <summary>
///     A base class for view models.
/// </summary>
/// <seealso cref="IX.StandardExtensions.ComponentModel.ViewModelBase" />
[PublicAPI]
public class ViewModelBase : ComponentModel.ViewModelBase
{
#region Constructors and destructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="ViewModelBase" /> class.
    /// </summary>
    protected ViewModelBase() { }

    /// <summary>
    ///     Initializes a new instance of the <see cref="ViewModelBase" /> class.
    /// </summary>
    /// <param name="synchronizationContext">The specific synchronization context to use.</param>
    protected ViewModelBase(SynchronizationContext synchronizationContext)
        : base(synchronizationContext) { }

#endregion

#region Properties and indexers

    /// <summary>
    ///     Gets a value indicating whether this view model is in design mode.
    /// </summary>
    /// <value><see langword="true" /> if this view model is in design mode; otherwise, <see langword="false" />.</value>
    [Browsable(false)]
    public bool IsInDesignMode => DesignMode.IsInDesignMode;

#endregion

#region Methods

    /// <summary>
    ///     Sets a value in a property's backing field, then raises the <see cref="INotifyPropertyChanged.PropertyChanged" />.
    /// </summary>
    /// <typeparam name="T">The type of the property's backing field, and the value to set.</typeparam>
    /// <param name="propertyName">Name of the property.</param>
    /// <param name="backingField">The backing field.</param>
    /// <param name="value">The value.</param>
    protected void SetPropertyValue<T>(
        string propertyName,
        ref T backingField,
        T value) =>
        this.SetPropertyValue(
            propertyName,
            ref backingField,
            value,
            EqualityComparer<T>.Default);

    /// <summary>
    ///     Sets a value in a property's backing field, then raises the <see cref="INotifyPropertyChanged.PropertyChanged" />.
    /// </summary>
    /// <typeparam name="T">The type of the property's backing field, and the value to set.</typeparam>
    /// <param name="propertyName">Name of the property.</param>
    /// <param name="backingField">The backing field.</param>
    /// <param name="value">The value.</param>
    /// <param name="equalityComparer">The equality comparer for type <typeparamref name="T" />.</param>
    protected void SetPropertyValue<T>(
        string propertyName,
        ref T backingField,
        T value,
        IEqualityComparer<T> equalityComparer)
    {
        if (equalityComparer.Equals(
                backingField,
                value))
        {
            return;
        }

        backingField = value;

        this.RaisePropertyChanged(propertyName);
    }

#endregion
}