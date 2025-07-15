using HomeEnergyManagementSystem.Dtos;

namespace HomeEnergyManagementSystem.Services;

public class EnergyUsageService : IEnergyUsageService
{
	private readonly IDeviceService _deviceService;

	public EnergyUsageService(IDeviceService deviceService)
	{
		_deviceService = deviceService;
	}

	public EnergyUsageDto GetEnergyUsageAtTimestamp(DateTime time)
	{
		var devices = _deviceService.GetAllDevices().Where(device => device.Enabled);

		double totalConsumption = 0;
		double totalProduction = 0;

		foreach (var device in devices)
		{
			totalConsumption += device.GetConsumption(time);
			totalProduction += device.GetProduction(time);
		}

		double usage = totalConsumption - totalProduction;

		return new EnergyUsageDto
		{
			Time = time.ToString(),
			Consumption = Math.Round(totalConsumption),
			Production = Math.Round(totalProduction),
			Usage = Math.Round(usage),
		};
	}
}
