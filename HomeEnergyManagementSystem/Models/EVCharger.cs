namespace HomeEnergyManagementSystem.Models;

public class EVCharger : Device
{
	public override string Type => "EVCharger";

	public override double GetConsumption(DateTime time)
	{
		var hour = time.Hour;

		if (hour > 2 || hour < 7)
		{
			return 7000; ///W
		}
		return 0;
	}

	public override double GetProduction(DateTime time)
	{
		return 0;
	}
}
