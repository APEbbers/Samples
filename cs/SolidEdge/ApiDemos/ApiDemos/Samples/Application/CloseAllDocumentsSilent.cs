﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApiDemos.Application
{
    /// <summary>
    /// Closes all open documents without prompting to save.
    /// </summary>
    class CloseAllDocumentsSilent
    {
        static void RunSample(bool breakOnStart)
        {
            if (breakOnStart) System.Diagnostics.Debugger.Break();

            SolidEdgeFramework.Application application = null;
            SolidEdgeFramework.Documents documents = null;

            try
            {
                // Register with OLE to handle concurrency issues on the current thread.
                SolidEdgeCommunity.OleMessageFilter.Register();

                // Connect to or start Solid Edge.
                application = SolidEdgeCommunity.SolidEdgeUtils.Connect(false);

                if (application != null)
                {
                    // Disable alerts. This will prevent the Save dialog from showing.
                    application.DisplayAlerts = false;

                    // Get a reference to the documents collection.
                    documents = application.Documents;

                    // Close all documents.
                    documents.Close();
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (application != null)
                {
                    application.DisplayAlerts = true;
                }

                SolidEdgeCommunity.OleMessageFilter.Unregister();
            }
        }
    }
}
