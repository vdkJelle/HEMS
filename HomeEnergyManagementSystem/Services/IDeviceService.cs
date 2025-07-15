namespace HomeEnergyManagementSystem.Services;

using HomeEnergyManagementSystem.Dtos;
using HomeEnergyManagementSystem.Models;

public interface IDeviceService
{
	List<Device> GetAllDevices();
	Device AddNewDevice(CreateDeviceDto dto);
	Device UpdateStateDevice(UpdateStateDto dto);
	Device DeleteDevice(Guid guidId);
}
