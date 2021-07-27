// <copyright file="Unmanaged.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using Microsoft.Win32.SafeHandles;

namespace IX.StandardExtensions.WPF
{
    internal static class Unmanaged
    {
#region Methods

#region Static methods

        [DllImport("user32.dll")]
        internal static extern SafeIconHandle CreateIconIndirect(ref IconInfo icon);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetIconInfo(
            IntPtr hIcon,
            ref IconInfo pIconInfo);

        [DllImport("user32.dll")]
        private static extern bool DestroyIcon(IntPtr hIcon);

#endregion

#endregion

#region Nested types and delegates

        /// <summary>
        ///     The interop version of the ICONINFO struct.
        /// </summary>
        [UsedImplicitly]
        [SuppressMessage(
            "ReSharper",
            "InconsistentNaming",
            Justification = "These need to be named like this.")]
        [SuppressMessage(
            "CodeQuality",
            "IDE0079:Remove unnecessary suppression",
            Justification = "We're using ReSharper")]
        public struct IconInfo
        {
            /// <summary>
            ///     Specifies whether this structure defines an icon or a cursor.
            ///     A value of TRUE specifies an icon; FALSE specifies a cursor.
            /// </summary>
            [UsedImplicitly]
            public bool fIcon;

            /// <summary>
            ///     The x-coordinate of a cursor's hot spot.
            /// </summary>
            [UsedImplicitly]
            public int xHotspot;

            /// <summary>
            ///     The y-coordinate of a cursor's hot spot.
            /// </summary>
            [UsedImplicitly]
            public int yHotspot;

            /// <summary>
            ///     The icon bitmask bitmap.
            /// </summary>
            [UsedImplicitly]
            public IntPtr hbmMask;

            /// <summary>
            ///     A handle to the icon color bitmap.
            /// </summary>
            [UsedImplicitly]
            public IntPtr hbmColor;
        }

        [UsedImplicitly]
        internal class SafeIconHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
#region Constructors and destructors

            public SafeIconHandle()
                : base(true) { }

#endregion

#region Methods

            protected override bool ReleaseHandle() => DestroyIcon(this.handle);

#endregion
        }

#endregion
    }
}