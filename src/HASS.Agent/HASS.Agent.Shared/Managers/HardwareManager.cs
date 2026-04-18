using System.Collections.Generic;
using System.Linq;
using LibreHardwareMonitor.Hardware;
using Serilog;

namespace HASS.Agent.Shared.Managers;
public static class HardwareManager
{
	private static Computer s_computer;
	public static void Initialize()
	{
		s_computer = new Computer()
		{
			IsCpuEnabled = false,
			IsGpuEnabled = true,
			IsMemoryEnabled = false,
			IsMotherboardEnabled = false,
			IsControllerEnabled = false,
			IsNetworkEnabled = false,
			IsStorageEnabled = false,
		};

		s_computer.Open();

		var hw = s_computer.Hardware.ToList();
		Log.Information("[HWMGR] Initialized. Detected {count} hardware: {list}",
			hw.Count,
			string.Join(", ", hw.Select(h => $"{h.HardwareType}:{h.Name}")));
	}

	public static IList<IHardware> Hardware => s_computer?.Hardware;

	public static void Shutdown() => s_computer?.Close();
}
