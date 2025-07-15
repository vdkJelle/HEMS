namespace HomeEnergyManagementSystem.Services;

using HomeEnergyManagementSystem.Dtos;
using HomeEnergyManagementSystem.Models;

public class DeviceService : IDeviceService
{
	private readonly Dictionary<Guid, Device> _devices = [];

	public List<Device> GetAllDevices()
	{
		return [.. _devices.Values];
	}

	public Device AddNewDevice(CreateDeviceDto dto)
	{
		Device device = dto.Type switch
		{
			"Refrigerator" => new Refrigerator { Id = Guid.NewGuid() },
			"SolarPanel" => new SolarPanel { Id = Guid.NewGuid() },
			"Lights" => new Lights { Id = Guid.NewGuid() },
			"HeatingSystem" => new HeatingSystem { Id = Guid.NewGuid() },
			"EVCharger" => new EVCharger { Id = Guid.NewGuid() },
			"HamsterWheel" => new HamsterWheel { Id = Guid.NewGuid() },
			_ => throw new ArgumentException($"Unknown device type: {dto.Type}")
		};

		_devices[device.Id] = device;
		return device;
	}

	public Device UpdateStateDevice(UpdateStateDto dto)
	{
		if (_devices.TryGetValue(dto.Id, out var device))
		{
			device.Enabled = dto.State;
			return device;
		}
		else
		{
			throw new KeyNotFoundException("Device not found.");
		}
	}

	public Device DeleteDevice(Guid id)
	{
		if (_devices.TryGetValue(id, out var device))
		{
			_devices.Remove(id);
			return device;
		}
		else
		{
			throw new KeyNotFoundException("Device not found.");
		}
	}
}
