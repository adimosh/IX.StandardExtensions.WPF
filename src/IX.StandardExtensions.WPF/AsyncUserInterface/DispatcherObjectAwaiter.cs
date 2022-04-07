// <copyright file="DispatcherObjectAwaiter.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using IX.StandardExtensions.Contracts;
using JetBrains.Annotations;

namespace IX.StandardExtensions.WPF.AsyncUserInterface;

/// <summary>
///     An awaiter based on the dispatcher.
/// </summary>
/// <seealso cref="INotifyCompletion" />
[PublicAPI]
public class DispatcherObjectAwaiter : INotifyCompletion
{
#region Internal state

    private readonly DispatcherObject sourceObject;

#endregion

#region Constructors and destructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="DispatcherObjectAwaiter" /> class.
    /// </summary>
    /// <param name="sourceObject">The source object.</param>
    public DispatcherObjectAwaiter(DispatcherObject sourceObject)
    {
        Requires.NotNull(out this.sourceObject, sourceObject);
    }

#endregion

#region Properties and indexers

    /// <summary>
    ///     Gets a value indicating whether this <see cref="DispatcherObjectAwaiter" /> is is completed.
    /// </summary>
    /// <value>
    ///     <c>true</c> if is completed; otherwise, <c>false</c>.
    /// </value>
    [Obsolete("This is a typo, please use IsCompleted.")]
    [SuppressMessage(
        "ReSharper",
        "IdentifierTypo",
        Justification = "Obsoleted and created fixed overload.")]
    [SuppressMessage(
        "CodeQuality",
        "IDE0079:Remove unnecessary suppression",
        Justification = "ReSharper is used in this project.")]
    public bool Iscompleted => this.IsCompleted;

    /// <summary>
    ///     Gets a value indicating whether this <see cref="DispatcherObjectAwaiter" /> is is completed.
    /// </summary>
    /// <value>
    ///     <c>true</c> if is completed; otherwise, <c>false</c>.
    /// </value>
    public bool IsCompleted => this.sourceObject.CheckAccess();

#endregion

#region Methods

#region Interface implementations

    /// <summary>Schedules the continuation action that's invoked when the instance completes.</summary>
    /// <param name="continuation">The action to invoke when the operation completes.</param>
    /// <exception cref="ArgumentNullException">
    ///     The <paramref name="continuation" /> argument is null (Nothing in
    ///     Visual Basic).
    /// </exception>
    public void OnCompleted(Action continuation)
    {
        Requires.NotNull(continuation);

        if (this.sourceObject.Dispatcher?.CheckAccess() ?? true)
        {
            // We are either on the UI thread, or a dispatcher is not available at this time
            continuation();
        }
        else
        {
            // A dispatcher exists and we are not on the UI thread
            this.sourceObject.Dispatcher.Invoke(continuation);
        }
    }

#endregion

    /// <summary>
    ///     Gets the result.
    /// </summary>
    public void GetResult() { }

#endregion
}