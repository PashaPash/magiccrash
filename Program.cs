using ImageMagick;

const string imagePath = "sample.jpg";

if (!File.Exists(imagePath))
{
	Console.Error.WriteLine($"Could not find {imagePath} in the current working directory.");
	return;
}

try
{
	using var imageStream = File.OpenRead(imagePath);
	using (var image = new MagickImage(imageStream))
	{
		image.Quality = 100;
		image.Format = MagickFormat.Png;

		MemoryStream stream = new MemoryStream();
		image.Write(stream);
	}
}
catch (Exception ex)
{
	// not executed, segfault instead
	Console.WriteLine($"Caught: {ex.GetType().Name}: {ex.Message}");
}