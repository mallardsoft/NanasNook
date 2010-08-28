using System;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using NanasNook.Model;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.SSCE;
using UpdateControls.Correspondence.WebServiceClient;
using UpdateControls.XAML;

namespace NanasNook
{
	public partial class App : Application
	{
		private Community _community;
		private Machine _machine;
		private SynchronizationThread _synchronizationThread;

		private IDisposable _currentDuration;

		private void Application_Startup(
			object sender, StartupEventArgs e)
		{
			// Load the correspondence database.
			string databaseFileName = Directory.ApplicationData / "Correspondence" / "NanasNook" / "Correspondence.sdf";
			
			_community = new Community(new SSCEStorageStrategy(databaseFileName))
				.AddCommunicationStrategy(new WebServiceCommunicationStrategy())
				.RegisterAssembly(typeof(Machine))
                .Subscribe(() => _machine.CurrentProvisionFrontOffice.Select(p => p.Company))
                .Subscribe(() => _machine.CurrentProvisionKitchen.Select(p => p.Kitchen.Company));

			// Load or create the current machine.
			string machineIdentifier = "NanasNook.Model.Machine.thisMachine";
			_machine = _community.LoadFact<Machine>(machineIdentifier);
			if (_machine == null)
			{
				_machine = _community.AddFact(new Machine());
				_community.SetFact(machineIdentifier, _machine);
			}

			// Recycle durations when the application is idle.
			DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.ApplicationIdle);
			timer.Interval = TimeSpan.FromSeconds(1.0);
			timer.Tick += Timer_Tick;
			timer.Start();

			BeginDuration();

			// Display the main window.
			MainWindow = new Window1();
			MainWindow.DataContext = ForView.Wrap(new MainViewModel(_machine));
			MainWindow.Show();

			_synchronizationThread = new SynchronizationThread(_community);
			_community.FactAdded += () => _synchronizationThread.Wake();
			_synchronizationThread.Start();
		}

		private void Application_Exit(object sender, ExitEventArgs e)
		{
			_synchronizationThread.Stop();
			EndDuration();
		}

		void Timer_Tick(object sender, EventArgs e)
		{
			// End the last duration and begin the next.
			EndDuration();
			BeginDuration();
		}

		private void BeginDuration()
		{
			if (_currentDuration == null)
			{
				_currentDuration = _community.BeginDuration();
			}
		}

		private void EndDuration()
		{
			if (_currentDuration != null)
			{
				_currentDuration.Dispose();
				_currentDuration = null;
			}
		}
	}
}
