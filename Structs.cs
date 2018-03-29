using System;
using ObjCRuntime;
using OpenTK;
using System.Runtime.InteropServices;
using AVFoundation;
using CoreMotion;
using CoreVideo;
using Foundation;
using UIKit;
using OpenGLES;
using CoreMedia;

namespace Structure
{
    public enum STSensorControllerInitStatus
    {
        /// Indicates controller uninitialized because the sensor was not found.
        STSensorControllerInitStatusSensorNotFound = 0,
        /// Indicates controller initialization succeeded.
        STSensorControllerInitStatusSuccess = 1,
        /// Indicates controller was previously initialized.
        STSensorControllerInitStatusAlreadyInitialized = 2,
        /// Indicates controller uninitialized because sensor is waking up.
        STSensorControllerInitStatusSensorIsWakingUp = 3,
        /// Indicates controller uninitialized because of a failure to open the connection.
        STSensorControllerInitStatusOpenFailed = 4,
        /** Indicates controller is uninitialized and sensor is not opened because the application is running in the background.
            At the moment we do not support initialiazing the sensor while in the background.
        */
        STSensorControllerInitStatusAppInBackground = 5,
    }

    public enum STSensorControllerDidStopStreamingReason
    {
        /** Sensor stops streaming because of iOS app will resign active.
            This can occur when apps are sent to the background, during fast app switching, or when the notification/control center appears.
        */
        STSensorControllerDidStopStreamingReasonAppWillResignActive = 0
    }

    public enum STStreamConfig
    {
        /// Invalid stream configuration.
        STStreamConfigInvalid = -1,

        /// QVGA depth at 30 FPS.
        STStreamConfigDepth320x240 = 0,

        /// QVGA depth at 30 FPS, aligned to the color camera.
        [Obsolete("Please use STStreamConfigDepth320x240 with registeredToColorFrame instead.")]
        STStreamConfigRegisteredDepth320x240,

        /// QVGA depth and infrared at 30 FPS.
        STStreamConfigDepth320x240AndInfrared320x248,

        /// QVGA infrared at 30 FPS.
        STStreamConfigInfrared320x248,

        /// VGA depth at 30 FPS.
        STStreamConfigDepth640x480,

        /// VGA infrared at 30 FPS.
        STStreamConfigInfrared640x488,

        /// VGA depth and infrared at 30 FPS.
        STStreamConfigDepth640x480AndInfrared640x488,

        /// VGA depth at 30 FPS, aligned to the color camera.
        [Obsolete("Please use STStreamConfigDepth640x480 with registeredToColorFrame instead.")]
        STStreamConfigRegisteredDepth640x480,

        /// QVGA depth at 60 FPS. Note: frame sync is not supported with this mode.
        STStreamConfigDepth320x240_60FPS
    }

    public enum STFrameSyncConfig
    {
        /// Default mode, frame sync is off.
        STFrameSyncOff = 0,

        /// Frame sync between AVFoundation video frame and depth frame.
        STFrameSyncDepthAndRgb,

        /// Frame sync between AVFoundation video frame and infrared frame.
        STFrameSyncInfraredAndRgb
    }

    public enum STCalibrationType
    {
        /// There is no calibration for Structure Sensor + iOS device combination.
        STCalibrationTypeNone = 0,

        /// There exists an approximate calibration Structure Sensor + iOS device combination.
        STCalibrationTypeApproximate,

        /// There exists a device specific calibration from Calibrator.app of this Structure Sensor + iOS device combination.
        STCalibrationTypeDeviceSpecific
    }

    [Native]
    public enum STDepthToRgbaStrategy
    {
        /// Linear mapping using a color gradient – pure red encodes the minimal depth, and pure blue encodes the farthest possible depth.
        STDepthToRgbaStrategyRedToBlueGradient = 0,

        /// Linear mapping from closest to farthest depth as a grayscale intensity.
        STDepthToRgbaStrategyGray,
    }

    public enum STErrorCode
    {
        /// The operation could not be completed because parameters passed to the SDK method contains non-valid values.
        STErrorInvalidValue = 10,

        /// STCameraPoseInitializer tried to initialize a camera pose using without a depth frame.
        STErrorCameraPoseInitializerDepthFrameMissing = 20,

        /// The operation could not be completed because file output path is invalid.
        STErrorFileWriteInvalidFileName = 31,

        /// `STTracker` is not initialized yet.
        STErrorTrackerNotInitialized = 41,

        /// `STTracker` detected the color sample buffer exposure has changed.
        STErrorTrackerColorExposureTimeChanged = 44,

        /// `STTracker` doesn't have device motion and cannot continue tracking.
        STErrorTrackerDeviceMotionMissing = 45,

        /// The STMesh operation could not be completed because it contains an empty mesh.
        STErrorMeshEmpty = 60,

        /// The STMesh operation could not be completed because it was cancelled.
        STErrorMeshTaskCancelled = 61,

        /// The STMesh operation could not be completed because it contains an invalid texture format.
        STErrorMeshInvalidTextureFormat = 62,

        /// The colorize operation could not be completed because `STColorizer` doesn't have keyframes.
        STErrorColorizerNoKeyframes = 80,

        /// The colorize operation could not be completed because `STColorizer` doesn't have a mesh.
        STErrorColorizerEmptyMesh = 81,
    }

    public enum STMeshWriteOptionFileFormat
    {
        /** Wavefront OBJ format.
            If the mesh has a texture, an MTL file will also be generated, and the texture saved to a JPEG file.
            Filenames with spaces are not supported by OBJ and will return an error.
        */
        STMeshWriteOptionFileFormatObjFile = 0,

        /** Wavefront OBJ format, compressed into a ZIP file.
            The archive will also embed the MTL and JPEG file if the mesh has a texture.
        */
        STMeshWriteOptionFileFormatObjFileZip = 1
    }

    public enum STTrackerType
    {
        /** Specifies a tracker that will only use the depth information from Structure Sensor.
            This tracker works best at close/mid-range, in combination with `kSTTrackerTrackAgainstModelKey`.
        */
        STTrackerDepthBased = 0,

        /** Specifies a tracker that will use both the depth information from Structure Sensor and the color information from the iOS device camera.
        Only `kCVPixelFormatType_420YpCbCr8BiPlanarFullRange` is supported for the color buffer format.
        */
        STTrackerDepthAndColorBased = 1,
    }

    public enum STTrackerQuality
    {
        /// Best during scanning, but it will also take more CPU resources.
        STTrackerQualityAccurate = 0,

        /// Designed for very fast tracking, it works best when tracking against a static mesh (e.g. after a scan has been done), or when the available CPU resources are limited.
        STTrackerQualityFast,
    }

    public enum STTrackerPoseAccuracy
    {
        /// The tracker cannot provide any accuracy information. This typically happens when it gets lost or before it sees any frame.
        STTrackerPoseAccuracyNotAvailable,

        /// The tracker pose accuracy is very low. This can happen when the model has been out of view for a long time or if very few pixels have depth data.
        STTrackerPoseAccuracyVeryLow,

        /// The tracker pose accuracy is low. This can happen when tracking against a model that is getting too close or out of view.
        STTrackerPoseAccuracyLow,

        /// The tracker pose accuracy is approximate. This can happen during fast movements.
        STTrackerPoseAccuracyApproximate,

        /// The tracker pose accuracy is nominal.
        STTrackerPoseAccuracyHigh,
    }

    public enum STCameraPoseInitializerStrategy
    {
        /// <summary>
        ///     Try to detect a ground plane and set the camera pose such that the cuboid scanning volume lies on top of it.
        ///     If no ground plane is found or if the device is not looking downward, place the scanning volume at the distance given by the central depth pixels.
        ///     In both cases, the cuboid orientation will be aligned with the provided gravity vector.
        /// </summary>
        /// <remarks>
        ///     This strategy requires depth information from the Structure Sensor.
        /// </remarks>
        STCameraPoseInitializerStrategyTableTopCube = 0,

        /// <summary>
        ///     Align the camera orientation using the gravity vector, leaving the translation component to (0,0,0).
        /// </summary>
        /// <remarks>
        ///     This strategy does not require depth information.
        /// </remarks>
        STCameraPoseInitializerStrategyGravityAlignedAtOrigin = 1,

        /// <summary>
        ///     Align the camera orientation using the gravity vector, and places the camera center at the center of the scanning volume.
        /// </summary>
        /// <remarks>
        ///     This strategy does not require depth information.
        /// </remarks>
        STCameraPoseInitializerStrategyGravityAlignedAtVolumeCenter = 2,
    }

    public enum STColorizerType
    {
        /// <summary>
        ///     Generate a color for each vertext of the mesh. Best for small objects.
        /// </summary>
        PerVertex,

        /// <summary>
        ///     Generate a global texture map, and UV coordinates for each vertext of the mesh. Optimized for large rooms.
        /// </summary>
        /// <remarks>
        ///     Only 640x480 color images are supported by this colorizer.
        /// </remarks>
        TextureMapForRoom,

        /// <summary>
        ///     Generate a global texture map, and UV coordinates for each vertext of the mesh. Optimized for people and objects. 
        /// </summary>
        TextureMapForObject
    }

    public enum STColorizerQuality
    {
        /// <summary>
        ///     Use this when speed is not a concern.
        /// </summary>
        UltraHigh,

        /// <summary>
        ///     Use this to balance between quality and speed
        /// </summary>
        High,

        /// <summary>
        ///     Use this option when speed is a concern
        /// </summary>
        Normal
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct STTrackerHints
    {
        [Export("trackerIsLost")]
        public bool TrackerIsLost { get; set; }

        [Export("sceneIsTooClose")]
        public bool SceneIsTooClose { get; set; }

        [Export("modelOutOfView")]
        public bool ModelOutOfView { get; set; }
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct STCameraPoseInitializerOutput
    {
        /// Whether the pose initializer could find a good pose.
        [Export("hasValidPose")]
        public bool HasValidPose { get; set; }

        /// Estimated camera pose, taking Structure Sensor as a reference.
        [Export("cameraPose")]
        public Matrix4 CameraPose { get; set; }

        /// Whether the last cube placement was made with a supporting plane. Useful for STMapper.
        [Export("hasSupportPlane")]
        public bool HasSupportPlane { get; set; }

        /// Equation of the detected support plane (if hasSupportPlane is true)
        [Export("supportPlane")]
        public Vector4 SupportPlane { get; set; }
    }
}

