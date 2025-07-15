using HomeEnergyManagementSystem.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeEnergyManagementSystem.Tests;

public class DeviceJsonConverter : JsonConverter<Device>
{
	public override Device? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		// Load the JSON object
		using var jsonDoc = JsonDocument.ParseValue(ref reader);
		var jsonObject = jsonDoc.RootElement;

		if (!jsonObject.TryGetProperty("type", out var typeProperty))
			throw new JsonException("Missing Type discriminator");

		var typeDiscriminator = typeProperty.GetString();

		Device? device = typeDiscriminator switch
		{
			"Refrigerator" => JsonSerializer.Deserialize<Refrigerator>(jsonObject.GetRawText(), options),
			"SolarPanel" => JsonSerializer.Deserialize<SolarPanel>(jsonObject.GetRawText(), options),
			"Lights" => JsonSerializer.Deserialize<Lights>(jsonObject.GetRawText(), options),
			"HeatingSystem" => JsonSerializer.Deserialize<HeatingSystem>(jsonObject.GetRawText(), options),
			"EVCharger" => JsonSerializer.Deserialize<EVCharger>(jsonObject.GetRawText(), options),
			"HamsterWheel" => JsonSerializer.Deserialize<HamsterWheel>(jsonObject.GetRawText(), options),
			_ => throw new JsonException($"Unknown device type discriminator: {typeDiscriminator}")
		};

		return device;
	}

	public override void Write(Utf8JsonWriter writer, Device value, JsonSerializerOptions options)
	{
		// Create a copy of the options *without* this converter to avoid recursion
		var optionsWithoutConverter = new JsonSerializerOptions(options);

		// Remove this converter from the copy
		for (int i = optionsWithoutConverter.Converters.Count - 1; i >= 0; i--)
		{
			if (optionsWithoutConverter.Converters[i] is DeviceJsonConverter)
				optionsWithoutConverter.Converters.RemoveAt(i);
		}

		// Serialize using the filtered options
		JsonSerializer.Serialize(writer, (object)value, value.GetType(), optionsWithoutConverter);
	}
}