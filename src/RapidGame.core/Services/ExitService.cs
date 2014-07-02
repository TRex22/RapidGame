using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidGame.core.services
{
    /// <summary>
    /// This Service provides core engine level exit and kill methods to terminate RapidEngine.
    /// It is not produced by the ServiceBuilder due to is low level and high importance.
    /// </summary>
    public class ExitService
    {
        /// <summary>
        /// This function will kill the game engine
        /// side-effects: no finalizers, no finally blocks or anything else is run
        /// Will not return a message
        /// Uses: Environment.FailFast()
        /// For more information please refer to <see href="http://msdn.microsoft.com/en-us/library/system.environment.failfast(v=vs.110).aspx">Environment.FailFast Method</see>
        /// </summary>
        public static void Kill()
        {
            Environment.FailFast(null);
        }

        /// <summary>
        /// This function will kill the game engine
        /// side-effects: no finalizers, no finally blocks or anything else is run
        /// Will return a custom message
        /// /// Uses: Environment.FailFast()
        /// For more information please refer to <see href="http://msdn.microsoft.com/en-us/library/system.environment.failfast(v=vs.110).aspx">Environment.FailFast Method</see>
        /// </summary>
        public static void Kill(string message)
        {
            Environment.FailFast(message);
        }

        /// <summary>
        /// This function will kill the game engine
        /// side-effects: no finalizers, no finally blocks or anything else is run
        /// Will return a custom message and an exception
        /// Uses: Environment.FailFast()
        /// For more information please refer to <see href="http://msdn.microsoft.com/en-us/library/system.environment.failfast(v=vs.110).aspx">Environment.FailFast Method</see>
        /// </summary>
        public static void Kill(string message, Exception e)
        {
            Environment.FailFast(message, e);
        }

        /// <summary>
        /// This will find and kill the current process without any exception messages, excluding ones caused by the method.
        /// Can be used to silently kill the Engine after fatal crash.
        /// There are three exceptions which may occur:
        /// <list>    
        ///     <listItem>Win32Exception</listItem>
        ///     <listItem>NotSupportedException</listItem>
        ///     <listItem>InvalidOperationException</listItem>         
        /// </list>   
        /// 
        /// This method should be avoided.
        /// Finally blocks will not be executed and this method may cause resource leaks at the system level.
        /// For more information please refer to <see href="http://msdn.microsoft.com/en-us/library/system.diagnostics.process.kill(v=vs.110).aspx">Process.GetCurrentProcess().Kill()</see>
        /// </summary>
        public static void KillProcess()
        {
            Process.GetCurrentProcess().Kill();
        }

        /// <summary>
        /// This will send the exit code to exit the current environment.
        /// Garbage collections is first run before the environment exits.
        /// For more information please refer to <see href="http://msdn.microsoft.com/en-us/library/system.environment.exit.aspx">Environment.Exit Method</see>
        /// </summary>
        public static void Exit()
        {
            GC.Collect();
            Environment.Exit(0);
        }

        
    }
}
