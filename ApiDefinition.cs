using System;
using AVFoundation;
using CoreMotion;
using CoreVideo;
using ObjCRuntime;
using Foundation;
using UIKit;
using OpenGLES;
using OpenTK;
using CoreMedia;
using CoreGraphics;
using CoreFoundation;
using System.Collections.Generic;

namespace Structure
{
    public class Constants {
    
        #region Constants

        #region Structure

        [Export("kSTDepthToRgbaStrategyKey")]
        public static string STDepthToRgbaStrategyKey { get; }

        [Export("StructureSDKErrorDomain")]
        public static string StructureSdkErrorDomain { get; }

        [Export("kSTStreamConfigKey")]
        public static string STStreamConfigKey { get; }

        [Export("kSTFrameSyncConfigKey")]
        public static string STFrameSyncConfigKey { get; }

        [Export("kSTHoleFilterEnabledKey")]
        public static string STHoleFilterEnabledKey { get; }

        [Export("kSTHighGainEnabledKey")]
        public static string STHighGainEnabledKey { get; }

        [Export("kSTColorCameraFixedLensPositionKey")]
        public static string STColorCameraFixedLensPositionKey { get; }

        #endregion

        #region StructureSLAM

        [Export("kSTMeshWriteOptionFileFormatKey")]
        public static string STMeshWriteOptionFileFormatKey { get; }

        [Export("kSTTrackerTypeKey")]
        public static string STTrackerTypeKey { get; }

        [Export("kSTTrackerQualityKey")]
        public static string STTrackerQualityKey { get; }

        [Export("kSTTrackerTrackAgainstModelKey")]
        public static string STTrackerTrackAgainstModelKey { get; }

        [Export("kSTTrackerAvoidPitchRollDriftKey")]
        public static string STTrackerAvoidPitchRollDriftKey { get; }

        [Export("kSTTrackerAvoidHeightDriftKey")]
        public static string STTrackerAvoidHeightDriftKey { get; }

        [Export("kSTTrackerAcceptVaryingColorExposureKey")]
        public static string STTrackerAcceptVaryingColorExposureKey { get; }

        [Export("kSTTrackerBackgroundProcessingEnabledKey")]
        public static string STTrackerBackgroundProcessingEnabledKey { get; }

        [Export("kSTMapperVolumeResolutionKey")]
        public static string STMapperVolumeResolutionKey { get; }

        [Export("kSTMapperVolumeBoundsKey")]
        public static string STMapperVolumeBoundsKey { get; }

        [Export("kSTMapperVolumeHasSupportPlaneKey")]
        public static string STMapperVolumeHasSupportPlaneKey { get; }

        [Export("kSTMapperEnableLiveWireFrameKey")]
        public static string STMapperEnableLiveWireFrameKey { get; }

        [Export("kSTMapperDepthIntegrationFarThresholdKey")]
        public static string STMapperDepthIntegrationFarThresholdKey { get; }

        [Export("kSTMapperLegacyKey")]
        public static string STMapperLegacyKey { get; }

        [Export("kSTCameraPoseInitializerStrategyKey")]
        public static string STCameraPoseInitializerStrategyKey { get; }

        [Export("kSTKeyFrameManagerMaxSizeKey")]
        public static string STKeyFrameManagerMaxSizeKey { get; }

        [Export("kSTKeyFrameManagerMaxDeltaRotationKey")]
        public static string STKeyFrameManagerMaxDeltaRotationKey { get; }

        [Export("kSTKeyFrameManagerMaxDeltaTranslationKey")]
        public static string STKeyFrameManagerMaxDeltaTranslationKey { get; }

        [Export("kSTColorizerTypeKey")]
        public static string STColorizerTypeKey { get; }

        [Export("kSTColorizerPrioritizeFirstFrameColorKey")]
        public static string STColorizerPrioritizeFirstFrameColorKey { get; }

        [Field("kSTColorizerTargetNumberOfFacesKey")]
        public static string STColorizerTargetNumberOfFacesKey { get; }

        [Field("kSTColorizerQualityKey", "Structure.a")]
        public static string STColorizerQualityKey { get; }

        #endregion

        #endregion

    }

    [BaseType(typeof(NSObject))]
    public interface STDepthFrame
    {
        [Export("width")]
        int Width { get; }

        [Export("height")]
        int Height { get; }

        /// <summary>
        ///     ushort[]
        /// </summary>
        [Export("shiftData")]
        IntPtr ShiftData { get; }

        [Export("depthInMillimeters")]
        float DepthInMillimeters { get; }

        [Export("halfResolutionDepthFrame")]
        STDepthFrame HalfResolutionDepthFrame { get; }

        [Export("timestamp")]
        double TimeStamp { get; }

        [Export("registeredToColorFrame:")]
        STDepthFrame RegisteredToColorFrame(STColorFrame colorFrame);

        [Export("glProjectionMatrix")]
        Matrix4 GlProjectionMatrix();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="matrix4x4">float[]</param>
        [Export("colorCameraPoseInDepthCoordinateFrame:")]
        void ColorCameraPoseInDepthCoordinateFrame(IntPtr matrix4x4);

        [Export("applyExpensiveCorrection")]
        bool ApplyExpensiveCorrection();
    }

    [BaseType(typeof(NSObject))]
    public interface STInfraredFrame
    {
        /// <summary>
        ///     ushort[]
        /// </summary>
        [Export("data")]
        IntPtr Data { get; set; }

        [Export("width")]
        int Width { get; set; }

        [Export("height")]
        int Height { get; set; }

        [Export("timestamp")]
        double Timestamp { get; set; }
    }

    [BaseType(typeof(NSObject))]
    public interface STColorFrame
    {
        /// <summary>
        ///     CMSampleBuffer
        /// </summary>
        [Export("sampleBuffer")]
        IntPtr SampleBuffer { get; }

        [Export("width")]
        int Width { get; set; }

        [Export("height")]
        int Height { get; set; }

        [Export("timestamp")]
        double Timestamp { get; set; }

        [Export("halfResolutionColorFrame")]
        STColorFrame HalfResolutionColorFrame { get; }

        [Export("glProjectionMatrix")]
        Matrix4 GlProjectionMatrix();
    }

    [BaseType(typeof(NSObject))]
    [Model]
    [Protocol]
    public interface STSensorControllerDelegate
    {
        [Abstract]
        [Export("sensorDidConnect")]
        void SensorDidConnect();

        [Abstract]
        [Export("sensorDidDisconnect")]
        void SensorDidDisconnect();

        [Abstract]
        [Export("sensorDidStopStreaming:")]
        void SensorDidStopStreaming(STSensorControllerDidStopStreamingReason reason);

        [Abstract]
        [Export("sensorDidLeaveLowPowerMode")]
        void SensorDidLeaveLowPowerMode();

        [Abstract]
        [Export("sensorBatteryNeedsCharging")]
        void SensorBatteryNeedsCharging();

        // Optional
        [Export("sensorDidOutputDepthFrame:")]
        void SensorDidOutputDepthFrame(STDepthFrame depthFrame);
        [Export("sensorDidOutputInfraredFrame:")]
        void SensorDidOutputInfraredFrame(STInfraredFrame irFrame);
        [Export("sensorDidOutputSynchronizedDepthFrame:colorFrame:")]
        void SensorDidOutputSynchronizedDepthFrame(STDepthFrame depthFrame, STColorFrame colorFrame);
        [Export("sensorDidOutputSynchronizedInfraredFrame:colorFrame:")]
        void SensorDidOutputSynchronizedInfraredFrame(STInfraredFrame irFrame, STColorFrame colorFrame);
    }

    [BaseType(typeof(NSObject))]
    public interface STSensorController
    {
        [Static, Export("sharedController")]
        STSensorController SharedController();

        [Export("delegate", ArgumentSemantic.Assign)]
        STSensorControllerDelegate Delegate { get; set; }

        [Export("initializeSensorConnection")]
        STSensorControllerInitStatus InitializeSensorConnection();

        [Export("startStreamingWithOptions:error:")]
        bool StartStreamingWithOptions(NSDictionary options, NSError error);

        [Export("stopStreaming")]
        void StopStreaming();

        [Export("frameSyncNewColorBuffer:")]
        void FrameSyncNewColorBuffer(CMSampleBuffer sampleBuffer);

        [Export("isConnected")]
        bool IsConnected();

        [Export("isLowPower")]
        bool IsLowPower();

        [Export("getBatteryChargePercentage")]
        int GetBatteryChargePercentage();

        [Export("getName")]
        string GetName();

        [Export("getSerialNumber")]
        string GetSerialNumber();

        [Export("getFirmwareRevision")]
        string GetFirmwareRevision();

        [Export("getHardwareRevision")]
        string GetHardwareRevision();

        [Static, Export("launchCalibratorAppOrGoToAppStore")]
        bool LaunchCalibratorAppOrGoToAppStore();

        [Static, Export("approximateCalibrationGuaranteedForDevice")]
        bool ApproximateCalibrationGuaranteedForDevice();

        [Static, Export("calibrationType")]
        STCalibrationType CalibrationType();

        [Export("setHighGainEnabled:")]
        void SetHighGainEnabled(bool enabled);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="matrix4x4">float[]</param>
        [Export("colorCameraPoseInSensorCoordinateFrame:")]
        void colorCameraPoseInSensorCoordinateFrame(IntPtr matrix4x4);

    }

    [BaseType(typeof(NSObject))]
    public interface STGLTextureShaderRGBA
    {
        [Export("useShaderProgram")]
        void UseShaderProgram();

        [Export("renderTexture:")]
        void RenderTexture(int textureUnit);
    }

    [BaseType(typeof(NSObject))]
    public interface STGLTextureShaderYCbCr
    {
        [Export("useShaderProgram")]
        void UseShaderProgram();

        [Export("renderWithLumaTexture:chromaTexture:")]
        void RenderWithLumaTexture(int lumaTextureUnit, int chromaTextureUnit);
    }

    [BaseType(typeof(NSObject))]
    public interface STGLTextureShaderGray
    {
        [Export("useShaderProgram")]
        void UseShaderProgram();

        [Export("renderWithLumaTexture:")]
        void RenderWithLumaTexture(int lumaTextureUnit);
    }

    [BaseType(typeof(NSObject))]
    public interface STDepthToRgba
    {
        /// <summary>
        ///     byte[]
        /// </summary>
        [Export("rgbaBuffer")]
        IntPtr RgbaBuffer { get; }

        [Export("width")]
        int Width { get; }

        [Export("height")]
        int Height { get; }

        [Export("initWithOptions:")]
        IntPtr Constructor(NSDictionary options);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frame"></param>
        /// <returns>byte[]</returns>
        [Export("convertDepthFrameToRgba:")]
        IntPtr ConvertDepthFrameToRgba(STDepthFrame frame);
    }

    [BaseType(typeof(NSObject))]
    public interface STWirelessLog
    {
        [Static, Export("broadcastLogsToWirelessConsoleAtAddress:usingPort:error:")]
        void BroadCastLogsToWirelessConsoleAtAddress(string ipv4Address, int port, out NSError error);
    }

    [BaseType(typeof(NSObject))]
    [Model][Protocol]
    public interface STBackgroundTaskDelegate
    {
        
        [Export("backgroundTask:didUpdateProgress:")]
        void BackgroundTask(STBackgroundTask sender, double progress);
    }

    [BaseType(typeof(NSObject))]
    public interface STBackgroundTask
    {
        [Export("start")]
        void Start();

        [Export("cancel")]
        void Cancel();

        [Export("waitUntilCompletion")]
        void WaitUntilCompletion();

        [Export("isCancelled")]
        bool IsCancelled { get; }

        [Export("delegate", ArgumentSemantic.Assign)]
        STBackgroundTaskDelegate Delegate { get; set; }
    }
    
    public delegate void CompletionHandler(STMesh result, NSError error);

    [BaseType(typeof(NSObject))]
    public interface STMesh
    {
        [Export("numberOfMeshes")]
        int NumberOfMeshes();

        [Export("numberOfMeshFaces:")]
        int NumberOfMeshFaces(int meshIndex);

        [Export("numberOfMeshVertices:")]
        int NumberOfMeshVertices(int meshIndex);

        [Export("numberOfMeshLines:")]
        int NumberOfMeshLines(int meshIndex);

        [Export("hasPerVertexNormals")]
        bool HasPerVertexNormals();

        [Export("hasPerVertexColors")]
        bool HasPerVertexColors();

        [Export("hasPerVertexUVTextureCoords")]
        bool HasPerVertexUVTextureCoords();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="meshIndex"></param>
        /// <returns>Vector3[]</returns>
        [Export("meshVertices:")]
        IntPtr MeshVertices(int meshIndex);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="meshIndex"></param>
        /// <returns>Vector3[]</returns>
        [Export("meshPerVertexNormals:")]
        IntPtr MeshPerVertexNormals(int meshIndex);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="meshIndex"></param>
        /// <returns>Vector3[]</returns>
        [Export("meshPerVertexColors:")]
        IntPtr MeshPerVertexColors(int meshIndex);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="meshIndex"></param>
        /// <returns>Vector2[]</returns>
        [Export("meshPerVertexUVTextureCoords:")]
        IntPtr MeshPerVertexUVTextureCoords(int meshIndex);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="meshIndex"></param>
        /// <returns>ushort[]</returns>
        [Export("meshFaces:")]
        IntPtr MeshFaces(int meshIndex);

        [Export("meshYCbCrTexture")]
        CVPixelBuffer MeshYCbCrTexture();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="meshIndex"></param>
        /// <returns>ushort[]</returns>
        [Export("meshLines:")]
        IntPtr MeshLines(int meshIndex);

        [Export("writeToFile:options:error:")]
        bool WriteToFile(string filePath, NSDictionary options, NSError error);

        [Export("initWithMesh:")]
        IntPtr Constructor(STMesh mesh);

        [Static, Export("newDecimateTaskWithMesh:numFaces:completionHandler:")]
        STBackgroundTask NewDecimateTaskWithMesh(STMesh inputMesh, uint numFaces, [NullAllowed]CompletionHandler completionHandler);

        [Static, Export("newFillHolesTaskWithMesh:completionHandler:")]
        STBackgroundTask NewFillHolesTaskWithMesh(STMesh inputMesh, [NullAllowed]CompletionHandler completionHandler);
    }

    [BaseType(typeof(NSObject))]
    public interface STMeshIntersector
    {
        [Export("initWithMesh:")]
        IntPtr Constructor(STMesh inputMesh);

        [Export("intersectWithRayOrigin:rayEnd:intersection:normal:ignoreBackFace:ignoreBackFace")]
        bool IntersectWithRayOrigin(Vector3 origin, Vector3 end, Vector3 intersection, Vector3 normal, bool ignoreBackFace );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="end"></param>
        /// <param name="intersection"></param>
        /// <param name="intersectionFaceIndex">int[]</param>
        /// <param name="ignoreBackFace"></param>
        /// <returns></returns>
        [Export("intersectWithRayOrigin:rayEnd:intersection:intersectionFaceIndex:ignoreBackFace:")]
        bool IntersectWithRayOrigin(Vector3 origin, Vector3 end, Vector3 intersection, IntPtr intersectionFaceIndex, bool ignoreBackFace );

        [Export("faceIsOnAPlane:normal:")]
        bool FaceIsOnAPlane(int faceIndex, Vector3 normal);
    }

    [BaseType(typeof(NSObject))]
    public interface STScene
    {
        [Export("initWithContext:")]
        IntPtr Constructor(EAGLContext glContext);

        [Export("lockAndGetSceneMesh")]
        STMesh LockAndGetSceneMesh();

        [Export("unlockSceneMesh")]
        void UnlockSceneMesh();

        [Export("renderMeshFromViewpoint:cameraGLProjection:alpha:highlightOutOfRangeDepth:wireframe:")]
        bool RenderMeshFromViewpoint(Matrix4 cameraPose, Matrix4 glProjection, float alpha,
            bool highlightOutOfRangeDepth, bool wireframe);

        [Export("clear")]
        void Clear();
    }

    [BaseType(typeof(NSObject))]
    public interface STTracker
    {
        [Export("scene", ArgumentSemantic.Retain)]
        STScene scene { get; set; }

        [Export("initialCameraPose")]
        Matrix4 InitialCameraPose { get; set; }

        [Export("trackerHints")]
        STTrackerHints TrackerHints { get; }

        [Export("poseAccuracy")]
        STTrackerPoseAccuracy PoseAccuracy { get; }

        [Export("initWithScene:options:")]
        IntPtr Constructor(STScene scene, NSDictionary options);

        [Export("reset")]
        void Reset();

        [Export("updateCameraPoseWithDepthFrame:colorFrame:error:")]
        bool UpdateCameraPoseWithDepthFrame(STDepthFrame depthFrame, STColorFrame colorFrame, NSError error);

        [Export("updateCameraPoseWithMotion:")]
        void UpdateCameraPoseWithMotion(CMDeviceMotion motionData);

        [Export("lastFrameCameraPose")]
        Matrix4 LastFrameCameraPose();

        [Export("setOptions:")]
        void SetOptions(NSDictionary options);
    }

    [BaseType(typeof(NSObject))]
    public interface STMapper
    {
        [Export("scene", ArgumentSemantic.Retain)]
        STScene Scene { get; set; }

        [Export("initWithScene:options:")]
        IntPtr Constructor(STScene scene, NSDictionary options);

        [Export("reset")]
        void Reset();

        [Export("integrateDepthFrame:cameraPose:")]
        void IntegrateDepthFrame(STDepthFrame depthFrame, Matrix4 cameraPose);

        [Export("finalizeTriangleMesh")]
        void FinalizeTriangleMesh();
    }

    [BaseType(typeof(NSObject))]
    public interface STCameraPoseInitializer
    {
        [Export("volumeSizeInMeters")]
        Vector3 VolumeSizeInMeters { get; set; }

        [Export("lastOutput")]
        STCameraPoseInitializerOutput LastOutput { get; }

        [Obsolete("use lastOutput instead.")]
        [Export("cameraPose")]
        Matrix4 CameraPose { get; }

        [Obsolete("use lastOutput instead.")]
        [Export("hasValidPose")]
        bool HasValidPose { get; }

        [Obsolete("use lastOutput instead.")]
        [Export("hasSupportPlane")]
        bool HasSupportPlane { get; }

        [Export("initWithVolumeSizeInMeters:options:")]
        IntPtr Constructor(Vector3 volumeSize, NSDictionary options);

        [Export("updateCameraPoseWithGravity:depthFrame:error:")]
        bool UpdateCameraPoseWithGravity(Vector3 gravityInDeviceFrame, STDepthFrame depthFrame, NSError error);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="depthFrame"></param>
        /// <param name="outputMask">byte[]</param>
        [Export("detectInnerPixelsWithDepthFrame:mask:")]
        void DetectInnerPixelsWithDepthFrame(STDepthFrame depthFrame, IntPtr outputMask);
    }

    [BaseType(typeof(NSObject))]
    public interface STCubeRenderer
    {
        [Export("initWithContext:")]
        IntPtr Constructor(EAGLContext glContext);

        [Export("setDepthFrame:")]
        void SetDepthFrame(STDepthFrame depthFrame);

        [Export("setCubeHasSupportPlane:")]
        void SetCubeHasSupportPlane(bool hasSupportPlane);

        [Export("adjustCubeSize:")]
        void AdjustCubeSize(Vector3 sizeInMeters);

        [Export("renderHighlightedDepthWithCameraPose:alpha:")]
        void RenderHighlightedDepthWithCameraPose(Matrix4 cameraPose, float alpha);

        [Export("renderCubeOutlineWithCameraPose:depthTestEnabled:occlusionTestEnabled:")]
        void RenderCubeOutlineWithCameraPose(Matrix4 cameraPose, bool depthTestEnabled, bool occlusionTestEnabled);
    }

    [BaseType(typeof(NSObject))]
    public interface STNormalFrame
    {
        [Export("width")]
        int Width { get; }

        [Export("height")]
        int Height { get; }

        [Export("normals")]
        IntPtr Normals { get; }
    }

    [BaseType(typeof(NSObject))]
    public interface STNormalEstimator
    {
        [Export("calculateNormalsWithDepthFrame:")]
        STNormalFrame CalculateNormalsWithDepthFrame(STDepthFrame floatDepthFrame);
    }

    [BaseType(typeof(NSObject))]
    public interface STKeyFrame
    {
        [Export("initWithColorCameraPose:colorFrame:depthFrame:")]
        IntPtr Constructor(Matrix4 colorCameraPose, STColorFrame colorFrame, STDepthFrame depthFrame);
    }

    [BaseType(typeof(NSObject))]
    public interface STKeyFrameManager
    {
        [Export("initWithOptions:")]
        IntPtr Constructor(NSDictionary options);

        [Export("wouldBeNewKeyframeWithColorCameraPose:")]
        bool WouldBeNewKeyframeWithColorCameraPose(Matrix4 colorCameraPose);

        [Export("processKeyFrameCandidateWithColorCameraPose:colorFrame:depthFrame:")]
        bool ProcessKeyFrameCandidateWithColorCameraPose(Matrix4 colorCameraPose, STColorFrame colorFrame,
            STDepthFrame depthFrame);

        [Export("addKeyFrame:")]
        void AddKeyFrame(STKeyFrame keyFrame);

        [Export("getKeyFrames")]
        NSArray GetKeyFrames();

        [Export("clear")]
        void Clear();
    }

    [BaseType(typeof(NSObject))]
    public interface STColorizer
    {
        [Static]
        [Export("newColorizeTaskWithMesh:scene:keyframes:completionHandler:options:error:")]
        STBackgroundTask NewColorizeTaskWithMesh(STMesh mesh, STScene scene, NSArray keyframes,
            [NullAllowed] CompletionHandler completionHandler, NSDictionary options, NSError error);
    }

    //[BaseType(typeof(NSObject))]
    //public interface STTrackerHints
    //{
    //    [Export("trackerIsLost")]
    //    public bool TrackerIsLost { get; set; }

    //    [Export("sceneIsTooClose")]
    //    public bool SceneIsTooClose { get; set; }

    //    [Export("modelOutOfView")]
    //    public bool ModelOutOfView { get; set; }
    //}


    //[BaseType(typeof(NSObject))]
    //public interface STCameraPoseInitializerOutput
    //{
    //    /// Whether the pose initializer could find a good pose.
    //    [Export("hasValidPose")]
    //    public bool HasValidPose { get; set; }

    //    /// Estimated camera pose, taking Structure Sensor as a reference.
    //    [Export("cameraPose")]
    //    public Matrix4 CameraPose { get; set; }

    //    /// Whether the last cube placement was made with a supporting plane. Useful for STMapper.
    //    [Export("hasSupportPlane")]
    //    public bool HasSupportPlane { get; set; }

    //    /// Equation of the detected support plane (if hasSupportPlane is true)
    //    [Export("supportPlane")]
    //    public Vector4 SupportPlane { get; set; }
    //}
}
