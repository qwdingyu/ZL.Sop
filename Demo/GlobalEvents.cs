using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo
{
    public static class GlobalEvents
    {
        public static Action<string> OnGlobalSnScanReceived;

        public static Func<string, int, Task<string>> BarcodeCheckRequestScanAsync;

        public static Action<TimeSpan> OnTestTotalTimeChanged;

        public static Action<KeyTrafficLogModel> OnKeyTrafficLogged;

        public static readonly Dictionary<string, Action<object>> RealTimeUpdaters = new Dictionary<string, Action<object>>();

        public static Action<string, bool> OnUiMainTipChanged { get; set; }

        public static Action<int, string> OnSbrStepTipChanged { get; set; }

        public static Action<int, string> OnSelfStudyStepTipChanged { get; set; }
    }
}
