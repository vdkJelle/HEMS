using System.Text.Json.Serialization;

namespace HomeEnergyManagementSystem.Models;

public abstract class Device
{
	public Guid Id { get; set; }
	public abstract string Type { get; }
	public bool Enabled { get; set; } = true;

	public abstract double GetConsumption(DateTime time);
	public abstract double GetProduction(DateTime time);
}
