// <copyright file="EqualsConverter.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using IX.StandardExtensions.WPF.Mitigation;
using JetBrains.Annotations;

namespace IX.StandardExtensions.WPF.ValueConverters
{
    /// <summary>
    /// A converter that defines whether or not the value is equal to a specified comparison value, sent as parameter.
    /// </summary>
    [PublicAPI]
    public class EqualsConverter : ValueConverterBase
    {
        /// <summary>
        ///     Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property. This parameter is ignored.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter. This parameter is ignored.</param>
        /// <returns>
        ///     A converted value. If the method returns <see langword="null" />, the valid <see langword="null" /> value is
        ///     used.
        /// </returns>
        [SuppressMessage(
            "Performance",
            "HAA0601:Value type to reference type conversion causing boxing allocation",
            Justification = "Unavoidable in WPF converters")]
        public override object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            if (value == null)
            {
                return parameter == null;
            }

            if (parameter == null)
            {
                return false;
            }

            var valueType = value.GetType();
            if (valueType != parameter.GetType())
            {
                throw new InvalidOperationException("The value and the parameter must be of the same type, or null, or at least the parameter should be assignable to the value type, in order to compare.");
            }

            return EqualityComparer.ForType(valueType)
                .Equals(
                    value,
                    parameter);
        }
    }
}