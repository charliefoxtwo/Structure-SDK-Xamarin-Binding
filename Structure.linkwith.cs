using System;
using ObjCRuntime;

[assembly: LinkWith ("Structure.a",
    LinkTarget = LinkTarget.Arm64 | LinkTarget.Simulator, 
    ForceLoad = false,
    SmartLink = true,
    LinkerFlags = "-lc++",
    Frameworks = "Foundation AVFoundation GLKit Accelerate Metal OpenGLES CoreVideo CoreMedia CoreImage ImageIO CoreGraphics CoreMotion QuartzCore UIKit ExternalAccessory")]
