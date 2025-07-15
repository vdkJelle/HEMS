using HomeEnergyManagementSystem.Models;
using HomeEnergyManagementSystem.Dtos;

namespace HomeEnergyManagementSystem.Services;

public interface IEnergyUsageService
{
	EnergyUsageDto GetEnergyUsageAtTimestamp(DateTime time);
}
