// <copyright file="UnderscoreEscapeConverter.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Globalization;
using JetBrains.Annotations;

namespace IX.StandardExtensions.WPF.ValueConverters;

/// <summary>
/// A value converter that escapes underscores, for use with menu or menu item titles.
/// </summary>
[PublicAPI]
public class UnderscoreEscapeConverter : ValueConverterBase
{
    /// <summary>
    ///     Converts a value.
    /// </summary>
    /// <param name="value">The value produced by the binding source.</param>
    /// <param name="targetType">The type of the binding target property. This parameter is ignored.</param>
    /// <param name="parameter">The converter parameter to use. This parameter is ignored.</param>
    /// <param name="culture">The culture to use in the converter. This parameter is ignored.</param>
    /// <returns>
    ///     A converted value. If the method returns <see langword="null" />, the valid <see langword="null" /> value is
    ///     used.
    /// </returns>
    public override object Convert(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture)
    {
        if (value == null)
        {
            return null;
        }

        if (value is not string s)
        {
            s = value.ToString();
        }

        return s.Replace(
            "_",
            "__");
    }
}