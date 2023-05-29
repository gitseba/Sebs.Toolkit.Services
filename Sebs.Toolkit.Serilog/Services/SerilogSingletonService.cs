using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Debugging;
using System.Diagnostics;

namespace Sebs.Toolkit.Serilog
{
    public class SerilogSingletonService
    {
        private static SerilogSingletonService? _instance;
        public static SerilogSingletonService Instance
        {
            get
            {
                _instance ??= new SerilogSingletonService();

                return _instance;
            }
        }

        // Private constructor to prevent instantiation from outside the class
        private SerilogSingletonService()
        {
            // Initialization code here
        }

        /// <summary>
        /// Serilog configuration
        /// </summary>
        /// <param name="configFilePath"> Path where the Serilog configuration file exists </param>
        public static ILogger Config(string configFilePath)
        {
            var serilogConfiguration = new ConfigurationBuilder()
                .AddJsonFile(configFilePath)
                .Build();

            // Configure Serilog and the sinks at the startup of the app
            ILogger logger = new LoggerConfiguration()
                .ReadFrom
                .Configuration(serilogConfiguration)
                .MinimumLevel.Information()
                .CreateLogger();

            SelfLog.Enable(msg => Debug.WriteLine(msg));
            SelfLog.Enable(Console.Error);

            try
            {
                logger.Information("Serilog configuration was successfully.");
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "Serilog configuration did not succeed.");
            }
            finally
            {
                //If there's log messages in pending, this will make sure the messages will be written and close safe.
                Log.CloseAndFlush();
            }

            return logger;
        }
    }


    /* ---- Level                           ------------------   Usage  -----------------
     *     
     * ---  Verbose - Verbose is the noisiest level, rarely(if ever) enabled for a production app.
     * 
     * ---  Debug   - Debug is used for internal system events that are not necessarily observable from the outside, 
     *                but useful when determining how something happened.
     * 
     * ---  Information - Information events describe things happening in the system that correspond to its responsibilities and functions.
     *                    Generally these are the observable actions the system can perform.
     * 
     * ---  Warning  - When service is degraded, endangered, or may be behaving outside of its expected parameters, Warning level events are used.
     * 
     * ---  Error - When functionality is unavailable or expectations broken, an Error event is used.
     * 
     * ---  Fatal The most critical level, Fatal events demand immediate attention.
     */

}
