using SoUnBot.AccessControl;
using SoUnBot.ModuleLoader;
using SoUnBot.Modules.OttoLinux;
using SoUnBot.Telegram;

var dataPath = Environment.GetEnvironmentVariable( "DATA_PATH" ) ?? "Data";
var aclPath = Environment.GetEnvironmentVariable( "ACL_PATH" ) ?? "Data";
var tgToken = Environment.GetEnvironmentVariable( "TELEGRAM_TOKEN" ) ?? "this-string-is-not-a-token";
var tgAdminId = Environment.GetEnvironmentVariable( "TELEGRAM_ADMIN_ID" ) ?? "000000";

Console.WriteLine( "Welcome to SO un bot!" );

long tgAdminLong;
if ( !long.TryParse( tgAdminId, out tgAdminLong ) ) {
	Console.WriteLine( "Telegram Admin ID is invalid or unset" );
	return;
}

var acl = new AccessManager( aclPath, tgAdminLong );
var moduleLoader = new ModuleLoader();

try {
	foreach ( var f in Directory.GetFiles( dataPath + "/Questions" ) ) {
		if ( !f.EndsWith( "txt" ) ) {
			Console.WriteLine( "Skipping " + Path.GetFileName( f ) + " as the file extension is not supported" );
			continue;
		}

		Console.WriteLine( "Loading module " + Path.GetFileNameWithoutExtension( f ) );
		moduleLoader.LoadModule( new BotGame( acl, Path.GetFileNameWithoutExtension( f ), f, false ) );
	}

	foreach ( var d in Directory.GetDirectories( dataPath + "/Questions" ) ) {
		Console.WriteLine( "Loading module " + Path.GetFileName( d ) );
		moduleLoader.LoadModule( new BotGame( acl, Path.GetFileName( d ), d, false, 2 ) );
	}
} catch ( Exception ex ) {
	Console.WriteLine( "There was an issue loading the module: " + ex.Message );
	return;
}

Console.WriteLine( "Starting Telegram bot listener..." );
new TelegramBot( tgToken, acl, dataPath + "/motd.txt", moduleLoader.Modules );

// worst way ever to keep the main thread running, I know
while ( true )
	Thread.Sleep( 10000 );