using HomeEnergyManagementSystem.Dtos;
using HomeEnergyManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeEnergyManagementSystem.Tests;

[TestClass]
[DoNotParallelize]
public class DevicesApiIntegrationsTests
{
	private static WebApplicationFactory<Program> _factory = default!;
	private static HttpClient _httpClient = default!;
	private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
	{
		PropertyNameCaseInsensitive = true,
		Converters =
		{
			new DeviceJsonConverter(),
		}
	};

	[ClassInitialize]
	public static void ClassInitialize(TestContext context)
	{
		_factory = new WebApplicationFactory<Program>();
		_httpClient = _factory.CreateClient();
	}

	[ClassCleanup]
	public static void ClassCleanup()
	{
		_httpClient.Dispose();
		_factory.Dispose();
	}

	[TestMethod]
	public async Task GetAllDevices_ReturnSuccessStatusCode()
	{
		await CleanupDevices();

		var url = new Uri("api/devices", UriKind.Relative);
		var response = await _httpClient.GetAsync(url);
		response.EnsureSuccessStatusCode();

		var devices = await response.Content.ReadFromJsonAsync<List<Device>>(_jsonOptions);
		Assert.IsNotNull(devices);
	}

	[TestMethod]
	public async Task AddNewDevice_ReturnsCreated()
	{
		await CleanupDevices();

		var newDevice = new { Type = "Refrigerator" };

		var url = new Uri("api/device", UriKind.Relative);
		var response = await _httpClient.PostAsJsonAsync(url, newDevice);

		Assert.AreEqual(System.Net.HttpStatusCode.Created, response.StatusCode);
		var createdDevice = await response.Content.ReadFromJsonAsync<Device>(_jsonOptions);
		Assert.IsNotNull(createdDevice);
		Assert.AreEqual("Refrigerator", createdDevice.Type);
	}

	[TestMethod]
	public async Task UpdateStateDevice_SetsEnabledToFalse_()
	{
		await CleanupDevices();

		var device = await CreateDevice("Refrigerator");
		var updatedDevice = await UpdateDevice(device.Id, false);
		Assert.IsFalse(updatedDevice.Enabled);
	}

	[TestMethod]
	public async Task DeleteDevice_RemovesDeviceFromHEMS_()
	{
		await CleanupDevices();

		var device1 = await CreateDevice("Refrigerator");
		var device2 = await CreateDevice("Lights");

		var devices = await GetAllDevices();
		Assert.AreEqual(2, devices.Count);
		Assert.IsTrue(devices.Any(d => d.Id == device1.Id));
		Assert.IsTrue(devices.Any(d => d.Id == device2.Id));

		await DeleteDevice(device1.Id);

		devices = await GetAllDevices();
		Assert.AreEqual(1, devices.Count);
		Assert.AreEqual(device2.Id, devices[0].Id);
	}

	private static async Task<List<Device>> GetAllDevices()
	{
		var url = new Uri("api/devices", UriKind.Relative);
		var response = await _httpClient.GetAsync(url);
		response.EnsureSuccessStatusCode();

		var result = await response.Content.ReadFromJsonAsync<List<Device>>(_jsonOptions);
		Assert.IsNotNull(result);

		return result;
	}

	private static async Task<Device> CreateDevice(string type)
	{
		var device = new { Type  = type };
		var url = new Uri("api/device", UriKind.Relative);
		var response = await _httpClient.PostAsJsonAsync(url, device);
		response.EnsureSuccessStatusCode();

		var result = await response.Content.ReadFromJsonAsync<Device>(_jsonOptions);
		Assert.IsNotNull(result);

		return result;
	}

	private static async Task<Device> UpdateDevice(Guid id, bool state)
	{
		var updatedDevice = new { Id = id, State = state };
		var url = new Uri("api/device", UriKind.Relative);
		var response = await _httpClient.PutAsJsonAsync(url, updatedDevice);
		response.EnsureSuccessStatusCode();

		var result = await response.Content.ReadFromJsonAsync<Device>(_jsonOptions);
		Assert.IsNotNull(result);

		return result;
	}

	private static async Task DeleteDevice(Guid id)
	{
		var url = new Uri($"api/device?id={id}", UriKind.Relative);
		var response = await _httpClient.DeleteAsync(url);
		response.EnsureSuccessStatusCode();
	}

	private static async Task CleanupDevices()
	{
		var devices = await GetAllDevices();
		foreach (var device in devices)
		{
			await DeleteDevice(device.Id);
		}
	}
}
