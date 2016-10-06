using UnityEngine;
using ErrorCollectClient;
using System;
using System.Reflection;
using System.Collections.Generic;

public static class ErrorCollect {

    static RemoteErrorCollect _remoteLog;
    static List<string> _occurances = new List<string>();

    public static void StartSession(string server, int port, string userName) {
        _remoteLog = new RemoteErrorCollect(
            server,
            port,
            SystemInfo.deviceUniqueIdentifier,
            Application.platform.ToString(),
            userName
        );
        _remoteLog.StartSession( () => {} );
        Application.logMessageReceived += LogErrorToServer;
    }

    static void LogErrorToServer(string condition, string stackTrace, LogType type) {

        var notError = type != LogType.Exception && type != LogType.Error;
        if (notError) return;

        try {
            var exception = ReconstructException(condition, stackTrace);

            var errHash = HashError(exception);
            if (!_occurances.Contains(errHash)) {
                _remoteLog.LogError(exception);
                _occurances.Add(errHash);
            }

        } catch /*Em All*/ {
            // PokeMan!
        }
        
    }

    static Exception ReconstructException(string condition, string stackTrace) {
        string exMessage;
        Type exType;
        GetMessage(condition, out exType, out exMessage);

        var exception = Activator.CreateInstance(exType) as Exception;
        SetExPrivateField("stack_trace", stackTrace, exception);
        SetExPrivateField("source", Application.productName, exception);
        SetExPrivateField("message", exMessage, exception);

        return exception;
    }

    static string HashError(Exception exception) {

        var errStr = 
            exception.GetType().Name +
            exception.Message +
            exception.StackTrace;
        var errStrBytes = System.Text.Encoding.ASCII.GetBytes(errStr);

        var md5 = System.Security.Cryptography.MD5.Create();
        var hash = md5.ComputeHash(errStrBytes);

        var stringBuilder = new System.Text.StringBuilder();
        var strFormat = "X2";
        foreach (var str in hash)
            stringBuilder.Append(str.ToString(strFormat));

        return stringBuilder.ToString();
    }

    static void GetMessage(string condition, out Type type, out string message) {
        var messageSplit = condition.Split(new[] { ':' }, 2);
        type = GetType(messageSplit[0]);
        message = messageSplit[1].TrimStart(new []{ ' ' });
    }

    static void SetExPrivateField(string fieldName, string value, Exception ex) {
        var sourseStrField = typeof(Exception)
            .GetField(
                fieldName,
                BindingFlags.NonPublic | BindingFlags.Instance
            );
        sourseStrField.SetValue(ex, value);
    }

    // http://answers.unity3d.com/questions/206665/typegettypestring-does-not-work-in-unity.html
    static Type GetType(string typeName) {

        // Try Type.GetType() first. This will work with types defined
        // by the Mono runtime, in the same assembly as the caller, etc.
        var type = Type.GetType(typeName);

        // If it worked, then we're done here
        if (type != null)
            return type;

        type = Type.GetType("System." + typeName);
        // If it worked, then we're done here
        if (type != null)
            return type;

        // If the TypeName is a full name, then we can try loading the defining assembly directly
        if (typeName.Contains(".")) {

            // Get the name of the assembly (Assumption is that we are using 
            // fully-qualified type names)
            var assemblyName = typeName.Substring(0, typeName.IndexOf('.'));

            // Attempt to load the indicated Assembly
            var assembly = Assembly.Load(assemblyName);
            if (assembly == null)
                return null;

            // Ask that assembly to return the proper Type
            type = assembly.GetType(typeName);
            if (type != null)
                return type;

        }

        // If we still haven't found the proper type, we can enumerate all of the 
        // loaded assemblies and see if any of them define the type

        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        //var currentAssembly = Assembly.GetExecutingAssembly();
        //var referencedAssemblies = currentAssembly.GetReferencedAssemblies();
        foreach (var assemblyName in assemblies) {

            // Load the referenced assembly
            var assembly = Assembly.Load(assemblyName.FullName);
            if (assembly != null) {
                // See if that assembly defines the named type
                type = assembly.GetType(typeName);
                if (type != null)
                    return type;
            }
        }

        // The type just couldn't be found...
        return null;

    }

}
