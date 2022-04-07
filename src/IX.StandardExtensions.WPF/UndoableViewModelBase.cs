// <copyright file="UndoableViewModelBase.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.ComponentModel;
using IX.Undoable;
using JetBrains.Annotations;

namespace IX.StandardExtensions.WPF;

/// <summary>
///     A base class for undo-able view models.
/// </summary>
[PublicAPI]
public abstract class UndoableViewModelBase : EditableItemBase,
    IEditCommittableItem
{
#region Constructors and destructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="UndoableViewModelBase" /> class.
    /// </summary>
    protected UndoableViewModelBase()
        : base(50) { }

    /// <summary>
    ///     Initializes a new instance of the <see cref="UndoableViewModelBase" /> class.
    /// </summary>
    /// <param name="limit">The limit of the undo/redo context.</param>
    protected UndoableViewModelBase(int limit)
        : base(limit) { }

#endregion

#region Properties and indexers

    /// <summary>
    ///     Gets a value indicating whether this view model is in design mode.
    /// </summary>
    /// <value><see langword="true" /> if this view model is in design mode; otherwise, <see langword="false" />.</value>
    [Browsable(false)]
    public bool IsInDesignMode => DesignMode.IsInDesignMode;

#endregion
}