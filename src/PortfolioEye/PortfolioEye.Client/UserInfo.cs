using MudBlazor;

namespace PortfolioEye.Client
{
	// Add properties to this class and update the server and client AuthenticationStateProviders
	// to expose more information about the authenticated user to the client.
	public class UserInfo
	{
		public required string UserId { get; set; }
		public required string Email { get; set; }
	}

	public class CustomMudTheme
	{
		public static MudTheme LightTheme => new MudTheme
		{
			Palette = new Palette()
			{
				Primary = "#7B1FA2",
				Secondary = "#9C27B0",
				AppbarBackground = "#7B1FA2",
				Background = "#FFFFFF",
				TextPrimary = "#212121",
				TextSecondary = "#757575",
				DrawerBackground = "#F5F5F5",
				DrawerText = "#212121",
				Surface = "#FFFFFF",
				ActionDefault = "#9C27B0",
				ActionDisabled = "#BDBDBD",
				ActionDisabledBackground = "#F5F5F5"
			},
			Typography = new Typography
			{
				Default = new Default
				{
					FontFamily = new[] { "Roboto", "Helvetica", "Arial", "sans-serif" },
					FontSize = "1rem",
					FontWeight = 400,
					LineHeight = 1.5,
					LetterSpacing = ".00938em"
				}
			},
			LayoutProperties = new LayoutProperties
			{
				DefaultBorderRadius = "4px"
			}
		};

		public static MudTheme DarkTheme => new MudTheme
		{
			Palette = new Palette()
			{
				Primary = "#7B1FA2",
				Secondary = "#9C27B0",
				AppbarBackground = "#7B1FA2",
				Background = "#121212",
				TextPrimary = "#FFFFFF",
				TextSecondary = "#BDBDBD",
				DrawerBackground = "#212121",
				DrawerText = "#FFFFFF",
				Surface = "#212121",
				ActionDefault = "#9C27B0",
				ActionDisabled = "#424242",
				ActionDisabledBackground = "#212121"
			},
			Typography = new Typography
			{
				Default = new Default
				{
					FontFamily = new[] { "Roboto", "Helvetica", "Arial", "sans-serif" },
					FontSize = "1rem",
					FontWeight = 400,
					LineHeight = 1.5,
					LetterSpacing = ".00938em"
				}
			},
			LayoutProperties = new LayoutProperties
			{
				DefaultBorderRadius = "4px"
			}
		};
	}
}
