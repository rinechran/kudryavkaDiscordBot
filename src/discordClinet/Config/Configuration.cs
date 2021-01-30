using kudryavkaDiscordBot.discondClinet.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace kudryavkaDiscordBot.discondClinet.Config
{

    /// <summary>
    /// Class for stroing global setting values in memory
    /// </summary>
    public class Configuration 
    {
        public string DISCORD_TOKEN;
        public string NAVER_CLIENT_ID;
        public string NAVER_CLINET_SECRET;
        public string DB_CONNECT;


    }

    /// <summary>
    /// Class that provides only Static function to manage data
    /// </summary>
    public class ConfigurationHelper
    {
        /// <summary>   
        /// Read data from env if not read data to File
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static public Configuration Load()
        {

            Configuration configuration = Singleton<Configuration>.Instance;
            if (LoadEnvironment(configuration))
            {
                return configuration;
            }
            else if (LoadFile(configuration, "config/bot.json"))
            {   
                return configuration;
            }

            return null;
        }

        /// <summary>
        /// Read Data From Config File
        /// </summary>
        /// <param name="config">Configuration Class</param>
        /// <param name="filePath">FIle Path</param>
        /// <returns></returns>
        public static bool LoadFile(Configuration config, string filePath)
        {
            bool result = true;
            string jsonFileText = File.ReadAllText(filePath);
            config = JsonConvert.DeserializeObject<Configuration>(jsonFileText);
            
            if (config.DISCORD_TOKEN == null)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Read Data From Environment Variable
        /// </summary>
        /// <param name="config">Configuration Class</param>
        /// <returns> </returns>
        static public bool LoadEnvironment(Configuration config)
        {
            bool result = true;

            try
            {
                IDictionary env = System.Environment.GetEnvironmentVariables();
                config.DISCORD_TOKEN = env["DISCORD_TOKEN"].ToString();
                config.NAVER_CLIENT_ID = env["NAVER_CLIENT_ID"].ToString();
                config.NAVER_CLINET_SECRET = env["NAVER_CLINET_SECRET"].ToString();
                config.DB_CONNECT = env["MYSQL_CONNECT"].ToString();
            }
            catch (Exception )
            {
                result = false;
#if DEBUG_EXCEPT
                throw new SystemException(e);
#endif
            }

            return result;
        }

    }
}
