using MudBlazor;

namespace PortfolioEye.Client
{
	public class PortfolioEyeTheme : MudTheme
	{
		public PortfolioEyeTheme()
		{
			Palette = new PaletteLight()
			{
				Primary = "#0A2342", 
				Secondary = "#3498DB", 
				Tertiary = "#8E44AD",
				Info = "#17A2B8",
				Success = "#27AE60", 
				Warning = "#F39C12",
				Error = "#E74C3C", 
				Background = "#F2F2F2", 
				AppbarBackground = "#0A2342", 
				TextPrimary = "#4F4F4F", 
				TextSecondary = "#828282", 
				DrawerBackground = "#FFFFFF", 
				DrawerText = "#4F4F4F" 
			};
			PaletteDark = new PaletteDark()
			{
				Primary = "#3498DB", 
				Secondary = "#27AE60", 
				Tertiary = "#8E44AD",
				Info = "#17A2B8",
				Success = "#27AE60", 
				Warning = "#F39C12",
				Error = "#E74C3C", 
				Background = "#1A1A1A", 
				AppbarBackground = "#0A2342", 
				TextPrimary = "#FFFFFF", 
				TextSecondary = "#B3B3B3", 
				DrawerBackground = "#333333", 
				DrawerText = "#FFFFFF" 
			};
			LayoutProperties = new LayoutProperties()
			{
				DefaultBorderRadius = "3px"
			};

			Typography = new Typography()
			{
				Default = new Default()
				{
					FontFamily = new[] { "Montserrat", "Helvetica", "Arial", "sans-serif" },
					FontSize = ".875rem",
					FontWeight = 400,
					LineHeight = 1.43,
					LetterSpacing = ".01071em"
				},
				H1 = new H1()
				{
					FontFamily = new[] { "Montserrat", "Helvetica", "Arial", "sans-serif" },
					FontSize = "6rem",
					FontWeight = 300,
					LineHeight = 1.167,
					LetterSpacing = "-.01562em"
				},
				H2 = new H2()
				{
					FontFamily = new[] { "Montserrat", "Helvetica", "Arial", "sans-serif" },
					FontSize = "3.75rem",
					FontWeight = 300,
					LineHeight = 1.2,
					LetterSpacing = "-.00833em"
				},
				H3 = new H3()
				{
					FontFamily = new[] { "Montserrat", "Helvetica", "Arial", "sans-serif" },
					FontSize = "3rem",
					FontWeight = 400,
					LineHeight = 1.167,
					LetterSpacing = "0"
				},
				H4 = new H4()
				{
					FontFamily = new[] { "Montserrat", "Helvetica", "Arial", "sans-serif" },
					FontSize = "2.125rem",
					FontWeight = 400,
					LineHeight = 1.235,
					LetterSpacing = ".00735em"
				},
				H5 = new H5()
				{
					FontFamily = new[] { "Montserrat", "Helvetica", "Arial", "sans-serif" },
					FontSize = "1.5rem",
					FontWeight = 400,
					LineHeight = 1.334,
					LetterSpacing = "0"
				},
				H6 = new H6()
				{
					FontFamily = new[] { "Montserrat", "Helvetica", "Arial", "sans-serif" },
					FontSize = "1.25rem",
					FontWeight = 400,
					LineHeight = 1.6,
					LetterSpacing = ".0075em"
				},
				Button = new Button()
				{
					FontFamily = new[] { "Montserrat", "Helvetica", "Arial", "sans-serif" },
					FontSize = ".875rem",
					FontWeight = 500,
					LineHeight = 1.75,
					LetterSpacing = ".02857em"
				},
				Body1 = new Body1()
				{
					FontFamily = new[] { "Montserrat", "Helvetica", "Arial", "sans-serif" },
					FontSize = "1rem",
					FontWeight = 400,
					LineHeight = 1.5,
					LetterSpacing = ".00938em"
				},
				Body2 = new Body2()
				{
					FontFamily = new[] { "Montserrat", "Helvetica", "Arial", "sans-serif" },
					FontSize = ".875rem",
					FontWeight = 400,
					LineHeight = 1.43,
					LetterSpacing = ".01071em"
				},
				Caption = new Caption()
				{
					FontFamily = new[] { "Montserrat", "Helvetica", "Arial", "sans-serif" },
					FontSize = ".75rem",
					FontWeight = 400,
					LineHeight = 1.66,
					LetterSpacing = ".03333em"
				},
				Subtitle2 = new Subtitle2()
				{
					FontFamily = new[] { "Montserrat", "Helvetica", "Arial", "sans-serif" },
					FontSize = ".875rem",
					FontWeight = 500,
					LineHeight = 1.57,
					LetterSpacing = ".00714em"
				}
			};
			Shadows = new Shadow();
			ZIndex = new ZIndex();
		}
	}
}
