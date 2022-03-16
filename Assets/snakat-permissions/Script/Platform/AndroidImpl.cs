#if !UNITY_EDITOR && UNITY_ANDROID
using UnityEngine;

namespace Snakat
{
    internal static class PlatformImpl
    {
        private const string UNITY_PLAYER_CLASS_NAME = "com.unity3d.player.UnityPlayer";

        private const string PERMISSIONS_PACKAGE_NAME = "com.snakat.permissions";
        private const string PERMISSIONS_CALLBACK_PACKAGE_NAME = PERMISSIONS_PACKAGE_NAME + ".callback";
        private const string PERMISSIONS_CLASS_NAME = PERMISSIONS_PACKAGE_NAME + ".Permissions";

#region Check for Permissions
        internal static bool HasPermissions (int[] permissions)
        {
            using (var unityPlayerClass = new AndroidJavaClass (UNITY_PLAYER_CLASS_NAME)) {
                using (var activity = unityPlayerClass.GetStatic<AndroidJavaObject> ("currentActivity")) {
                    using (var permissionsClass = new AndroidJavaClass (PERMISSIONS_CLASS_NAME)) {
                        return permissionsClass.CallStatic<bool> ("HasPermissions", activity, permissions);
                    }
                }
            }
        }

        internal static bool HasAlwaysDeniedPermission (int[] permissions)
        {
            using (var unityPlayerClass = new AndroidJavaClass (UNITY_PLAYER_CLASS_NAME)) {
                using (var activity = unityPlayerClass.GetStatic<AndroidJavaObject> ("currentActivity")) {
                    using (var permissionsClass = new AndroidJavaClass (PERMISSIONS_CLASS_NAME)) {
                        return permissionsClass.CallStatic<bool> ("HasAlwaysDeniedPermission", activity, permissions);
                    }
                }
            }
        }
#endregion


#region Request for Permissions
        private class RequestPermissionCallback : AndroidJavaProxy
        {
            private readonly IPermissionRequestInvoke _PermissionRequest;
            public RequestPermissionCallback (IPermissionRequestInvoke permissionRequest) : base (PERMISSIONS_CALLBACK_PACKAGE_NAME + ".RequestPermissionCallback")
            {
                _PermissionRequest = permissionRequest;
            }
            internal void OnGranted ()
            {
                Permissions.Instance.Enqueue (() =>
                {
                    _PermissionRequest.InvokeOnGranted ();
                });
            }
            internal void OnDenied (int[] permissions)
            {
                Permissions.Instance.Enqueue (() =>
                {
                    _PermissionRequest.InvokeOnDenied (permissions);
                });
            }
        }
        internal static void RequestPermissions (IPermissionRequestInvoke permissionRequest)
        {
            using (var unityPlayerClass = new AndroidJavaClass (UNITY_PLAYER_CLASS_NAME)) {
                using (var activity = unityPlayerClass.GetStatic<AndroidJavaObject> ("currentActivity")) {
                    using (var permissionsClass = new AndroidJavaClass (PERMISSIONS_CLASS_NAME)) {

                        var permissions = permissionRequest.Permissions;
                        var callback = new RequestPermissionCallback (permissionRequest);

                        permissionsClass.CallStatic ("RequestPermissions", activity, permissions, callback);
                    }
                }
            }
        }
#endregion


#region Open Setting
        private class OpenSettingCallback : AndroidJavaProxy
        {
            private readonly ISettingRequestInvoke _SettingRequest;
            public OpenSettingCallback (ISettingRequestInvoke settingRequest) : base (PERMISSIONS_CALLBACK_PACKAGE_NAME + ".OpenSettingCallback")
            {
                _SettingRequest = settingRequest;
            }
            internal void OnClosed ()
            {
                Permissions.Instance.Enqueue (() =>
                {
                    _SettingRequest.InvokeOnClosed ();
                });
            }
        }

        internal static void OpenSetting (ISettingRequestInvoke settingRequest)
        {
            using (var unityPlayerClass = new AndroidJavaClass (UNITY_PLAYER_CLASS_NAME)) {
                using (var activity = unityPlayerClass.GetStatic<AndroidJavaObject> ("currentActivity")) {
                    using (var permissionsClass = new AndroidJavaClass (PERMISSIONS_CLASS_NAME)) {

                        var callback = new OpenSettingCallback (settingRequest);

                        permissionsClass.CallStatic ("OpenSetting", activity, callback);
                    }
                }
            }
        }
#endregion


#region Notification
        private class RequestNotificationCallback : AndroidJavaProxy
        {
            private readonly INotificationRequestInvoke _NotificationRequest;
            public RequestNotificationCallback (INotificationRequestInvoke notificationRequest) : base (PERMISSIONS_CALLBACK_PACKAGE_NAME + ".RequestNotificationCallback")
            {
                _NotificationRequest = notificationRequest;
            }
            internal void OnGranted ()
            {
                Permissions.Instance.Enqueue (() =>
                {
                    _NotificationRequest.InvokeOnGranted ();
                });
            }
            internal void OnDenied ()
            {
                Permissions.Instance.Enqueue (() =>
                {
                    _NotificationRequest.InvokeOnDenied ();
                });
            }
        }

        internal static void RequestNotification (INotificationRequestInvoke notificationRequest)
        {
            using (var unityPlayerClass = new AndroidJavaClass (UNITY_PLAYER_CLASS_NAME)) {
                using (var activity = unityPlayerClass.GetStatic<AndroidJavaObject> ("currentActivity")) {
                    using (var permissionsClass = new AndroidJavaClass (PERMISSIONS_CLASS_NAME)) {

                        var callback = new RequestNotificationCallback (notificationRequest);

                        permissionsClass.CallStatic ("RequestNotification", activity, callback);
                    }
                }
            }
        }

        internal static void RequestNotificationListener (INotificationRequestInvoke notificationRequest)
        {
            using (var unityPlayerClass = new AndroidJavaClass (UNITY_PLAYER_CLASS_NAME)) {
                using (var activity = unityPlayerClass.GetStatic<AndroidJavaObject> ("currentActivity")) {
                    using (var permissionsClass = new AndroidJavaClass (PERMISSIONS_CLASS_NAME)) {

                        var callback = new RequestNotificationCallback (notificationRequest);

                        permissionsClass.CallStatic ("RequestNotificationListener", activity, callback);
                    }
                }
            }
        }
#endregion


#region Write Setting
        private class WriteSettingCallback : AndroidJavaProxy
        {
            private readonly IWriteRequestInvoke _WriteRequest;
            public WriteSettingCallback (IWriteRequestInvoke writeRequest) : base (PERMISSIONS_CALLBACK_PACKAGE_NAME + ".WriteSettingCallback")
            {
                _WriteRequest = writeRequest;
            }
            internal void OnGranted ()
            {
                Permissions.Instance.Enqueue (() =>
                {
                    _WriteRequest.InvokeOnGranted ();
                });
            }
            internal void OnDenied ()
            {
                Permissions.Instance.Enqueue (() =>
                {
                    _WriteRequest.InvokeOnDenied ();
                });
            }
        }

        internal static void RequestWriteSystemSetting (IWriteRequestInvoke writeRequest)
        {
            using (var unityPlayerClass = new AndroidJavaClass (UNITY_PLAYER_CLASS_NAME)) {
                using (var activity = unityPlayerClass.GetStatic<AndroidJavaObject> ("currentActivity")) {
                    using (var permissionsClass = new AndroidJavaClass (PERMISSIONS_CLASS_NAME)) {

                        var callback = new WriteSettingCallback (writeRequest);

                        permissionsClass.CallStatic ("RequestWriteSystemSetting", activity, callback);
                    }
                }
            }
        }
#endregion


#region Screen capture
        private class ScreenCaptureCallback : AndroidJavaProxy
        {
            private readonly IScreenCaptureRequestInvoke _ScreenCaptureRequest;
            public ScreenCaptureCallback (IScreenCaptureRequestInvoke screenCaptureRequest) : base (PERMISSIONS_CALLBACK_PACKAGE_NAME + ".ScreenCaptureCallback")
            {
                _ScreenCaptureRequest = screenCaptureRequest;
            }
            internal void OnGranted (AndroidJavaObject intent)
            {
                Permissions.Instance.Enqueue (() =>
                {
                    _ScreenCaptureRequest.InvokeOnGranted (intent);
                });
            }
            internal void OnDenied ()
            {
                Permissions.Instance.Enqueue (() =>
                {
                    _ScreenCaptureRequest.InvokeOnDenied ();
                });
            }
        }

        internal static void RequestScreenCapture (IScreenCaptureRequestInvoke screenCaptureRequest)
        {
            using (var unityPlayerClass = new AndroidJavaClass (UNITY_PLAYER_CLASS_NAME)) {
                using (var activity = unityPlayerClass.GetStatic<AndroidJavaObject> ("currentActivity")) {
                    using (var permissionsClass = new AndroidJavaClass (PERMISSIONS_CLASS_NAME)) {

                        var callback = new ScreenCaptureCallback (screenCaptureRequest);

                        permissionsClass.CallStatic ("RequestScreenCapture", activity, callback);
                    }
                }
            }
        }
#endregion
    }
}
#endif
