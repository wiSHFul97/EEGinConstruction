// COPYRIGHT 1995-2021 ESRI
// TRADE SECRETS: ESRI PROPRIETARY AND CONFIDENTIAL
// Unpublished material - all rights reserved under the
// Copyright Laws of the United States and applicable international
// laws, treaties, and conventions.
//
// For additional information, contact:
// Environmental Systems Research Institute, Inc.
// Attn: Contracts and Legal Services Department
// 380 New York Street
// Redlands, California, 92373
// USA
//
// email: contracts@esri.com

using System.Runtime.InteropServices;
using System;

namespace Esri.GameEngine.Elevation.Base
{
    [StructLayout(LayoutKind.Sequential)]
    internal partial class ArcGISElevationSourceCollection
    {
        #region Constructors
        /// Creates a vector. This allocates memory that must be deleted.
        /// 
        /// - Since: 100.10.0
        internal ArcGISElevationSourceCollection()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            Handle = PInvoke.RT_ArcGISElevationSourceCollection_create(errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        #endregion // Constructors
        
        #region Properties
        /// Determines the number of values in the vector.
        /// 
        /// - Remark: The number of values in the vector. If an error occurs a 0 will be returned.
        /// - Since: 100.10.0
        internal ulong Size
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISElevationSourceCollection_getSize(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult.ToUInt64();
            }
        }
        #endregion // Properties
        
        #region Methods
        /// Add a value to the vector.
        /// 
        /// - Parameter value: The value that is to be added.
        /// - Returns: The position of the value. Max value of size_t if an error occurs.
        /// - Since: 100.10.0
        internal ulong Add(ArcGISElevationSource value)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localValue = value.Handle;
            
            var localResult = PInvoke.RT_ArcGISElevationSourceCollection_add(Handle, localValue, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            return localResult.ToUInt64();
        }
        
        /// Appends a vector to a vector.
        /// 
        /// - Parameter vector2: The value that is to be added.
        /// - Returns: The new size of vector_1. Max value of size_t if an error occurs.
        /// - Since: 100.10.0
        internal ulong AddArray(Unity.Collection<ArcGISElevationSource> vector2)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localVector2 = vector2.Handle;
            
            var localResult = PInvoke.RT_ArcGISElevationSourceCollection_addArray(Handle, localVector2, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            return localResult.ToUInt64();
        }
        
        /// Get a value at a specific position.
        /// 
        /// - Remark: Retrieves the value at the specified position.
        /// - Parameter position: The position which you want to get the value.
        /// - Returns: The value, Element, at the position requested.
        /// - Since: 100.10.0
        internal ArcGISElevationSource At(ulong position)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localPosition = new UIntPtr(position);
            
            var localResult = PInvoke.RT_ArcGISElevationSourceCollection_at(Handle, localPosition, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            ArcGISElevationSource localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new ArcGISElevationSource(localResult);
            }
            
            return localLocalResult;
        }
        
        /// Does the vector contain the given value.
        /// 
        /// - Remark: Does the vector contain a specific value.
        /// - Parameter value: The value you want to find.
        /// - Returns: True if the value is in the vector otherwise false.
        /// - Since: 100.10.0
        internal bool Contains(ArcGISElevationSource value)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localValue = value.Handle;
            
            var localResult = PInvoke.RT_ArcGISElevationSourceCollection_contains(Handle, localValue, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            return localResult;
        }
        
        /// Returns true if the two vectors are equal, false otherwise.
        /// 
        /// - Parameter vector2: The second vector.
        /// - Returns: Returns true if the two vectors are equal, false otherwise.
        /// - Since: 100.10.0
        internal bool Equals(Unity.Collection<ArcGISElevationSource> vector2)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localVector2 = vector2.Handle;
            
            var localResult = PInvoke.RT_ArcGISElevationSourceCollection_equals(Handle, localVector2, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            return localResult;
        }
        
        /// Get the first value in the vector.
        /// 
        /// - Returns: The value, Element, at the position requested.
        /// - Since: 100.10.0
        internal ArcGISElevationSource First()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_ArcGISElevationSourceCollection_first(Handle, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            ArcGISElevationSource localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new ArcGISElevationSource(localResult);
            }
            
            return localLocalResult;
        }
        
        /// Retrieves the position of the given value in the vector.
        /// 
        /// - Parameter value: The value you want to find.
        /// - Returns: The position of the value in the vector, Max value of size_t otherwise.
        /// - Since: 100.10.0
        internal ulong IndexOf(ArcGISElevationSource value)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localValue = value.Handle;
            
            var localResult = PInvoke.RT_ArcGISElevationSourceCollection_indexOf(Handle, localValue, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            return localResult.ToUInt64();
        }
        
        /// Insert a value at the specified position in the vector.
        /// 
        /// - Remark: Insert a value at a specified position to the vector.
        /// - Parameters:
        ///   - position: The position which you want to insert the value.
        ///   - value: The value that is to be added.
        /// - Since: 100.10.0
        internal void Insert(ulong position, ArcGISElevationSource value)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localPosition = new UIntPtr(position);
            var localValue = value.Handle;
            
            PInvoke.RT_ArcGISElevationSourceCollection_insert(Handle, localPosition, localValue, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Determines if there are any values in the vector.
        /// 
        /// - Remark: Check if the vector object has any values in it.
        /// - Returns: true if the  vector object contains no values otherwise false. Will return true if an error occurs.
        /// - Since: 100.10.0
        internal bool IsEmpty()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_ArcGISElevationSourceCollection_isEmpty(Handle, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            return localResult;
        }
        
        /// Get the last value in the vector.
        /// 
        /// - Returns: The value, Element, at the position requested.
        /// - Since: 100.10.0
        internal ArcGISElevationSource Last()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_ArcGISElevationSourceCollection_last(Handle, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            ArcGISElevationSource localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new ArcGISElevationSource(localResult);
            }
            
            return localLocalResult;
        }
        
        /// Move a value from the current position to a new position in the string vector.
        /// 
        /// - Remark: Move a value from the current position to a new position in the vector.
        /// - Parameters:
        ///   - oldPosition: The current position of the value.
        ///   - newPosition: The position which you want to move the value to.
        /// - Since: 100.10.0
        internal void Move(ulong oldPosition, ulong newPosition)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localOldPosition = new UIntPtr(oldPosition);
            var localNewPosition = new UIntPtr(newPosition);
            
            PInvoke.RT_ArcGISElevationSourceCollection_move(Handle, localOldPosition, localNewPosition, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Returns a value indicating a bad position within the vector.
        /// 
        /// - Returns: A size_t.
        /// - Since: 100.10.0
        internal static ulong Npos()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_ArcGISElevationSourceCollection_npos(errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            return localResult.ToUInt64();
        }
        
        /// Remove a value at a specific position in the vector.
        /// 
        /// - Parameter position: The position which you want to remove the value.
        /// - Since: 100.10.0
        internal void Remove(ulong position)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localPosition = new UIntPtr(position);
            
            PInvoke.RT_ArcGISElevationSourceCollection_remove(Handle, localPosition, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Remove all values from the vector.
        /// 
        /// - Since: 100.10.0
        internal void RemoveAll()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            PInvoke.RT_ArcGISElevationSourceCollection_removeAll(Handle, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        #endregion // Methods
        
        #region Internal Members
        internal ArcGISElevationSourceCollection(IntPtr handle) => Handle = handle;
        
        ~ArcGISElevationSourceCollection()
        {
            if (Handle != IntPtr.Zero)
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                PInvoke.RT_ArcGISElevationSourceCollection_destroy(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
            }
        }
        
        internal IntPtr Handle { get; set; }
        #endregion // Internal Members
    }
    
    internal static partial class PInvoke
    {
        #region P-Invoke Declarations
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISElevationSourceCollection_create(IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern UIntPtr RT_ArcGISElevationSourceCollection_getSize(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern UIntPtr RT_ArcGISElevationSourceCollection_add(IntPtr handle, IntPtr value, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern UIntPtr RT_ArcGISElevationSourceCollection_addArray(IntPtr handle, IntPtr vector2, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISElevationSourceCollection_at(IntPtr handle, UIntPtr position, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_ArcGISElevationSourceCollection_contains(IntPtr handle, IntPtr value, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_ArcGISElevationSourceCollection_equals(IntPtr handle, IntPtr vector2, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISElevationSourceCollection_first(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern UIntPtr RT_ArcGISElevationSourceCollection_indexOf(IntPtr handle, IntPtr value, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISElevationSourceCollection_insert(IntPtr handle, UIntPtr position, IntPtr value, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_ArcGISElevationSourceCollection_isEmpty(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISElevationSourceCollection_last(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISElevationSourceCollection_move(IntPtr handle, UIntPtr oldPosition, UIntPtr newPosition, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern UIntPtr RT_ArcGISElevationSourceCollection_npos(IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISElevationSourceCollection_remove(IntPtr handle, UIntPtr position, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISElevationSourceCollection_removeAll(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISElevationSourceCollection_destroy(IntPtr handle, IntPtr errorHandle);
        #endregion // P-Invoke Declarations
    }
}