using ImageMagick;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/sample", () =>
{
	const string imagePath = "sample.jpg";

	if (!File.Exists(imagePath))
	{
		return Results.NotFound(new { error = $"Could not find {imagePath} in the current working directory." });
	}

	try
	{
		using var imageStream = File.OpenRead(imagePath);
		var pngBytes = ConvertImageToPngBytes(imageStream);
		return Results.File(pngBytes, "image/png", "sample.png");
	}
	catch (Exception ex)
	{
		return Results.Problem($"An error occurred: {ex.Message}");
	}
});

app.Run();

static byte[] ConvertImageToPngBytes(Stream inputStream)
{
	using var image = new MagickImage(inputStream);
	image.Quality = 100;
	image.Format = MagickFormat.Png;

	using var outputStream = new MemoryStream();
	image.Write(outputStream);
	return outputStream.ToArray();
}