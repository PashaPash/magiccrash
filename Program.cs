using ImageMagick;

const string imagePath = "sample.jpg";

if (!File.Exists(imagePath))
{
	Console.Error.WriteLine($"Could not find {imagePath} in the current working directory.");
	return;
}

using (var image = new MagickImage(imagePath))
{
	image.Quality = 100;
	image.Format = MagickFormat.Png;

	MemoryStream stream = new MemoryStream();
	image.Write(stream);
}