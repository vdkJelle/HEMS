namespace HomeEnergyManagementSystem.Models;

public class HamsterWheel : Device
{
	public override string Type => "HamsterWheel";

	public override double GetConsumption(DateTime time)
	{
		return 0; // no consumption
	}

	public override double GetProduction(DateTime time)
	{
		var hour = time.Hour;
		if ((hour >= 10 && hour <= 11) || (hour >= 19 && hour <= 20))
			return 10; //W

		return 0;
	}
}
