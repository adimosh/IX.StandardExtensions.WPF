// <copyright file="FrameworkElementExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Windows;
using JetBrains.Annotations;

namespace IX.StandardExtensions.WPF.Extensions;

/// <summary>
/// Extensions for the <see cref="FrameworkElement"/> class.
/// </summary>
[PublicAPI]
public static class FrameworkElementExtensions
{
    /// <summary>
    /// Searches for a specific resource within the given <see cref="FrameworkElement"/>.
    /// </summary>
    /// <typeparam name="T">The type of resource to search.</typeparam>
    /// <param name="frameworkElement">The framework element to search into.</param>
    /// <param name="resourceKey">The resource key.</param>
    /// <returns>The resource of the desired type, if such a resource exists.</returns>
    /// <exception cref="InvalidCastException">The searched resource is not of the desired type.</exception>
    public static T FindResource<T>(this FrameworkElement frameworkElement, object resourceKey)
    {
        var resource = frameworkElement.FindResource(resourceKey);

        if (resource is not T convertedResource)
        {
            throw new InvalidCastException();
        }

        return convertedResource;
    }

    /// <summary>
    /// Tries searching for a specific resource within the given <see cref="FrameworkElement"/>.
    /// </summary>
    /// <typeparam name="T">The type of resource to search.</typeparam>
    /// <param name="frameworkElement">The framework element to search into.</param>
    /// <param name="resourceKey">The resource key.</param>
    /// <returns>The resource of the desired type, if such a resource exists and can be cast to the desired type, otherwise <c>null</c> (<c>Nothing</c> in Visual Basic).</returns>
    public static T TryFindResource<T>(
        this FrameworkElement frameworkElement,
        object resourceKey) =>
        frameworkElement.FindResource(resourceKey) is not T convertedResource ? default : convertedResource;

}