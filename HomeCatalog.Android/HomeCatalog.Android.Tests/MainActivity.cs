using System.Reflection;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Xamarin.Android.NUnitLite;

namespace HomeCatalog.Android.Tests
{
	[Activity (Label = "HomeCatalog.Android.Tests", MainLauncher = true)]
	public class MainActivity : TestSuiteActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			this.Intent.PutExtra ("automated", true);
			AddTest (Assembly.GetExecutingAssembly ());
			// AddTest (typeof (Your.Library.TestClass).Assembly);
			base.OnCreate (bundle);
		}

		protected override void OnDestroy ()
		{
			Process.KillProcess(Process.MyPid());
			base.OnDestroy ();
		}
	}
}

