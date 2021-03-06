//-----------------------------------------------------------------------
// <copyright file="TrackableListApi.cs" company="Google">
//
// Copyright 2017 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------

namespace GoogleARCoreInternal
{
    using System;
    using System.Collections.Generic;
    using GoogleARCore;
    using UnityEngine;

#if UNITY_IOS && !UNITY_EDITOR
    using AndroidImport = GoogleARCoreInternal.DllImportNoop;
    using IOSImport = System.Runtime.InteropServices.DllImportAttribute;
#else
    using AndroidImport = System.Runtime.InteropServices.DllImportAttribute;
    using IOSImport = GoogleARCoreInternal.DllImportNoop;
#endif

    internal class TrackableListApi
    {
        private NativeSession m_NativeSession;

        public TrackableListApi(NativeSession nativeSession)
        {
            m_NativeSession = nativeSession;
        }

        public IntPtr Create()
        {
            IntPtr handle = IntPtr.Zero;
            ExternApi.ArTrackableList_create(m_NativeSession.SessionHandle, ref handle);
            return handle;
        }

        public void Destroy(IntPtr listHandle)
        {
            ExternApi.ArTrackableList_destroy(listHandle);
        }

        public int GetCount(IntPtr listHandle)
        {
            int count = 0;
            ExternApi.ArTrackableList_getSize(m_NativeSession.SessionHandle, listHandle, ref count);
            return count;
        }

        public IntPtr AcquireItem(IntPtr listHandle, int index)
        {
            IntPtr trackableHandle = IntPtr.Zero;
            ExternApi.ArTrackableList_acquireItem(
                m_NativeSession.SessionHandle, listHandle, index, ref trackableHandle);
            return trackableHandle;
        }

        private struct ExternApi
        {
#pragma warning disable 626
            [AndroidImport(ApiConstants.ARCoreNativeApi)]
            public static extern void ArTrackableList_create(
                IntPtr sessionHandle, ref IntPtr trackableListHandle);

            [AndroidImport(ApiConstants.ARCoreNativeApi)]
            public static extern void ArTrackableList_destroy(IntPtr trackableListHandle);

            [AndroidImport(ApiConstants.ARCoreNativeApi)]
            public static extern void ArTrackableList_getSize(
                IntPtr sessionHandle, IntPtr trackableListHandle, ref int outSize);

            [AndroidImport(ApiConstants.ARCoreNativeApi)]
            public static extern void ArTrackableList_acquireItem(
                IntPtr sessionHandle, IntPtr trackableListHandle, int index,
                ref IntPtr outTrackable);
#pragma warning restore 626
        }
    }
}
