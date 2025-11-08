using System.Globalization;
using System.Text.Json.Nodes;
using Application.Models;
using Domain.Exceptions;

public class WeatherService
{
    private readonly HttpClient _httpClient;

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherDto> GetWeather(string? city)
    {
        city = (city ?? "").Trim().ToLower();

        double lat, lon;
        string resolvedCity;

        switch (city)
        {
            case "":
            case "rosario":
                lat = -32.9587; lon = -60.6939; resolvedCity = "Rosario";
                break;
            case "funes":
                lat = -32.9182; lon = -60.8115; resolvedCity = "Funes";
                break;
            default:
                throw new AppValidationException("Ciudad no encontrada.");
        }

        var url = FormattableString.Invariant(
            $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&current=temperature_2m&daily=temperature_2m_min,temperature_2m_max&timezone=America/Argentina/Buenos_Aires"
        );

        var resp = await _httpClient.GetAsync(url);
        resp.EnsureSuccessStatusCode();

        var json = await resp.Content.ReadAsStringAsync();
        var root = JsonNode.Parse(json) ?? throw new AppValidationException("No se pudo interpretar la respuesta del clima.");
        var current = root["current"] ?? throw new AppValidationException("No se encontró información actual del clima.");
        var daily   = root["daily"]   ?? throw new AppValidationException("No se encontró información diaria del clima.");

        var tempNow = decimal.Parse(current["temperature_2m"]!.ToString(), CultureInfo.InvariantCulture);
        var tempMin = decimal.Parse(daily["temperature_2m_min"]![0]!.ToString(), CultureInfo.InvariantCulture);
        var tempMax = decimal.Parse(daily["temperature_2m_max"]![0]!.ToString(), CultureInfo.InvariantCulture);

        return new WeatherDto { City = resolvedCity, Temperature = tempNow, TempMin = tempMin, TempMax = tempMax };
    }
}
