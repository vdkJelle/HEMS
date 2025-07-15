namespace HomeEnergyManagementSystem.Models;

public class Refrigerator : Device
{
	public override string Type => "Refrigerator";

	public override double GetConsumption(DateTime time)
	{
		return  150; //W
	}

	public override double GetProduction(DateTime time)
	{
		return 0;
	}
}
