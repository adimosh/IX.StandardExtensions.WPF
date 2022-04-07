// <copyright file="DragDropExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Windows;
using JetBrains.Annotations;

namespace IX.StandardExtensions.WPF.Extensions;

/// <summary>
/// Extensions for various drag&amp;drop-related functionality.
/// </summary>
[PublicAPI]
public static class DragDropExtensions
{
    /// <summary>
    /// Gets an object of a given type from the drag&amp;drop data object.
    /// </summary>
    /// <typeparam name="T">The type of object to get.</typeparam>
    /// <param name="dataObject">The data object to get from.</param>
    /// <returns>An object of the desired type, if one was contained in the data object.</returns>
    /// <exception cref="InvalidCastException">The raw object cannot be converted to the desired type.</exception>
    public static T GetData<T>(this IDataObject dataObject)
    {
        object rawObject = dataObject.GetData(typeof(T));

        if (rawObject is not T convertedObject)
        {
            throw new InvalidCastException();
        }

        return convertedObject;
    }

    /// <summary>
    /// Gets an object of a given type from the drag&amp;drop data object.
    /// </summary>
    /// <typeparam name="T">The type of object to get.</typeparam>
    /// <param name="dataObject">The data object to get from.</param>
    /// <param name="format">The string format representation.</param>
    /// <returns>An object of the desired type, if one was contained in the data object.</returns>
    /// <exception cref="InvalidCastException">The raw object cannot be converted to the desired type.</exception>
    public static T GetData<T>(this IDataObject dataObject, string format)
    {
        object rawObject = dataObject.GetData(format);

        if (rawObject is not T convertedObject)
        {
            throw new InvalidCastException();
        }

        return convertedObject;
    }

    /// <summary>
    /// Gets an object of a given type from the drag&amp;drop data object.
    /// </summary>
    /// <typeparam name="T">The type of object to get.</typeparam>
    /// <param name="dataObject">The data object to get from.</param>
    /// <param name="format">The string format representation.</param>
    /// <param name="autoConvert">Automatically convert to the desired format.</param>
    /// <returns>An object of the desired type, if one was contained in the data object.</returns>
    /// <exception cref="InvalidCastException">The raw object cannot be converted to the desired type.</exception>
    public static T GetData<T>(this IDataObject dataObject, string format, bool autoConvert)
    {
        object rawObject = dataObject.GetData(format, autoConvert);

        if (rawObject is not T convertedObject)
        {
            throw new InvalidCastException();
        }

        return convertedObject;
    }
}