using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.AVFoundation;

namespace CaptureDevice
{
    public partial class MainWindowController : MonoMac.AppKit.NSWindowController
    {
        #region Constructors

        // Called when created from unmanaged code
        public MainWindowController(IntPtr handle) : base(handle)
        {
            Initialize();
        }
        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public MainWindowController(NSCoder coder) : base(coder)
        {
            Initialize();
        }
        // Call to load from the XIB/NIB file
        public MainWindowController() : base("MainWindow")
        {
            Initialize();
        }
        // Shared initialization code
        void Initialize()
        {
        }

        #endregion

        //strongly typed window accessor
        public new MainWindow Window
        {
            get
            {
                return (MainWindow)base.Window;
            }
        }

        AVCaptureSession session;

        public override void WindowDidLoad()
        {
            base.WindowDidLoad();
            session = new AVCaptureSession() { SessionPreset = AVCaptureSession.PresetMedium };
            var captureDevice = AVCaptureDevice.DefaultDeviceWithMediaType(AVMediaType.Video);

            var input = AVCaptureDeviceInput.FromDevice(captureDevice);
            if (input == null)
            {
                Console.WriteLine("No input - this won't work on the simulator, try a physical device");

            }
            session.AddInput(input);
            var captureVideoPreviewLayer = new AVCaptureVideoPreviewLayer(session);

            var contentView = Window.ContentView;
            contentView.WantsLayer = true;
            captureVideoPreviewLayer.Frame = contentView.Bounds;
            contentView.Layer.AddSublayer(captureVideoPreviewLayer);

            session.StartRunning();
        }
    }
}

